using ProyectoGenerico.Data;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using ProyectoGenerico.Services;

namespace ProyectoGenerico.BusinessRules
{
    public class TestBusinessRules
    {
        public TestEntity Save(TestAdd param)
        {
            var data = new TestData();

            //Validaciones
            
            TestService.Servicio(); //Asi se llamaria a otro servicio (API, SAP, ETC)

            return data.Add(param);
        }
    }
}