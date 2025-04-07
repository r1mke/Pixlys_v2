using Microsoft.AspNetCore.Mvc;
using Pixly.Model.Requests;
using Pixly.Services.Interfaces;
using Pixly.Services.Services;

namespace PIxly_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CRUDController<TModel, TSearch, TInsert, TUpdate> : Controller
    {
        protected ICRUDServis<TModel,TSearch,TInsert,TUpdate> _service;

        public CRUDController(ICRUDServis<TModel, TSearch, TInsert,TUpdate> service)
        {
            _service = service;
        }

        [HttpGet]
        public  List<TModel> GetPaged([FromQuery] TSearch search) 
        {
           return _service.GetPaged(search);
        }

        [HttpGet("{id}")]
        public TModel GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public TModel Insert(TInsert request)
        {
            return _service.Insert(request);
        }

        [HttpPost("{id}/update")]
        public TModel Update(int id,TUpdate request)
        {
            return _service.Update(id,request);
        }
    }
}
