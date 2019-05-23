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
    [Activity(Label = "BolumEkle", Theme = "@style/AppTheme")]
    public class BolumEkleActivity : AppCompatActivity
    {
        private Spinner Hastaneler;
        private Button BolumKaydet;
        private EditText BolumAd;
        IBolumService bolumService;
        IHastaneService hastaneService;
        private List<Hastane> hastaneler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.bolumEkle_layout);
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            Hastaneler = FindViewById<Spinner>(Resource.Id.spinnerHastaneler);
            BolumAd = FindViewById<EditText>(Resource.Id.txtBolumAd);
            BolumKaydet = FindViewById<Button>(Resource.Id.btnBolumKaydet);

            hastaneler = hastaneService.TumHastaneler();
            List<string> hastaneAdlari = new List<string>();

            foreach (var item in hastaneler)
            {
                hastaneAdlari.Add(item.Ad);
            }
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Hastaneler.Adapter = adapter;
            BolumKaydet.Click += BolumKaydet_Click;

        }

        private void BolumKaydet_Click(object sender, EventArgs e)
        {
            Bolum bolum = new Bolum();
            if (BolumAd.Text == "")
            {
                Toast.MakeText(Application.Context, "Lütfen hastane adını boş bırakmayınız.", ToastLength.Long).Show();
                return;
            }
            bolum.Ad = BolumAd.Text;
            int hastaneid = (int) Hastaneler.SelectedItemId;
            bolum.HastaneId = hastaneid + 1;
            bolumService.Ekle(bolum);
            var intent = new Intent(this, typeof(AdminAnaSayfaActivity));
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
                        var intent = new Intent(this, typeof(AdminAnaSayfaActivity));
                        StartActivity(intent);
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

            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
