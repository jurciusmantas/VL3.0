using System.Collections.Generic;
using VirtualLibDatabase;
using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Models.Entities
{
    public class BooksAndCategories
    {
        List<books> Books { get; set; }
        string[] Categories { get; set; }
    }
}