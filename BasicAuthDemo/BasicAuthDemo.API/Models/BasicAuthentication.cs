using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicAuthDemo.API.Models
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            // Check client passed any value in header or not
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // get the header values
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                // decoded the authenticationToken values because client passed the username and password in encoded form
                string decodedAuthenticationToken =
                    Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                // split the username and password by : because client passed the username and password as "username : password"
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');

                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                UserManager userRepository = new UserManager();

                //validate from the database for this username or password
                if (userRepository.ValidateUser(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), roles: null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

        }
    }
}