
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "LoginProxyActivity")]
    public class LoginProxyActivity : Activity
    {
        private int _id;
        private RequestSender _requestSender = new RequestSender();
        private bool _isAuto;
        private string _email;
        private string _password;
        private UserToLoginResponse2 user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login_proxy);
            SendRequest();

        }
        private void SendRequest()
        {
            _isAuto = Intent.GetBooleanExtra("isAuto", false);
            if (_isAuto)
            {
                string image = Intent.GetStringExtra("image");
                user = LoginAuto(image);
            }
            else
            {
                _email = Intent.GetStringExtra("email");
                _password = Intent.GetStringExtra("password");
                user = LoginManual(_email, _password);
            }
            TryUserIfNull();
        }
        private UserToLoginResponse2 LoginAuto(string image)
        {
            return _requestSender.SendLoginRequest(image);
        }

        private UserToLoginResponse2 LoginManual(string email, string password)
        {
            return _requestSender.SendLoginRequest(email, password);
        }
        private void TryUserIfNull()
        {
            if (user.UserInfo == null)
            {
                Toast.MakeText(this, "Did not recieve response", ToastLength.Long).Show();
                GoBack();
            }
            else
            {
                SendUser();
            }
        }
        private void SendUser()
        {
            if (user.Exception != null)
            {
                Toast.MakeText(this, user.Exception, ToastLength.Long).Show();
                GoBack();
            }
            else
            {
                Intent intent = new Intent(this, typeof(UserInfoActivity));
                string userString = JsonConvert.SerializeObject(user);
                intent.PutExtra("user", userString);
                StartActivity(intent);
            }
        }
        private void GoBack()
        {

        }
    }
}