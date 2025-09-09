using System;

namespace LibraryApp
{
    // Custom exception for when a book is not found
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(string title)
            : base($"The book '{title}' was not found in the library.") { }
    }
}
