using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    [Serializable]
    public class Librarian : User
    {
        public Librarian(string username, string password) : base(username, password, "Librarian")
        {
        }

        public void CheckOutBook(string bookName)
        {
            // Add user-specific functionality here
        }

        public void CheckInBook(string bookName)
        {
            // Add user-specific functionality here
        }
    }
}
