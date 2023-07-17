using ProyectoGenerico.Data;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;

namespace ProyectoGenerico.BusinessRules
{
    public class TestBusinessRules
    {
        public Entities.TestAdd Save(Entities.ViewModel.TestAdd param)
        {
            var data = new TestData();
            return data.Add(param);
        }
    }
}