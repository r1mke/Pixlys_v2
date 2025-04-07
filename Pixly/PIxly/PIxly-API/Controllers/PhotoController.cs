using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixly.Model;
using Pixly.Model.Requests;
using Pixly.Model.SearchObjects;
using Pixly.Services.Interfaces;

namespace PIxly_API.Controllers
{

    public class PhotoController : CRUDController<Photo, PhotoSearchObject, PhotoInsertObject, PhotoUpdateRequest>
    {

        public PhotoController(IPhotoService service) : base(service)
        {
        }

        [HttpPost("{id}/submit")]
        public Photo Submit(int id)
        {
            return (_service as IPhotoService).Submit(id);
        }

        [HttpPost("{id}/approve")]
        public Photo Approve(int id)
        {
            return (_service as IPhotoService).Approve(id);
        }

        [HttpPost("{id}/reject")]
        public Photo Reject(int id)
        {
            return (_service as IPhotoService).Reject(id);
        }

        [HttpPost("{id}/edit")]
        public Photo Edit(int id)
        {
            return (_service as IPhotoService).Edit(id);
        }

        [HttpPost("{id}/hide")]
        public Photo Hide(int id)
        {
            return (_service as IPhotoService).Hide(id);
        }

        [HttpGet("{id}/allowedActions")]
        public List<string> AllowedActions(int id)
        {
            return (_service as IPhotoService).AllowedActions(id);
        }
    }
}
