using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(WebAPIProject.OwinHelper.Startup))]

namespace WebAPIProject.OwinHelper
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // enable CORS inside the application
            app.UseCors(CorsOptions.AllowAll);

            var options = new OAuthAuthorizationServerOptions
            {
                // url to authenticate a user. HttpPost request will be made 
                // to this url with username and password
                TokenEndpointPath = new PathString("/token"),
                // provider where we authenticate the user and create claims
                Provider = new ApplicationOAuthProvider(),
                // expiration time for access token form token request's response 
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                // to access token form unsecured HTTP
                AllowInsecureHttp = true
            };
            // this middleware performs the request processing of the Authorize
            // and Token endpoints defined by the OAuth2 specification.
            app.UseOAuthAuthorizationServer(options);
            // adds Bearer token processing to an OWIN application pipeline
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
