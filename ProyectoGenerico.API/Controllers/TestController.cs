using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using System;
using System.Web.Http;
using ProyectoGenerico.Helper;

namespace ProyectoGenerico.API.Controllers
{
    public class TestController : ApiController
    {
        public TestEntity Post([FromBody] TestAdd value)
        {
            try
            {
                //Es un log de prueba, quitarlo
                LogHelper.GetInstance().PrintDebug("Llamo a la api");
                TestBusinessRules testBusinessRules = new TestBusinessRules();
                return testBusinessRules.Save(value);
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex.Message);
                throw ex;
            }
        }
    }
}