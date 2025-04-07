using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Pixly.Model;
using Pixly.Model.Requests;
using Pixly.Model.SearchObjects;
using Pixly.Services.Database;
using Pixly.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.Services
{
    public class UserService : CRUDServis<Model.User, UserSearchObject, UserInsertRequest,UserUpdateRequest ,Database.User>, IUserService
    {
        public UserService(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.User> AddFilter(UserSearchObject? search, IQueryable<Database.User> query)
        {
            if (!string.IsNullOrWhiteSpace(search?.FirstName))
            {
                query = query.Where(x=>x.FirstName.StartsWith(search.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(search?.LastName))
            {
                query = query.Where(x => x.FirstName.StartsWith(search.LastName));
            }

            return query;
        }

        protected override void BeforeInsert(UserInsertRequest request, Database.User entity)
        {
            if(Context.Users.Any(u=>u.Email == request.Email)) 
            {
                throw new Exception("Email već postoji u bazi");    
            }
        }
            
    }
}
