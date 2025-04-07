using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pixly.Model.Requests
{
    public class PhotoInsertObject
    {
        public string Title { get; set; }
        public string Resolution { get; set; }
        public int UserId { get; set; } 
        public IFormFile File { get; set; }
        public List<int> TagIds { get; set; }
        public bool? IsDraft { get; set; }
    }
}
