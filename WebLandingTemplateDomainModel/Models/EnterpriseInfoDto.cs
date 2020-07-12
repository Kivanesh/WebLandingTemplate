using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLandingTemplateDomainModel.Models
{
    public class EnterpriseInfoDto
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BusinessHours { get; set; }
        public string SocialMedia { get; set; }
        public string Mision { get; set; }
        public string Vision { get; set; }
        public string Valores { get; set; }
        public string EmailOptional1 { get; set; }
        public string EmailOptional2 { get; set; }
        public byte[] Logo { get; set; }
        public int IdInfo { get; set; }
        public string PromotionalText1 { get; set; }
        public string PromotionalText2 { get; set; }
        public string PromotionalText3 { get; set; }
        public string PromotionalText4 { get; set; }
    }
}
