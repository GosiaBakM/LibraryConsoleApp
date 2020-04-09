using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BL
{
    public class Book
    {
        public Book()
        {
        }
        public Book(string title, List<Author> authorList)
        {
            Title = title;
            AuthorList = authorList;
        }

        public string Title { get; set; }
        public List<Author> AuthorList { get; set; }
        public DateTime LastBorrowDate { get; set; }
        public Customer customer { get; set; }
        public bool isBorrowed { get; set; }

        public override string ToString()
        {
            StringBuilder AuthorsSb = new StringBuilder();
            foreach(Author author in AuthorList)
            {
                AuthorsSb.Append(author.ToString()).Append(", ");
            }
            return $"Tytuł książki: {Title}, Autor: {AuthorsSb.ToString()}";
        }
}
        delegate string Print(List<Author> list);
}
