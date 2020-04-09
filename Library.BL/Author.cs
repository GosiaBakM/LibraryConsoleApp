using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BL
{
    public class Author
    {
        public Author()
        {
        }
        public Author(string authorFullName)
        {
            AuthorFullName = authorFullName;
            AutorId = 0;
        }
        public int AutorId { get; private set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorFullName { get; set; }

        public override string ToString()
        {
            return $"{AuthorFullName}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Author author = (Author)obj;
            return (this.AuthorFullName.Equals(author.AuthorFullName));
        }
    }
}
