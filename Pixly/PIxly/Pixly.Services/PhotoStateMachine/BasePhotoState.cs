using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Pixly.Model.Requests;
using Pixly.Services.Database;
using Pixly.Services.PhotoStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.ProizvodiStateMachine
{
    public class BasePhotoState
    {
        protected readonly CloudinaryDotNet.Cloudinary _cloudinary;
        public Context Context { get; set; }
        public IMapper Mapper { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        public BasePhotoState(Context context, IMapper mapper,
            IServiceProvider serviceProvider, CloudinaryDotNet.Cloudinary cloudinary)
        {
            Context = context;
            Mapper = mapper;
            ServiceProvider = serviceProvider;
            _cloudinary = cloudinary;   
        }

        public virtual Model.Photo Insert(PhotoInsertObject request)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Photo Update(int id,PhotoUpdateRequest request)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Photo Submit(int id)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Photo Approve(int id)
        {
            throw new Exception("Method not allowed");
        }


        public virtual Model.Photo Reject(int id)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Photo Edit(int id)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Photo Hide(int id)
        {
            throw new Exception("Method not allowed");
        }

        public virtual List<string> AllowedActions(Database.Photo enitity)
        {
            throw new Exception("Method not allowed");
        }

        protected Model.Photo SetState(int id, string state)
        {
            var entity = Context.Photos.Find(id);

            if (entity == null)
                throw new Exception("Photo not found");

            entity.StateMachine = state;

            Context.SaveChanges();

            return Mapper.Map<Model.Photo>(entity);
        }


        public BasePhotoState CreateState(string stateName)
        {
            switch(stateName)
            {
                case "initial":
                    return ServiceProvider.GetService<InitialPhotoState>();
                case "draft":
                    return ServiceProvider.GetService<DraftPhotoState>();
                case "pending":
                    return ServiceProvider.GetService<PendingPhotoState>();
                case "approved":
                    return ServiceProvider.GetService<ApprovedPhotoState>();
                case "hidden":
                    return ServiceProvider.GetService<HiddenPhotoState>();
                default: throw new Exception("State not recognized");
            }
        }
    }
}
