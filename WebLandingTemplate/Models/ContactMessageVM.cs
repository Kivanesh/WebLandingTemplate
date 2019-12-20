using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class ContactMessageVM
    {
        [Key]
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> ContactDate { get; set; }
        public string ComeFrom { get; set; }
    }
}