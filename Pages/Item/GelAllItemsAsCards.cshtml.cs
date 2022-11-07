using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazor.Pages.Item
{
    public class GelAllItemsAsCardsModel : PageModel
    {
        private ItemService itemService;
        public List<Models.Item> Items { get; private set; }


        public GelAllItemsAsCardsModel(ItemService itemService)
        {
            this.itemService = itemService;
        }


        public IActionResult OnGet()
        {
            Items = itemService.GetItems().ToList();
            return Page();
        }
    }
}
