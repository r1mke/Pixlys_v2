using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixly.Model.Requests;
using Pixly.Services.Interfaces;
using Pixly.Model;
using Pixly.Model.SearchObjects;
namespace PIxly_API.Controllers
{
   
    public class UserController : CRUDController<User, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        public UserController(IUserService service) : base(service)
        {
        }
    }
}
