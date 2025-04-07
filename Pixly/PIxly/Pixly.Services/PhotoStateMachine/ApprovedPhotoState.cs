using MapsterMapper;
using Pixly.Services.Database;
using Pixly.Services.ProizvodiStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.PhotoStateMachine
{
    public class ApprovedPhotoState : BasePhotoState
    {
        public ApprovedPhotoState(Context context, IMapper mapper, IServiceProvider serviceProvider, CloudinaryDotNet.Cloudinary cloudinary) : base(context, mapper, serviceProvider, cloudinary)
        {
        }

        public override Model.Photo Edit(int id)
        {
            return SetState(id, "draft");
        }

        public override Model.Photo Hide(int id)
        {
            return SetState(id, "hidden");
        }

        public override List<string> AllowedActions(Photo enitity)
        {
            return new List<string>() { nameof(Hide), nameof(Edit) };
        }

    }
}
