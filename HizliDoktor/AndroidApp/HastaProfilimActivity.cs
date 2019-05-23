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
    [Activity(Label = "Profilim", Theme = "@style/AppTheme")]
    public class HastaProfilimActivity : AppCompatActivity
    {
        IHastaService hastaService;

        Hasta hasta = new Hasta();

        private TextView hastaAd, hastaSoyad, hastaTCKN, hastaCinsiyet,  hastaDogumTarihi;

        private Button hastaProfiliGuncelle;

        public HastaProfilimActivity()
        {
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaProfilim_layout);

            // Create your application here

            hastaAd = FindViewById<TextView>(Resource.Id.lblHastaAdi);

            hastaSoyad = FindViewById<TextView>(Resource.Id.lblHastaSoyadi);

            hastaTCKN = FindViewById<TextView>(Resource.Id.lblHastaTCKN);

            hastaCinsiyet = FindViewById<TextView>(Resource.Id.lblHastaCinsiyet);

            hastaDogumTarihi = FindViewById<TextView>(Resource.Id.lblHastaDogumTarihi);

            hastaProfiliGuncelle = FindViewById<Button>(Resource.Id.btnHastaBilgiDuzenle);

            hasta = hastaService.Getir(Intent.GetStringExtra("tc"));

            hastaTCKN.Text += hasta.TC;

            hastaAd.Text += hasta.Ad;

            hastaSoyad.Text += hasta.Soyad;

            hastaCinsiyet.Text += hasta.Cinsiyet;

            hastaDogumTarihi.Text += hasta.DogumTarihi;

            hastaProfiliGuncelle.Click += HastaProfiliGuncelle_Click;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.hastaMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnHastaProfilim:
                    {
                        return true;
                    }
                case Resource.Id.menuBtnHastaRandevuAl:
                    {
                        var intent = new Intent(this, typeof(RandevuAlActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaRandevuListele:
                    {
                        var intent = new Intent(this, typeof(HastaRandevularimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaFavorilerim:
                    {
                        var intent = new Intent(this, typeof(HastaFavoriDoktorlarimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaCikisYap:
                    {
                        var intent = new Intent(this, typeof(GirisYapActivity));
                        StartActivity(intent);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void HastaProfiliGuncelle_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HastaProfilGuncelleActivity));
            intent.PutExtra("tc", Intent.GetStringExtra("tc"));
            StartActivity(intent);
        }
    }
}
