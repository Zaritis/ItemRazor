using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ItemRazor.Models;
using ItemRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazor.Pages
{
    public class CreateItemModel : PageModel
    {
        private ItemService itemService;
        private List<Models.Item> items;
       
        [BindProperty]
        public Models.Item Item { get; set; }

        public CreateItemModel(ItemService iS)
        {
            itemService = iS;
            items = itemService.GetItems().ToList();
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            itemService.AddItem(Item);
            return RedirectToPage("GetAllItems");
        }

    }
}
