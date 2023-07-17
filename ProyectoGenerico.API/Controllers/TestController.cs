using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using System.Collections.Generic;
using System.Web.Http;

namespace ProyectoGenerico.API.Controllers
{
    public class TestController : ApiController
    {
        public Entities.TestAdd Post([FromBody] Entities.ViewModel.TestAdd value)
        {
            TestBusinessRules testBusinessRules = new TestBusinessRules();
            return testBusinessRules.Save(value);
        }
    }
}