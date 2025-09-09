using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book SearchBook(string title)
        {
            var book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (book == null)
            {
                throw new BookNotFoundException(title); // ⬅️ custom exception
            }

            return book;
        }
    }
}
