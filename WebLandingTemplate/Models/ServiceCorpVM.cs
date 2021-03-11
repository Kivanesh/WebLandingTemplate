using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class ServiceCorpVM
    {
        [Key]
        public int ServiceId { get; set; } 

        public int ServiceType { get; set; }

        [DisplayName("Nombre")]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        [StringLength(maximumLength: 150)]
        public string Description { get; set; }

        [DisplayName("Categoria")]
        [StringLength(maximumLength: 150)]
        public string CategoryName { get; set; }
    }
}