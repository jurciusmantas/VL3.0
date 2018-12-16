using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using VirtualLibrarity.Adapters;
using VirtualLibrarity.Models;
using ZXing.Mobile;
using AlertDialog = Android.App.AlertDialog;
using VirtualLibrarity.Activities;
using Android.Support.V7.App;

namespace VirtualLibrarity
{
    [Activity(Label = "Logged In Activity")]
    public class UserInfoActivity : AppCompatActivity
    {
        private UserToLoginResponse2 user;
        private LinearLayout _container;
        private RequestSender _RequestSender = new RequestSender();
        private Button _buttonTake;
        private Button _buttonReturn;
        private MobileBarcodeScanner _scanner;
        private string _QrCode;
        ListAdapter AllBooksListAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_user_info);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //------------------------------

            string userString = Intent.GetStringExtra("user");
            user = JsonConvert.DeserializeObject<UserToLoginResponse2>(userString);
            MobileBarcodeScanner.Initialize(Application);
            _scanner = new MobileBarcodeScanner();
            ListView AllBooksListView = FindViewById<ListView>(Resource.Id.listView);

            TextView NameTV = FindViewById<TextView>(Resource.Id.infoUserNameTV);
            NameTV.Text += user.UserInfo.Firstname;

            _buttonTake = FindViewById<Button>(Resource.Id.btnTake);
            _buttonReturn = FindViewById<Button>(Resource.Id.btnReturn);

            _buttonTake.Click += async delegate
                {
                    if (user.BorrowedBooks.Count < 3)
                    {
                        _scanner.UseCustomOverlay = false;
                        _scanner.TopText = "Hold the camera up to the barcode\nAbout 15 centimeters away";
                        _scanner.BottomText = "Wait for the barcode to automatically scan!";
                        var result = await _scanner.Scan();

                        HandleScanResultBorrow(result);
                    }
                    else
                        RunOnUiThread(() =>
                       Toast.MakeText(this, "Please return some books first!", ToastLength.Long).Show());
                };

            _buttonReturn.Click += async delegate 
            {
                _scanner.UseCustomOverlay = false;
                _scanner.TopText = "Hold the camera up to the barcode\nAbout 15 centimeters away";
                _scanner.BottomText = "Wait for the barcode to automatically scan!";
                var result = await _scanner.Scan();
                HandleScanResultReturn(result);
            };

            if (user.BorrowedBooks == null)
            {
                user.BorrowedBooks = new List<Book>();
            }


            AllBooksListAdapter = new ListAdapter(this, user.BorrowedBooks);
            AllBooksListView.Adapter = AllBooksListAdapter;
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (ZXing.Net.Mobile.Android.PermissionsHandler.NeedsPermissionRequest(this))
                ZXing.Net.Mobile.Android.PermissionsHandler.RequestPermissionsAsync(this);
        }

        private void AddBooksToReturnList()
        {
            _container = FindViewById<LinearLayout>(Resource.Id.container);

            if (user.BorrowedBooks == null)
            {
                user.BorrowedBooks = new List<Book>();
            }
            if (user.BorrowedBooks.Count > 0)
            {
                LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                foreach (Book bookModel in user.BorrowedBooks)
                {
                    View addView = layoutInflater.Inflate(Resource.Layout.book_list_item, null);

                    TextView AuthorTV = addView.FindViewById<TextView>(Resource.Id.TVAuthor);
                    TextView TitleTV = addView.FindViewById<TextView>(Resource.Id.TVTitle);
                    TextView QRCodeTV = addView.FindViewById<TextView>(Resource.Id.TVQRCode);

                    AuthorTV.Text += bookModel.Author;
                    TitleTV.Text += bookModel.Title;
                    QRCodeTV.Text += bookModel.QRCode;
                    _container.AddView(addView);
                }
            }
        }

        void HandleScanResultBorrow(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
            BookResponse bookResponse = _RequestSender.SendUpdateCopiesRequest(result.Text, true,user.UserInfo.Id);

            if (bookResponse != null && bookResponse.WasUpdated)
            {
                user.BorrowedBooks.Add(bookResponse.BookInfo);
                Refresh();
            }

        }

        void HandleScanResultReturn(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
            _QrCode = result.Text;
            BookResponse bookResponse = _RequestSender.SendUpdateCopiesRequest(_QrCode, false, user.UserInfo.Id);


            if (bookResponse != null && bookResponse.WasUpdated)
            {
                foreach(var book in user.BorrowedBooks)
                {
                    if (book.QRCode.ToString() == _QrCode)
                    {
                        user.BorrowedBooks.Remove(book);
                        break;
                    }
                }
                Refresh();
            }
        }

        private void Refresh()
        {
            string userinfo = JsonConvert.SerializeObject(user);
            Intent intent = new Intent(this, typeof(UserInfoActivity));
            intent.PutExtra("user", userinfo);
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent;
            switch(item.ItemId)
            {
                case Resource.Id.menu_search:
                    {
                        intent = new Intent(this, typeof(SearchActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.menu_logout:
                    {
                        intent = new Intent(this, typeof(LoginActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.menu_delete:
                    {
                        RequestSender rSender = new RequestSender();
                        if (user.BorrowedBooks.Count > 0)
                        {
                            AlertDialog.Builder builder = new AlertDialog.Builder(this);
                            AlertDialog alertDialog = builder.Create();

                            alertDialog.Show();

                            new AlertDialog.Builder(this)
                                .SetPositiveButton("Yes", (sent, args) =>
                                {
                                    if (rSender.SendDeleteUser(user.UserInfo.Id))
                                    {
                                        intent = new Intent(this, typeof(MainActivity));
                                        StartActivity(intent);
                                    }
                                    else
                                        Toast.MakeText(this, "Unable to delete account", ToastLength.Long).Show();
                                })
                                .SetNegativeButton("No", (sent, args) => { })
                                .SetMessage("Are you sure you want to delete your account?")
                                .SetTitle("DELETE ACCOUNT")
                                .SetCancelable(false)
                                .Show();
                        }
                        else
                            Toast.MakeText(this, "Please return all your books first!", ToastLength.Long).Show();
                        break;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}