using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Models;

namespace ItemRazor.Services
{
    public class OrderService
    {
        public List<Order> OrderList { get; set; }

       public DbGenericService<Order> DbService { get; set; }

       public OrderService(DbGenericService<Order> dbService)
       {
          DbService = dbService;
          OrderList = DbService.GetObjectsAsync().Result.ToList();
       }

       public async void AddOrder(Order order)
       {
          OrderList.Add(order);
          await DbService.AddObjectAsync(order);
       }
    }
}
