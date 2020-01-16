using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLandingTemplateDomainModel.Enums;

namespace WebLandingTemplate.Models
{
    public class CategoryVM
    {
       [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Nombre")]
        [StringLength(maximumLength: 40)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Tipo")]
        public Nullable<int> ItemCodeType { get; set; }

        [Required]
        public ItemCodeTypeEnum ItemCodeTypeEnum { get; set; }
    }
}