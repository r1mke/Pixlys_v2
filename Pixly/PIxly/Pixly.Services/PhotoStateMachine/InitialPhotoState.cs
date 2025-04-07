using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Pixly.Model.Requests;
using Pixly.Services.Database;
using Pixly.Services.ProizvodiStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.PhotoStateMachine
{
    public class InitialPhotoState : BasePhotoState
    {
        public InitialPhotoState(Context context, IMapper mapper,
            IServiceProvider serviceProvider, CloudinaryDotNet.Cloudinary cloudinary) : base(context, mapper,serviceProvider,cloudinary)
        {
        }

        public override Model.Photo Insert(PhotoInsertObject request)
        {
            var entity = Mapper.Map<Database.Photo>(request);
            BeforeInsert(request,entity);

            if(request.IsDraft == true)
            {
                entity.StateMachine = "draft";
            }
            else
            {
                entity.StateMachine = "pending";
            }

            Context.Photos.Add(entity);
            Context.SaveChanges();  

            return Mapper.Map<Model.Photo>(entity);    
        }

        private void BeforeInsert(PhotoInsertObject request, Photo entity)
        {
            if (request.File != null)
            {
                var url = UploadToCloudinary(request.File);
                entity.Url = url;
            }
            else
            {
                throw new Exception("Greška prilikom postavljanje slike");
            }

            foreach (var id in request.TagIds)
            {
                var photoTag = new PhotoTag()
                {
                    PhotoId = entity.PhotoId,
                    TagId = id
                };

                entity.PhotoTags.Add(photoTag);
            }
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

        public override List<string> AllowedActions(Photo enitity)
        {
            return new List<string>() { nameof(Insert) };
        }
    }
}
