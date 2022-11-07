using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.DAO;
using ItemRazor.MockData;
using ItemRazor.Models;

namespace ItemRazor.Services
{
    public class UserService
    {

       public List<User> Users { get; set; }
       
        private JsonFileService<User> JsonFileService { get; set; }
        //public DbGenericService<User> DbService { get; set; }
        public UserDbService DbService { get; set; }

        public UserService(JsonFileService<User> jsonFileService, UserDbService dbService)
        {
            //JsonFileService = jsonFileService;
            DbService = dbService;
            //Users = MockUsers.GetMockUsers();
            //Users = JsonFileService.GetJsonObjects().ToList();
            Users = DbService.GetObjectsAsync().Result.ToList();
            //JsonFileService.SaveJsonObjects(Users);
            //foreach (User user in Users)
            //{
            //    DbService.AddObjectAsync(user);
            //}
            
        }
        
        public void AddUser(User user)
        {
            Users.Add(user);
            //JsonFileService.SaveJsonObjects(Users);
            DbService.AddObjectAsync(user);
        }

        //public IEnumerable<OrderDAO> GetUserOrders(User user)
        //{
        //   return DbService.GetOrdersByUserIdAsync(user.UserId).Result;
        //}

        public User GetOrdersByUser(User user)
        {
            return DbService.GetOrdersByUserIdAsync(user.UserId).Result;
        }

        public User GetUserByUserName(string username)
        {
            //return DbService.GetObjectByIdAsync(username).Result;
            return Users.Find(user=>user.UserName == username);
        }

    }
}
