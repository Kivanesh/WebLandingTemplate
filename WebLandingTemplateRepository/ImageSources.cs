//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebLandingTemplateRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageSources
    {
        public int ImageId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public byte[] Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> ItemCodeType { get; set; }
    
        public virtual Dashboard Dashboard { get; set; }
        public virtual Products Products { get; set; }
        public virtual ServiceCorp ServiceCorp { get; set; }
    }
}
