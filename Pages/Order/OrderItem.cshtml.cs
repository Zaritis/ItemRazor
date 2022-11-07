using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Models;
using ItemRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazor.Pages.Item
{
    public class OrderItemModel : PageModel
    {
        private ItemService itemService;
        private UserService userService;
        private OrderService orderService;

     
        public Models.Item Item { get; set; }
    
        public User User { get; set; }
        
        public Models.Order Order { get; set; } = new Models.Order();

        [BindProperty] public int Count { get; set; }

        public OrderItemModel(ItemService itemService, UserService userService, OrderService orderService)
        {
            this.itemService = itemService;
            this.userService = userService;
            this.orderService = orderService;
        }


        public void OnGet(int id)
        {
            Item = itemService.GetItem(id);
            User = userService.GetUserByUserName(HttpContext.User.Identity.Name);
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Item = itemService.GetItem(id);
            User = userService.GetUserByUserName(HttpContext.User.Identity.Name);
            Order.UserId = User.UserId;
            Order.ItemId = Item.ItemId;
            Order.Date=DateTime.Now;
            Order.Count = Count;
            orderService.AddOrder(Order);
            return RedirectToPage("../Item/GetAllItems");
        }
    }
}
