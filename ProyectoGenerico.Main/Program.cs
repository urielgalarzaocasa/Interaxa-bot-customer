using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGenerico.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var testBussinessRules = new TestBusinessRules();
            var nombre = new Entities.ViewModel.TestAdd() { Nombre = "Nombre", Apellido = "Apellido" };
            var result = testBussinessRules.Save(nombre);

            Console.WriteLine(result.Nombre);
            Console.WriteLine(result.Apellido);
            Console.WriteLine(result.Id);
            Console.WriteLine(result.Fecha_sys);
            Console.ReadLine();
        }
    }
}
