using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class SupplierVM
    {
        public int ProveedorId { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public string Descripcion { get; set; }
    }
}