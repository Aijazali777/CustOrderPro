using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using CustOrderPro.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationController(AppDbContext context , UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            
        }

        #region Action Method
        public IActionResult ManageRoles()
        {
            var model = roleManager.Roles;
            return View(model);
        }
        #endregion

        #region REST API Methods
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var roles = context.Roles;
            return DataSourceLoader.Load(roles, loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new IdentityRole();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var existingRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == model.Name);
            if (existingRole != null)
                return BadRequest($"This role '{existingRole.Name}' already exists.");

            var result = context.Roles.Add(model);
            await context.SaveChangesAsync();
            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values)
        {
            var model = await context.Roles.FirstOrDefaultAsync(item => item.Id == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var existingRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == model.Name);
            if (existingRole != null)
                return BadRequest($"This role '{existingRole.Name}' already exists.");

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            var model = await context.Roles.FirstOrDefaultAsync(item => item.Id == key);
            var currentUser = await userManager.GetUserAsync(User);
            var currentUserInRole = await context.UserRoles.AnyAsync(u => u.UserId == currentUser.Id && u.RoleId == model.Id);

            if (currentUserInRole)
                return BadRequest("Cannot delete role. The current user is assigned to this role.");

            context.Roles.Remove(model);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private void PopulateModel(IdentityRole model, IDictionary values)
        {
            string ID = nameof(IdentityRole.Id);
            string NAME = nameof(IdentityRole.Name);

            if (values.Contains(ID))
            {
                model.Id = Convert.ToString(values[ID]);
            }

            if (values.Contains(NAME))
            {
                model.Name = Convert.ToString(values[NAME]);
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
