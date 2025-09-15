using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ProyectoGenerico.Helper
{
    public class ApiKeyAuthAttribute : AuthorizeAttribute
    {
        private const string _apiKeyHeaderName = "X-API-KEY";
        private readonly string _apiKey = ConfigurationHelper.GetValue("appSettings", "PublicApiKey");
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(_apiKeyHeaderName))
            {
                var apiKey = actionContext.Request.Headers.GetValues(_apiKeyHeaderName).FirstOrDefault();
                if (apiKey == _apiKey)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                new { message = "No autorizado" });
        }
    }
}
