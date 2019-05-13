using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp
{
    [Activity(Label = "Mail Onayı", Theme = "@style/AppTheme")]
    public class MailOnayActivity : AppCompatActivity
    {
        private Button btnDogrula;
        private EditText txtKod;
        private ImageButton btnImg;
        ILoginService loginService;
        IHastaService hastaService;
        private string dogrulamaKodu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mailOnay_layout);

            loginService = Business.IOCUtil.Container.Resolve<ILoginService>();
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();

            btnDogrula = FindViewById<Button>(Resource.Id.btnDogrula);
            txtKod = FindViewById<EditText>(Resource.Id.txtKod);
            btnImg = FindViewById<ImageButton>(Resource.Id.btnImg);

            btnDogrula.Click += BtnDogrula_Click;
            btnImg.Click += BtnImg_Click;

            dogrulamaKodu = loginService.DogrulamaMailiGonder(Intent.GetStringExtra("mail"));
        }

        private void BtnImg_Click(object sender, EventArgs e)
        {
            txtKod.Text = "";
            Toast.MakeText(Application.Context, "Yeni kod mail adresinize gönderildi.", ToastLength.Long).Show();
            dogrulamaKodu = loginService.DogrulamaMailiGonder(Intent.GetStringExtra("mail"));  
        }

        private void BtnDogrula_Click(object sender, EventArgs e)
        {
            string dogrulananTc = loginService.MailDogrula(Intent.GetStringExtra("mail"), dogrulamaKodu, txtKod.Text);

            if(!string.IsNullOrEmpty(dogrulananTc))
            {
                var intent = new Intent(this, typeof(ProfileActivity));
                intent.PutExtra("tc", dogrulananTc);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(Application.Context, "Girilen kod yanlış. Lütfen kodunuzu kontrol edin.", ToastLength.Long).Show();
            }
        }
    }
}