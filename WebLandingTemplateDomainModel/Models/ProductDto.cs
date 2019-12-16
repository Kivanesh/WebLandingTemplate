using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLandingTemplateDomainModel.Models
{
    public class ProductDto
    {   
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
