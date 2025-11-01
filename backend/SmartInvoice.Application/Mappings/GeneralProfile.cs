using AutoMapper;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Features.Clients.Commands.CreateClientCommand;
using SmartInvoice.Application.Features.Clients.Commands.DeleteClientCommand;
using SmartInvoice.Application.Features.Clients.Commands.UpdateClientCommand;
using SmartInvoice.Application.Features.Invoices.Commands.CancelInvoiceCommand;
using SmartInvoice.Application.Features.Invoices.Commands.CreateInvoicesCommand;
using SmartInvoice.Application.Features.Payments.Commands;
using SmartInvoice.Application.Features.Products.Commands.CreateProductCommand;
using SmartInvoice.Application.Features.Products.Commands.UpdateProductCommand;
using SmartInvoice.Application.Features.Users.Commands.DeleteUserCommand;
using SmartInvoice.Application.Features.Users.Commands.UpdateUserCommand;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Entities
            CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, ProductDto>()
                .ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.InvoiceItems,
                    opt => opt.MapFrom(src => src.InvoiceItems))
                .ReverseMap();
            
            CreateMap<InvoiceItem, InvoiceItemDto>()
                .ReverseMap();

            CreateMap<Client, ClientDto>()
                .ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
                
            
            CreateMap<Payment, PaymentDto>().ReverseMap()
                .ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
            #endregion

            #region Commands
            CreateMap<UpdateUserCommand, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
                
            CreateMap<DeleteUserCommand, User>();

            CreateMap<CreateInvoiceCommand, Invoice>();
            CreateMap<CancelInvoiceCommand, Invoice>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateClientCommand, Client>();
            CreateMap<UpdateClientCommand, Client>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
            CreateMap<DeleteClientCommand, Client>();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PayInvoiceCommand, Payment>()
                .ForMember(dest => dest.Method,
                           src => src.MapFrom(src => Enum.Parse<PaymentMethod>(src.PayMethod, true)));
            #endregion
        }
    }
}