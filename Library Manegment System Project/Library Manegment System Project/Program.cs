using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LoginSystem
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int UserId { get; set; }

        public User(string username, string password, string userType)
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

    class Program
    {
        static User[] users = new User[10]; // Create an array of User objects with initial capacity of 10
        static int userCount = 0; // Keep track of number of registered users

        static void Main(string[] args)
        {
            LoadData(); // Load user data from file on program start

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Login System!");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Please enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        SaveData(); // Save user data to file before exiting program
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Register()
        {
            Console.Clear();
            Console.WriteLine("Registration Page");
            Console.WriteLine("-----------------");

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Select your user type:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Librarian");
            Console.WriteLine("3. Student");

            int userTypeChoice = int.Parse(Console.ReadLine());

            User newUser = null;

            switch (userTypeChoice)
            {
                case 1:
                    // Create a new Admin object and add it to the array
                    newUser = new Admin(username, password);
                    break;
                case 2:
                    // Create a new Librarian object and add it to the array
                    newUser = new Librarian(username, password);
                    break;
                case 3:
                    // Create a new Student object and add it to the array
                    newUser = new Student(username, password);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey();
                    return;
            }

            // Check if the username already exists in the array
            if (Array.Exists(users, u => u != null && u.Username == username))
            {
                Console.WriteLine("Username already taken. Press any key to try again...");
                Console.ReadKey();
                return;
            }

            // If the array is full, resize it to double its current capacity
            if (userCount == users.Length)
            {
                Array.Resize(ref users, users.Length * 2);
            }

            // Add the new user object to the array and update the user count
            users[userCount] = newUser;
            userCount++;

            Console.WriteLine("Registration successful! Press any key to continue...");
            Console.ReadKey();
        }
        static void Login()
        {
            Console.Clear();
            Console.WriteLine("Login Page");
            Console.WriteLine("----------");

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            // Find the User object in the array that matches the entered username and password
            User user = Array.Find(users, u => u != null && u.Username == username && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Invalid username or password. Press any key to try again...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Login successful! Welcome, " + user.Username + "!");

                // Check the user's type and call appropriate functions
                if (user.UserType == "Admin")
                {
                    Admin admin = user as Admin;
                    Console.WriteLine("You are an Admin user.");
                    // Call functions specific to the Admin user here
                }
                else if (user.UserType == "Librarian")
                {
                    Librarian librarian = user as Librarian;
                    Console.WriteLine("You are a Librarian user.");
                    // Call functions specific to the Librarian user here
                }
                else if (user.UserType == "Student")
                {
                    Student student = user as Student;
                    Console.WriteLine("You are a Student user.");
                    // Call functions specific to the Student user here
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void SaveData()
        {
            // Serialize the array of User objects and save it to a file
            FileStream stream = new FileStream("users.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, users);
            stream.Close();
        }

        static void LoadData()
        {
            // Load the serialized User data from the file, if it exists
            if (File.Exists("users.dat"))
            {
                FileStream stream = new FileStream("users.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                users = (User[])formatter.Deserialize(stream);
                userCount = Array.FindLastIndex(users, u => u != null) + 1; // Update the user count to the last index + 1
                stream.Close();
            }
        }
    }
}

