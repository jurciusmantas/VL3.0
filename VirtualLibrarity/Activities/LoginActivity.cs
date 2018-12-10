using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using System;
using VirtualLibrarity.Activities;
using VirtualLibrarity.Database;

namespace VirtualLibrarity
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        DbHelper _dbHelper;
        SQLiteDatabase _database;
        RequestSender _requestSender=new RequestSender();
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            Button loginManualyButton = FindViewById<Button>(Resource.Id.ManualLoginButton);

                _dbHelper = new DbHelper(this);
                _database = _dbHelper.WritableDatabase;

            loginManualyButton.Click += delegate
            {
                EditText email = FindViewById<EditText>(Resource.Id.EmailET);
                EditText password = FindViewById<EditText>(Resource.Id.PasswordET);
               

                ICursor selectData = _database.RawQuery("select * from User where Email = '"
                    + email.Text + "' AND Password = '" + password.Text + "'", new string[] { });

                if (selectData.Count > 0)
                {
                    selectData.MoveToFirst();
                    Intent intent = new Intent(this, typeof(LoggedInActivity));
                    intent.PutExtra("userID", selectData.GetInt(selectData.GetColumnIndex("Id")).ToString());
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "No user found", ToastLength.Long).Show();
                }
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
            intent.PutExtra("image", image64string);
            StartActivity(intent);

        }
    }
}