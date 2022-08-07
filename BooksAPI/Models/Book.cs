using BooksAPI.Models.Base;
using System;

namespace BooksAPI.Models
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public DateTime releaseDate { get; set; }
        public short Pages { get; set; }
    }
}
