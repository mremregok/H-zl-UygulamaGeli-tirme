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
using static AndroidApp.UyeOlActivity;

namespace AndroidApp
{
    [Activity(Label = "Doktor Listele", Theme = "@style/AppTheme")]
    public class DoktorGuncelleActivity : AppCompatActivity
    {
        private Spinner hastaneler, bolumler;
        private Button  btnDoktorGuncelle;
        private EditText  txtAd, txtSoyad, txtUnvan;
        IBolumService bolumService;
        IHastaneService hastaneService;
        IDoktorService doktorService;
        private List<Hastane> Hastaneler;
        private List<Bolum> Bolumler;
        Doktor doktor;
        string Sifre, TC;
        int Cinsiyet;
        string dogumTarihi;
        int ID;
        public DoktorGuncelleActivity()
        {
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktorGuncelle_layout);
            btnDoktorGuncelle = FindViewById<Button>(Resource.Id.btnDoktorGuncelle);
            hastaneler = FindViewById<Spinner>(Resource.Id.spinnerGuncelleHastaneler);
            bolumler = FindViewById<Spinner>(Resource.Id.spinnerGuncelleBolumler);
            txtUnvan = FindViewById<EditText>(Resource.Id.txtGuncelleUnvan);
            txtAd = FindViewById<EditText>(Resource.Id.txtGuncelleAd);
            txtSoyad = FindViewById<EditText>(Resource.Id.txtGuncelleSoyad);
            Hastaneler = hastaneService.TumHastaneler();

            List<string> hastaneAdlari = new List<string>();

            foreach (var item in Hastaneler)
            {
                hastaneAdlari.Add(item.Ad);
            }
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            hastaneler.Adapter = adapter;
           

            doktor = doktorService.Getir(Intent.GetIntExtra("guncelleDoktorId", 0));
            TC = doktor.TC;
            Sifre = doktor.Sifre;
            Cinsiyet = doktor.Cinsiyet;
            dogumTarihi = doktor.DogumTarihi.ToString();
            ID = doktor.Id;
            txtAd.Text = doktor.Ad;
            txtSoyad.Text = doktor.Soyad;
            txtUnvan.Text = doktor.Unvan;
            hastaneler.ItemSelected += Hastaneler_ItemSelected;
        }

        private void Hastaneler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Hastane hastane = Hastaneler[e.Position];

            Bolumler = bolumService.Bolumler(hastane.Id);

            List<string> bolumAdlari = new List<string>();

            foreach (var item in Bolumler)
            {
                bolumAdlari.Add(item.Ad);
            }

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, bolumAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            bolumler.Adapter = adapter;
            btnDoktorGuncelle.Click += BtnDoktorGuncelle_Click;
        }

        private void BtnDoktorGuncelle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == null || txtSoyad.Text == null || txtUnvan.Text == null)
            {
                Toast.MakeText(Application.Context, "Doktor Güncellenemedi. Lütfen bilgilerin tamamını doldurduğunuzdan emin olun.", ToastLength.Long).Show();
                return;
            }
            Doktor doktorveri = new Doktor();
            doktorveri.Ad = txtAd.Text;
            doktorveri.Soyad = txtSoyad.Text;
            doktorveri.Unvan = txtUnvan.Text;
            doktorveri.TC = TC;
            doktorveri.Sifre = Sifre;
            doktorveri.DogumTarihi = Convert.ToDateTime(dogumTarihi);
            doktorveri.Cinsiyet = Cinsiyet;
            doktorveri.Id = ID;
            int hastaneid = Hastaneler.SingleOrDefault(x => x.Ad == hastaneler.SelectedItem.ToString()).Id;
            doktorveri.HastaneId = hastaneid;
            int bolumid = Bolumler.SingleOrDefault(x => x.Ad == bolumler.SelectedItem.ToString()).Id;
            doktorveri.BolumId = bolumid;
            doktorService.Guncelle(doktorveri);
            var intent = new Intent(this, typeof(DoktorGuncellemeOnayActivity));
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