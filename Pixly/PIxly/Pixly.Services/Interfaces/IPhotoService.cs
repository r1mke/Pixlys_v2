using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixly.Model;
using Pixly.Model.Requests;
using Pixly.Model.SearchObjects;
namespace Pixly.Services.Interfaces
{
    public interface IPhotoService : ICRUDServis<Model.Photo, PhotoSearchObject, PhotoInsertObject,PhotoUpdateRequest>
    {
        public Model.Photo Submit(int id);
        public Model.Photo Approve(int id);
        public Model.Photo Reject(int id);
        public Model.Photo Edit(int id);
        public Model.Photo Hide(int id);
        public List<string> AllowedActions(int id);
    }
}
