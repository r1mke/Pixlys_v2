using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.Database
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Resolution { get; set; }
        public int UserId { get; set; }  
        public User User { get; set; }  
        public string StateMachine {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<PhotoTag> PhotoTags { get; set; } = new List<PhotoTag>();
    }
}
