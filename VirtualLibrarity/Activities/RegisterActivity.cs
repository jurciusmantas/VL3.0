using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using System;
using VirtualLibrarity.Activities;

namespace VirtualLibrarity
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private bool isPhotoTaken;
        private string image64String;
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
                    && isPhotoTaken)
                {
                    User user = new User();
                    user.Firstname = NameET.Text;
                    user.Lastname = SurnameET.Text;
                    user.Email = EmailET.Text;
                    user.Password = PasswordET.Text;
                    
                    int ok=_requestSender.SendRegisterRequest(image64String,user);
                    if (ok >= 0)
                        Toast.MakeText(this, "User succesfully added", ToastLength.Long).Show();
                    else
                        Toast.MakeText(this, "User not added. Something has happened. Please, try later.",ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                else
                    Toast.MakeText(this, "Please fill all spaces or take a photo", ToastLength.Long).Show();

            };
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            isPhotoTaken = true;
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            byte[] bitmapData =_requestSender.ConvertToByteArray(bitmap);
            image64String = Convert.ToBase64String(bitmapData);
            //await _requestSender.SendFaceAsync(image64String, true);
        }
        
    }
}