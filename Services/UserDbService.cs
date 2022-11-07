﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ItemRazor.DAO;
using ItemRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemRazor.Services
{
    public class UserDbService : DbGenericService<User>
    {
        public async Task<User> GetOrdersByUserIdAsync(int id)
        {
            User user;

            using (var context = new ItemDbContext())
            {
                user = context.Users
                    .Include(u => u.Orders)
                    .ThenInclude(i => i.Item)
                    .AsNoTracking()
                    .FirstOrDefault(u => u.UserId == id);
            }

            return user;
        }


        //public async Task<List<OrderDAO>> GetOrdersByUserIdAsync(int id)
        //{
        //    List<OrderDAO> orderList = new List<OrderDAO>();

        //    using (var context = new ItemDbContext())
        //    {
               
        //      var orders = from order in context.Orders
        //           join item in context.Items on order.ItemId equals item.ItemId
        //           join user in context.Users on order.UserId equals user.UserId
        //           where user.UserId == id
        //           select new OrderDAO()
        //           {
        //               OrderId = order.OrderId,
        //               Date = order.Date,
        //               UserId = user.UserId,
        //               UserName = user.UserName,
        //               ItemId = item.ItemId,
        //               ItemName = item.Name,
        //               Price = item.Price,
        //               Count = order.Count
        //           };

        //      foreach (var order in orders)
        //      {
        //          orderList.Add(order);
        //      }
        //    }

        //    return orderList;
        //}

    }
}
