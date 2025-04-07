using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Model.SearchObjects
{
    public class PhotoSearchObject
    {
        public string? Title { get; set; }
        public bool? IsUserIncluded { get; set; }
        public bool? IsPhotoTagsIncluded { get; set; }
    }
}
