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
using Newtonsoft.Json;

using Android.Support.V7.App;

using ZXing;
using ZXing.Mobile;

using Android.Content.PM;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "TakeABookQrActivity")]
    public class QrActivity : Activity
    {
        RequestSender _RequestSender = new RequestSender();
        User _user;
        Button buttonBorrowBook;
        Button buttonReturnBook;
        MobileBarcodeScanner scanner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_qr);

            _user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("User"));

            // Initialize the scanner first so we can track the current context
            MobileBarcodeScanner.Initialize(Application);

            //Create a new instance of our Scanner
            scanner = new MobileBarcodeScanner();

            buttonBorrowBook = this.FindViewById<Button>(Resource.Id.buttonBorrowBook);
            buttonReturnBook = this.FindViewById<Button>(Resource.Id.buttonReturnBook);

            buttonBorrowBook.Click += async delegate {

                //Tell our scanner to use the default overlay
                scanner.UseCustomOverlay = false;

                //We can customize the top and bottom text of the default overlay
                scanner.TopText = "Hold the camera up to the barcode\nAbout 15 centimeters away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                //Start scanning
                var result = await scanner.Scan();

                HandleScanResultBorrow(result);
            };

            buttonReturnBook.Click += async delegate {

                //Tell our scanner to use the default overlay
                scanner.UseCustomOverlay = false;

                //We can customize the top and bottom text of the default overlay
                scanner.TopText = "Hold the camera up to the barcode\nAbout 15 centimeters away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                //Start scanning
                var result = await scanner.Scan();

                HandleScanResultReturn(result);
            };


        }

        protected override void OnResume()
        {
            base.OnResume();

            if (ZXing.Net.Mobile.Android.PermissionsHandler.NeedsPermissionRequest(this))
                ZXing.Net.Mobile.Android.PermissionsHandler.RequestPermissionsAsync(this);
        }

        void HandleScanResultBorrow(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());

            _RequestSender.SendBookRequest(result.Text, true);
            //istrinti is bibliotekos knygu saraso (pagal barcode, kuris yra result.Text)
            //prideti prie user knygu saraso


        }

        void HandleScanResultReturn(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());

            _RequestSender.SendBookRequest(result.Text, false);
            //istrinti is user knygu saraso (pagal barcode, kuris yra result.Text
            //prideti atga prie bibliotekos knygu saraso


        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}