using System.Collections.Generic;

namespace BooksAPI.DTOs
{
    public class ListDto<T>
    {
        public List<T> ListItemDtos { get; set; }
        public int Count { get; set; }
    }
}
