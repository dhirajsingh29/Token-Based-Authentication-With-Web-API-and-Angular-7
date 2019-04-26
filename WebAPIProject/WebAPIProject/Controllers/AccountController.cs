
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Web.Http;
using WebAPIProject.Models;
using WebAPIProject.CustomAttributes;

namespace WebAPIProject.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/user/register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };

            // here user is created and stored into User table
            var result = manager.Create(user, model.Password);
            // here user Id and role Id for different kind of roles
            // are stored in UserRole table
            manager.AddToRoles(user.Id, model.Roles);
            return result;
        }

        [HttpGet]
        [Route("api/GetUserClaims")]
        [Authorize]
        public AccountModel GetUserDetailsFromClaims()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var accountModel = new AccountModel
            {
                UserName = claimsIdentity.FindFirst("Username").Value,
                FirstName = claimsIdentity.FindFirst("FirstName").Value,
                LastName = claimsIdentity.FindFirst("LastName").Value,
                Email = claimsIdentity.FindFirst("Email").Value,
                LoggedOn = claimsIdentity.FindFirst("LoggedOn").Value
            };
            return accountModel;
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Admin")]
        [Route("api/ForAdmin")]
        public string ForAdmin()
        {
            return "for admin role users";
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Author")]
        [Route("api/ForAuthor")]
        public string ForAuthor()
        {
            return "For author role users";
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Author, Reader")]
        [Route("api/ForAuthorOrReader")]
        public string ForAuthorOrReader()
        {
            return "For author/reader role users";
        }
    }
}
