using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLandingTemplateDomainModel.Models
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> ContactDate { get; set; }
        public string ComeFrom { get; set; }
        public Nullable<bool> IsRead { get; set; }
    }
}
