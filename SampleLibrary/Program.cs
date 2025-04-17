// See https://aka.ms/new-console-template for more information
class SampleLibrary
{
    static void Main()
    {
        DisplayControlUI();
        while (true)
        {
            ConsoleKeyInfo option;
            Console.WriteLine("Choose an action (press 1-9): ");

            option = Console.ReadKey(intercept: true);

            if (option.Key == ConsoleKey.D1)
            {
                DisplayControlUI();
            }
            else if (option.Key == ConsoleKey.D2)
            {
                Library.ListAllBooks();
            }
            else if (option.Key == ConsoleKey.D3)
            {
                Console.WriteLine("Not yet implemented");
            }
            else if (option.Key == ConsoleKey.D4)
            {
                Console.WriteLine("Not yet implemented");
            }
            else if (option.Key == ConsoleKey.D5)
            {
                SearchBookByIDUI();
            }
            else if (option.Key == ConsoleKey.D6)
            {
                BorrowBookUI();
            }
            else if (option.Key == ConsoleKey.D7)
            {
                ReturnBookUI();
            }
            else if (option.Key == ConsoleKey.D8)
            {
                AddNewBookUI();
            }
            else if (option.Key == ConsoleKey.D9)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Press key numbered 1 - 9");
            }
        }
    }

    public static void DisplayControlUI()
    {
        Console.WriteLine(
                "Welcome to GPA's Library \n" +
                "Please choose an action: \n" +
                "\t1. Display this control list\n" +
                "\t2. Check Library collection\n" +
                "\t3. Search books by title\n" +
                "\t4. Search books by author\n" +
                "\t5. Search books by ID\n" +
                "\t6. Borrow a book\n" +
                "\t7. Return borrowed books\n" +
                "\t8. Add a new book\n" + 
                "\t9. Quit"
            );
    }

    public static void BorrowBookUI()
    {
        Console.Write("Enter the ID number of the book you want to borrow, then press enter\n(or press C to cancel): ");
        string? bookIDString = Console.ReadLine();

        if (bookIDString?.ToLower() == "c")
        {
            return;
        }
        else if (Int32.TryParse(bookIDString, out int bookID))
        {
            Library.BorrowBook(bookID);
        }
        else
        {
            Console.WriteLine("Please enter a valid Book ID");
        }
    }

    public static void ReturnBookUI()
    {
        Console.Write("Enter the ID number of the book you want to return, then press enter\n(or press C to cancel): ");
        string? bookIDString = Console.ReadLine();

        if (bookIDString?.ToLower() == "c")
        {
            return;
        }
        else if (Int32.TryParse(bookIDString, out int bookID))
        {
            Library.ReturnBook(bookID);
        }
        else
        {
            Console.WriteLine("Please enter a valid Book ID");
        }
    }

    public static void SearchBookByIDUI()
    {
        Console.Write("Enter the book's ID number, then press enter\n(or press C to cancel): ");
        string? bookIDString = Console.ReadLine();

        if (bookIDString.ToLower() == "c")
        {
            return;
        }
        else if (Int32.TryParse(bookIDString, out int bookID))
        {
            Library.DisplayBookInfo(bookID);
        }
        else
        {
            Console.WriteLine("Please enter a valid Book ID");
        }
    }

    static void AddNewBookUI(){
        Console.WriteLine("Enter the information of the new book (or press C and enter to cancel)");
        Console.Write("\tBook Title: ");
        string? title = Console.ReadLine();
        // q check
        if (string.IsNullOrEmpty(title)){
            Console.WriteLine("Please enter a valid title");
            return;
        } else if (title.ToLower() == "c"){
            return;
        }

        Console.Write("\tAuthor: ");

        string? author = Console.ReadLine();

        // q check
        if (string.IsNullOrEmpty(author)){
            Console.WriteLine("Please enter a valid name");
            return;
        } else if (author.ToLower() == "c"){
            return;
        }

        Library.AddNewBook(title, author);
    }
}
