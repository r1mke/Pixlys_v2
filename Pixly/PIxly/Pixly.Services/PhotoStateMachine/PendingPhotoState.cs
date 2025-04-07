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
    public class PendingPhotoState : BasePhotoState
    {
        public PendingPhotoState(Context context, IMapper mapper, IServiceProvider serviceProvider, CloudinaryDotNet.Cloudinary cloudinary) : base(context, mapper, serviceProvider, cloudinary)
        {
        }

        public override Model.Photo Approve(int id)
        {
            return SetState(id, "approved");
        }

        public override Model.Photo Reject(int id)
        {
            return SetState(id, "rejected");
        }

        public override List<string> AllowedActions(Photo enitity)
        {
            return new List<string>() { nameof(Reject), nameof(Approve) };
        }

    }
}
