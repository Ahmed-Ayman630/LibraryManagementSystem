using System;
using System.Collections.Generic;
using Serilog;

namespace LibraryManagement
{
    // Book class
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }

    // Library class
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Log.Information("Book added: {Title} by {Author}", book.Title, book.Author);
        }

        public Book SearchBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("❌ Invalid title. Please enter a valid book title.");
                Log.Warning("Empty title entered in search.");
                return null;
            }

            foreach (var book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Log.Information("Book found: {Title}", book.Title);
                    return book;
                }
            }

            Log.Warning("Book not found: {Title}", title);
            return null;
        }


        public void DisplayBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books in the library.");
                return;
            }

            Console.WriteLine("\n📚 Books in Library:");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} by {book.Author}");
            }
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File("library_log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Library library = new Library();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== Library Menu =====");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Search Book");
                Console.WriteLine("3. Display All Books");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter author name: ");
                        string author = Console.ReadLine();

                        library.AddBook(new Book(title, author));
                        Console.WriteLine("Book added successfully!");
                        break;

                    case "2":
                        Console.Write("Enter book title to search: ");
                        string searchTitle = Console.ReadLine();

                        var foundBook = library.SearchBook(searchTitle);
                        if (foundBook != null)
                        {
                            Console.WriteLine($"✅ Found: {foundBook.Title} by {foundBook.Author}");
                        }
                        else
                        {
                            Console.WriteLine("❌ Book not found.");
                        }
                        break;

                    case "3":
                        library.DisplayBooks();
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("Exiting... Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
