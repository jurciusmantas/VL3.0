using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Views;
using VirtualLibrarity.Models;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "Top 3")]
    public class TopBooksActivity : AppCompatActivity
    {
        private RequestSender _requestSender;
        private List<Book2> _allBooks;
        private List<Book2> _sortedBooks;
        private TextView firstTV;
        private TextView secondTV;
        private TextView thirdTV;
        private string user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            user = Intent.GetStringExtra("user");
            SetContentView(Resource.Layout.activity_top_books);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_top);
            SetSupportActionBar(toolbar);

            firstTV = FindViewById<TextView>(Resource.Id.txtTopFirst);
            secondTV = FindViewById<TextView>(Resource.Id.txtTopSecond);
            thirdTV = FindViewById<TextView>(Resource.Id.txtTopThird);

            _requestSender = new RequestSender();
            _allBooks = _requestSender.SendGetBooksRequest();
    
            _sortedBooks = _allBooks.OrderByDescending(b => b.BookInfo.Popularity).ToList();
            var topThreeBooks = _sortedBooks.Take(3).ToList();

            firstTV.Text += topThreeBooks[0].BookInfo.Author + " " + topThreeBooks[0].BookInfo.Title;
            secondTV.Text += topThreeBooks[1].BookInfo.Author + " " + topThreeBooks[1].BookInfo.Title;
            thirdTV.Text += topThreeBooks[2].BookInfo.Author + " " + topThreeBooks[2].BookInfo.Title;

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_top, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent;
            switch (item.ItemId)
            {
                case Resource.Id.menu_back_from_top:
                    {
                        intent = new Intent(this, typeof(UserInfoActivity));
                        intent.PutExtra("user", user);
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.menu_search_from_topbooks:
                    {
                        intent = new Intent(this, typeof(SearchActivity));
                        StartActivity(intent);
                        break;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}