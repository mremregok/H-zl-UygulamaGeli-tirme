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
    [Activity(Label = "HastaneEkle", Theme = "@style/AppTheme")]
    public class HastaneEkleActivity : AppCompatActivity
    {
        string[] iller= new string[] { "Manisa", "İstanbul", "İzmir", "Ankara","Denizli"};
        string[] Manisailçeler = new string[] { "Turgutlu", "Merkez", "Salihli","Soma","Akhisar" };
        string[] İzmirilçeler = new string[] { "Karşıyaka", "Bornova", "Göztepe","Buca","Tepecik" };
        string[] İstanbulilçeler = new string[] { "Üsküdar", "Kadıköy", "Bağcılar","Kartal","Beşiktaş"};
        string[] Ankarailçeler = new string[] { "Sincan", "Çankaya", "Altındağ" , "Gölbaşı","Mamak"};
        string[] Denizliilçeler = new string[] { "Pamukkale", "Bozkurt", "Çardak","Serinhisar","Tavas" };
        private Spinner spinnerIller, spinnerIlceler;
        private EditText hastaneAd;
        private Button Kaydet;
        IHastaneService hastaneService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaneEkle_layout);

            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();

            spinnerIller = FindViewById<Spinner>(Resource.Id.spinnerIller);
            spinnerIlceler = FindViewById<Spinner>(Resource.Id.spinnerIlceler);
            hastaneAd = FindViewById<EditText>(Resource.Id.txtHastaneAd);
            Kaydet = FindViewById<Button>(Resource.Id.btnHastaneKaydet);
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, iller);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerIller.Adapter = adapter;
            spinnerIller.ItemSelected += SpinnerIller_ItemSelected;
            Kaydet.Click += Kaydet_Click;
            // Create your application here
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            Hastane hastane = new Hastane();
            if(hastaneAd.Text == "")
            {
                Toast.MakeText(Application.Context, "Lütfen hastane adını boş bırakmayınız.", ToastLength.Long).Show();
                return;
            }
            hastane.Ad = hastaneAd.Text;
            hastane.Il = spinnerIller.SelectedItem.ToString();
            hastane.Ilce = spinnerIlceler.SelectedItem.ToString();
            hastaneService.Ekle(hastane);

            var intent = new Intent(this, typeof(DoktorEkleOnayActivity));
            StartActivity(intent);
        }

        private void SpinnerIller_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(spinnerIller.SelectedItemPosition==0)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Manisailçeler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerIlceler.Adapter = adapter;
            }
            if (spinnerIller.SelectedItemPosition == 1)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, İstanbulilçeler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerIlceler.Adapter = adapter;
            }
            if (spinnerIller.SelectedItemPosition == 2)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem,İzmirilçeler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerIlceler.Adapter = adapter;
            }
            if (spinnerIller.SelectedItemPosition == 3)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Ankarailçeler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerIlceler.Adapter = adapter;
            }
            if (spinnerIller.SelectedItemPosition == 4)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Denizliilçeler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerIlceler.Adapter = adapter;
            }
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