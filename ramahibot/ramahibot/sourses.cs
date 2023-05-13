using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ramahibot
{
    /*static void Main(string[] args)
        {
            OtherMethods otherMethods = new OtherMethods();
            UserViewModel userViewModel = new UserViewModel();
            BookViewModel bookViewModel = new BookViewModel();
            BookIssueViewModel bookIssueViewModel = new BookIssueViewModel();
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
        }*/
}
