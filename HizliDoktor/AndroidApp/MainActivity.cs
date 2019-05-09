using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Autofac;
using Business.Abstract;
using Android.Content;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnLogin, btnUyeOl;
        private EditText txtTC, txtPass;
        ILoginService loginService;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            loginService = Business.IOCUtil.Container.Resolve<ILoginService>();

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnUyeOl = FindViewById<Button>(Resource.Id.btnUyeOl);
            txtTC = FindViewById<EditText>(Resource.Id.txtTC);
            txtPass = FindViewById<EditText>(Resource.Id.txtPass);

            btnLogin.Click += BtnLogin_Click;
            btnUyeOl.Click += BtnUyeOl_Click;
        }

        private void BtnUyeOl_Click(object sender, System.EventArgs e)
        {
            //üye ol activity çağır
            var intent = new Intent(this, typeof(RandevuActivity));
            StartActivity(intent);
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = loginService.Login("tc", "pass");

            if (isLoggedIn)
            {
                //randevu alma activity çağır
                var intent = new Intent(this, typeof(RandevuActivity));
                intent.PutExtra("tc", txtTC.Text);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(Application.Context, "Giriş yapılamadı. Lütfen bilgilerinizi kontrol edin.", ToastLength.Long);
            }
        }
    }
}