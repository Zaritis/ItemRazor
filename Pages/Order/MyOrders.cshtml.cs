using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.DAO;
using ItemRazor.Models;
using ItemRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazor.Pages.Order
{
    public class MyOrdersModel : PageModel
    {
        public UserService UserService { get; set; }
       
        //public IEnumerable<OrderDAO> MyOrders { get; set; }
        public IEnumerable<Models.Order> MyOrders { get; set; }

        public MyOrdersModel(UserService userService)
        {
            UserService = userService;
        }
        public IActionResult OnGet()
        {
            User CurrentUser = UserService.GetUserByUserName(HttpContext.User.Identity.Name); 
            //MyOrders = UserService.GetUserOrders(CurrentUser);
            MyOrders = UserService.GetOrdersByUser(CurrentUser).Orders;
            return Page();
        }
    }
}
