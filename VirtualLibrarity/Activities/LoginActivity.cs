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
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        RequestSender _requestSender=new RequestSender();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            Button loginManualyButton = FindViewById<Button>(Resource.Id.ManualLoginButton);

            loginManualyButton.Click += delegate
            {
                EditText email = FindViewById<EditText>(Resource.Id.EmailET);
                EditText password = FindViewById<EditText>(Resource.Id.PasswordET);
                Intent intent = new Intent(this, typeof(LoginProxyActivity));
                intent.PutExtra("email", email.Text);
                intent.PutExtra("password", password.Text);
                StartActivity(intent);
                
            };
            Button loginAutoButton = FindViewById<Button>(Resource.Id.FaceRecognitionButton);
            loginAutoButton.Click += delegate
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);

            };
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            byte[] bitmapData = _requestSender.ConvertToByteArray(bitmap);
            string image64string = Convert.ToBase64String(bitmapData);
            Intent intent = new Intent(this, typeof(LoginProxyActivity));
            intent.PutExtra("isAuto", true);
            intent.PutExtra("image", image64string);
            StartActivity(intent);
        }
    }
}