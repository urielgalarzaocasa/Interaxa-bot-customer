using System;
using System.Data;

namespace ProyectoGenerico.Helper
{
    public class FileHelper
    {
        public static bool GuardarFileString(string ruta, string text)
        {
            bool result = false;
            try
            {
                System.IO.File.WriteAllText(ruta, text);
                result = true;
                LogHelper.GetInstance().PrintDebug("GuardarFileString log : " + ruta);
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex);
            }
            return result;
        }

        public static bool GuardarFileXml(string ruta, DataSet data)
        {
            bool result = false;
            try
            {
                data.WriteXml(ruta);
                result = true;
                LogHelper.GetInstance().PrintDebug("GuardarFileXml log : " + ruta);
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex);
            }
            return result;
        }
    }
}