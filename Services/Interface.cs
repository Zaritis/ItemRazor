using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Models;

namespace ItemRazor.Services
{
    interface IItemService
    {
        IEnumerable<Item> GetItems();
        void AddItem(Item item);

        IEnumerable<Item> NameSearch(string str);

        IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0);
    }
}
