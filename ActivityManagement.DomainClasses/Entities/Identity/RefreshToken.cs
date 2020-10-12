using System;
using Microsoft.VisualBasic;

namespace ActivityManagement.DomainClasses.Entities.Identity
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Ip { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Value { get; set; }
        public DateTime ExpireDate { get; set; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        
        
    }
}