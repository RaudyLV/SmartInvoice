using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInvoice.Application.Dtos.Auth
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpireTime;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}