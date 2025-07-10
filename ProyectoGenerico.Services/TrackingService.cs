using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Helper;
//using static System.Net.WebRequestMethods;

namespace ProyectoGenerico.Services
{
    public class TrackingService
    {
        private readonly string url = "\r\n https://tracking.ocasa.com/api/ServiceTracking/GetTrack";
        private readonly string token = "98099c50f91f30d06c38a42cf37cbec94c8b7e965a0b6a5ed1ab67820ee7ca42";



        public static Tracking GetTracking(string NroSeguimiento, string Trackingtype)
        {
            Tracking tracking = new Tracking();

            string url = "https://tracking.ocasa.com/api/ServiceTracking/GetTrack";


            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
                SslProtocols = SslProtocols.Tls12 // version TLS
            };

            using (HttpClient client = new HttpClient(handler))
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    string jsonBody = $"{{'token':'98099c50f91f30d06c38a42cf37cbec94c8b7e965a0b6a5ed1ab67820ee7ca42' , 'tracking':'{NroSeguimiento}', 'Trackingtype':'{Trackingtype}'}}";
                    var httpContent = new StringContent(jsonBody, null, "application/json");
                    var response = client.PostAsync(url, httpContent).Result;
                    var res = response.Content.ReadAsStringAsync().Result;

                    JsonSerializerOptions options = new JsonSerializerOptions(); //{ Converters = {  new CustomDateTimeConverterPostal(), new CustomDateTimeConverter() } };
                    
                    options.Converters.Add(new CustomDateTimeConverter());

                    tracking = JsonSerializer.Deserialize<Tracking>(res, options);

                }
                catch (Exception ex)
                {
                    LogHelper.GetInstance().PrintError($"Error en llamada a API de tracking: {ex.Message}");
                }
                return tracking;
            }
        }

        internal class CustomDateTimeConverter : JsonConverter<DateTime>
        {
            private string DateFormat = "dd/MM/yyyy HH:mm:ss";


            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string[] DateFormats = { "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy HH:mm" };
                string date = reader.GetString();

                DateTime.TryParseExact(date, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None , out DateTime result );
                return result;

            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(DateFormat));
            }
        }
    }
}