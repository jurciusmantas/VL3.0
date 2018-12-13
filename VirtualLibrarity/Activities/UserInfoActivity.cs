﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using VirtualLibrarity.Activities;
using VirtualLibrarity.Models;
using ZXing.Mobile;

namespace VirtualLibrarity
{
    [Activity(Label = "ManualLoginActivity")]
    public class UserInfoActivity : Activity
    {
        private UserToLoginResponse2 user;
        private LinearLayout _container;
        private RequestSender _RequestSender = new RequestSender();
        private Button _buttonTake;
        private Button _buttonReturn;
        private MobileBarcodeScanner _scanner;
        private ImageButton _buttonQuit;
        private string _QrCode;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_user_info);

            string userString = Intent.GetStringExtra("user");
            user = JsonConvert.DeserializeObject<UserToLoginResponse2>(userString);
            MobileBarcodeScanner.Initialize(Application);
            _scanner = new MobileBarcodeScanner();

            TextView NameTV = FindViewById<TextView>(Resource.Id.infoUserNameTV);
            NameTV.Text += user.UserInfo.Firstname;
            // TextView SurnameTV = FindViewById<TextView>(Resource.Id.infoUserSurnameTV);
            // SurnameTV.Text += user.UserInfo.Lastname;
            // TextView EmailTV = FindViewById<TextView>(Resource.Id.infoUserEmailTV);
            // EmailTV.Text += user.UserInfo.Email;

            _buttonQuit = FindViewById<ImageButton>(Resource.Id.outButton);

            _buttonQuit.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            _buttonTake = FindViewById<Button>(Resource.Id.btnTake);
            _buttonReturn = FindViewById<Button>(Resource.Id.btnReturn);

            _buttonTake.Click += async delegate
                {
                    if (user.BorrowedBooks.Count >= 3)
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

            AddBooksToReturnList();
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
                foreach (Book bookModel in user.BorrowedBooks)
                {
                    LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
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
            BookResponse bookResponse = _RequestSender.SendBookRequest(result.Text, true,user.UserInfo.Id);
            //istrinti is bibliotekos knygu saraso (pagal barcode, kuris yra result.Text)
            //prideti prie user knygu saraso
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
            BookResponse bookResponse = _RequestSender.SendBookRequest(_QrCode, false, user.UserInfo.Id);
            //istrinti is user knygu saraso (pagal barcode, kuris yra result.Text
            //prideti atga prie bibliotekos knygu saraso


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
    }
}