using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Autofac;//servisi kullanabilmek için
using Business.Abstract;//servisi kullanabilmek için
using Android.Content;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnLogin, btnUyeOl;
        private EditText txtTC, txtPass;
        ILoginService loginService; //login servisim

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //bu satırı servisi kullanabilmek için yazıyoruz.
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
            bool isLoggedIn = loginService.GirisYap(txtTC.Text, txtPass.Text);

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