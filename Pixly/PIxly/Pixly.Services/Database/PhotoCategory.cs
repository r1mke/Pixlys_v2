using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.Database
{
    public class PhotoCategory
    {
        public int PhotoCategoryId { get; set; }
        public int PhotoId { get; set; }       
        public Photo Photo { get; set; }        
        public int CategoryId { get; set; }     
        public Category Category { get; set; }
    }
}
