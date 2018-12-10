using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VirtualLibrarity.Database;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "LoginProxyActivity")]
    public class LoginProxyActivity : Activity
    {
        private int _id;
        private RequestSender _requestSender=new RequestSender();
        private string _image;
        private SQLiteDatabase _database;
        private DbHelper _dbHelper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login_proxy);
            _image = Intent.GetStringExtra("image");
            _dbHelper = new DbHelper(this);
            _database = _dbHelper.WritableDatabase;
            Login();
        }
        private async void Login()
        {
            _id = await _requestSender.SendFaceAsync(_image, false); ///

            if (_id != 0)
            {
                _id += 2;
                ICursor selectData = _database.RawQuery("select * from User where Id = '"
                    + _id + "'", new string[] { });

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
            }
            else
            {
                Toast.MakeText(this, "No user found", ToastLength.Long).Show();
            }
        }
    }
}