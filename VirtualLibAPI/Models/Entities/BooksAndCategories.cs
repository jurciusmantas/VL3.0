using System.Collections.Generic;
using VirtualLibDatabase;

namespace VirtualLibAPI.Models.Entities
{
    public class BooksAndCategories
    {
        List<books> Books { get; set; }
        string[] Categories { get; set; }
    }
}