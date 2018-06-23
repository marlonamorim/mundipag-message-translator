using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;

namespace MundiPag.MessageTranslator.SharedKernel.Api.Authorization
{
    public class BasicAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];
                
                if (IsAuthorized(username, password))
                {
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
        }

        public bool IsAuthorized(string username, string password)
        {
            return username.Equals("mundipag", StringComparison.InvariantCultureIgnoreCase)
                   && password.Equals("123456");
        }
    }
}
