using System;
using System.Web.Http;
using ProyectoGenerico.Helper;
using ProyectoGenerico.Entities;
using ProyectoGenerico.BusinessRules;

namespace ProyectoGenerico.API.Controllers
{
    public class BotMessageController : ApiController
    {
        public BotMessageResponse Get(string seguimiento, string tipo = "")
        {
            try
            {
                LogHelper.GetInstance().PrintDebug("Api inicio: ");
                return new BotMessageBusinessRules(seguimiento, tipo).GetMessage();
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("Api error: " + ex.Message);
                return new BotMessageResponse().ErrorResponse();
            }
            finally
            {
                LogHelper.GetInstance().PrintDebug("Api fin");
            }
        }

        [ApiKeyAuth]
        public BotMessageResponse Post([FromBody] BotMessageRequest request)
        {
            try
            {
                LogHelper.GetInstance().PrintDebug("Api inicio: ");
                return new BotMessageBusinessRules(request.Seguimiento, request.Tipo, request.TipoCliente).GetMessage();
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("Api error: " + ex.Message);
                return new BotMessageResponse().ErrorResponse();
            }
            finally
            {
                LogHelper.GetInstance().PrintDebug("Api fin");
            }
        }
    }
}