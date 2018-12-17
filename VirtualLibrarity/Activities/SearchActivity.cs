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
    [Activity(Label = "Search")]
    public class SearchActivity : Activity
    {
        ListAdapterLibrary AllBooksListAdapter;
        RequestSender _requestSender = new RequestSender();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_search);

            //-------------Fill list of books------------------------------

            var AllBooksListView = FindViewById<ListView>(Resource.Id.lstSearch);
            AllBooksListAdapter = new ListAdapterLibrary(this, _requestSender.SendGetBooksRequest()); //<<-- ideti bibliotekos knygu sarasa cia
            AllBooksListView.Adapter = AllBooksListAdapter;



            //---------Spinner------------------------------
            var spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.Prompt = "Search by";

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Counter, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            //---------Spinner2--------------------------------
            List<string> categoriesOrAuthorsList = new List<string>(); //<<-- uzpildyti kategorijomis arba autoriaus. Priklausomai nuo spinner1 gauto rez


            var spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);
            spinner2.Prompt = "Choose author/catgeory";

            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);
            var adapter2 = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleSpinnerItem, categoriesOrAuthorsList);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

        }

        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner2 = sender as Spinner;
            string option = Java.Lang.String.ValueOf(spinner2.GetItemIdAtPosition(e.Position));

        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            string option = Java.Lang.String.ValueOf(spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, "Your choose: " + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();

            if (option == "Popularity")
            {
               // return "Popularity";
            } else if (option == "Genre")
            {
                //return "Category";
            } else if (option == "Author")
            {
                //return "Author";
            }
            //return null;
        }
    }
}