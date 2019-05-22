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
    [Activity(Label = "DoktorEkle", Theme = "@style/AppTheme")]
    public class DoktorEkleActivity : AppCompatActivity
    {
        private Spinner hastaneler, bolumler;
        private Button btnTarih, btnDoktorEkle;
        private EditText txtTC, txtAd, txtSoyad, txtPass, txtDate, txtUnvan;
        private RadioButton rbErkek, rbKadin;
        IBolumService bolumService;
        IHastaneService hastaneService;
        IDoktorService doktorService;
        private List<Hastane> Hastaneler;
        private List<Bolum> Bolumler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktorEkle_layout);

            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();

            btnDoktorEkle = FindViewById<Button>(Resource.Id.btnDoktorEkle);
            btnTarih = FindViewById<Button>(Resource.Id.btnTarihSec);
            hastaneler = FindViewById<Spinner>(Resource.Id.spinnerHastaneler);
            bolumler = FindViewById<Spinner>(Resource.Id.spinnerBolumler);
            txtUnvan = FindViewById<EditText>(Resource.Id.txtUnvan);
            txtTC = FindViewById<EditText>(Resource.Id.txtTC);
            txtAd = FindViewById<EditText>(Resource.Id.txtAd);
            txtSoyad = FindViewById<EditText>(Resource.Id.txtSoyad);
            txtPass = FindViewById<EditText>(Resource.Id.txtPass);
            txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            rbErkek = FindViewById<RadioButton>(Resource.Id.rbErkek);
            rbKadin = FindViewById<RadioButton>(Resource.Id.rbKadin);

            Hastaneler = hastaneService.TumHastaneler();
            List<string> hastaneAdlari = new List<string>();

            foreach (var item in Hastaneler)
            {
                hastaneAdlari.Add(item.Ad);
            }
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            hastaneler.Adapter = adapter;
            hastaneler.ItemSelected += Hastaneler_ItemSelected;
            btnTarih.Click += BtnTarih_Click;
            btnDoktorEkle.Click += BtnDoktorEkle_Click;
        }

        private void BtnDoktorEkle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == null || txtSoyad.Text == null || txtTC.Text == null || txtPass.Text == null || txtDate.Text == null  || txtUnvan.Text == null)
            {
                Toast.MakeText(Application.Context, "Üyelik oluşturulamadı. Lütfen bilgilerin tamamını doldurduğunuzdan emin olun.", ToastLength.Long).Show();
                return;
            }

            if (txtTC.Text.Length < 11)
            {
                Toast.MakeText(Application.Context, "TC kimlik no 11 hane az olamaz.", ToastLength.Long).Show();
                return;
            }

            if (txtPass.Text.Length < 6)
            {
                Toast.MakeText(Application.Context, "Oluşturulan şifre 6 karakterden az olamaz.", ToastLength.Long).Show();
                return;
            }
           
            Doktor doktor = new Doktor();
            doktor.Ad = txtAd.Text;
            doktor.Soyad = txtSoyad.Text;
            doktor.Unvan = txtUnvan.Text;
            doktor.TC = txtTC.Text;
            doktor.Sifre = txtPass.Text;
            doktor.DogumTarihi = Convert.ToDateTime(txtDate.Text);
            int hastaneid = (int)hastaneler.SelectedItemId;
            doktor.HastaneId = hastaneid + 1;
            int bolumid = (int)bolumler.SelectedItemId;
            doktor.BolumId = bolumid + 1;
            doktorService.Ekle(doktor);

        }

        private void BtnTarih_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                txtDate.Text = time.ToLongDateString();
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
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

            }
            return base.OnOptionsItemSelected(item);
        }
    }
}