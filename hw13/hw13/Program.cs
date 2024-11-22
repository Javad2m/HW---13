using hw13.Contract;
using hw13.Enum;
using hw13.SeedUser;
using hw13.Services;

IUserServices userServices = new UserServices();
ILibraryServices libraryServices = new LibraryServices();

MainMenu();

void MainMenu()
{

    Console.Clear();
    Console.WriteLine("Welcome To Library");
    Console.WriteLine("-------     Main Menu     -------");
    Console.Write("Pls Select Your Action (1:Register, 2:Login) : ");
    if (!Int32.TryParse(Console.ReadLine(), out int ActionID))
    {
        Console.WriteLine("Selected Action Is Invalid");
        Console.ReadKey();
        MainMenu();
    }
    switch (ActionID)
    {
        case 1:
            Register();
            break;
        case 2:
            Login();
            break;
        default:
            Console.WriteLine("Selected Action Is Invalid");
            Console.ReadKey();
            MainMenu();
            break;


    }
}
void Register()
{
    Console.Clear();
    Console.WriteLine("Pls Enter The UserName");
    string userName = Console.ReadLine();
    Console.WriteLine("Pls Enter The Password");
    string password = Console.ReadLine();
    Console.WriteLine("Pls Select The Role (1.User 2.Admin)");
    if (!Int32.TryParse(Console.ReadLine(), out int ActionID))
    {
        Console.WriteLine("Selected Role Is Invalid");
        Console.ReadKey();
        Register();
    }
    switch (ActionID)
    {
        case 1:
            try
            {
                userServices.Register(userName, password, RoleUserEnum.User);
                Console.WriteLine("Register Succesful");
                Console.ReadKey();
                MainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
                MainMenu();
            }

            break;
        case 2:
            try
            {
                userServices.Register(userName, password, RoleUserEnum.Admin);
                Console.WriteLine("Register Succesful");
                Console.ReadKey();
                MainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
                MainMenu();
            }
            break;
        default:
            Console.WriteLine("Selected Role Is Invalid");
            MainMenu();
            break;


    }
}

void Login()
{
    Console.Clear();
    Console.WriteLine("Pls Enter The Username");
    string userName = Console.ReadLine();
    Console.WriteLine("Pls Enter The Password");
    string password = Console.ReadLine();
    try
    {
        userServices.Login(userName, password);
        Console.WriteLine("Login Succesfully");
        Console.ReadKey();
        if (InMemoryDB.CurrentUser.Role == RoleUserEnum.User)
        {
            UserMenu();
        }
        else
        {
            AdminMenu();
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        MainMenu();
    }



}

void UserMenu()
{
    try
    {
        if (InMemoryDB.CurrentUser == null)
        {
            Console.WriteLine("Pls Login First");
            Console.ReadKey();
            MainMenu();
        }
        Console.Clear();
        Console.WriteLine($"Remaining time to use the library: {userServices.EndTime(InMemoryDB.CurrentUser.Id)} Days");
        Console.WriteLine("--- --- ---");
        Console.WriteLine("1.Borrowing a book");
        Console.WriteLine("2.Returning a book");
        Console.WriteLine("3.View the list of borrowed books");
        Console.WriteLine("4.View all library books");
        Console.WriteLine("5.Log out of account");

        if (userServices.EndTime(InMemoryDB.CurrentUser.Id) < 0)
        {
            Console.WriteLine("--- --- ---");
            Console.WriteLine("--- --- ---");
            Console.WriteLine("--- --- ---");
            Console.WriteLine("The library usage period has ended for you");
            InMemoryDB.CurrentUser = null;
            Console.ReadKey();
            MainMenu();
        }

        if (!Int32.TryParse(Console.ReadLine(), out int ActionID))
        {
            Console.WriteLine("Selected Action Is Invalid");
            UserMenu();
        }
        switch (ActionID)
        {
            case 1:
                var boks = libraryServices.ShowBooks();
                if (boks == null)
                {
                    Console.WriteLine("There are currently no books available.");
                    Console.ReadKey();
                    UserMenu();
                }
                else
                {
                    foreach (var b in boks)
                    {
                        Console.WriteLine($"ID: {b.Id} - Name: {b.Name} - Author: {b.Author}");

                    }
                }
                Console.WriteLine("Pls Enter The Book ID");
                int idb = Convert.ToInt32(Console.ReadLine());
                bool iiq = boks.Any(b => b.Id == idb);
                if (!iiq)
                {
                    Console.WriteLine("The entered ID is not correct.");
                    Console.ReadKey();
                    UserMenu();

                }
                else
                {
                    libraryServices.Barrow(InMemoryDB.CurrentUser.Id, idb);
                    Console.WriteLine("Done");
                    Console.ReadKey();
                    UserMenu();


                }
                break;
            case 2:
                var bList = libraryServices.BarrowList(InMemoryDB.CurrentUser.Id);
                if (bList.Count == 0)
                {
                    Console.WriteLine("You Dont Have Any Book!");
                    Console.ReadKey();
                    UserMenu();
                }
                else
                {
                    foreach (var b in bList)
                    {
                        Console.WriteLine($"ID: {b.Id} - Name: {b.Name} - Author: {b.Author}");
                    }
                }
                Console.WriteLine("--- --- ---");
                Console.WriteLine("Pls Enter The Book ID");
                int bkId = Convert.ToInt32(Console.ReadLine());
                bool isT = bList.Any(b => b.Id == bkId);
                if (!isT)
                {
                    Console.WriteLine("The entered ID is not correct.");
                    Console.ReadKey();
                    UserMenu();

                }
                else
                {
                    libraryServices.Rbarrow(bkId);
                    Console.WriteLine("Done");
                    Console.ReadKey();
                    UserMenu();
                }
                break;
            case 3:
                var shList = libraryServices.BarrowList(InMemoryDB.CurrentUser.Id);
                if (shList.Count == 0)
                {
                    Console.WriteLine("You Dont Have Any Book!");
                    Console.ReadKey();
                    UserMenu();
                }
                else
                {
                    foreach (var b in shList)
                    {
                        Console.WriteLine($"ID: {b.Id} - Name: {b.Name} - Author: {b.Author}");
                        Console.ReadKey();
                        UserMenu();
                    }
                }

                break;
            case 4:
                var lists = libraryServices.ShowBooks();
                if (lists.Count == 0)
                {
                    Console.WriteLine("There are currently no books available.");
                    Console.ReadKey();
                    UserMenu();
                }
                else
                {
                    foreach (var b in lists)
                    {
                        Console.WriteLine($"ID: {b.Id} - Name: {b.Name} - Author: {b.Author}");
                    }
                    Console.ReadKey();
                    UserMenu();
                }
                break;

            case 5:
                InMemoryDB.CurrentUser = null;
                MainMenu();
                break;
            default:
                Console.WriteLine("Selected Action Is Invalid");
                Console.ReadKey();
                UserMenu();
                break;


        }
    }

    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        UserMenu();
    }
}

void AdminMenu()
{
    try
    {
        if (InMemoryDB.CurrentUser == null)
        {
            Console.WriteLine("Pls Login First");
            Console.ReadKey();
            MainMenu();
        }
        Console.Clear();
        Console.WriteLine("1.Display the list of all books in the library");
        Console.WriteLine("2.Display the list of users along with their details e");
        Console.WriteLine("3.Charge Account");
        Console.WriteLine("4.Log out of account");
        if (!Int32.TryParse(Console.ReadLine(), out int ActionID))
        {
            Console.WriteLine("Selected Action Is Invalid");
            Console.ReadKey();
            AdminMenu();
        }
        switch (ActionID)
        {
            case 1:
                var lists = libraryServices.ShowAllBooks();
                foreach (var item in lists)
                {

                    if (item.UserID == null)
                    {
                        Console.WriteLine($"ID: {item.Id} - Name: {item.Name} - Author: {item.Author} - Barrow To: Nothing");
                    }
                    else
                    {
                        Console.WriteLine($"ID: {item.Id} - Name: {item.Name} - Author: {item.Author} - Barrow To: {item.UserID}");
                    }

                }
                Console.ReadKey();
                AdminMenu();
                break;
            case 2:
                var lis = userServices.ShowList();
                foreach (var item in lis)
                {
                    if (item.Role == RoleUserEnum.User)
                    {
                        Console.WriteLine($"ID: {item.Id} - Username: {item.UserName} - Role: {item.Role} - EndTime: {userServices.EndTime(item.Id)} Days");
                    }
                    else
                    {
                        Console.WriteLine($"ID: {item.Id} - Username: {item.UserName} - Role: {item.Role}");
                    }
                }
                Console.ReadKey();
                AdminMenu();
                break;
            case 3:
                var los = userServices.ShowList();
                foreach (var item in los)
                {
                    if (item.Role == RoleUserEnum.User)
                    {
                        Console.WriteLine($"ID: {item.Id} - Username: {item.UserName} - Role: {item.Role} - EndTime: {userServices.EndTime(item.Id)} Days");
                    }
                }
                Console.WriteLine("Pls Enter The User ID");
                int input = Convert.ToInt32(Console.ReadLine());
                bool isTr = los.Any(x => x.Id == input);
                if (!isTr)
                {
                    Console.WriteLine("The entered ID is not correct");
                    Console.ReadKey();
                    AdminMenu();

                }
                else
                {
                    Console.WriteLine("Pls Enter The Days To Charge");
                    int dy = Convert.ToInt32(Console.ReadLine());
                    userServices.Charge(input, dy);
                    Console.WriteLine("Done");
                    Console.ReadKey();
                    AdminMenu();
                }
                break;
            case 4:
                InMemoryDB.CurrentUser = null;
                MainMenu();
                break;
            default:
                Console.WriteLine("Selected Action Is Invalid");
                Console.ReadKey();
                MainMenu();
                break;


        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        AdminMenu();
    }
}