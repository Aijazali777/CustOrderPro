using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using CustOrderPro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Controllers
{
    public class UsersInRoleController : Controller
    {
        private readonly AppDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersInRoleController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager, SignInManager<Customer> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        #region REST API Methods
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions, string roleId)
        {
            httpContextAccessor.HttpContext.Session.SetString("RoleID", roleId);
            var usersInRole = context.UserRoles.Where(m => m.RoleId == roleId);
            return DataSourceLoader.Load(usersInRole, loadOptions);  
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new IdentityUserRole<string>();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var existingUserRole = await context.UserRoles.SingleOrDefaultAsync(ur => ur.UserId == model.UserId && ur.RoleId == model.RoleId);
            if (existingUserRole != null)
            {
                return BadRequest("User is already in the specified role.");
            }

            var result = context.UserRoles.Add(model);
            await context.SaveChangesAsync();

            var currentUser = await userManager.GetUserAsync(User);
            if (model.UserId == currentUser.Id)
            {
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(currentUser, isPersistent: false);
            }

            return Json(new { result.Entity.UserId });
        }

        [HttpDelete]
        public async Task Delete(string key)
        {
            string roleId = httpContextAccessor.HttpContext.Session.GetString("RoleID");
            var model = await context.UserRoles.FirstOrDefaultAsync(item => (item.UserId == key && item.RoleId == roleId));
            context.UserRoles.Remove(model);
            await context.SaveChangesAsync();

            var currentUser = await userManager.GetUserAsync(User); 
            if (model.UserId == currentUser.Id && (await userManager.IsInRoleAsync(currentUser, "Admin") || await userManager.IsInRoleAsync(currentUser, "User")))
            {
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(currentUser, isPersistent: false);
            }
            else
            {
                await signInManager.SignOutAsync();
            }
        }


        [HttpGet]
        public async Task<IActionResult> RolesLookup(DataSourceLoadOptions loadOptions, string roleId)
        {
            if (roleId != null)
            {
                var lookupOne = from i in context.Roles
                                where i.Id == roleId
                                select new
                                {
                                    Value = i.Id,
                                    Text = i.Name
                                };
                return Json(await DataSourceLoader.LoadAsync(lookupOne, loadOptions));
            }
            else
            {
                var lookup = from i in context.Roles
                             orderby i.Name
                             select new
                             {
                                 Value = i.Id,
                                 Text = i.Name
                             };
                return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
            }
        }

        [HttpGet]
        public async Task<IActionResult> UsersLookup(DataSourceLoadOptions loadOptions, string userId)
        {
            if (userId != null)
            {
                var lookupOne = from i in context.Users
                                where i.Id == userId
                                select new
                                {
                                    Value = i.Id,
                                    Text = i.Name
                                };
                return Json(await DataSourceLoader.LoadAsync(lookupOne, loadOptions));
            }
            else
            {
                var lookup = from i in context.Users
                             orderby i.Name
                             select new
                             {
                                 Value = i.Id,
                                 Text = i.Name
                             };
                return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
            }
        }

        private void PopulateModel(IdentityUserRole<string> model, IDictionary values)
        {
            string ROLEID = nameof(IdentityUserRole<string>.RoleId);
            string USERID = nameof(IdentityUserRole<string>.UserId);

            if (values.Contains(ROLEID))
            {
                model.RoleId = Convert.ToString(values[ROLEID]);
            }

            if (values.Contains(USERID))
            {
                model.UserId = Convert.ToString(values[USERID]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
        #endregion
    }
}
