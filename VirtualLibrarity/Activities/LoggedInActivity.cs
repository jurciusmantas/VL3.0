using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using VirtualLibrarity.Models;

namespace VirtualLibrarity
{
    [Activity(Label = "ManualLoginActivity")]
    public class LoggedInActivity : Activity
    {
        UserToLoginResponse2 user;
        LinearLayout _container;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_logged_in);

            string userString = Intent.GetStringExtra("user");
            user = JsonConvert.DeserializeObject<UserToLoginResponse2>(userString);

            TextView NameTV = FindViewById<TextView>(Resource.Id.infoUserNameTV);
            NameTV.Text += user.UserInfo.Firstname;
            TextView SurnameTV = FindViewById<TextView>(Resource.Id.infoUserSurnameTV);
            SurnameTV.Text += user.UserInfo.Lastname;
            TextView EmailTV = FindViewById<TextView>(Resource.Id.infoUserEmailTV);
            EmailTV.Text += user.UserInfo.Email;

            AddBooksToReturnList();
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
                    AuthorTV.Text += bookModel.Author;
                    TitleTV.Text += bookModel.Title;
                    _container.AddView(addView);
                }
            }
        }
    }
}