using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazor.Pages.Item
{
    public class GetAllItemsPagePaginationModel : PageModel
    {
        private ItemService itemService;
        public List<Models.Item> Items { get; private set; }
        [BindProperty] public string SearchString { get; set; }
        [BindProperty] public int MaxPrice { get; set; }
        [BindProperty] public int MinPrice { get; set; }

        //Page pagination:
        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));





        public GetAllItemsPagePaginationModel(ItemService itemService) //Dependency Injection
        {
            this.itemService = itemService;
        }

        //public IActionResult OnGet()
        //{
        //    Items = itemService.GetItems().ToList();
        //    return Page();
        //}

        public IActionResult OnGetSortById()
        {
            Items = itemService.SortById().ToList();
            return Page();
        }

        public IActionResult OnGetSortByIdDescending()
        {
            Items = itemService.SortByIdDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByName()
        {
            Items = itemService.SortByName().ToList();
            return Page();
        }

        public IActionResult OnGetSortByNameDescending()
        {
            Items = itemService.SortByNameDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByPrice()
        {
            Items = itemService.SortByPrice().ToList();
            return Page();
        }

        public IActionResult OnGetSortByPriceDescending()
        {
            Items = itemService.SortByPriceDescending().ToList();
            return Page();
        }


        public IActionResult OnPostNameSearch()
        {
            Items = itemService.NameSearch(SearchString).ToList();
            return Page();
        }

        public IActionResult OnPostPriceFilter(int maxPrice, int minPrice = 0)
        {
            Items = itemService.PriceFilter(maxPrice, minPrice).ToList();
            return Page();
        }


        //Page pagination
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public async Task OnGetAsync()
        {
            Items = await itemService.GetPaginatedResult(CurrentPage, PageSize);
            Count = await itemService.GetCount();
        }
    }

}

