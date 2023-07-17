using System.Collections.Specialized;
using System.Configuration;

namespace ProyectoGenerico.Helper
{
    public class ConfigurationHelper
    {
        public static string GetValue(string seccion, string value)
        {
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection(seccion);
            return config.Get(value);
        }
    }
}