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

        [DisplayName("Tipo")]
        [StringLength(maximumLength: 150)]
        public string TypeName { get; set; }

        //[Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct Option")]
        public ItemCodeTypeEnum? ItemCodeTypeEnum { get; set; }
    }
}