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
    
    public partial class Dashboard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dashboard()
        {
            this.ImageSources = new HashSet<ImageSources>();
        }
    
        public int ItemImageId { get; set; }
        public string ElementName { get; set; }
        public Nullable<int> ElementId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageSources> ImageSources { get; set; }
    }
}
