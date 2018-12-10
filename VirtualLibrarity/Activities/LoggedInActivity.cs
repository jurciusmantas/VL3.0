using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Views;
using Android.Widget;
using VirtualLibrarity.Database;

namespace VirtualLibrarity
{
    [Activity(Label = "ManualLoginActivity")]
    public class LoggedInActivity : Activity
    {
        LinearLayout _container;
        DbHelper _dbHelper;
        SQLiteDatabase _database;
        User _loggedUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_logged_in);

            string extraData = Intent.GetStringExtra("userID");
            _dbHelper = new DbHelper(this);
            _database = _dbHelper.WritableDatabase;

            ICursor selectData = _database.RawQuery("select * from User where Id = '"
                    + extraData + "'", new string[] { });

            selectData.MoveToFirst();

            _loggedUser = new User
            {
                Id = selectData.GetInt(selectData.GetColumnIndex("Id")),
                Face = selectData.GetInt(selectData.GetColumnIndex("Face")),
                Email = selectData.GetString(selectData.GetColumnIndex("Email")),
                Name = selectData.GetString(selectData.GetColumnIndex("Name")),
                Surname = selectData.GetString(selectData.GetColumnIndex("Surname")),
            };


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

            ICursor selectData = _database.RawQuery("select * from Copy where UserID='" + _loggedUser.Id + "'", new string[] { });
            selectData.MoveToFirst();

            if (selectData.Count > 0)
            {
                if (selectData.Count != 1)
                {
                    while (selectData.MoveToNext())
                    {
                        ICursor bookInfo = _database.RawQuery("select * from Book where QRCode ='" +
                            selectData.GetInt(selectData.GetColumnIndex("QRCode")) + "'", new string[] { });
                        LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                        View addView = layoutInflater.Inflate(Resource.Layout.book_list_item, null);

                        TextView AuthorTV = addView.FindViewById<TextView>(Resource.Id.TVAuthor);
                        TextView TitleTV = addView.FindViewById<TextView>(Resource.Id.TVTitle);
                        TextView QRCodeTV = addView.FindViewById<TextView>(Resource.Id.TVQRCode);

                        AuthorTV.Text += bookInfo.GetString(selectData.GetColumnIndex("Author"));
                        TitleTV.Text += bookInfo.GetString(selectData.GetColumnIndex("Title"));
                        QRCodeTV.Text += bookInfo.GetInt(selectData.GetColumnIndex("QRCode")).ToString();

                        _container.AddView(addView);
                    }
                }
                else
                {
                    ICursor bookInfo = _database.RawQuery("select * from Book where QRCode ='" +
                            selectData.GetInt(selectData.GetColumnIndex("QRCode")) + "'", new string[] { });
                    bookInfo.MoveToFirst();

                    LayoutInflater layoutInflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                    View addView = layoutInflater.Inflate(Resource.Layout.book_list_item, null);

                    TextView AuthorTV = addView.FindViewById<TextView>(Resource.Id.TVAuthor);
                    TextView TitleTV = addView.FindViewById<TextView>(Resource.Id.TVTitle);
                    TextView QRCodeTV = addView.FindViewById<TextView>(Resource.Id.TVQRCode);
                    TextView ReturnDateTV = addView.FindViewById<TextView>(Resource.Id.TVReturnDate);

                    AuthorTV.Text += bookInfo.GetString(bookInfo.GetColumnIndex("Author"));
                    TitleTV.Text += bookInfo.GetString(bookInfo.GetColumnIndex("Title"));
                    QRCodeTV.Text += bookInfo.GetInt(bookInfo.GetColumnIndex("QRCode")).ToString();
                    ReturnDateTV.Text += selectData.GetInt(selectData.GetColumnIndex("ReturnDate")).ToString(); 

                    _container.AddView(addView);
                }
            }
        }
    }
}