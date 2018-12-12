using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using VirtualLibrarity.Adapters;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "AvailableBooksActivity")]
    public class AvailableBooksActivity : AppCompatActivity
    {
        List<Book> AllBooksList;
        ListAdapter AllBooksListAdapter;
        User _user;


        public List<string> checkedBooks = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_available_books);

            _user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("User"));

            ListView AllBooksListView = FindViewById<ListView>(Resource.Id.listView2);
            Button OKbtn = FindViewById<Button>(Resource.Id.OKbtn);

            AllBooksList = GetStoredBooks();

            AllBooksListAdapter = new ListAdapter(this, AllBooksList);
            AllBooksListView.Adapter = AllBooksListAdapter;

            OKbtn.Click += delegate
            {
                checkedBooks = AllBooksListAdapter.GetItems(); 

                List<Book> booksInLibrary = GetStoredBooks();

                for (int i = 0; i <= (checkedBooks.Count() - 1); i++)
                {
                   //prideti useriui prie jo saraso pasiskolinta knyga
                    booksInLibrary = ChangeList(booksInLibrary, i);
                }

                UpdateList(booksInLibrary);
                Toast.MakeText(ApplicationContext, "All books you have selected were added to your borrowed books list", ToastLength.Long).Show();

            };

            List<Book> GetStoredBooks()
            {
                List<Book> books = new List<Book>();

                //gauti is duombazes bibliotekos knygas
             
           

                if (books == null)
                {
                    return null;
                }
                return books;
            }

            List<Book> UpdateList(List<Book> books)
            {
                //istrinti is knygu saraso ta knyga, kuria pasieme useris

                return null;
            }

        }

        private List<Book> ChangeList(List<Book> books, int i)
        {

            Book book = new Book();
            string[] bookInfo = checkedBooks[i].ToString().Split(',');

            book.Author = bookInfo[0];
            book.Title = bookInfo[1];
            Book foundBook = new Book();
            foreach (Book b in books)
            {
                if ((b.Author == book.Author) && (b.Title == book.Title))
                    foundBook = b;
            }
            books.Remove(foundBook);
            return books;
        }
    }

}