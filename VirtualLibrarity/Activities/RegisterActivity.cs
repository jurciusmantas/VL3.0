using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using VirtualLibClient;
using VirtualLibrarity.Activities;

namespace VirtualLibrarity
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        RequestSender _requestSender = new RequestSender();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);

            Button button = FindViewById<Button>(Resource.Id.TakeAPhotoButton);
            button.Click += delegate
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            };



            Button registerButton = FindViewById<Button>(Resource.Id.FinishRegisteringButton);
            registerButton.Click += delegate
            {
                TextView NameET = FindViewById<TextView>(Resource.Id.NameET);
                TextView SurnameET = FindViewById<TextView>(Resource.Id.SurameET);
                TextView EmailET = FindViewById<TextView>(Resource.Id.EmailET);
                TextView PasswordET = FindViewById<TextView>(Resource.Id.PasswordET);

                if (NameET.Text != "" && SurnameET.Text != "" &&
                    EmailET.Text != "" && PasswordET.Text != ""
                    /* && isPhotoTaken */)
                {

                    ContentValues content = new ContentValues();
                    content.Put("Name", NameET.Text);
                    content.Put("Surname", SurnameET.Text);
                    content.Put("Email", EmailET.Text);
                    content.Put("Password", PasswordET.Text);
                    content.Put("Id", 10);
                    content.Put("Face", 100);

                    // _database.Insert("user", null, content);
                   

                    Toast.MakeText(this, "User succesfully added", ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                else
                    Toast.MakeText(this, "Please fill all spaces", ToastLength.Long).Show();

            };
        }
    }
}