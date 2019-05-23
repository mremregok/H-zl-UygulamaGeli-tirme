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
    [Activity(Label = "HastaProfilGuncelle", Theme = "@style/AppTheme")]
    public class HastaProfilGuncelleActivity : Activity
    {
        IHastaService hastaService;
        Hasta hasta = new Hasta();
        private Button btnHastaBilgiGuncelle;
        private EditText txtHastaYeniAd, txtHastaYeniSoyad, txtHastaEskiSifre, txtHastaYeniSifre;

        public HastaProfilGuncelleActivity()
        {
            hastaService = Business.IOCUtil.Container.Resolve <IHastaService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaProfilGuncelle_layout);

            // Create your application here

            txtHastaYeniAd = FindViewById<EditText>(Resource.Id.txtYeniHastaAdi);

            txtHastaYeniSoyad = FindViewById<EditText>(Resource.Id.txtYeniHastaSoyadi);

            txtHastaEskiSifre = FindViewById<EditText>(Resource.Id.txtEskiHastaŞifresi);

            txtHastaYeniSifre = FindViewById<EditText>(Resource.Id.txtYeniHastaŞifresi);

            btnHastaBilgiGuncelle = FindViewById<Button>(Resource.Id.btnHastaGuncelle);

            btnHastaBilgiGuncelle.Click += BtnHastaBilgiGuncelle_Click;

            hasta = hastaService.Getir(Intent.GetStringExtra("tc"));

            txtHastaYeniAd.Text = hasta.Ad;
            txtHastaYeniSoyad.Text = hasta.Soyad;
        }

        private void BtnHastaBilgiGuncelle_Click(object sender, EventArgs e)
        {

            if (txtHastaYeniAd.Text == null || txtHastaYeniSoyad.Text == null || txtHastaEskiSifre.Text == null || txtHastaYeniSifre.Text == null)
            {
                Toast.MakeText(Application.Context, "Lütfen bilgilerin tamamını doldurduğunuzdan emin olun.", ToastLength.Long).Show();
                return;
            }
            else
            {
                if (txtHastaEskiSifre.Text == hasta.Sifre)
                {
                    hasta.Ad = txtHastaYeniAd.Text;
                    hasta.Soyad = txtHastaYeniSoyad.Text;
                    hasta.Sifre = txtHastaYeniSifre.Text;

                    hastaService.Guncelle(hasta);

                    var intent = new Intent(this, typeof(HastaProfilimActivity));
                    intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(Application.Context, "Eski şifre doğru değil.", ToastLength.Long).Show();
                }
            }
        }        
    }
}