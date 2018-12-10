using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using VirtualLibClient;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "LoginProxyActivity")]
    public class LoginProxyActivity : Activity
    {
        private int _id;
        private RequestSender _requestSender;
        private string _image;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login_proxy);
            _image = Intent.GetStringExtra("image");
            _requestSender = new RequestSender();
            Login();

            //cia kodint animacija uzsikrovimo, kadangi login - async
        }
        private async void Login()
        {
            _id = await _requestSender.SendFaceAsync(_image, false);

            // handlinam kai gryzta useris

            if (_id < 0)
                Toast.MakeText(this, "No user found", ToastLength.Long).Show();
            
        }
    }
}