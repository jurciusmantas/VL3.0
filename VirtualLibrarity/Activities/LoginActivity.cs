using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using System;
using VirtualLibrarity.Activities;
using Android.Support.V7.App;
using Android.Views;



namespace VirtualLibrarity
{
    [Activity(Label = "Login")]
    public class LoginActivity : AppCompatActivity
    {
        RequestSender _requestSender=new RequestSender();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            if (Intent.HasExtra("FromProxy"))
                Toast.MakeText(this, "Could not find user", ToastLength.Long).Show();

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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_login, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent;
            switch (item.ItemId)
            {
                case Resource.Id.menu_back:
                    {
                        intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.menu_register:
                    {
                        intent = new Intent(this, typeof(RegisterActivity));
                        StartActivity(intent);
                        break;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}