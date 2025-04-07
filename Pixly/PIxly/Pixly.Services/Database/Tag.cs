using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.Database
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        //public ICollection<PhotoTag> PhotoTags { get; set; }
    }
}
