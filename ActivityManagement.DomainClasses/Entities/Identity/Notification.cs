using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ActivityManagement.DomainClasses.Entities.Identity
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [Required]
        public bool EnterEmail { get; set; }
        [Required]
        public bool EnterSms { get; set; }
        [Required]
        public bool EnterTelegram { get; set; }
        [Required]
        public bool ExitEmail { get; set; }
        [Required]
        public bool ExitSms { get; set; }
        [Required]
        public bool ExitTelegram { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
