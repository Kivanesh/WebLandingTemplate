using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLandingTemplateDomainModel.Models
{
    public class SupplierDto
    {
        [Key]
        public int ProveedorId { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public string Descripcion { get; set; }
    }
}
