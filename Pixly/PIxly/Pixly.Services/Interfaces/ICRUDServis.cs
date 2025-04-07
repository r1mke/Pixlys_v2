using Pixly.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.Interfaces
{
    public interface ICRUDServis<TModel, TSearch, TInsert, TUpdate>
    {
        public List<TModel> GetPaged(TSearch search);
        public TModel GetById(int id);

        public TModel Insert(TInsert request);

        public TModel Update(int id,TUpdate request);
        
    }
}
