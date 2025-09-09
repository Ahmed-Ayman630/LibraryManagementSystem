using System;
using Serilog;

namespace LibraryApp
{
    class Program
    {
        static void Main()
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Library library = new Library();

            try
            {
                Console.WriteLine("Welcome to the Library System 📚");
                Console.WriteLine("Adding some books...");

                library.AddBook(new Book("C# Basics", "John Doe"));
                library.AddBook(new Book("Advanced C#", "Jane Smith"));

                Console.WriteLine("\nEnter a book title to search:");
                string input = Console.ReadLine();

                // ⬅️ ممكن يحصل Exception هنا لو الكتاب مش موجود
                var foundBook = library.SearchBook(input);
                Console.WriteLine($"✅ Found: {foundBook.Title} by {foundBook.Author}");
                Log.Information("Book '{Title}' found successfully.", input);
            }
            catch (BookNotFoundException ex) // ⬅️ custom exception
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Log.Error(ex, "Book search failed");
            }
            catch (Exception ex) // ⬅️ أي خطأ تاني غير متوقع
            {
                Console.WriteLine($"⚠️ Unexpected error: {ex.Message}");
                Log.Fatal(ex, "Unexpected error occurred");
            }
            finally
            {
                Console.WriteLine("\nThank you for using the Library System!");
                Log.CloseAndFlush(); // ⬅️ يغلق الـ logger
            }
        }
    }
}
