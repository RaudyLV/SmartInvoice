using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos.Invoices;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Features.Invoices.Commands.CreateInvoicesCommand
{
    public class CreateInvoiceCommand : IRequest<Response<InvoiceDto>>
    {
        public CreateInvoiceRequest Request { get; set; }

        public CreateInvoiceCommand(CreateInvoiceRequest request)
        {
            Request = request;
        }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Response<InvoiceDto>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;
        private readonly IClientServices _clientServices;
        private readonly IInvoiceServices _invoiceServices;
        private readonly ICacheServices _cacheServices;
        public CreateInvoiceCommandHandler(IProductServices productServices, IMapper mapper,
         IClientServices clientServices, IInvoiceServices invoiceServices, ICacheServices cacheServices)
        {
            _productServices = productServices;
            _mapper = mapper;
            _clientServices = clientServices;
            _invoiceServices = invoiceServices;
            _cacheServices = cacheServices;
        }

        public async Task<Response<InvoiceDto>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var client = await _clientServices.GetClientById(command.Request.ClientId);

            var productsIds = command.Request.Items.Select(p => p.ProductId).ToList();

            var existingProducts = await _productServices.GetProductsByIds(productsIds);

            var missingIds = productsIds.Except(existingProducts.Select(p => p.Id)).ToList();

            if (missingIds.Any())
            {
                throw new NotFoundException($"Some products were not found: {string.Join(", ", missingIds)}");
            }

            var invoice = new Invoice
            {
                ClientId = command.Request.ClientId,
            };

            var productsDict = existingProducts.ToDictionary(p => p.Id); //Busqueda mas rapida de producto 

            invoice.InvoiceItems = new List<InvoiceItem>();

            foreach (var p in command.Request.Items)
            {
                var productDto = productsDict[p.ProductId];

                if (productDto == null)
                    throw new NotFoundException("Product not found");

                decimal subTotal = productDto.Price * p.Quantity;
                decimal taxAmount = productDto.TaxRate * subTotal;
                decimal total = subTotal + taxAmount;

                await _productServices.ReduceStock(p.ProductId, p.Quantity);

                await _cacheServices.RemoveByPrefixAsync("products_list", cancellationToken);

                invoice.InvoiceItems.Add(new InvoiceItem
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = productDto.Price,
                    TaxRate = productDto.TaxRate,
                    Total = total
                });
            }

            invoice.SubTotal = invoice.InvoiceItems.Sum(p => p.UnitPrice * p.Quantity);
            invoice.TaxTotal = invoice.InvoiceItems.Sum(p => p.Total - (p.UnitPrice * p.Quantity));
            invoice.Discount = invoice.SubTotal > 1000 ? invoice.SubTotal * 0.10m : 0;
            invoice.Total = invoice.SubTotal + invoice.TaxTotal - invoice.Discount;

            await _invoiceServices.CreateInvoice(invoice);

            await _cacheServices.RemoveByPrefixAsync("invoices_list", cancellationToken);

            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
            invoiceDto.ClientName = client.Name;

            return new Response<InvoiceDto>(invoiceDto, "Invoice created succesfully");
        }
    }
}