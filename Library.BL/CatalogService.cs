using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.BL
{
    class CatalogService
    {
        public Dictionary<string, Book> BookCatalog { get; private set;}
        private static CatalogService instance;

        public CatalogService()
        {
            BookCatalog = FileConnector.LoadCatalogFromFile();
        }

        public static CatalogService getInstance()
        {
            if (instance == null)
                instance = new CatalogService();
            return instance;
        }
       
        public bool AddBookToCatalog(string ISBN, Book book)
        {
            BookCatalog.Add(ISBN, book);
            FileConnector.SaveCatalogToFile(BookCatalog);
            Console.WriteLine("\nKsiążka została dodana do katalogu.\n\n");
            return true;
        }

        public bool RemoveBookFromCatalog(string ISBN)
        {
            BookCatalog.Remove(ISBN);
            FileConnector.SaveCatalogToFile(BookCatalog);
            Console.WriteLine("Książka została usunięta z katalogu.\n");  
            return true;
        }

        public List<KeyValuePair<string, Book>> FindBookByTitle(string title) 
        {
            var listOfBooks = BookCatalog
                .Where(x => x.Value
                .Title.ToLower().Equals(title))
                .ToList();
            return listOfBooks;
        }

        public List<KeyValuePair<string, Book>> FindBookByAuthor(Author author)
        {
            var listOfBooks = BookCatalog
                .Where(x => x.Value.AuthorList.Contains(author))
                .ToList();
            return listOfBooks;
        }

        public Book FindBookByISBN(string ISBN)
        {
            Book book;
            BookCatalog.TryGetValue(ISBN, out book);
            return book;
        }

        public bool BorrowBook(string ISBN, Customer customer)
        {
            var book = FindBookByISBN(ISBN);
            book.isBorrowed = true;
            book.customer = customer;
            book.LastBorrowDate = DateTime.Now;
            BookCatalog[ISBN] = book;
            FileConnector.SaveCatalogToFile(BookCatalog);
            return true;
        }

        public List<KeyValuePair<string, Book>> FindBooksUnborrowedByNWeeks(int weeks)
        {
            var borrowingDateBound = DateTime.Now.AddDays(-weeks * 7);
            Console.WriteLine(borrowingDateBound);
            var listOfBooks = BookCatalog
                .Where(book => (book.Value.LastBorrowDate >= borrowingDateBound) && (!book.Value.isBorrowed))
                .ToList();
            return listOfBooks;
        }
        public List<Customer> FindCustomersWithBorrowedBooks()
        {
            var list = BookCatalog.Values
                .Where(book => book.isBorrowed == true)
                .Select(book => book.customer)
                .ToList();
            return list;
        }

    }
}
