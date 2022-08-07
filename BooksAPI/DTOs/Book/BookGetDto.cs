using System;

namespace BooksAPI.DTOs.Book
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime releaseDate { get; set; }
        public short Pages { get; set; }
        public CategoryAndBookGetDto Category { get; set; }
    }

    public class CategoryAndBookGetDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
    }
}
