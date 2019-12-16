using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLandingTemplateDomainModel.Models
{
    public class ServiceCorpDto
    {
        public int ServiceId { get; set; }
        public Nullable<int> ServiceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
