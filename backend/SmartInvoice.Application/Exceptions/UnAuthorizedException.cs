using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInvoice.Application.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message) : base(message)
        {
            
        }
    }
}