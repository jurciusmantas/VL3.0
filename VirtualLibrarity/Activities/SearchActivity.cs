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
using VirtualLibrarity.Models;

namespace VirtualLibrarity
{
    [Activity(Label = "Search")]
    public class SearchActivity : Activity
    {
        private ListAdapterLibrary AllBooksListAdapter;
        private RequestSender _requestSender;
        private List<Book2> _allBooks;
        private Spinner _spinner;
        private Spinner _spinner2;
        private ArrayAdapter _adapter;
        private ArrayAdapter _adapter2;
        private ListView _allBooksListView;
        private ListAdapterLibrary _allBooksListAdapter;
        private string _spinner1Choose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_search);
            _requestSender = new RequestSender();
            _allBooks = _requestSender.SendGetBooksRequest();

            //-------------Fill list of books------------------------------
            _allBooks = _allBooks.OrderByDescending(b => b.BookInfo.Popularity).ToList();
            _allBooksListView = FindViewById<ListView>(Resource.Id.lstSearch);
            _allBooksListAdapter = new ListAdapterLibrary(this, _allBooks);
            _allBooksListView.Adapter = AllBooksListAdapter;


            //---------Spinner------------------------------
            _spinner = FindViewById<Spinner>(Resource.Id.spinner);
            _spinner.Prompt = "Search by";
            _spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            _adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Counter, Android.Resource.Layout.SimpleSpinnerItem);
            _adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _spinner.Adapter = _adapter;
            _spinner1Choose = "Popularity";

            //---------Spinner2--------------------------------
            _spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);
            _spinner2.Prompt = "Choose filter option";
            _spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);
            _adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.ByPopularity, Android.Resource.Layout.SimpleSpinnerItem);
            _adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _spinner2.Adapter = _adapter2;
        }

        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (_spinner1Choose)
            {
                case "Popularity":
                    {

                        if (_spinner2.GetItemAtPosition(e.Position).ToString() == "Most popular in top")
                            _allBooksListAdapter = new ListAdapterLibrary(this, _allBooks.OrderByDescending(b => b.BookInfo.Popularity).ToList());
                        else
                            _allBooksListAdapter = new ListAdapterLibrary(this, _allBooks.OrderBy(b => b.BookInfo.Popularity).ToList());

                        _allBooksListView.Adapter = _allBooksListAdapter;
                        break;
                    }
                case "Genre":
                    {
                        _allBooksListAdapter = new ListAdapterLibrary(this, _allBooks.Where(b => b.BookInfo.Category == _spinner2.GetItemAtPosition(e.Position).ToString()).ToList());
                        _allBooksListView.Adapter = _allBooksListAdapter;
                        break;
                    }
                case "Author":
                    {
                        _allBooksListAdapter = new ListAdapterLibrary(this, _allBooks.Where(b => b.BookInfo.Author == _spinner2.GetItemAtPosition(e.Position).ToString()).ToList());
                        _allBooksListView.Adapter = _allBooksListAdapter;
                        break;
                    }
            }

        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (_spinner.GetItemAtPosition(e.Position).ToString())
            {
                case "Popularity":
                    {
                        _adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.ByPopularity, Android.Resource.Layout.SimpleSpinnerItem);
                        _adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                        _spinner2.Adapter = _adapter2;

                        _allBooksListAdapter = new ListAdapterLibrary(this, _allBooks.OrderByDescending(b => b.BookInfo.Popularity).ToList());
                        _allBooksListView.Adapter = _allBooksListAdapter;
                        _spinner1Choose = "Popularity";
                        break;
                    }
                case "Genre":
                    {
                        _adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem,
                            _allBooks.Select(b => b.BookInfo.Category).Distinct().ToList());
                        _adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                        _spinner2.Adapter = _adapter2;
                        _spinner1Choose = "Genre";
                        break;
                    }
                case "Author":
                    {
                        _adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem,
                            _allBooks.Select(b => b.BookInfo.Author).Distinct().ToList());
                        _adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                        _spinner2.Adapter = _adapter2;
                        _spinner1Choose = "Author";
                        break;
                    }
            }

        }
    }
}