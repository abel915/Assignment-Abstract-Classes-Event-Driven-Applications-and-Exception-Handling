using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybridApp.Data
{
    public class UserManager
    {

        readonly string USER_TXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\.\\Resources\\Users.txt");

        private static readonly List<User> users = [];

        public UserManager()
        {
            PopulateUsers();
        }

        private void PopulateUsers()
        {
            User user;

            foreach (string line in File.ReadLines(USER_TXT))
            {
                string[] parts = line.Split(",");
                int id = int.Parse(parts[0]);
                string email = parts[1];

                user = new User(id, email);
                users.Add(user);


            }
        }

        public static List<User> GetUsers()
        {
            return users;
        }


    }
}


