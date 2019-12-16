using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class CategoryVM
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ItemCodeType { get; set; }
    }
}