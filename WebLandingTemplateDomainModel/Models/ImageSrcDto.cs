using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLandingTemplateDomainModel.Models
{
    public class ImageSrcDto
    {
        public int ImageId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public byte[] Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> ItemCodeType { get; set; }

    }
}
