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
        private RadioButton rbVatandas, rbYonetici;
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
            rbVatandas = FindViewById<RadioButton>(Resource.Id.rbVatandas);
            rbYonetici = FindViewById<RadioButton>(Resource.Id.rbYonetici);

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
            bool isLoggedIn = loginService.GirisYap(txtTC.Text, txtPass.Text, rbYonetici.Checked);

            if (isLoggedIn)
            {
                if (txtTC.Text == "1")
                {
                    //admin activity çağır
                    var intent = new Intent(this, typeof(AdminAnaSayfaActivity));
                    StartActivity(intent);
                }
                else if (rbYonetici.Checked)
                {
                    //doktor activity çağır
                    var intent = new Intent(this, typeof(DoktorAnaSayfaActivity));
                    intent.PutExtra("tc", txtTC.Text);
                    StartActivity(intent);
                }
                else
                {
                    //randevu alma activity çağır
                    var intent = new Intent(this, typeof(RandevuActivity));
                    intent.PutExtra("tc", txtTC.Text);
                    StartActivity(intent);
                }
            }
            else
            {
                Toast.MakeText(Application.Context, "Giriş yapılamadı. Lütfen bilgilerinizi kontrol edin.", ToastLength.Long);
            }
        }
    }
}