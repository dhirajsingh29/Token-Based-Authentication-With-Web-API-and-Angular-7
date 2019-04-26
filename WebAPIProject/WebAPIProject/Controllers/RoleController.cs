﻿
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    public class RoleController : ApiController
    {
        [HttpGet]
        [Route("api/GetAllRoles")]
        [AllowAnonymous]
        public HttpResponseMessage GetRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = roleManager.Roles
                .Select(x => new { x.Id, x.Name })
                .ToList();
            return this.Request.CreateResponse(HttpStatusCode.OK, roles);
        }
    }
}
