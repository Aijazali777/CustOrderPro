using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustOrderPro.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace CustOrderPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private AppDbContext _context;
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CustomersController(AppDbContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        #region Customers ActionMethods
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [Authorize(Roles = "Admin")]
        public IActionResult CustomersOrdersAdminView()
        {
            var orderReceipts = _context.OrderReceipts.ToList();
            return View(orderReceipts);
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [HttpPost]
        public IActionResult Profile(Customer model)
        {
            if (model != null)
            {
                var user = _context.Users.FirstOrDefault(c => c.UserName == User.Identity.Name);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Address = model.Address;
                    user.City = model.City;
                    user.Country = model.Country;
                    _context.SaveChanges();
                }
            }
            return View();
        }
        #endregion

        #region REST API Methods
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var customers = _context.Users;
            return DataSourceLoader.Load(customers, loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Customer();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
                return BadRequest($"This email '{existingUser.Email}' is already regiestered.");

            var result = await userManager.CreateAsync(model, "User123#");
            if (result.Succeeded)
            {
                var roleExists = await roleManager.RoleExistsAsync("User");
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
                await userManager.AddToRoleAsync(model, "User");
            }
            return Json(new { result});
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values)
        {
            var model = await _context.Users.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Id != model.Id);
            if (existingUser != null)
                return BadRequest($"This email '{existingUser.Email}' is already regiestered.");

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            var model = await _context.Users.FirstOrDefaultAsync(item => item.Id == key);
            var currentUser = await userManager.GetUserAsync(User);
            if (model.Id == currentUser.Id)
                return BadRequest("Currently signed-in user cannot be deleted");

            _context.Users.Remove(model);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private void PopulateModel(Customer model, IDictionary values)
        {
            string ID = nameof(Customer.Id);
            string NAME = nameof(Customer.Name);
            string EMAIL = nameof(Customer.Email);
            string PASSWORD = nameof(Customer.PasswordHash);
            string GENDER = nameof(Customer.Gender);
            string PHONE = nameof(Customer.PhoneNumber);
            string ADDRESS = nameof(Customer.Address);
            string CITY = nameof(Customer.City);
            string COUNTRY = nameof(Customer.Country);

            if (values.Contains(ID)) {
                model.Id = Convert.ToString(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if (values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
                model.NormalizedEmail = Convert.ToString(values[EMAIL]);
                model.UserName = Convert.ToString(values[EMAIL]);
                model.NormalizedUserName = Convert.ToString(values[EMAIL]);
            }

            if (values.Contains(PASSWORD)) {
                model.PasswordHash = Convert.ToString(values[PASSWORD]);
            }

            if (values.Contains(GENDER)) {
                model.Gender = Convert.ToString(values[GENDER]);
            }

            if (values.Contains(PHONE)) {
                model.PhoneNumber = Convert.ToString(values[PHONE]);
            }

            if (values.Contains(ADDRESS)) {
                model.Address = Convert.ToString(values[ADDRESS]);
            }

            if (values.Contains(CITY)) {
                model.City = Convert.ToString(values[CITY]);
            }

            if (values.Contains(COUNTRY)) {
                model.Country = Convert.ToString(values[COUNTRY]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
        #endregion
    }
}