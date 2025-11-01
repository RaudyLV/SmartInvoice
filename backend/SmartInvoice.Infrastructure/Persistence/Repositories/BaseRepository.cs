using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Infrastructure.Persistence.Data;

namespace SmartInvoice.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : RepositoryBase<T>, IBaseRepository<T> where T : class
    {
        public BaseRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }
      
    }
}