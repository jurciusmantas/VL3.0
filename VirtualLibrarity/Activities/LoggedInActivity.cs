using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using VirtualLibClient;

namespace VirtualLibrarity
{
    [Activity(Label = "ManualLoginActivity")]
    public class LoggedInActivity : Activity
    {
        LinearLayout _container;
        User _loggedUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_logged_in);
   
            string extraData = Intent.GetStringExtra("userID");

            // kreiptis i db ir gauti useri

            _loggedUser = new User(); // <- pakeisti i grazinta is db


            TextView NameTV = FindViewById<TextView>(Resource.Id.infoUserNameTV);
            NameTV.Text += _loggedUser.Name;
            TextView SurnameTV = FindViewById<TextView>(Resource.Id.infoUserSurnameTV);
            SurnameTV.Text += _loggedUser.Surname;
            TextView EmailTV = FindViewById<TextView>(Resource.Id.infoUserEmailTV);
            EmailTV.Text += _loggedUser.Email;

            AddBooksToReturnList();
        }

        private void AddBooksToReturnList()
        {
            _container = FindViewById<LinearLayout>(Resource.Id.container);


            /*** LOGIKA KAIP SUTALPINT GAUTA ATS I UI******
            LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
            View addView = layoutInflater.Inflate(Resource.Layout.book_list_item, null);

            TextView AuthorTV = addView.FindViewById<TextView>(Resource.Id.TVAuthor);
            TextView TitleTV = addView.FindViewById<TextView>(Resource.Id.TVTitle);
            TextView QRCodeTV = addView.FindViewById<TextView>(Resource.Id.TVQRCode);

            AuthorTV.Text += bookInfo.GetString(selectData.GetColumnIndex("Author"));
            TitleTV.Text += bookInfo.GetString(selectData.GetColumnIndex("Title"));
            QRCodeTV.Text += bookInfo.GetInt(selectData.GetColumnIndex("QRCode")).ToString();

            _container.AddView(addView);*/
        }
    }
}