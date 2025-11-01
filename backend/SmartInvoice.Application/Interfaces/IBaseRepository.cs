
using Ardalis.Specification;

namespace SmartInvoice.Application.Interfaces
{
    public interface IBaseRepository<T> : IRepositoryBase<T> where T : class
    {
       
    }
}