using Pixly.Model;
using Pixly.Model.Requests;
using Pixly.Model.SearchObjects;
namespace Pixly.Services.Interfaces
{
    public interface IUserService : ICRUDServis<Model.User, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
       
    }
}
