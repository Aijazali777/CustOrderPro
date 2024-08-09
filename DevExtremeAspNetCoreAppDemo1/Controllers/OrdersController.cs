using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CustOrderPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CustOrderPro.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class OrdersController : Controller
    {
        private AppDbContext _context;
        private UserManager<Customer> userManager;

        public OrdersController(AppDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        #region Orders ActionMethod
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [Authorize(Roles = "User")]
        public IActionResult CustomerOrdersUserView()
        {
            var orderReceipts = _context.OrderReceipts.ToList();
            return View(orderReceipts);
        }
        #endregion

        #region REST API Methods
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions, string customerId)
        {
            var orders = _context.Orders.Where(order => order.CustomerId == customerId);
            return DataSourceLoader.Load(orders, loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Order();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            var customerName = _context.Users.Where(cust => cust.Id == model.CustomerId).Select(cust => cust.Name).FirstOrDefault();
            var receiptModel = new OrderReceipt
            {
                ReceiptDetails = "#CustOrderPro Order Receipt#\tCustomerName: " + customerName + ", Total Items: " + model.NumberOfItems + ", Total Price: " + model.TotalPrice + ", Bill: " + model.PaymentStatus + ".",
                OrderId = model.OrderId
            };
            var orderReceipt = _context.OrderReceipts.Add(receiptModel);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.OrderId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Orders.FirstOrDefaultAsync(item => item.OrderId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();

            var customerName = _context.Users.Where(cust => cust.Id == model.CustomerId).Select(cust => cust.Name).FirstOrDefault();
            var receiptModel = await _context.OrderReceipts.FirstOrDefaultAsync(or => or.OrderId == model.OrderId);
            receiptModel.ReceiptDetails = "#CustOrderPro Order Receipt#\tCustomerName: " + customerName + ", Total Items: " + model.NumberOfItems + ", Total Price: " + model.TotalPrice + ", Bill: " + model.PaymentStatus + ".";
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Orders.FirstOrDefaultAsync(item => item.OrderId == key);

            _context.Orders.Remove(model);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IActionResult> CustomersLookup(DataSourceLoadOptions loadOptions, string custId)
        {
            if (custId != null)
            {
                var lookupOne = from i in _context.Users
                                where i.Id == custId
                                select new
                                {
                                    Value = i.Id,
                                    Text = i.Name
                                };
                return Json(await DataSourceLoader.LoadAsync(lookupOne, loadOptions));
            }
            else
            {
                var lookup = from i in _context.Users
                             orderby i.Name
                             select new
                             {
                                 Value = i.Id,
                                 Text = i.Name
                             };
                return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
            }
        }

        private void PopulateModel(Order model, IDictionary values) {
            string ORDER_ID = nameof(Order.OrderId);
            string CUSTOMER_ID = nameof(Order.CustomerId);
            string NUMBER_OF_ITEMS = nameof(Order.NumberOfItems);
            string TOTAL_PRICE = nameof(Order.TotalPrice);
            string PAYMENT_STATUS = nameof(Order.PaymentStatus);

            if (values.Contains(ORDER_ID)) {
                model.OrderId = Convert.ToInt32(values[ORDER_ID]);
            }

            if (values.Contains(CUSTOMER_ID))
            {
                model.CustomerId = Convert.ToString(values[CUSTOMER_ID]);
            }

            if (values.Contains(NUMBER_OF_ITEMS))
            {
                model.NumberOfItems = Convert.ToInt32(values[NUMBER_OF_ITEMS]);
            }

            if (values.Contains(TOTAL_PRICE)) {
                model.TotalPrice = Convert.ToDouble(values[TOTAL_PRICE], CultureInfo.InvariantCulture);
            }

            if (values.Contains(PAYMENT_STATUS))
            {
                model.PaymentStatus = Convert.ToString(values[PAYMENT_STATUS]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
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