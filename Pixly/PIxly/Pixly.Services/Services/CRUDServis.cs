using MapsterMapper;
using Pixly.Services.Database;
using Pixly.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixly.Model.Requests;
namespace Pixly.Services.Services
{
    public class CRUDServis<TModel, TSearch, TInsert,TUpdate, TDbEntity> : ICRUDServis<TModel, TSearch, TInsert, TUpdate> where TDbEntity : class where TModel:class
    {
        public Context Context { get; set; }
        public IMapper Mapper { get; set; }

        public CRUDServis(Context context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public TModel GetById(int id)
        {
            var entity = Context.Set<TDbEntity>().Find(id);
            if (entity == null)
            {
                return null;
            }
            else
            {
                return Mapper.Map<TModel>(entity);
            }
        }

        public List<TModel> GetPaged(TSearch search)
        {
            List<TModel> models = new List<TModel>();
            var query = Context.Set<TDbEntity>().AsQueryable();

            query = AddFilter(search,query);

            var list = query.ToList();

            return Mapper.Map(list, models);
        }

        protected virtual IQueryable<TDbEntity> AddFilter(TSearch? search, IQueryable<TDbEntity> query)
        {
            return query;
        }

        public virtual TModel Insert(TInsert request)
        {
            TDbEntity entity = Mapper.Map<TDbEntity>(request);
       
            BeforeInsert(request, entity);
            Context.Add(entity);
            Context.SaveChanges();
            return Mapper.Map<TModel>(entity);
        }

        protected virtual void BeforeInsert(TInsert request, TDbEntity entity)
        {
            
        }

        public virtual TModel Update(int id, TUpdate request)
        {
            var set = Context.Set<TDbEntity>();

            var entity = set.Find(id);

            Mapper.Map(request, entity);
            BeforeUpdate(request, entity);

            Context.SaveChanges();

            return Mapper.Map<TModel>(entity);
        }

        private void BeforeUpdate(TUpdate request, TDbEntity? entity)
        {
            
        }

    }
}
