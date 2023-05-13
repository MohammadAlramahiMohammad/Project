using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    [Serializable]
    public class Admin : User
    {
        public Admin(string username, string password) : base(username, password, "Admin")
        {
        }

        public void AddUser(User user)
        {
            // Add user-specific functionality here
        }

        public void RemoveUser(User user)
        {
            // Add user-specific functionality here
        }
    }
}
