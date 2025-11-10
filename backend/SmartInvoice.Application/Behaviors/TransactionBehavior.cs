using MediatR;
using SmartInvoice.Application.Interfaces;

namespace SmartInvoice.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!typeof(TRequest).Name.EndsWith("Command"))
            {
                return await next(cancellationToken);
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var response = await next();

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return response;
            }
            catch
            {
                await _unitOfWork.RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}

