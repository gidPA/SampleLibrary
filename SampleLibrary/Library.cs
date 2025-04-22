enum SearchOptions{
    Title,
    Author,
    AllCategories,
    Availability
}

static class Library
{
    //TODO: Ubah inisialisasi LibraryItems jadi loop dalam constructor -> Done
    //TODO: Ubah IdNumber jadi IdNumber -> Done
    private static List<Book> LibraryItems = new List<Book>();

    static Library()
    {
        (string Title, string Author)[] initialBookDetails =
        {
            ("The Open Society and Its Enemies", "Karl Popper"),
            ("The Poverty of Historicism", "Karl Popper"),

            ("Nineteen Eighty-Four", "George Orwell"),
            ("Animal Farm", "George Orwell"),

            ("To Kill a Mockingbird", "Harper Lee"),
            ("Go Set a Watchman", "Harper Lee"),

            ("The Catcher in the Rye", "J.D. Salinger"),
            ("Franny and Zooey", "J.D. Salinger"),

            ("The Great Gatsby", "F. Scott Fitzgerald"),
            ("Tender Is the Night", "F. Scott Fitzgerald"),

            ("Sapiens: A Brief History of Humankind", "Yuval Noah Harari"),
            ("Homo Deus: A Brief History of Tomorrow", "Yuval Noah Harari"),

            ("Thinking, Fast and Slow", "Daniel Kahneman"),
            ("Noise: A Flaw in Human Judgment", "Daniel Kahneman"),

            ("The Road", "Cormac McCarthy"),
            ("No Country for Old Men", "Cormac McCarthy"),

            ("Man's Search for Meaning", "Viktor E. Frankl"),
            ("The Will to Meaning", "Viktor E. Frankl"),

            ("Clean Code: A Handbook of Agile Software Craftmanship", "Robert C. Martin"),
            ("Clean Architecture: A Craftsman's Guide to Software Structure and Design", "Robert C. Martin"),

            ("The C++ Programming Language", "Bjarne Strousroup"),
            ("Programming: Principles and Practice Using C++", "Bjarne Strousroup"),
        };

        for (int i = 0; i < initialBookDetails.Length; i++)
        {
            LibraryItems.Add
            (
                new Book
                {
                    Title = initialBookDetails[i].Title,
                    Author = initialBookDetails[i].Author,
                    IdNumber = 11 + i,
                    IsAvailable = true
                }
            );
        }
    }

    public static void MarkAsBorrowed(int bookID)
    {
        int index = GetBookIndex(bookID);
        LibraryItems[index].IsAvailable = false;
    }

    public static void MarkAsReturned(int bookID)
    {
        int index = GetBookIndex(bookID);
        LibraryItems[index].IsAvailable = true;
    }

    public static int GetBookIndex(int bookID)
    {
        for (int i = 0; i < LibraryItems.Count; i++)
        {
            if (LibraryItems[i].IdNumber == bookID)
            {
                //Console.WriteLine("Book with title {0} by {1} found.", LibraryItems[i].Title, LibraryItems[i].Author);
                return i;
            }
        }

        //Console.WriteLine("Book with ID {0} not found", bookID);
        return -1;
    }

    private static int GetNewID()
    {
        return LibraryItems.Any() ? LibraryItems.Max(book => book.IdNumber) + 1 : 1;
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
        foreach (Book book in LibraryItems)
        {
            Console.WriteLine(
                "{0,-10} | {1,-20} | {2,-40} | {3,-12}",
                book.IdNumber, TruncateString(book.Author, 20), TruncateString(book.Title, 40), book.IsAvailable ? "Available" : "Not Available"
            );
        }
    }

    public static void DisplayBookInfo(int bookID)
    {
        Book? obtainedBook = GetBook(bookID);
        if (obtainedBook is null)
        {
            Console.WriteLine("Book with ID {0} not found", bookID);
        }
        else
        {
            Console.WriteLine(
                "Book Information\n\tBook ID: {0}\n\tTitle: {1}\n\tAuthor: {2}\n\tAvailability: {3}\n",
                obtainedBook.IdNumber,
                obtainedBook.Title,
                obtainedBook.Author,
                obtainedBook.IsAvailable ? "Available" : "Not available"
            );
        }
    }

    public static void DisplayBookInfo(Book book)
    {
        Console.WriteLine(
            "Book Information\n\tBook ID: {0}\n\tTitle: {1}\n\tAuthor: {2}\n\tAvailability: {3}\n",
            book.IdNumber,
            book.Title,
            book.Author,
            book.IsAvailable ? "Available" : "Not available"
        );
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
            return LibraryItems[bookIndex];
        }
    }

    public static List<Book> FilterBooks(SearchOptions option, string keyword){
        var filterMethod = new Dictionary<SearchOptions, Func<string, List<Book>>>(){
            {
                SearchOptions.Title,
                (filter) => {
                    return LibraryItems.Where(b => b.Title.ToLower().Contains(filter)).ToList();
                }
            },
            {
                SearchOptions.Author,
                (filter) => {
                    return LibraryItems.Where(b => b.Author.ToLower().Contains(filter)).ToList();
                }
            },
            {
                SearchOptions.AllCategories,
                (filter) => {
                    int idFilter = 0;
                    int.TryParse(filter, out idFilter);

                    if(filter == "available" || filter == "unavailable"){
                        bool availabilityFilter = filter == "available";
                            return LibraryItems
                                .Where(b =>
                                    b.Title.ToLower().Contains(filter) ||
                                    b.Author.ToLower().Contains(filter) ||
                                    b.IdNumber.Equals(idFilter) ||
                                    b.IsAvailable.Equals(availabilityFilter))
                                .ToList();
                    }

                    return LibraryItems
                        .Where(b =>
                            b.Title.ToLower().Contains(filter) ||
                            b.Author.ToLower().Contains(filter) ||
                            b.IdNumber.Equals(idFilter))
                        .ToList();
                }
            },
            {
                SearchOptions.Availability,
                (filter) => {
                    if (filter == "1"){
                        return LibraryItems.Where(b => b.IsAvailable.Equals(true)).ToList();
                    } else{
                        return LibraryItems.Where(b => b.IsAvailable.Equals(false)).ToList();
                    }
                }
            },
            
        };
        return filterMethod[option](keyword.ToLower());
    }

    public static void DisplayFilterResult(SearchOptions option, string keyword){
        var bookList = FilterBooks(option, keyword);
        if (!bookList.Any() || bookList is null){
            Console.WriteLine("No results");
            return;
        } else {
            Console.WriteLine("Found {0} entries", bookList.Count);
            foreach(Book book in bookList){
                DisplayBookInfo(book);
            }

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
            Console.WriteLine("You have successfully borrowed \"{0}\" by {1}, id {2}", obtainedBook.Title, obtainedBook.Author, obtainedBook.IdNumber);
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

    public static int AddNewBook(string title, string author)
    {
        int newID = GetNewID();
        Book newBook = new Book { Title = title, Author = author, IdNumber = newID, IsAvailable = true };

        LibraryItems.Add(newBook);

        Console.WriteLine("Successfully added a new book titled \"{0}\" authored by \"{1}\"", newBook.Title, newBook.Author);

        return newID;
    }
}