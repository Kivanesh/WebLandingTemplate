using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class ServiceCorpVM
    {
        [Key]
        public int ServiceId { get; set; }
        public Nullable<int> ServiceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}