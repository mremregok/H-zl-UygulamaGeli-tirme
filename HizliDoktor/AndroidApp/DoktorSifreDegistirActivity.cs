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
    [Activity(Label = "lblDoktorSifreDegistir", Theme = "@style/AppTheme")]
    public class DoktorSifreDegistirActivity : AppCompatActivity
    {
        private EditText txtDoktorEskiSifre,txtDoktorYeniSifre,txtDoktorSifreOnay;
        private Button btnDoktorSifreGuncelle;
        private IDoktorService doktorService;

        public DoktorSifreDegistirActivity()
        {
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktorSifreDegistir_layout);

            txtDoktorEskiSifre = FindViewById<EditText>(Resource.Id.txtDoktorEskiSifre);
            txtDoktorYeniSifre = FindViewById<EditText>(Resource.Id.txtDoktorYeniSifre);
            txtDoktorSifreOnay = FindViewById<EditText>(Resource.Id.txtDoktorSifreOnay);
            btnDoktorSifreGuncelle = FindViewById<Button>(Resource.Id.btnDoktorSifreGuncelle);

            btnDoktorSifreGuncelle.Click += BtnDoktorSifreGuncelle_Click;
            // Create your application here
        }

        private void BtnDoktorSifreGuncelle_Click(object sender, EventArgs e)
        {
            if (txtDoktorYeniSifre.Text == txtDoktorSifreOnay.Text)
            {
                Doktor doktor = doktorService.Getir(Intent.GetStringExtra("tc"));
                if (txtDoktorEskiSifre.Text == doktor.Sifre)
                {
                    doktor.Sifre = txtDoktorYeniSifre.Text;

                    doktorService.Guncelle(doktor);

                    var intent = new Intent(this, typeof(DoktorProfilimActivity));
                    intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(Application.Context, "Eski şifre doğru değil.", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(Application.Context, "Bu şifre yeni şifreyle aynı değil", ToastLength.Long).Show();
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.doktorMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnDoktorProfilim:
                    {
                        var intent = new Intent(this, typeof(DoktorProfilimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorRandevularim:
                    {
                        var intent = new Intent(this, typeof(DoktorRandevularimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorSifreDegistir:
                    {
                        return true;
                    }
                case Resource.Id.menuBtnDoktorCikisYap:
                    {
                        var intent = new Intent(this, typeof(GirisYapActivity));
                        StartActivity(intent);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }
    }


}