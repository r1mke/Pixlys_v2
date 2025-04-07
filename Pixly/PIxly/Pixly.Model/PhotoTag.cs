using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Model
{
    public class PhotoTag
    {
        public int PhotoId { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
