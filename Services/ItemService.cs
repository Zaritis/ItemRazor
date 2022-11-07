using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Comperators;
using ItemRazor.Models;
using ItemRazor.MockData;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ItemRazor.Services
{
    public class ItemService : IItemService
    {

        private List<Item> items;
        private JsonFileService<Item> JsonFileService { get; set; }
        public DbGenericService<Item> DbService { get; set; }

        public ItemService(JsonFileService<Item> jsonFileService, DbGenericService<Item> dbService)
        {
            //JsonFileService = jsonFileService;
            DbService = dbService;
            //items = MockItems.GetMockItems();
            //items = JsonFileService.GetJsonObjects().ToList();
            //foreach (Item item in items)
            //{
            //    dbService.AddObjectAsync(item);
            //}

            items = dbService.GetObjectsAsync().Result.ToList();

        }

        public async void AddItem(Item item)
        {
            items.Add(item);
            //JsonFileService.SaveJsonObjects(items);
            await DbService.AddObjectAsync(item);
        }

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(int id)
        {
            foreach (Item item in items)
            {
                if (item.ItemId == id) return item;
            }
            return null;
        }

        public Item DeleteItem(int itemId)
        {
            Item itemToBeDeleted = null;
            foreach (Item i in items)
            {
                if (i.ItemId == itemId)
                {
                    itemToBeDeleted = i;
                    break;
                }
            }
            if (itemToBeDeleted != null)
            {
                items.Remove(itemToBeDeleted);
                //JsonFileService.SaveJsonObjects(items);
                DbService.DeleteObjectAsync(itemToBeDeleted);
            }
            return itemToBeDeleted;
        }


        public void UpdateItem(Item item)
        {
            if (item != null)
            {
                //foreach (Item i in items)
                //{
                //    if (i.Id == item.Id)
                //    {
                //        i.Name = item.Name;
                //        i.Price = item.Price;
                //    }
                //}
                //JsonFileService.SaveJsonObjects(items);
                DbService.UpdateObjectAsync(item);
            }
        }

        //public IEnumerable<Item> NameSearch(string str)
        //{
        //    List<Item> nameSearch = new List<Item>();
        //    foreach (Item item in items)
        //    {
        //        if (item.Name.ToLower().Contains(str.ToLower()))
        //        {
        //            nameSearch.Add(item);
        //        }
        //    }

        //    return nameSearch;
        //}


        //public IEnumerable<Item> NameSearch(string str)
        //{
        //    if (string.IsNullOrEmpty(str)) return items;
        //    return items.FindAll(item => item.Name.ToLower().Contains(str.ToLower()));
        //}


        public IEnumerable<Item> NameSearch(string str)
        {
            if (string.IsNullOrEmpty(str)) return items;
            return from item in items where item.Name.ToLower().Contains(str.ToLower()) select item;
        }




        //public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        //{
        //    List<Item> filterList = new List<Item>();
        //    foreach (Item item in items)
        //    {
        //        if ((minPrice==0 && item.Price<=maxPrice) || (maxPrice==0 && item.Price>=minPrice)||(item.Price>=minPrice && item.Price<=maxPrice))
        //        {
        //            filterList.Add(item);
        //        }
        //    }

        //    return filterList;
        //}


        //public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        //{

        //    return items.FindAll(item =>
        //        (minPrice == 0 && item.Price <= maxPrice) || 
        //        (maxPrice == 0 && item.Price >= minPrice) ||
        //        (item.Price >= minPrice && item.Price <= maxPrice));
        //}

        public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        {
            return  from item in items
                    where (minPrice == 0 && item.Price <= maxPrice) ||
                      (maxPrice == 0 && item.Price >= minPrice) ||
                      (item.Price >= minPrice && item.Price <= maxPrice)
                    select item;
        }


        //public IEnumerable<Item> SortById()
        //{
        //    items.Sort();
        //    return items;
        //}

        public IEnumerable<Item> SortById()
        {
            return  from item in items
                    orderby item.ItemId
                    select item;
        }

        public IEnumerable<Item> SortByIdDescending()
        {
            return from item in items
                orderby item.ItemId descending 
                select item;
        }



        //public IEnumerable<Item> SortByName()
        //{
        //    items.Sort(new NameComperator());
        //    return items;
        //}

        public IEnumerable<Item> SortByName()
        {
            return  from item in items
                    orderby item.Name
                    select item;
        }

        public IEnumerable<Item> SortByNameDescending()
        {
            return from item in items
                orderby item.Name descending 
                select item;
        }


        //public IEnumerable<Item> SortByPrice()
        //{
        //    items.Sort(new PriceComperator());
        //    return items;
        //}

        public IEnumerable<Item> SortByPrice()
        {
            return  from item in items
                    orderby item.Price 
                    select item ;
        }

        public IEnumerable<Item> SortByPriceDescending()
        {
            return from item in items
                orderby item.Price descending
                select item;
        }

        //Page Pagination
        public async Task<List<Item>> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = items;
            return data.OrderBy(d => d.ItemId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            return items.Count;
        }


    }
}
