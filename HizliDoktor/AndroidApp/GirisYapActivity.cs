using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Autofac;//servisi kullanabilmek için
using Business.Abstract;//servisi kullanabilmek için
using Android.Content;
using Android.Views.InputMethods;
using Entities.Concrete;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class GirisYapActivity : AppCompatActivity
    {
        private Button btnLogin, btnUyeOl;
        private EditText txtTC, txtPass;
        private RadioButton rbVatandas, rbYonetici;
        ILoginService loginService; //login servisim
        IHastaService hastaService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //bu satırı servisi kullanabilmek için yazıyoruz.
            loginService = Business.IOCUtil.Container.Resolve<ILoginService>();
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();

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
            var intent = new Intent(this, typeof(UyeOlActivity));
            StartActivity(intent);
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            KlavyeGizle();

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
                    var intent = new Intent(this, typeof(DoktorProfilimActivity));
                    intent.PutExtra("tc", txtTC.Text);
                    StartActivity(intent);
                }
                else
                {
                    Hasta hasta = hastaService.Getir(txtTC.Text);

                    if (hasta.IsMailVerified)
                    {
                        //randevu alma activity çağır
                        var intent = new Intent(this, typeof(RandevuAlActivity));
                        intent.PutExtra("tc", txtTC.Text);
                        StartActivity(intent);
                    }
                    else
                    {
                        //mail dogrulama activity çağır
                        var intent = new Intent(this, typeof(MailOnayActivity));
                        intent.PutExtra("mail", hasta.Mail);
                        StartActivity(intent);
                    }
                }
            }
            else
            {
                Toast.MakeText(Application.Context, "Giriş yapılamadı. Lütfen bilgilerinizi kontrol edin.", ToastLength.Long).Show();
            }
        }

        private void KlavyeGizle()
        {
            try
            {
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
            }
            catch (System.Exception) { }

        }
    }
}