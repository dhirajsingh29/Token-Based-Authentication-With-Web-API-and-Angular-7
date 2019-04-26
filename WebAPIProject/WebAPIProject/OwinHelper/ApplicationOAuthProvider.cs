
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPIProject.Models;

namespace WebAPIProject.OwinHelper
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(
            OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = await manager.FindAsync(context.UserName, context.Password);
            if (user == null) return;
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaims(new List<Claim>
            {
                new Claim("Username", user.UserName),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("LoggedOn", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            });

            var userRoles = manager.GetRoles(user.Id);
            foreach (var roleName in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }

            var roles = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "role", Newtonsoft.Json.JsonConvert.SerializeObject(userRoles) }
            });
            var token = new AuthenticationTicket(identity, roles);
            // context.Validated(identity);
            context.Validated(token);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}