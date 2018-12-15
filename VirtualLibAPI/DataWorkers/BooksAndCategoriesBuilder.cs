using VirtualLibAPI.Models.Entities;

namespace VirtualLibrarity.DataWorkers
{
    public class BooksAndCategoriesBuilder
    {
        public BooksAndCategories CreateBooksAndCategoriesList()
        {
            var booksAndCategories = new BooksAndCategories();
            return booksAndCategories;
        }
    }
}