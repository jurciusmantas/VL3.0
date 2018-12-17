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
using VirtualLibrarity.Models;

namespace VirtualLibrarity.Adapters
{
    public class ListAdapterLibrary : BaseAdapter<Book2>
    {
        public List<Book2> nItems;
        private Context nContext;
        private SearchActivity searchActivity;
        private IEnumerable<Book2> enumerable;

        public ListAdapterLibrary(Context context, List<Book2> items)
        {
            nItems = items;
            nContext = context;
        }

        public Book2 getItem(int id)
        {
            return nItems.FirstOrDefault(b => b.BookInfo.Id == id);
        }

        public override int Count => nItems.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Book2 this[int position] => nItems[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(nContext).Inflate(Resource.Layout.library_books_list_item, null, false);
            }

            TextView authorTV = row.FindViewById<TextView>(Resource.Id.TVAuthor2);
            TextView titleTV = row.FindViewById<TextView>(Resource.Id.TVTitle2);
            TextView categoryTV = row.FindViewById<TextView>(Resource.Id.TVCategory2);
            TextView popularityTV = row.FindViewById<TextView>(Resource.Id.TVPopularity2);
            TextView countTV = row.FindViewById<TextView>(Resource.Id.TVCount2);

            authorTV.Text += nItems[position].BookInfo.Author;
            titleTV.Text += nItems[position].BookInfo.Title;
            categoryTV.Text += nItems[position].BookInfo.Category;
            popularityTV.Text += nItems[position].BookInfo.Popularity.ToString();
            countTV.Text += nItems[position].Amount.ToString();
            return row;
        }
    }
}