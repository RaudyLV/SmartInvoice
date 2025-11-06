using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> UserRoles { get; set; }
    }
}