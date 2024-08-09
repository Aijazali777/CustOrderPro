using CustOrderPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nager.Country;
using Newtonsoft.Json;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext context;
        private UserManager<Customer> userManager;
        private SignInManager<Customer> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private PhoneNumberUtil phoneNumberUtil;
        private CountryProvider countryProvider;

        public AccountController(AppDbContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            phoneNumberUtil = PhoneNumberUtil.GetInstance();
            countryProvider = new CountryProvider();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Gender = SelectGender();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            ViewBag.Gender = SelectGender();

            if (ModelState.IsValid)
            {
                try
                {
                    var phoneNumberProto = phoneNumberUtil.Parse(model.PhoneNumber, null);
                    if (!phoneNumberUtil.IsValidNumber(phoneNumberProto))
                    {
                        ModelState.AddModelError("PhoneNumber", "Invalid phone number");
                        return View(model);
                    }
                }
                catch(NumberParseException)
                {
                    ModelState.AddModelError("PhoneNumber", "Invalid phone number");
                    return View(model);
                }

                string CountryName = GetCountryName(model.Country);

                var user = new Customer { UserName = model.Email, Email = model.Email, Name = model.Name, Gender = model.Gender, PhoneNumber = model.PhoneNumber, Country = CountryName, Address = model.Address, City = model.City};
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleExists = await roleManager.RoleExistsAsync("User");
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole("User"));
                    }
                    await userManager.AddToRoleAsync(user, "User");

                    return RedirectToAction("login");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public List<SelectListItem> SelectGender()
        {
            List<SelectListItem> genderList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Male", Text = "Male" },
                new SelectListItem { Value = "Female", Text = "Female" },
                new SelectListItem { Value = "Other", Text = "Other" }
            };

            return genderList;
        }

        public string GetCountryName(string iso2Code)
        {
            if (string.IsNullOrWhiteSpace(iso2Code))
            {
                throw new ArgumentException("ISO2 code cannot be null or empty", nameof(iso2Code));
            }

            iso2Code = iso2Code.ToUpper();

            var country = countryProvider.GetCountry(iso2Code);
            if (country == null)
            {
                return "Unknown Country Code";
            }

            return country.CommonName;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       /* [HttpPost]
        public async Task<IActionResult> Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var user = context.Users.FirstOrDefault(e => e.Email == model.Email);

                    if (user != null)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        if (roles.Count == 0)
                        {
                            ModelState.AddModelError(string.Empty, "You donot have access");
                            return View(model);
                        }

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {   
                            if(roles.Contains("Admin") && !roles.Contains("User"))
                                return RedirectToAction("CustomersOrdersAdminView", "customers", user);
                            else
                                return RedirectToAction("CustomerOrdersUserView", "Orders", user);
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }*/

        [HttpPost]
        public async Task<IActionResult> Login(string values, string returnUrl)
        {
            var model = JsonConvert.DeserializeObject<Login>(values);

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var user = context.Users.FirstOrDefault(e => e.Email == model.Email);

                    if (user != null)
                    {
                        var roles = await userManager.GetRolesAsync(user);

                        if (roles.Count == 0)
                        {
                            ModelState.AddModelError(string.Empty, "You do not have access");
                            return BadRequest(ModelState);
                        }

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            if (roles.Contains("Admin") && !roles.Contains("User"))
                                return RedirectToAction("CustomersOrdersAdminView", "customers", user);
                            else
                                return RedirectToAction("CustomerOrdersUserView", "Orders", user);
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if(user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View();
                }
                await signInManager.RefreshSignInAsync(user);
                return View();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
