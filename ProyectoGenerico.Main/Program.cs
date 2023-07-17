using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities.ViewModel;
using ProyectoGenerico.Helper;
using System;

namespace ProyectoGenerico.Main
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var testBussinessRules = new TestBusinessRules();
                var nombre = new TestAdd() { Nombre = "Nombre", Apellido = "Apellido" };
                var result = testBussinessRules.Save(nombre);

                Console.WriteLine(result.Nombre);
                Console.WriteLine(result.Apellido);
                Console.WriteLine(result.Id);
                Console.WriteLine(result.Fecha_sys);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex.Message);
                MailHelper.SendMail($"Se produjo un error: {ex.Message} {ex.StackTrace }" + ex.InnerException, ConfigurationHelper.GetValue("Configuration", "Mail_Error"));
            }
        }
    }
}