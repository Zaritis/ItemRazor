using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItemRazor.DAO
{
    public class OrderDAO
    {
        public int OrderId { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Count { get; set; }

        //public OrderDAO()
        //{
            
        //}

        //public OrderDAO(int orderId, int itemId, string itemName, decimal price, int userId, string userName, int count, DateTime date)
        //{
        //    OrderId = orderId;
        //    Date = date;
        //    ItemId = itemId;
        //    ItemName = itemName;
        //    Price = price;
        //    UserId = userId;
        //    UserName = userName;
        //    Count = count;
        //}
    }
}
