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
    [Activity(Label = "Bolum Listele", Theme = "@style/AppTheme")]
    public class BolumListeleActivity : AppCompatActivity
    {
        private readonly Context context;
        ListView listView;
        List<Bolum> bolumler;
        IHastaneService hastaneService;
        IBolumService bolumService;
        Spinner spinnerhastaneler;
        Bolum bölüm;
        private List<Hastane> Hastaneler;
        public BolumListeleActivity()
        {
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.bolumListele_layout);

            spinnerhastaneler = FindViewById<Spinner>(Resource.Id.spinnerHastaneler);

            listView = FindViewById<ListView>(Resource.Id.listBolum);

            Hastaneler = hastaneService.TumHastaneler();

            List<string> hastaneAdlari = new List<string>();

            foreach (var item in Hastaneler)
            {
                hastaneAdlari.Add(item.Ad);
            }
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneAdlari);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerhastaneler.Adapter = adapter;

            spinnerhastaneler.ItemSelected += Spinnerhastaneler_ItemSelected;
        }

        private void Spinnerhastaneler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int id = (int)spinnerhastaneler.SelectedItemId + 1;
            bolumler =  bolumService.Bolumler(id);
            BolumListeleAdapter adapter = new BolumListeleAdapter(this, bolumler);
            listView.Adapter = adapter;

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