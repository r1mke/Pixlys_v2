using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Model
{
    public class Photo
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Resolution { get; set; }
        public User User { get; set; }
        public string StateMachine { get; set; }
        public ICollection<PhotoTag> PhotoTags { get; set; } = new List<PhotoTag>();
    }
}
