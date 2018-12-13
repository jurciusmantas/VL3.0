
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;


namespace VirtualLibrarity.Activities
{
    [Activity(Label = "LoggedInActivity")]
    public class LoggedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_logged);

            ImageButton logOutButton = FindViewById<ImageButton>(Resource.Id.logOutButton);
            Button borrowedBooksButton = FindViewById<Button>(Resource.Id.borrowedBooksButton);
            Button takeOrReturnBookQRButton = FindViewById<Button>(Resource.Id.QRButton);
            Button availableBooksButton = FindViewById<Button>(Resource.Id.AvailableBooksButton);
            TextView userNameTV = FindViewById<TextView>(Resource.Id.infoUserNameTV);


            logOutButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            borrowedBooksButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(UserInfoActivity));
                StartActivity(intent);
            };


            availableBooksButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(AvailableBooksActivity));
                StartActivity(intent);
            };
        }

    }
}