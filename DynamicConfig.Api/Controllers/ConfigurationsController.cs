using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DynamicConfig.Lib.DataAccess.MongoDB;
using DynamicConfig.Lib.IOC;
using DynamicConfig.Lib.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DynamicConfig.Api.Controllers
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string ExceptionMessage { get; set; }
        public object Data { get; set; }
        
    }
    
    
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ConfigurationsController : Controller
    {
        private readonly IContainer _container;
        private readonly IConfigurationRepository _configurationRepository;
        private ResponseModel _responseModel;

        public ConfigurationsController()
        {
            _responseModel = new ResponseModel();   
            _container = DependencyService.Instance.CurrentResolver;
            _configurationRepository = _container.Resolve<IConfigurationRepository>();
        }
        
        // GET
        [HttpGet]
        public async Task<IActionResult> List(string searchModel)
        {
            
            var list = await _configurationRepository.GetAll();
            if (!string.IsNullOrEmpty(searchModel))
            {
                list = list.Where(v => v.Name.ToLower().Contains(searchModel.ToLower()));
            }

            _responseModel.Data = list;

            return Ok(_responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Configuration config)
        {
            await _configurationRepository.Create(config);
            
            return Ok(_responseModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Configuration config)
        {
            _responseModel.Status = await _configurationRepository.Update(config);
            return Ok(_responseModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string _id)
        {
            _responseModel.Status = await _configurationRepository.Delete(_id);
            return Ok(_responseModel);
        }
    }
}