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
    [Activity(Label = "Bolum Listele", Theme = "@style/AppTheme")]
    public class BolumGuncelleActivity : AppCompatActivity
    {
        private Button btnBolumGuncelle;
        private EditText txtGuncelleBolumAd;
        IBolumService bolumService;
        Bolum bolum;
        int hastaneid,id;
        
        public BolumGuncelleActivity()
        {
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.bolumGuncelle_layout);

            btnBolumGuncelle = FindViewById<Button>(Resource.Id.btnGuncelleBolumKaydet);
            txtGuncelleBolumAd = FindViewById<EditText>(Resource.Id.txtGuncelleBolumAd);
            bolum = bolumService.Getir(Intent.GetIntExtra("guncelleBolumId", 0));
            hastaneid = bolum.HastaneId;
            id = bolum.Id;
            txtGuncelleBolumAd.Text = bolum.Ad;
            btnBolumGuncelle.Click += BtnBolumGuncelle_Click;
        }

        private void BtnBolumGuncelle_Click(object sender, EventArgs e)
        {
            
            if (txtGuncelleBolumAd.Text == "")
            {
                Toast.MakeText(Application.Context, "Lütfen hastane adını boş bırakmayınız.", ToastLength.Long).Show();
                return;
            }

            bolum.Ad = txtGuncelleBolumAd.Text;
            bolum.Id = id;
            bolum.HastaneId = hastaneid;
            bolumService.Guncelle(bolum);
            var intent = new Intent(this, typeof(BolumGuncelleOnayActivity));
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.adminMenu, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnAnasayfa:
                    {
                        return true;
                    }
                case Resource.Id.menuBtnHastaneEkle:
                    {
                        var intent = new Intent(this, typeof(HastaneEkleActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnBolumEkle:
                    {
                        var intent = new Intent(this, typeof(BolumEkleActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorEkle:
                    {
                        var intent = new Intent(this, typeof(DoktorEkleActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnAdminRandevuKapat:
                    {
                        var intent = new Intent(this, typeof(AdminRandevuKapatActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnAdminCikisYap:
                    {
                        var intent = new Intent(this, typeof(GirisYapActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaneListele:
                    {
                        var intent = new Intent(this, typeof(HastaneListeleActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorListele:
                    {
                        var intent = new Intent(this, typeof(DoktorListeleActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnBolumListele:
                    {
                        var intent = new Intent(this, typeof(BolumListeleActivity));
                        StartActivity(intent);
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}