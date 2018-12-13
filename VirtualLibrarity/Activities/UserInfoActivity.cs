using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
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
                    _scanner.UseCustomOverlay = false;
                    _scanner.TopText = "Hold the camera up to the barcode\nAbout 15 centimeters away";
                    _scanner.BottomText = "Wait for the barcode to automatically scan!";
                    var result = await _scanner.Scan();

                    HandleScanResultBorrow(result);
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

            if (user.BorrowedBooks.Count > 0)
            {
                foreach (Book bookModel in user.BorrowedBooks)
                {
                    LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                    View addView = layoutInflater.Inflate(Resource.Layout.book_list_item, null);

                    TextView AuthorTV = addView.FindViewById<TextView>(Resource.Id.TVAuthor);
                    TextView TitleTV = addView.FindViewById<TextView>(Resource.Id.TVTitle);
                    TextView QRCodeTV = addView.FindViewById<TextView>(Resource.Id.TVQRCode);
                    TextView ReturnDateTV = addView.FindViewById<TextView>(Resource.Id.TVReturnDate);

                    AuthorTV.Text += bookModel.Author;
                    TitleTV.Text += bookModel.Title;
                    QRCodeTV.Text += bookModel.QRCode;
                    //ReturnDateTV.Text += bookModel.ReturnDate.ToString();
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
            BookResponse bookResponse = _RequestSender.SendBookRequest(result.Text, true);
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
            BookResponse bookResponse = _RequestSender.SendBookRequest(result.Text, false);
            //istrinti is user knygu saraso (pagal barcode, kuris yra result.Text
            //prideti atga prie bibliotekos knygu saraso


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}