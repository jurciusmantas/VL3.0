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

namespace VirtualLibrarity.Adapters
{
    public class ListAdapterLibrary : BaseAdapter<Book>
    {
        public List<Book> nItems;
        private Context nContext;

        public ListAdapterLibrary(Context context, List<Book> items)
        {
            nItems = items;
            nContext = context;
        }

        public Book getItem(int id)
        {
            return nItems.FirstOrDefault(b => b.QRCode == id);
        }

        public override int Count => nItems.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Book this[int position] => nItems[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(nContext).Inflate(Resource.Layout.library_books_list_item, null, false);
            }

            TextView authorTV = row.FindViewById<TextView>(Resource.Id.TVAuthor2);
            TextView titleTV = row.FindViewById<TextView>(Resource.Id.TVTitle2);

            authorTV.Text = (position + 1).ToString() + ". Author: " + nItems[position].Author;
            titleTV.Text = "Title: " + nItems[position].Title;
            return row;
        }
    }
}