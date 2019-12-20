using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLandingTemplate.Models
{
    public class FrontDashboardVM
    {
        [Key]
        public int ItemImageId { get; set; }
        public string ElementName { get; set; }
        public Nullable<int> ElementId { get; set; }
    }
}