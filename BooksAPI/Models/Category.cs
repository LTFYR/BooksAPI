using BooksAPI.Models.Base;
using System.Collections.Generic;

namespace BooksAPI.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
