using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Users
{
    public class GetAllUsersSpec : Specification<User, UserDto>
    {
        public GetAllUsersSpec()
        {
            Query.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .Select(user => new UserDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        CreatedAt = user.CreatedAt,
                        UserRoles = user.UserRoles.Select(ur => string.Join(",", ur.Role.Name)).ToList(),
                        // Invoices = user.Invoices.Select(i => new Invoice
                        // {
                        //     Id = i.Id,
                        //     InvoiceNumber = i.InvoiceNumber,
                        //     Total = i.Total,
                        //     TaxTotal = i.TaxTotal,
                        //     CreatedAt = i.CreatedAt,
                        //     DueDate = i.DueDate,
                        //     Status = i.Status,
                        //     ClientId = i.ClientId
                        // }).ToList()
                    });   
        }   
    }
}