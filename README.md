using LoginSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
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
        public void AddBook(string title, string author, int noOfCopies)
        {
            // Check if the book already exists
            Book existingBook = Array.Find(Program.books, book => book != null && book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (existingBook != null)
            {
                Console.WriteLine("Book with the same title already exists. Please update the existing book or choose a different title.");
            }
            else
            {
                // Find the first available slot to add the book
                int emptyIndex = Array.IndexOf(Program.books, null);

                if (emptyIndex != -1)
                {
                    Program.books[emptyIndex] = new Book(title, author, noOfCopies);
                    Console.WriteLine("Book added successfully!");
                }
                else
                {
                    Console.WriteLine("No available space to add the book. Please remove some books before adding a new one.");
                }
            }
        }

        public void DeleteBook(string title)
        {
            // Find the book by title
            Book bookToDelete = Array.Find(Program.books, book => book != null && book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (bookToDelete != null)
            {
                // Remove the book from the array
                Array.Clear(Program.books, Array.IndexOf(Program.books, bookToDelete), 1);
                Console.WriteLine("Book deleted successfully!");
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }
    }

    public class Student : User
    {
        private double fineAmount;

        public Student(string username, string password) : base(username, password, "Student")
        {
        }

        public void ViewGrades()
        {
            // Add user-specific functionality here
        }
        public void SearchBook(string bookTitle)
        {
            // Search for a book by title
            Book foundBook = Array.Find(Program.books, book => book != null && book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

            if (foundBook != null)
            {
                Console.WriteLine("Book found!");
                Console.WriteLine("Title: " + foundBook.Title);
                Console.WriteLine("Author: " + foundBook.Author);
                Console.WriteLine("Availability: " + (foundBook.NoOfAvailableCopies > 0 ? "Available" : "Not available"));
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }
        public void IssueBook(string bookTitle)
        {
            // Search for a book by title
            Book foundBook = Array.Find(Program.books, book => book != null && book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

            if (foundBook != null)
            {
                if (foundBook.NoOfAvailableCopies > 0)
                {
                    foundBook.NoOfAvailableCopies--;
                    Console.WriteLine("Book issued successfully!");
                }
                else
                {
                    Console.WriteLine("Sorry, the book is currently not available for issue.");
                }
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }
        public void BorrowBook(string bookTitle, int numberOfDays)
        {
            // Search for a book by title
            Book foundBook = Array.Find(Program.books, book => book != null && book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

            if (foundBook != null)
            {
                if (foundBook.NoOfAvailableCopies > 0)
                {
                    foundBook.NoOfAvailableCopies--;
                    DateTime dueDate = DateTime.Today.AddDays(numberOfDays);
                    Console.WriteLine("Book borrowed successfully!");
                    Console.WriteLine("Due Date: " + dueDate.ToShortDateString());
                }
                else
                {
                    Console.WriteLine("Sorry, the book is currently not available for borrowing.");
                }
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }

        public void ReturnBook(string bookTitle)
        {
            // Search for a book by title
            Book foundBook = Array.Find(Program.books, book => book != null && book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

            if (foundBook != null)
            {
                foundBook.NoOfAvailableCopies++;
                Console.WriteLine("Book returned successfully!");
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }
        public void PayFine(double amount)
        {
 
        }
    }

    class Program
    {
        static User[] users = new User[10]; // Create an array of User objects with initial capacity of 10
        static int userCount = 0; // Keep track of number of registered users
        internal static Book[] books;

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

            User newUser = new Student(username, password);



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
            FileStream stream = new FileStream("users.dat", FileMode.OpenOrCreate);
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
class Book
{
    private int noOfCopies;

    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int NoOfAvailableCopies { get; set; }
    public bool IsActive { get; set; }
    public Book(int bookId, string tittle, string author, int noOfAvailableCopies, bool isActive)
    {
        BookId = bookId;
        Title = tittle;
        Author = author;
        NoOfAvailableCopies = noOfAvailableCopies;
        IsActive = isActive;

    }

    public Book(string title, string author, int noOfCopies)
    {
        Title = title;
        Author = author;
        this.noOfCopies = noOfCopies;
    }
}
internal class BookIssue
{
    public int IssueId { get; set; }
    public DateTime DateOfIsuue { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool IsReturned { get; set; }
    public User UserId { get; set; }
    public Book BookId { get; set; }
    public BookIssue(DateTime dateOfIssue, DateTime returnDate, bool isReturned, User userId, Book bookId)
    {

        DateOfIsuue = dateOfIssue;
        ReturnDate = returnDate;
        IsReturned = isReturned;
        UserId = userId;
        BookId = bookId;

    }
}
interface IBook
{
    void IssuedBooks(string str);
    void ReturnBookHistory(string str);
}
public class BookIssueViewModel
{

    static List<BookIssue> bookIssues = new List<BookIssue>();

    public void Add(DateTime dateOfIsuue, DateTime returnDate, bool isReturned, object obj1, object obj2)
    {

        User user = (User)obj1;


        Book book = (Book)obj2;


        BookIssue bookIssue = new BookIssue(dateOfIsuue, returnDate, isReturned, user, book);
        bookIssue.IssueId = bookIssues.Count() + 1;
        bookIssues.Add(bookIssue);
        Console.WriteLine("Issued successfully");





    }
    public bool IsInIndex(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {

            if (bookIssues[i].IssueId == index)
            {
                temp = true;
                break;
            }

        }
        return temp;
    }
    public void Returned(int index1, int index2)
    {
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].BookId.BookId == index1 && bookIssues[i].UserId.UserId == index2)
            {
                bookIssues[i].IsReturned = true;
                DateTime cdate = DateTime.Now;

                ReturnDate(cdate, bookIssues[i].ReturnDate);

            }

        }
    }
    public void ReturnDate(DateTime currentDate, DateTime returnDate)
    {
        if (currentDate > returnDate)
        {
            Console.WriteLine("you have to pay 10 rupees for late return");

        }
        else
        {
            Console.WriteLine("you returned book on time");
        }
    }
    public string IssuedOnHandBooks(int res)
    {
        string str = "";
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].UserId.UserId == res && !bookIssues[i].IsReturned)
            {
                str = str + $"User : {bookIssues[i].UserId.FullName} has book called: {bookIssues[i].BookId.Title} by {bookIssues[i].BookId.Author}  \n";
            }
        }
        return str;
    }
    public string IssuedBook(int index)
    {
        string str = "";
        foreach (BookIssue bookIssue in bookIssues)
        {
            if (bookIssue.UserId.UserId == index)
            {
                str = str + $"Issued id :{bookIssue.IssueId} User : {bookIssue.UserId.FullName} Book Name : {bookIssue.BookId.Title} by {bookIssue.BookId.Author} IsReturn:{bookIssue.IsReturned} \n";
            }

        }
        return str;
    }
    public void View()
    {
        foreach (BookIssue bookIssue in bookIssues)
        {
            Console.Write($"user name : {bookIssue.UserId.FullName} book name : {bookIssue.BookId.Title} ");
        }
    }
    public void BookIssueView()
    {
        string str = "";
        foreach (BookIssue bookIssue in bookIssues)
        {
            str = str + $"issued id: {bookIssue.IssueId} user name : {bookIssue.UserId.FullName} book title : {bookIssue.BookId.Title} by {bookIssue.BookId.Author} \n";
        }

    }
    public string CurrentIssuedBook(int res)
    {
        string str = "";
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].BookId.BookId == res && !bookIssues[i].IsReturned)
            {
                str = str + $"Book Name : {bookIssues[i].BookId.Title} Author:{bookIssues[i].BookId.Author} is Issued to: {bookIssues[i].UserId.FullName}   \n";
            }
        }
        return str;
    }
    public string ReturnedBookHistory(int index)
    {
        string str = "";

        foreach (BookIssue bookIssue in bookIssues)
        {
            if (bookIssue.IsReturned && bookIssue.BookId.BookId == index)
            {
                str = str + $"Book Name :{bookIssue.BookId.Title} Author:{bookIssue.BookId.Author} returned by {bookIssue.UserId.FullName}   \n";
            }
        }
        return str;
    }
    public void IssuedList()
    {
        string str = "";
        foreach (BookIssue bookIssue in bookIssues)
        {

            str = str + $"issued book id: {bookIssue.IssueId} name of user : {bookIssue.UserId.FullName} book name : {bookIssue.BookId.Title} written by {bookIssue.BookId.Author} IsReturned:{bookIssue.IsReturned}\n";

        }
        Console.WriteLine(str);
    }

    public void IssuedBookList()
    {
        Console.WriteLine($"Issued book id: {bookIssues[bookIssues.Count() - 1].IssueId} User Name : {bookIssues[bookIssues.Count() - 1].UserId.FullName} Book Name : {bookIssues[bookIssues.Count() - 1].BookId.Title} Written by {bookIssues[bookIssues.Count() - 1].BookId.Author} is issued");
    }
    public bool IsIssuedBookNull()
    {
        bool temp = true;
        if (bookIssues.Count != 0)
        {
            temp = false;
        }
        return temp;

    }
    public bool IsUserId(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].UserId.UserId == index)
            {
                temp = true;
                break;
            }

        }
        return temp;
    }
    public bool IsBookId(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].BookId.BookId == index)
            {
                temp = true;
                break;
            }

        }
        return temp;
    }
    public bool IsReturned(int index1, int index2)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].UserId.UserId == index2 && bookIssues[i].BookId.BookId == index1 && bookIssues[i].IsReturned == true)
            {

                temp = true;
                break;

            }

        }
        return temp;
    }
    public bool isDeletableBook(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].BookId.BookId == index)
            {
                if (!bookIssues[i].IsReturned)
                {
                    temp = false;
                    break;
                }
                else
                {
                    temp = true;
                }

            }

        }
        return temp;
    }

    public bool BookinBookIssue(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].BookId.BookId == index)
            {
                temp = true;
                break;
            }
        }
        return temp;
    }

    public bool isDeletableUser(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].UserId.UserId == index)
            {
                if (!bookIssues[i].IsReturned)
                {
                    temp = false;
                    break;
                }
                else
                {
                    temp = true;
                }

            }
        }
        return temp;
    }

    public bool UserinBookIssue(int index)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].UserId.UserId == index)
            {
                temp = true;
                break;
            }
        }
        return temp;
    }
    public bool UserBookCheck(int index1, int index2)
    {
        bool temp = false;
        for (int i = 0; i < bookIssues.Count(); i++)
        {
            if (bookIssues[i].UserId.UserId == index1 && bookIssues[i].BookId.BookId == index2)
            {
                temp = true;
                break;
            }
        }
        return temp;
    }
}
interface IUser
{
    void OnHandBooks(string str);
    void IssuedBooksHistory(string str);

}
public abstract class AbstractMethod
{

    public abstract void Add(object obj);

    public abstract void Edit(int index, object obj);

    public abstract void Delete(int index);

    public abstract bool ReturnToMainMenu();

    public abstract void Search(string search);
}
internal class UserViewModel : AbstractMethod, IUser
{
    public static List<User> users = new List<User>
        {
            new User(1,"john", "carter", true),
            new User(2,"james", "johnson", true)

    };


    public override void Add(object obj)
    {
        User user = (User)obj;
        new User(user.UserId, user.FirstName, user.LastName, user.IsActive);

        users.Add(user);
        view();


    }

    public override void Edit(int index, object obj)
    {
        User user = (User)obj;
        for (int i = 0; i < users.Count(); i++)
        {
            if (users[i].UserId == index)
            {
                users[i].FirstName = user.FirstName;
                users[i].LastName = user.LastName;
                users[i].IsActive = user.IsActive;
            }
        }


        Console.WriteLine("edited successfully");

    }
    public bool IsInIndex(int index)
    {
        bool temp = false;
        for (int i = 0; i < users.Count(); i++)
        {
            if (users[i].UserId == index)
            {
                temp = true;
                break;
            }

        }
        return temp;
    }
    public override void Delete(int index)
    {
        for (int i = 0; i < users.Count(); i++)
        {
            if (users[i].UserId == index)
            {
                users.Remove(users[i]);
            }
        }

        Console.WriteLine("deleted successfully");
    }


    public void OnHandBooks(string str)
    {
        Console.WriteLine(str);
    }

    public void IssuedBooksHistory(string str)
    {
        Console.WriteLine(str);
    }
    public override void Search(string search)
    {
        string ans = "";
        for (int i = 0; i < users.Count(); i++)
        {
            if (users[i].FirstName.Contains(search) || users[i].LastName.Contains(search) || (users[i].FirstName + " " + users[i].LastName).Contains(search))
            {
                ans = ans + "userID: " + users[i].UserId + " " + "FirstName:" + users[i].FirstName + " " + "LastName: " + users[i].LastName + " " + "ISActive: " + users[i].IsActive + "\n";
            }

        }
        Console.WriteLine(ans);
    }
    public override bool ReturnToMainMenu()
    {
        return false;
    }
    public User GetUserID(int Id)
    {
        User user = null;
        for (int i = 0; i < users.Count(); i++)
        {
            if (users[i].UserId == Id)
            {
                user = users[i];
                break;
            }
        }
        return user;
    }
    public void view()
    {
        Console.WriteLine($"User Id :{users[users.Count() - 1].UserId} User name : {users[users.Count() - 1].FullName} is added");
    }
    public bool NotNullList()
    {
        bool temp = true;
        if (users.Count() == 0)
        {
            temp = false;
        }
        return temp;
    }
    public void ListOfUser()
    {
        foreach (User user in users)
        {
            Console.WriteLine($"User Id :{user.UserId} User name : {user.FullName} \n");
        }
    }
    public void FirstUsers()
    {



    }
}
internal class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }
    public bool IsActive { get; set; }
    public User(int userId, string firstName, string lastName, bool isActive)
    {

        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
    }
}
internal class OtherMethods
{
    public void MainMenuInterface()
    {
        Console.WriteLine("*********************************");
        Console.WriteLine("*       LIBRARY MANAGER         *");
        Console.WriteLine("*********************************");
        Console.WriteLine();
        Console.WriteLine("========================");
        Console.WriteLine("       MAIN MENU");
        Console.WriteLine("========================");
        Console.WriteLine("1. Users Management");
        Console.WriteLine("2. Books Management");
        Console.WriteLine("3. Search Users");
        Console.WriteLine("4. Search Books");
        Console.WriteLine("5. Issue / Return Books");
        Console.WriteLine("6. Exit");
    }
    public void UserInterface()
    {
        Console.WriteLine("========================");
        Console.WriteLine("       USER MENU");
        Console.WriteLine("========================");
        Console.WriteLine("1. Add User");
        Console.WriteLine("2. Edit User");
        Console.WriteLine("3. Delete User");
        Console.WriteLine("4. OnHand Books");
        Console.WriteLine("5. Issued Books History");
        Console.WriteLine("6. Return to Main Menu");

    }
    public void Bookinterface()
    {

        Console.WriteLine("========================");
        Console.WriteLine("       Book MENU");
        Console.WriteLine("========================");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Edit Book");
        Console.WriteLine("3. Delete Book");
        Console.WriteLine("4. Issued Books");
        Console.WriteLine("5. Returned Book History");
        Console.WriteLine("6. Return to Main Menu");
    }
    public int Input(string input)
    {

        int res = 0;
        bool inputLoop = true;
        while (inputLoop)
        {
            Console.Write(input);
            try
            {
                res = Convert.ToInt32(Console.ReadLine());

                inputLoop = false;
            }
            catch (FormatException)
            {
                Console.WriteLine("please enter in valid format");
            }

        }

        return res;

    }
    public string StringInput(string input)
    {
        string res = "";
        bool inputLoop = true;
        while (inputLoop)
        {
            Console.Write(input);
            res = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(res))
            {
                inputLoop = false;
            }
            else
            {
                Console.WriteLine("input cannot be empty");
            }

        }
        return res;
    }

    public bool LeaveOrNot()
    {
        bool temp = false;
        string res = StringInput("Do you want to add another user y to add and any other key to leave : ");
        if (res == "y")
        {
            temp = true;
        }
        return temp;
    }
    public bool ActiveEdit(string input)
    {
        bool temp = true;
        string res = StringInput(input);
        if (res == "y")
        {
            temp = true;
        }
        else if (res == "n")
        {
            temp = false;
        }
        else
        {
            Console.WriteLine("responce is not valid it will be active by defult");
        }
        return temp;
    }
    public int NoNegativeInput(string input)
    {
        int res = 0;
        bool inputLoop = true;
        while (inputLoop)
        {
            Console.Write(input);
            try
            {
                res = Convert.ToInt32(Console.ReadLine());
                if (res > 0)
                {
                    inputLoop = false;
                }
                else
                {
                    Console.WriteLine("you cannot use negative values");
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("please enter in valid format");
            }

        }

        return res;
    }

}
internal class BookViewModel : AbstractMethod, IBook
{

    public static List<Book> books = new List<Book>
        {
            new Book(1,"the silent patient","alex michaelides", 4, true),
            new Book(2,"gone girl", "gillian flynn", 4, true),
            new Book(3,"sherlock holmes", "sir arthur conan doyle", 4, true)

        };


    public override void Add(object obj)
    {

        Book book = (Book)obj;
        new Book(book.BookId, book.Title, book.Author, book.NoOfAvailableCopies, book.IsActive);
        books.Add(book);
        Console.WriteLine("added successfully");


    }
    public bool IsInIndex(int index)
    {
        bool temp = false;
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == index)
            {
                temp = true;
                break;
            }

        }
        return temp;
    }
    public override void Edit(int index, object obj)
    {
        Book book = (Book)obj;
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == index)
            {
                books[i].Title = book.Title;
                books[i].Author = book.Author;
                books[i].IsActive = book.IsActive;

            }

        }
        Console.WriteLine("edited successfully");

    }
    public override void Delete(int index)
    {
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == index)
            {
                books.Remove(books[i]);
            }
        }
        Console.WriteLine("deleted successfully");
    }

    public void IssuedBooks(string str)
    {

        Console.WriteLine(str);


    }

    public void ReturnBookHistory(string str)
    {
        Console.WriteLine(str);
    }


    public override void Search(string search)
    {
        string ans = "";
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].Title.Contains(search) || books[i].Author.Contains(search))
            {
                ans = ans + "BookID: " + books[i].BookId + " " + "Title : " + books[i].Title + " " + "Author: " + books[i].Author + " " + "NoOfCopiesAvailable : " + books[i].NoOfAvailableCopies + " " + "ISActive: " + books[i].IsActive + "\n";
            }

        }

        Console.WriteLine(ans);
    }
    public override bool ReturnToMainMenu()
    {
        return false;
    }
    public Book GetBookID(int Id)
    {
        Book book = null;
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == Id)
            {
                book = books[i];
                break;
            }
        }
        return book;
    }
    public void view()
    {
        foreach (Book book in books)
        {
            Console.WriteLine($"book id: {book.BookId} book name :{book.Title} book author :{book.Author} book available : {book.NoOfAvailableCopies} \n");

        }
    }
    public void BookView()
    {
        Console.WriteLine($"book id: {books[books.Count() - 1].BookId} book name :{books[books.Count() - 1].Title} book author :{books[books.Count() - 1].Author} book available : {books[books.Count() - 1].NoOfAvailableCopies} ");
    }
    public bool CheckNoOfBook(int index)
    {
        bool temp = false;
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == index)
            {
                if (books[i].NoOfAvailableCopies < 1)
                {
                    Console.WriteLine("there is not books left for issue");
                    temp = true;
                    break;
                }
                else
                {
                    temp = false;
                }
            }
        }


        return temp;
    }
    public void ChangeInNoOfBook(int index)
    {
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == index)
            {
                books[i].NoOfAvailableCopies--;
            }
        }

    }
    public void ReturnedChange(int index)
    {
        for (int i = 0; i < books.Count(); i++)
        {
            if (books[i].BookId == index)
            {
                books[i].NoOfAvailableCopies++;
            }
        }

    }
    public bool NotNullList()
    {
        bool temp = true;
        if (books.Count() == 0)
        {
            temp = false;
        }
        return temp;
    }


}
/*static void Main(string[] args)
{
    ViewModel.OtherMethods otherMethods = new ViewModel.OtherMethods();
    ViewModel.UserViewModel userViewModel = new ViewModel.UserViewModel();
    ViewModel.BookViewModel bookViewModel = new ViewModel.BookViewModel();
    ViewModel.BookIssueViewModel bookIssueViewModel = new ViewModel.BookIssueViewModel();
    int userIndex = 3;
    int bookIndex = 4;
    bool LibIsOn = true;
    while (LibIsOn)
    {
        otherMethods.MainMenuInterface();
        int option = otherMethods.Input("please enter number from given list: ");
        if (option == 1)
        {

            bool userLoop = true;
            while (userLoop)
            {
                otherMethods.UserInterface();
                int userOp = otherMethods.Input("enter any number from given number : ");
                if (userOp == 1)
                {
                    bool addLoop = true;
                    while (addLoop)
                    {
                        string fname = otherMethods.StringInput("enter first name of user : ");
                        string lname = otherMethods.StringInput("enter last name of user : ");
                        User user = new User(userIndex, fname.ToLower(), lname.ToLower(), true);

                        userViewModel.Add(user);
                        userIndex++;
                        addLoop = otherMethods.LeaveOrNot();
                    }


                }
                else if (userOp == 2)
                {
                    bool userLoopTwo = true;
                    userViewModel.ListOfUser();
                    while (userLoopTwo)
                    {

                        if (userViewModel.NotNullList())
                        {

                            int index = otherMethods.Input("enter userId that you want to edit: ");
                            bool isInrange = userViewModel.IsInIndex(index);
                            if (isInrange)
                            {
                                string fname = otherMethods.StringInput("edit first name of user : ");
                                string lname = otherMethods.StringInput("edit last name of user : ");
                                bool isActive = otherMethods.ActiveEdit("is user active y/n: ");
                                User user = new User(0, fname.ToLower(), lname.ToLower(), isActive);
                                userViewModel.Edit(index, user);
                                userLoopTwo = false;
                            }
                            else
                            {
                                Console.WriteLine("this id does not exist");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no user to edit");
                            userLoopTwo = false;
                        }

                    }


                }
                else if (userOp == 3)
                {
                    userViewModel.ListOfUser();
                    bool userLoopThree = true;
                    while (userLoopThree)
                    {
                        if (userViewModel.NotNullList())
                        {

                            int index = otherMethods.Input("enter userId that you want to delete: ");
                            bool isInrange = userViewModel.IsInIndex(index);

                            if (!bookIssueViewModel.UserinBookIssue(index))
                            {
                                if (isInrange)
                                {
                                    string ans = otherMethods.StringInput("Do you want to delete this User ? y to yes or any other key to leave :");
                                    if (ans == "y")
                                    {
                                        userViewModel.Delete(index);
                                        userLoopThree = false;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine(" please enter valid input");
                                }
                            }
                            else
                            {
                                if (bookIssueViewModel.isDeletableUser(index))
                                {
                                    if (isInrange)
                                    {
                                        string ans = otherMethods.StringInput("Do you want to delete this user ? y to yes or any other key to leave :");
                                        if (ans == "y")
                                        {
                                            userViewModel.Delete(index);
                                            userLoopThree = false;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine(" please enter valid input");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("we cannot delete this User because user have not returned book");
                                    userLoopThree = false;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no user to detete");
                            userLoopThree = false;
                        }

                    }


                }
                else if (userOp == 4)
                {
                    int res = otherMethods.Input("enter user id of the user :");
                    if (userViewModel.IsInIndex(res))
                    {
                        userViewModel.OnHandBooks(bookIssueViewModel.IssuedOnHandBooks(res));
                    }
                    else
                    {
                        Console.WriteLine("this user id does not exist");
                    }

                }
                else if (userOp == 5)
                {
                    int res = otherMethods.Input("enter user id of the user :");
                    if (userViewModel.IsInIndex(res))
                    {
                        userViewModel.IssuedBooksHistory(bookIssueViewModel.IssuedBook(res));
                    }
                    else
                    {
                        Console.WriteLine("this user id does not exist");
                    }

                }
                else if (userOp == 6)
                {
                    userLoop = userViewModel.ReturnToMainMenu();
                }
                else
                {
                    Console.WriteLine("please enter number from given list");
                }
            }
        }

        else if (option == 2)
        {
            bool bookLoop = true;
            while (bookLoop)
            {
                otherMethods.Bookinterface();
                int bookOp = otherMethods.Input("please enter number from given list: ");
                if (bookOp == 1)
                {
                    bool addLoop = true;
                    while (addLoop)
                    {
                        string title = otherMethods.StringInput("enter title of book : ");
                        string author = otherMethods.StringInput("enter author of the book : ");
                        int noOfCopy = otherMethods.NoNegativeInput("enter no. of copy : ");
                        Book book = new Book(bookIndex, title.ToLower(), author.ToLower(), noOfCopy, true);

                        bookViewModel.Add(book);
                        bookIndex++;
                        bookViewModel.BookView();
                        addLoop = otherMethods.LeaveOrNot();
                    }
                }
                else if (bookOp == 2)
                {
                    bookViewModel.view();
                    bool bookLoopTwo = true;
                    while (bookLoopTwo)
                    {
                        if (bookViewModel.NotNullList())
                        {

                            int index = otherMethods.Input("enter BookId that you want to edit: ");
                            bool isInrange = bookViewModel.IsInIndex(index);
                            if (isInrange)
                            {
                                string title = otherMethods.StringInput("edit name of book : ");
                                string author = otherMethods.StringInput("edit author name : ");
                                bool isActive = otherMethods.ActiveEdit("is user active y/n: ");
                                Book book = new Book(0, title.ToLower(), author.ToLower(), 0, isActive);

                                bookViewModel.Edit(index, book);
                                bookLoopTwo = false;
                            }
                            else
                            {
                                Console.WriteLine("this id does not exist");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there are no book to edit ");
                            bookLoopTwo = false;

                        }

                    }


                }
                else if (bookOp == 3)
                {
                    bookViewModel.view();
                    bool bookLoopThree = true;
                    while (bookLoopThree)
                    {
                        if (bookViewModel.NotNullList())
                        {

                            int index = otherMethods.Input("enter bookId that you want to delete: ");
                            bool isInrange = bookViewModel.IsInIndex(index);
                            if (!bookIssueViewModel.BookinBookIssue(index))
                            {
                                if (isInrange)
                                {
                                    string ans = otherMethods.StringInput("Do you want to delete this book ? y to yes or any other key to leave :");
                                    if (ans == "y")
                                    {
                                        bookViewModel.Delete(index);
                                        bookLoopThree = false;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine(" please enter valid input");
                                }
                            }
                            else
                            {
                                if (bookIssueViewModel.isDeletableBook(index))
                                {
                                    if (isInrange)
                                    {
                                        string ans = otherMethods.StringInput("Do you want to delete this book ? y to yes or any other key to leave :");
                                        if (ans == "y")
                                        {
                                            bookViewModel.Delete(index);
                                            bookLoopThree = false;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine(" please enter valid input");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("we cannot delete this book because it is not returned");
                                    bookLoopThree = false;
                                }
                            }


                        }
                        else
                        {
                            Console.WriteLine("there are not books to delete");
                        }

                    }

                }
                else if (bookOp == 4)
                {
                    int res = otherMethods.Input("enter book id of the Book :");
                    if (bookViewModel.IsInIndex(res))
                    {
                        bookViewModel.IssuedBooks(bookIssueViewModel.CurrentIssuedBook(res));
                    }
                    else
                    {
                        Console.WriteLine("this user id does not exist");
                    }
                }
                else if (bookOp == 5)
                {
                    int res = otherMethods.Input("enter book id of the Book :");
                    if (bookViewModel.IsInIndex(res))
                    {
                        bookViewModel.ReturnBookHistory(bookIssueViewModel.ReturnedBookHistory(res));
                    }
                    else
                    {
                        Console.WriteLine("this user id does not exist");
                    }

                }
                else if (bookOp == 6)
                {
                    bookLoop = userViewModel.ReturnToMainMenu();
                }
                else
                {
                    Console.WriteLine("please enter number from given list");
                }
            }
        }
        else if (option == 3)
        {
            string search = otherMethods.StringInput("enter name of user :");
            userViewModel.Search(search.ToLower());
        }
        else if (option == 4)
        {
            string search = otherMethods.StringInput("enter name of book or author :");
            bookViewModel.Search(search.ToLower());
        }
        else if (option == 5)
        {
            string response = otherMethods.StringInput("do you want to issue or return book i/r:");
            if (response == "i")
            {
                Console.WriteLine("here are list of users");
                userViewModel.ListOfUser();
                Console.WriteLine("here are list of books");
                bookViewModel.view();
                bool issueLoop = true;
                while (issueLoop)
                {
                    User user = null;
                    Book book = null;


                    int ID = otherMethods.Input("please enter ID of user:");
                    bool inRange = userViewModel.IsInIndex(ID);
                    if (inRange)
                    {
                        user = userViewModel.GetUserID(ID);
                        int bookID = otherMethods.Input("please enter ID of book:");
                        bool inRangeBook = bookViewModel.IsInIndex(bookID);
                        if (inRangeBook)
                        {
                            book = bookViewModel.GetBookID(bookID);
                            DateTime now = DateTime.Now;
                            DateTime returnDate = now.AddDays(10);
                            if (!bookViewModel.CheckNoOfBook(bookID))
                            {
                                if (!bookIssueViewModel.UserBookCheck(ID, bookID))
                                {
                                    bookIssueViewModel.Add(now, returnDate, false, user, book);
                                    bookViewModel.ChangeInNoOfBook(bookID);
                                    bookViewModel.view();
                                    bookIssueViewModel.IssuedBookList();
                                }
                                else
                                {
                                    Console.WriteLine("User has this book already!!!");

                                }

                                issueLoop = false;
                            }
                            else
                            {
                                Console.WriteLine("there are not book left to Issue");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID is invalid");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID is invalid");
                    }
                    //int ID = otherMethods.Input("please enter ID of user:");

                    //bool inRange = userViewModel.IsInIndex(ID);
                    //if (inRange)
                    //{
                    //    user = userViewModel.GetUserID(ID);

                    //}
                    //else
                    //{
                    //    Console.WriteLine("ID is invalid");
                    //}
                    //int bookID = otherMethods.Input("please enter ID of book:");
                    //bool inRangeBook = bookViewModel.IsInIndex(bookID);
                    //if (inRangeBook)
                    //{
                    //    book = bookViewModel.GetBookID(bookID);
                    //    DateTime now = DateTime.Now;
                    //    DateTime returnDate = now.AddDays(10);
                    //    if (!bookViewModel.CheckNoOfBook(bookID))
                    //    {
                    //        bookIssueViewModel.Add(now, returnDate, false, user, book);
                    //        bookViewModel.ChangeInNoOfBook(bookID);
                    //        bookViewModel.view();
                    //        bookIssueViewModel.IssuedBookList();
                    //        issueLoop = false;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("there are not book left to Issue");
                    //    }


                    //}
                    //else
                    //{
                    //    Console.WriteLine("ID is invalid");
                    //}

                }


            }
            else if (response == "r")
            {
                bookIssueViewModel.IssuedList();
                if (!bookIssueViewModel.IsIssuedBookNull())
                {
                    bool returnLoop = true;
                    while (returnLoop)
                    {
                        int responceBook = otherMethods.Input("enter book id :");
                        if (bookIssueViewModel.IsBookId(responceBook))
                        {
                            int responceUser = otherMethods.Input("enter User id :");

                            if (bookIssueViewModel.IsUserId(responceUser))
                            {
                                if (!bookIssueViewModel.IsReturned(responceBook, responceUser))
                                {
                                    bookIssueViewModel.Returned(responceBook, responceUser);
                                    Console.WriteLine("book is returned");
                                }
                                else
                                {
                                    Console.WriteLine("it is already returned");
                                }
                                returnLoop = false;
                            }
                            else
                            {
                                Console.WriteLine("user id does not  exist in book issues");
                            }
                        }
                        else
                        {
                            Console.WriteLine("book id does not exist in book issues");
                        }
                    }

                    //int responce = otherMethods.Input("enter issued id :");
                    //bookIssueViewModel.BookIssueView();
                    //if (bookIssueViewModel.IsInIndex(responce))
                    //{
                    //
                    //}
                    //else
                    //{
                    //    Console.WriteLine("id does not exist");
                    //}

                }
                else
                {
                    Console.WriteLine("there is not issued book to return");
                }

            }
            else
            {
                Console.WriteLine("please enetr valid responce");
            }
        }
        else if (option == 6)
        {
            string ans = otherMethods.StringInput("Do you want to exit the app? enter y to leave enter any other key  :");
            if (ans == "y")
            {
                LibIsOn = false;
            }

        }
        else
        {
            Console.WriteLine("Please enter number from given list");
        }

    }
    Console.WriteLine("Please enter any key to exit program ");
    Console.ReadKey();
}
}*/





