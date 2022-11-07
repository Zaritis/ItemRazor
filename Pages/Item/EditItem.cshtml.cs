using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazor.Pages.Item
{
    public class EditItemModel : PageModel
    {
        private ItemService itemService;

        [BindProperty]
        public Models.Item Item { get; set; }

        public EditItemModel(ItemService itemService)
        {
            this.itemService = itemService;
        }
        public IActionResult OnGet(int id)
        {
            Item = itemService.GetItem(id);
            if (Item == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Item = itemService.GetItem(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            itemService.UpdateItem(Item);
            return RedirectToPage("GetAllItems");
        }
    }
}
