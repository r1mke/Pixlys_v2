using MapsterMapper;
using Pixly.Model.Requests;
using Pixly.Services.Database;
using Pixly.Services.ProizvodiStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixly.Model.Requests;
namespace Pixly.Services.PhotoStateMachine
{
    public class DraftPhotoState : BasePhotoState
    {
        public DraftPhotoState(Context context, IMapper mapper,
            IServiceProvider serviceProvider, CloudinaryDotNet.Cloudinary cloudinary) : base(context, mapper, serviceProvider, cloudinary)
        {
            
        }

        public override Model.Photo Update(int id,PhotoUpdateRequest request)
        {

            var entity = Context.Photos.Find(id);

            Mapper.Map(request, entity);

            BeforeUpdate(request, entity);

            Context.SaveChanges();

            return Mapper.Map<Model.Photo>(entity);
        }

        private void BeforeUpdate(PhotoUpdateRequest request, Database.Photo entity)
        {

        }

        public override Model.Photo Submit(int id)
        {
           return SetState(id, "pending");
        }

        public override List<string> AllowedActions(Photo enitity)
        {
            return new List<string>() { nameof(Update), nameof(Submit) };
        }
    }
    
}
