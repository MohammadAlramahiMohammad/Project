using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int UserId { get; set; }

        public User(string username, string password, string userType = "Student")
        {
            Username = username;
            Password = password;
            UserType = userType;

        }

        public void DisplayInfo()
        {
            Console.WriteLine("Username: " + Username);
            Console.WriteLine("User type: " + UserType);
        }
    }
}
