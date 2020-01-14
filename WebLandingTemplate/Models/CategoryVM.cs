using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLandingTemplateDomainModel.Enums;

namespace WebLandingTemplate.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Nullable<int> ItemCodeType { get; set; }

        [Required]
        public ItemCodeTypeEnum ItemCodeTypeEnum { get; set; }
    }
}