using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using ProyectoGenerico.Helper;
using System;
using System.Collections.Generic;

namespace ProyectoGenerico.Main
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("No se pudo insertar estrategias: " + ex.Message);
                //MailHelper.SendMail($"Se produjo un error: {ex.Message} {ex.StackTrace }" + ex.InnerException, ConfigurationHelper.GetValue("Configuration", "Mail_Error"));
            }
        }
    }
}