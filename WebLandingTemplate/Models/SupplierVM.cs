using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class SupplierVM
    {
        [Key]
        public int ProveedorId { get; set; }

        [DisplayName("Nombre")]
        //[StringLength(maximumLength:20)]
        [Required]
        public string Name { get; set; }

        [DisplayName("Logo")]
        public byte[] Logo { get; set; }

        [DisplayName("Descripción")]
        [StringLength(maximumLength: 150)]
        public string Descripcion { get; set; }
    }
}
