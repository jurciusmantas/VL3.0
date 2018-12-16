using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using VirtualLibrarity.Adapters;

namespace VirtualLibrarity
{
    [Activity(Label = "SearchActivitycs")]
    public class SearchActivity : Activity
    {
        ListAdapterLibrary AllBooksListAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_search);

            //-------------Fill list of books------------------------------

            var AllBooksListView = FindViewById<ListView>(Resource.Id.lstSearch);
            //AllBooksListAdapter = new ListAdapterLibrary(this, user.BorrowedBooks); <<-- ideti bibliotekos knygu sarasa cia
            AllBooksListView.Adapter = AllBooksListAdapter;



            //---------Spinner------------------------------
            var spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.Prompt = "Search by";

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Counter, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            string option = Java.Lang.String.ValueOf(spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, "Your choose: " + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();

            if (option == "Popularity")
            {
                //do smth
            } else if (option == "Genre")
            {
                //do smth
            } else if (option == "Author")
            {
                //do smth
            }
        }
    }
}