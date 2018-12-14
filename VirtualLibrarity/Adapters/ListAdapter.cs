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
    public class ListAdapter : BaseAdapter<Book>
    {
        public List<Book> nItems;
        private Context nContext;

        public ListAdapter(Context context, List<Book> items)
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
                row = LayoutInflater.From(nContext).Inflate(Resource.Layout.book_list_item, null, false);
            }

            TextView authorTV = row.FindViewById<TextView>(Resource.Id.TVAuthor);
            TextView titleTV = row.FindViewById<TextView>(Resource.Id.TVTitle);
            TextView qrCodeTV = row.FindViewById<TextView>(Resource.Id.TVQRCode);

            authorTV.Text = (position+1).ToString()+". Author: "+nItems[position].Author;
            titleTV.Text = "Title: "+nItems[position].Title;
            qrCodeTV.Text = "QRCode: "+nItems[position].QRCode.ToString();
            return row;
        }
    }
}