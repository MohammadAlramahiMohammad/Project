using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    [Serializable]
    public class Student : User
    {
        public Student(string username, string password) : base(username, password, "Student")
        {
        }

        public void ViewGrades()
        {
            // Add user-specific functionality here
        }
    }
}
