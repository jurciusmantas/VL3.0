using System.Collections.Generic;

namespace VirtualLibDatabase.Entities
{
    public class BooksAndCategories
    {
        List<books> Books { get; set; }
        string[] Categories { get; set; }
    }
}