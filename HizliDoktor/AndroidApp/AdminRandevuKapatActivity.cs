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
using AndroidApp.Resources.Adapter;
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp
{
    [Activity(Label = "Randevu Kapat", Theme = "@style/AppTheme")]
    public class AdminRandevuKapatActivity : AppCompatActivity
    {
        IRandevuService randevuService;
        IHastaneService hastaneService;
        IBolumService bolumService;
        IDoktorService doktorService;

        private GridView gridTarihler;
        private Spinner spinnerIller, spinnerIlceler, spinnerHastaneler, spinnerBolumler, spinnerDoktorlar;
        private Button btnOncekiGun, btnSonrakiGun, btnRandevuKaydet;
        private TextView lblSeciliTarih;
        private List<Hastane> hastaneler;
        private List<Bolum> bolumler;
        private List<Doktor> doktorlar;
        private DateTime seciliTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 8, 0, 0);
        private RandevuAlGridViewAdapter timeAdapter;
        private GridView timeGrid;

        public AdminRandevuKapatActivity()
        {
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.adminRandevuKapat_layout);

            gridTarihler = FindViewById<GridView>(Resource.Id.gridTarihlerAdmin);

            spinnerIller = FindViewById<Spinner>(Resource.Id.spinnerIllerAdmin);
            spinnerIlceler = FindViewById<Spinner>(Resource.Id.spinnerIlcelerAdmin);
            spinnerHastaneler = FindViewById<Spinner>(Resource.Id.spinnerHastanelerAdmin);
            spinnerBolumler = FindViewById<Spinner>(Resource.Id.spinnerBolumlerAdmin);
            spinnerDoktorlar = FindViewById<Spinner>(Resource.Id.spinnerDoktorlarAdmin);

            btnOncekiGun = FindViewById<Button>(Resource.Id.btnOncekiGunAdmin);
            btnSonrakiGun = FindViewById<Button>(Resource.Id.btnSonrakiGunAdmin);
            btnRandevuKaydet = FindViewById<Button>(Resource.Id.btnRandevuKapatAdmin);

            timeGrid = FindViewById<GridView>(Resource.Id.gridTarihlerAdmin);

            lblSeciliTarih = FindViewById<TextView>(Resource.Id.lblSeciliTarihAdmin);
            lblSeciliTarih.Text = seciliTarih.ToShortDateString();

            spinnerIller.ItemSelected += SpinnerIller_ItemSelected;
            spinnerIlceler.ItemSelected += SpinnerIlceler_ItemSelected;
            spinnerHastaneler.ItemSelected += SpinnerHastaneler_ItemSelected;
            spinnerBolumler.ItemSelected += SpinnerBolumler_ItemSelected;
            spinnerDoktorlar.ItemSelected += SpinnerDoktorlar_ItemSelected;

            btnOncekiGun.Click += BtnOncekiGun_Click;
            btnSonrakiGun.Click += BtnSonrakiGun_Click;
            btnRandevuKaydet.Click += BtnRandevuKaydet_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            List<string> hastaneOlanIller = hastaneService.HastaneOlanIller();

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneOlanIller);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerIller.Adapter = adapter;
        }

        private void BtnRandevuKaydet_Click(object sender, EventArgs e)
        {
            if (spinnerDoktorlar.SelectedItem != null)
            {
                DateTime time = timeAdapter.GetSelectedTime();
                if (time.Hour == 0) return;

                DateTime randevuTarihi = new DateTime(seciliTarih.Year, seciliTarih.Month, seciliTarih.Day, time.Hour, time.Minute, time.Second);

                Randevu randevu = new Randevu();

                randevu.BolumId = bolumler[spinnerBolumler.SelectedItemPosition].Id;
                randevu.HastaneId = hastaneler[spinnerHastaneler.SelectedItemPosition].Id;
                randevu.DoktorId = doktorlar[spinnerDoktorlar.SelectedItemPosition].Id;
                randevu.Tarih = randevuTarihi;
                randevu.HastaId = 0;

                randevuService.Ekle(randevu);

                var intent = new Intent(this, typeof(AdminRandevuKapandiActivity));
                StartActivity(intent);
            }
        }

        private void BtnSonrakiGun_Click(object sender, EventArgs e)
        {
            seciliTarih = seciliTarih.AddDays(1);
            btnOncekiGun.Enabled = true;
            musaitTarihleriYenile(spinnerDoktorlar.SelectedItemPosition);
        }

        private void BtnOncekiGun_Click(object sender, EventArgs e)
        {
            if (seciliTarih.AddDays(-1) <= DateTime.Now.AddDays(1)) btnOncekiGun.Enabled = false;

            seciliTarih = seciliTarih.AddDays(-1);
            musaitTarihleriYenile(spinnerDoktorlar.SelectedItemPosition);
        }

        private void SpinnerIller_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<string> hastaneOlanIlceler = hastaneService.HastaneOlanIlceler((string)spinnerIller.SelectedItem);

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneOlanIlceler);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerIlceler.Adapter = adapter;
            spinnerHastaneler.Adapter = null;
            spinnerBolumler.Adapter = null;
            spinnerDoktorlar.Adapter = null;
            timeGrid.Adapter = null;
        }

        private void SpinnerIlceler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            hastaneler = hastaneService.Hastaneler((string)spinnerIller.SelectedItem, (string)spinnerIlceler.SelectedItem);

            List<string> hastaneAdlari = new List<string>();

            foreach (var item in hastaneler)
            {
                hastaneAdlari.Add(item.Ad);
            }

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerHastaneler.Adapter = adapter;
            spinnerBolumler.Adapter = null;
            spinnerDoktorlar.Adapter = null;
            timeGrid.Adapter = null;
        }

        private void SpinnerHastaneler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Hastane hastane = hastaneler[e.Position];

            bolumler = bolumService.Bolumler(hastane.Id);

            List<string> bolumAdlari = new List<string>();

            foreach (var item in bolumler)
            {
                bolumAdlari.Add(item.Ad);
            }

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, bolumAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerBolumler.Adapter = adapter;
            spinnerDoktorlar.Adapter = null;
            timeGrid.Adapter = null;
        }

        private void SpinnerBolumler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Bolum bolum = bolumler[e.Position];

            doktorlar = doktorService.Doktorlar(bolum.Id);

            List<string> doktorAdlari = new List<string>();

            foreach (var item in doktorlar)
            {
                doktorAdlari.Add(item.Unvan + " " + item.Ad + " " + item.Soyad);
            }

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, doktorAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerDoktorlar.Adapter = adapter;
            timeGrid.Adapter = null;
        }

        private void SpinnerDoktorlar_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (seciliTarih.AddDays(-1) >= DateTime.Now.AddDays(1)) btnOncekiGun.Enabled = true;
            musaitTarihleriYenile(e.Position);
        }

        private void musaitTarihleriYenile(int doktorPosition)
        {
            Doktor doktor = doktorlar[doktorPosition];

            List<DateTime> dateTimes = randevuService.MusaitTarihleriGetir(doktor.Id, seciliTarih);
            timeAdapter = new RandevuAlGridViewAdapter(dateTimes, this);
            gridTarihler.Adapter = timeAdapter;

            lblSeciliTarih.Text = seciliTarih.ToShortDateString();
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
                        return true;
                    }
                case Resource.Id.menuBtnAdminCikisYap:
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