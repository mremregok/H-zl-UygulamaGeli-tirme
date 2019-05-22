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
    [Activity(Label = "Doktor Randevularım", Theme = "@style/AppTheme")]
    public class DoktorRandevularimActivity : AppCompatActivity
    {
        ListView _listView;
        List<Randevu> _doktorRandevulari;
        IRandevuService randevuService;
        IDoktorService doktorService;
        private Button btnSonrakiSayfaRandevularim, btnOncekiSayfaRandevularim;
        int startIndex = 0;
        List<Randevu> tempList = new List<Randevu>();

        public DoktorRandevularimActivity()
        {
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktorRandevularim_layout);

            Doktor doktor = new Doktor();
            string tc = Intent.GetStringExtra("tc");
            doktor = doktorService.Getir(tc);

            btnOncekiSayfaRandevularim = FindViewById<Button>(Resource.Id.btnOncekiSayfaRandevularim);
            btnSonrakiSayfaRandevularim = FindViewById<Button>(Resource.Id.btnSonrakiSayfaRandevularim);
            _listView = FindViewById<ListView>(Resource.Id.customListView);
            _doktorRandevulari = randevuService.DoktorRandevulari(doktor.Id);
            _doktorRandevulari.Reverse();

            tempList = _doktorRandevulari.Skip(startIndex).Take(3).ToList();

            RandevularimListViewAdapter adapter = new RandevularimListViewAdapter(this, tempList);
            _listView.Adapter = adapter;

            btnOncekiSayfaRandevularim.Click += BtnOncekiSayfaRandevularim_Click;
            btnSonrakiSayfaRandevularim.Click += BtnSonrakiSayfaRandevularim_Click;
        }

        private void BtnSonrakiSayfaRandevularim_Click(object sender, EventArgs e)
        {
            startIndex += 3;
            tempList = _doktorRandevulari.Skip(startIndex).Take(3).ToList();
            
            RandevularimListViewAdapter adapter = new RandevularimListViewAdapter(this, tempList);
            _listView.Adapter = adapter;

            if (startIndex != 0) btnOncekiSayfaRandevularim.Enabled = true;
            if (_doktorRandevulari.Count <= startIndex + 3) btnSonrakiSayfaRandevularim.Enabled = false;
        }

        private void BtnOncekiSayfaRandevularim_Click(object sender, EventArgs e)
        {
            startIndex -= 3;
            tempList = _doktorRandevulari.Skip(startIndex).Take(3).ToList();
            
            RandevularimListViewAdapter adapter = new RandevularimListViewAdapter(this, tempList);
            _listView.Adapter = adapter;

            if (startIndex <= 0) btnOncekiSayfaRandevularim.Enabled = false;
            if (_doktorRandevulari.Count > startIndex - 3) btnSonrakiSayfaRandevularim.Enabled = true;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.doktorMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnDoktorProfilim:
                    {
                        var intent = new Intent(this, typeof(DoktorProfilimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorRandevularim:
                    {
                        
                        return true;
                    }
                case Resource.Id.menuBtnDoktorSifreDegistir:
                    {
                        var intent = new Intent(this, typeof(DoktorSifreDegistirActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorCikisYap:
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