static class Library
{
    //TODO: Ubah inisialisasi BookList jadi loop dalam constructor
    //TODO: Ubah IDNumber jadi IdNumber 
    private static List<Book> BookList = new List<Book>()
    {
        new Book { Title = "The Open Society and Its Enemies", Author = "Karl Popper", IDNumber=11, IsAvailable = true },
        new Book { Title = "Nineteen Eighty-Four", Author = "George Orwell", IDNumber=12, IsAvailable = true },
        new Book { Title = "Animal Farm", Author = "George Orwell", IDNumber=13, IsAvailable = true },
        new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", IDNumber=14, IsAvailable = true },
        new Book { Title = "Brave New World", Author = "Aldous Huxley", IDNumber=15, IsAvailable = true },
        new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", IDNumber=16, IsAvailable = true },
        new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", IDNumber=17, IsAvailable = true },
        new Book { Title = "Sapiens: A Brief History of Humankind", Author = "Yuval Noah Harari", IDNumber=18, IsAvailable = true },
        new Book { Title = "Thinking, Fast and Slow", Author = "Daniel Kahneman", IDNumber=19, IsAvailable = true },
        new Book { Title = "The Road", Author = "Cormac McCarthy", IDNumber=20, IsAvailable = true },
        new Book { Title = "Man's Search for Meaning", Author = "Viktor E. Frankl", IDNumber=21, IsAvailable = true },
        new Book { Title = "Clean Code: A Handbook of Agile Software Craftmanship", Author = "Robert C. Martin", IDNumber=22, IsAvailable = true }
    };

    public static void MarkAsBorrowed(int bookID)
    {
        int index = GetBookIndex(bookID);
        BookList[index].IsAvailable = false;
    }

    public static void MarkAsReturned(int bookID)
    {
        int index = GetBookIndex(bookID);
        BookList[index].IsAvailable = true;
    }

    public static int GetBookIndex(int bookID)
    {
        for (int i = 0; i < BookList.Count; i++)
        {
            if (BookList[i].IDNumber == bookID)
            {
                //Console.WriteLine("Book with title {0} by {1} found.", BookList[i].Title, BookList[i].Author);
                return i;
            }
        }

        //Console.WriteLine("Book with ID {0} not found", bookID);
        return -1;
    }

    private static int GetNewID(){
        List<int> idNumbers = new List<int>();

        foreach(Book book in BookList){
            idNumbers.Add(book.IDNumber);
        }

        // Array.Sort(idNumbers);
        return idNumbers.Max() + 1;
    }

    private static string TruncateString(string text, int maxLength)
    {
        if (text.Length <= maxLength) return text;
        return text.Substring(0, maxLength - 3) + "..."; // add ellipsis
    }

    public static void ListAllBooks()
    {
        Console.WriteLine("Showing Book Listing:\n");

        // Print header with fixed-width columns
        Console.WriteLine("{0,-10} | {1,-20} | {2,-40} | {3,-12}", "Book ID", "Author", "Title", "Availability");
        Console.WriteLine(new string('-', 90)); // separator line

        // Print each book row
        foreach (Book book in BookList)
        {
            Console.WriteLine(
                "{0,-10} | {1,-20} | {2,-40} | {3,-12}",
                book.IDNumber, TruncateString(book.Author, 20), TruncateString(book.Title, 40), book.IsAvailable ? "Available" : "Not Available"
            );
        }
    }

    public static void DisplayBookInfo(int bookID)
    {
        Book? obtainedBook = GetBook(bookID);
        if (obtainedBook is null)
        {
            Console.WriteLine("Book with ID {0} not found");
        }
        else
        {
            Console.WriteLine(
                "Book Information\n\tBook ID: {0}\n\tTitle: {1}\n\tAuthor: {2}\n\tAvailability: {3}\n",
                obtainedBook.IDNumber,
                obtainedBook.Title,
                obtainedBook.Author,
                obtainedBook.IsAvailable ? "Available" : "Not available"
            );
        }
    }

    public static Book? GetBook(int bookID)
    {
        int bookIndex = GetBookIndex(bookID);
        if (bookIndex < 0)
        {
            return null;
        }
        else
        {
            return BookList[bookIndex];
        }
    }

    public static int BorrowBook(int bookID)
    {
        Book? obtainedBook = GetBook(bookID);
        if (obtainedBook is null)
        {
            Console.WriteLine("Book with id {0} does not exists.", bookID);
            return -1;
        }
        if (!obtainedBook.IsAvailable)
        {
            Console.WriteLine("Book with id {0} titled \"{1}\" is currently not available", bookID, obtainedBook.Title);
            return -1;
        }
        else
        {
            MarkAsBorrowed(bookID);
            Console.WriteLine("You have successfully borrowed \"{0}\" by {1}, id {2}", obtainedBook.Title, obtainedBook.Author, obtainedBook.IDNumber);
            return bookID;
        }
    }

    public static int ReturnBook(int bookID)
    {
        Book? obtainedBook = GetBook(bookID);
        if (obtainedBook is null)
        {
            Console.WriteLine("Book with id {0} does not exists.", bookID);
            return -1;
        }
        if (!obtainedBook.IsAvailable)
        {
            
            Console.WriteLine("Book with title \"{0}\" authored by {1} has been returned. Thank you for returning your book in time.", obtainedBook.Title, obtainedBook.Author);
            return bookID;
        }
        else
        {
            Console.WriteLine("You did not borrow that book. Check the inputted book ID.");
            return -1;
        }
    }

    public static int AddNewBook(string title, string author){
        int newID = GetNewID();
        Book newBook = new Book{Title = title, Author = author, IDNumber = newID, IsAvailable = true};

        BookList.Add(newBook);

        Console.WriteLine("Successfully added a new book titled \"{0}\" authored by \"{1}\"", newBook.Title, newBook.Author);

        return newID;
    }
}