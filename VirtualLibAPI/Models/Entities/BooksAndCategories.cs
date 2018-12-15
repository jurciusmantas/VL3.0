using System.Collections.Generic;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Models
{
    public class BooksAndCategories
    {
        List<Book> Books { get; set; }
        string[] Categories { get; set; }
    }
}