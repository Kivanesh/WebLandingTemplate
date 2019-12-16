using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class ProductVM
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public Nullable<int> ProveedorId { get; set; }

        public Nullable<double> Price { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public Nullable<int> ProductType { get; set; }
    }
}