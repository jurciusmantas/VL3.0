using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using System;
using VirtualLibClient;
using VirtualLibrarity.Activities;
using VirtualLibrarity.Database;

namespace VirtualLibrarity
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        RequestSender _requestSender = new RequestSender();
        private bool _isPhotoTaken;
        private string _image;
        private User _user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            _isPhotoTaken = false;

            Button button = FindViewById<Button>(Resource.Id.TakeAPhotoButton);
            button.Click += delegate
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
                _isPhotoTaken = true;
            };



            Button registerButton = FindViewById<Button>(Resource.Id.FinishRegisteringButton);
            registerButton.Click += delegate
            {
                TextView FirstNameET = FindViewById<TextView>(Resource.Id.FirstNameET);
                TextView LastNameET = FindViewById<TextView>(Resource.Id.LastNameET);
                TextView EmailET = FindViewById<TextView>(Resource.Id.EmailET);
                TextView PasswordET = FindViewById<TextView>(Resource.Id.PasswordET);
                if (!_isPhotoTaken)
                    return;

                if (FirstNameET.Text != "" && LastNameET.Text != "" &&
                    EmailET.Text != "" && PasswordET.Text != "")
                {
                    _user = new User
                    {
                        FirstName = FirstNameET.Text,
                        LastName = LastNameET.Text,
                        Email = EmailET.Text,
                        Password = PasswordET.Text
                    };

                    DatabaseService.Register(_user, _image);
                   

                    Toast.MakeText(this, "User succesfully registered", ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                else
                    Toast.MakeText(this, "Please fill all spaces", ToastLength.Long).Show();

            };
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            byte[] bitmapData = _requestSender.ConvertToByteArray(bitmap);
            _image = Convert.ToBase64String(bitmapData);
        }
    }
}