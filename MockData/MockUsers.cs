using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazor.Models;
using Microsoft.AspNetCore.Identity;

namespace ItemRazor.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>() {
            new User("admin", passwordHasher.HashPassword(null, "secret")), 
            new User("heho", passwordHasher.HashPassword(null, "123")),
            new User("jaef", passwordHasher.HashPassword(null, "123"))
        };


        public static List<User> GetMockUsers()
        {
            return users;
        }
    }
}
