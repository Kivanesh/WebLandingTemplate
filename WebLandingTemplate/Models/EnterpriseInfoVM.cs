using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class EnterpriseInfoVM
    {
        [DisplayName("Domicilio")]
        public string Address { get; set; }

        [DisplayName("Correo electrónico")]
        public string Email { get; set; }

        [DisplayName("Teléfono")]
        public string Phone { get; set; }

        [DisplayName("Horario de trabajo")]
        public string BusinessHours { get; set; }

        [DisplayName("Social")]
        public string SocialMedia { get; set; }

        [DisplayName("Misión")]
        public string Mision { get; set; }

        [DisplayName("Visión")]
        public string Vision { get; set; }

        [DisplayName("Valores")]
        public string Valores { get; set; }

        [DisplayName("Correo electrónico 2")]

        public string EmailOptional1 { get; set; }
        [DisplayName("Correo electrónico 3")]
        public string EmailOptional2 { get; set; }

        [DisplayName("Logo")]
        public byte[] Logo { get; set; }


        public int IdInfo { get; set; }
        [DisplayName("Texto promocional 1")]
        public string PromotionalText1 { get; set; }
        [DisplayName("Texto promocional 2")]
        public string PromotionalText2 { get; set; }
        [DisplayName("Texto promocional 3")]
        public string PromotionalText3 { get; set; }
        [DisplayName("Texto promocional 4")]
        public string PromotionalText4 { get; set; }
    }
}