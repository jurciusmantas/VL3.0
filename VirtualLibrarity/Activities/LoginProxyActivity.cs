using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace VirtualLibrarity.Activities
{
    [Activity(Label = "LoginProxyActivity")]
    public class LoginProxyActivity : Activity
    {
        private RequestSender _requestSender = new RequestSender();
        private bool _isAuto;
        private bool _toTop;
        private bool _isSendingRequest;
        private string _email;
        private string _password;
        private Thread _thread, _threadAnim;
        private TextView _animTV;
        private UserToLoginResponse2 _userResponse;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login_proxy);

            _toTop = false;
            _isSendingRequest = true;
            _animTV = FindViewById<TextView>(Resource.Id.animTV);

            _thread = new Thread(delegate ()
            {
                SendRequest();
                _isSendingRequest = false;
                if (_userResponse == null)
                    return;
                string userinfo = JsonConvert.SerializeObject(_userResponse);
                Intent intent = new Intent(this, typeof(UserInfoActivity));
                intent.PutExtra("user", userinfo);
                _threadAnim.Abort();
                StartActivity(intent);

            });
            _threadAnim = new Thread(delegate ()
            {
                StartAnimation();
            });
            _threadAnim.Start();
            _thread.Start();
        }

        public void StartAnimation()
        {
            while (_isSendingRequest)
            {
                if (_animTV.Text.Length == 3)
                    RunOnUiThread( () =>
                    _animTV.Text = ".");
                else
                    RunOnUiThread( () =>
                    _animTV.Text += ".");

                Thread.Sleep(300);
            }          
        }

        private void SendRequest()
        {
            _isAuto = Intent.GetBooleanExtra("isAuto", false);
            if (_isAuto)
            {
                string image = Intent.GetStringExtra("image");
                _userResponse = LoginAuto(image);
            }
            else
            {
                _email = Intent.GetStringExtra("email");
                _password = Intent.GetStringExtra("password");
                _userResponse = LoginManual(_email, _password);
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
            try
            {
                if (_userResponse == null)
                {
                    RunOnUiThread(() =>
                   Toast.MakeText(this, "Did not get response", ToastLength.Long).Show());
                    GoBack();
                }

                if (_userResponse.Exception != null)
                {
                    RunOnUiThread(() =>
                   Toast.MakeText(this, _userResponse.Exception, ToastLength.Long).Show());
                    GoBack();
                }
            }
            catch (Exception exc)
            {
                RunOnUiThread(() =>
               Toast.MakeText(this, exc.Message, ToastLength.Long).Show());
                GoBack();
            }
        }

        private void GoBack()
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            intent.PutExtra("FromProxy", true);
            StartActivity(intent);
        }
    }
}