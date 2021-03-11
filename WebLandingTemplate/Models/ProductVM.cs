using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLandingTemplateDomainModel.Enums;

namespace WebLandingTemplate.Models
{
    public class ProductVM
    {
        [Key]
        public int ProductId { get; set; }

        [DisplayName("Nombre")]
        [StringLength(maximumLength: 150)]
        public string ProductName { get; set; }

        public int ProveedorId { get; set; }

        [DisplayName("Precio")]
        [StringLength(maximumLength: 50)]
        public Nullable<double> Price { get; set; }

        [DisplayName("Descripción")]
        [StringLength(maximumLength: 150)]
        public string Description { get; set; }

        [DisplayName("Tamaño")]
        [StringLength(maximumLength: 150)]
        public string Size { get; set; }

        [DisplayName("Color")]
        [StringLength(maximumLength: 150)]
        public string Color { get; set; }

        public int ProductType { get; set; }

        [DisplayName("Categoria")]
        [StringLength(maximumLength: 150)]
        public string CategoryName { get; set; }

        [DisplayName("Proveedor")]
        [StringLength(maximumLength: 150)]
        public string ProveedorName { get; set; }
        // public SupplierTypeEnum? ItemCodeTypeEnum { get; set; }

    }
}