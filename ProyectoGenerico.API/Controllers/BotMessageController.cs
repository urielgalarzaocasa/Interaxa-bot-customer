using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using System;
using System.Web.Http;
using ProyectoGenerico.Helper;

namespace ProyectoGenerico.API.Controllers
{
    public class BotMessageController : ApiController
    {
        public BotMessageResponse Get(string seguimiento, string tipo)
        {
            BotMessageResponse botMessageResponse = new BotMessageResponse();
            try
            {
                LogHelper.GetInstance().PrintDebug("Api inicio: ");

                BotMessageBusinessRules botMessageBusinessRules = new BotMessageBusinessRules(seguimiento, tipo);

                botMessageResponse = botMessageBusinessRules.GetMessage();

                return botMessageResponse;
            }
            catch (Exception ex)
            {
                //excepciones sin controlar, enviar por email!

                LogHelper.GetInstance().PrintError("Api error: " + ex.Message);
                
                botMessageResponse.derivaAsesor = true;
                botMessageResponse.error = true;
                botMessageResponse.message = "Derivar al asesor";
                
                return botMessageResponse;
            }
            finally
            {
                LogHelper.GetInstance().PrintDebug("Api fin");
            }
        }
    }
}