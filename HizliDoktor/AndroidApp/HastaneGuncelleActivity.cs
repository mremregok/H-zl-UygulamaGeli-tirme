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
    [Activity(Label = "Hastane Listele", Theme = "@style/AppTheme")]
    public class HastaneGuncelleActivity : AppCompatActivity
    {
        
        string[] iller = new string[] { "Manisa", "İstanbul", "İzmir", "Ankara", "Denizli" };
        string[] manisaIlceler = new string[] { "Turgutlu", "Merkez", "Salihli", "Soma", "Akhisar" };
        string[] izmiriIlceler = new string[] { "Karşıyaka", "Bornova", "Göztepe", "Buca", "Tepecik" };
        string[] istanbulIlceler = new string[] { "Üsküdar", "Kadıköy", "Bağcılar", "Kartal", "Beşiktaş" };
        string[] ankaraIlceler = new string[] { "Sincan", "Çankaya", "Altındağ", "Gölbaşı", "Mamak" };
        string[] denizliIlceler = new string[] { "Pamukkale", "Bozkurt", "Çardak", "Serinhisar", "Tavas" };
        private Spinner spinnerHastaneGuncelleIller, spinnerHastaneGuncelleIlceler;
        private EditText txtGuncelleHastaneAd;
        private Button btnHastaneGuncelle;
        IHastaneService hastaneService;
        Hastane hastane;
        int seciliIlceIndex = 0;

        public HastaneGuncelleActivity()
        {
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaneGuncelle_layout);


            spinnerHastaneGuncelleIller = FindViewById<Spinner>(Resource.Id.spinnerHastaneGuncelleIller);
            spinnerHastaneGuncelleIlceler = FindViewById<Spinner>(Resource.Id.spinnerHastaneGuncelleIlceler);
            txtGuncelleHastaneAd = FindViewById<EditText>(Resource.Id.txtGuncelleHastaneAd);
            btnHastaneGuncelle = FindViewById<Button>(Resource.Id.btnHastaneGuncelle);

            ArrayAdapter adapterIl = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, iller);
            adapterIl.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerHastaneGuncelleIller.Adapter = adapterIl;

            spinnerHastaneGuncelleIller.ItemSelected += SpinnerHastaneGuncelleIller_ItemSelected;
            btnHastaneGuncelle.Click += BtnHastaneGuncelle_Click;

            hastane = hastaneService.Getir(Intent.GetIntExtra("guncelleHastaneId",0));

            txtGuncelleHastaneAd.Text = hastane.Ad;

            if (hastane.Il == "Manisa")
            {
                spinnerHastaneGuncelleIller.SetSelection(0);

                for (int i = 0; i < manisaIlceler.Length; i++)
                {
                    if (manisaIlceler[i] == hastane.Ilce)
                    {
                        seciliIlceIndex = i;
                    }
                }
            }
            else if (hastane.Il == "İstanbul")
            {
                spinnerHastaneGuncelleIller.SetSelection(1);

                for (int i = 0; i < istanbulIlceler.Length; i++)
                {
                    if (istanbulIlceler[i] == hastane.Ilce)
                    {
                        seciliIlceIndex = i;
                    }
                }
            }
            else if (hastane.Il == "İzmir")
            {
                spinnerHastaneGuncelleIller.SetSelection(2);

                for (int i = 0; i < izmiriIlceler.Length; i++)
                {
                    if (izmiriIlceler[i] == hastane.Ilce)
                    {
                        seciliIlceIndex = i;
                    }
                }
            }
            else if (hastane.Il == "Ankara")
            {
                spinnerHastaneGuncelleIller.SetSelection(3);

                for (int i = 0; i < ankaraIlceler.Length; i++)
                {
                    if (ankaraIlceler[i] == hastane.Ilce)
                    {
                        seciliIlceIndex = i;
                    }
                }
            }
            else if (hastane.Il == "Denizli")
            {
                spinnerHastaneGuncelleIller.SetSelection(4);

                for (int i = 0; i < denizliIlceler.Length; i++)
                {
                    if (denizliIlceler[i] == hastane.Ilce)
                    {
                        seciliIlceIndex = i;
                    }
                }
            } 
        }

        private void BtnHastaneGuncelle_Click(object sender, EventArgs e)
        {
            if (txtGuncelleHastaneAd.Text == string.Empty)
            {
                Toast.MakeText(Application.Context, "Lütfen hastane adını boş bırakmayınız.", ToastLength.Long).Show();
                return;
            }
            hastane.Ad = txtGuncelleHastaneAd.Text;
            hastane.Il = spinnerHastaneGuncelleIller.SelectedItem.ToString();
            hastane.Ilce = spinnerHastaneGuncelleIlceler.SelectedItem.ToString();
            hastaneService.Guncelle(hastane);

            var intent = new Intent(this, typeof(HastaneGuncellemeOnayActivity));
            StartActivity(intent);
        }

        private void SpinnerHastaneGuncelleIller_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (spinnerHastaneGuncelleIller.SelectedItemPosition == 0)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, manisaIlceler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerHastaneGuncelleIlceler.Adapter = adapter;
            }
            else if (spinnerHastaneGuncelleIller.SelectedItemPosition == 1)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, istanbulIlceler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerHastaneGuncelleIlceler.Adapter = adapter;
            }
            else if (spinnerHastaneGuncelleIller.SelectedItemPosition == 2)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, izmiriIlceler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerHastaneGuncelleIlceler.Adapter = adapter;
            }
            else if (spinnerHastaneGuncelleIller.SelectedItemPosition == 3)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ankaraIlceler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerHastaneGuncelleIlceler.Adapter = adapter;
            }
            else if(spinnerHastaneGuncelleIller.SelectedItemPosition == 4)
            {
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, denizliIlceler);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerHastaneGuncelleIlceler.Adapter = adapter;
            }


            if (seciliIlceIndex != 0) spinnerHastaneGuncelleIlceler.SetSelection(seciliIlceIndex);
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

            }
            return base.OnOptionsItemSelected(item);
        }

    }
}