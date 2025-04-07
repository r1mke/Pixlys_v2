using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Pixly.Model.Requests;
using Pixly.Model.SearchObjects;
using Pixly.Services.Database;
using Pixly.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Pixly.Services.ProizvodiStateMachine;
using Azure.Core;

namespace Pixly.Services.Services
{
    public class PhotoService : CRUDServis<Model.Photo, PhotoSearchObject, PhotoInsertObject,PhotoUpdateRequest ,Database.Photo>, IPhotoService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;
        public BasePhotoState BasePhotoState { get; set; }
        public PhotoService(Context context, IMapper mapper, CloudinaryDotNet.Cloudinary cloudinary, BasePhotoState basePhotoState ) : base(context, mapper)
        {
            _cloudinary = cloudinary;
            BasePhotoState = basePhotoState;
        }

        protected override IQueryable<Photo> AddFilter(PhotoSearchObject? search, IQueryable<Photo> query)
        {
            if(!string.IsNullOrWhiteSpace(search?.Title))
            {
                query = query.Where(x=>x.Title.StartsWith(search.Title));
            }

            if(search.IsUserIncluded == true)
            {
                query=query.Include(p=>p.User);
            }

            if (search.IsPhotoTagsIncluded == true)
            {
                query = query.Include(p => p.PhotoTags).ThenInclude(t=>t.Tag);
            }

            return query;
        }

        public override Model.Photo Insert(PhotoInsertObject request)
        {
            var state = BasePhotoState.CreateState("initial");
            return state.Insert(request);
        }

        public override Model.Photo Update(int id, PhotoUpdateRequest request)
        {
            var entity = GetById(id);
            var state = BasePhotoState.CreateState(entity.StateMachine);
            return state.Update(id,request);
        }

        private string UploadToCloudinary(IFormFile file)
        {
           
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = "Pixly_V2",
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = false,
                    Transformation = new Transformation().Named("Pixly_V2_Compression")
                };


                var uploadResult = _cloudinary.Upload(uploadParams);


                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception("Greška prilikom slanja slike na Cloudinary");


                return uploadResult.SecureUrl.AbsoluteUri;
         
        }


        public Model.Photo Submit(int id)
        {
            var entity = GetById(id);
            var state = BasePhotoState.CreateState(entity.StateMachine);
            return state.Submit(id);
        }

        public Model.Photo Approve(int id)
        {
            var entity = GetById(id);
            var state = BasePhotoState.CreateState(entity.StateMachine);
            return state.Approve(id);
        }

        public Model.Photo Reject(int id)
        {
            var entity = GetById(id);
            var state = BasePhotoState.CreateState(entity.StateMachine);
            return state.Reject(id);
        }

        public Model.Photo Edit(int id)
        {
            var entity = GetById(id);
            var state = BasePhotoState.CreateState(entity.StateMachine);
            return state.Edit(id);
        }

        public Model.Photo Hide(int id)
        {
            var entity = GetById(id);
            var state = BasePhotoState.CreateState(entity.StateMachine);
            return state.Hide(id);
        }

        public List<string> AllowedActions(int id)
        {
            if(id <= 0)
            {
                var state = BasePhotoState.CreateState("initial");
                return state.AllowedActions(null);
            }
            else
            {
                var entity = Context.Photos.Find(id);
                if (entity == null) throw new Exception("Entity don't exist");
                var state = BasePhotoState.CreateState(entity.StateMachine);
                return state.AllowedActions(entity);
            }
        }
    }
}
