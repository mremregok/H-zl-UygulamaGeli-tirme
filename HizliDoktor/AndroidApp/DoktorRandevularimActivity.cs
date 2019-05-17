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
        private TextView txtAdSatir, txtRndSaatSatir, txtRndTarihSatir;

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

            _listView = FindViewById<ListView> (Resource.Id.customListView);
            _doktorRandevulari = randevuService.DoktorRandevulari(doktor.Id);

            RandevularimListViewAdapter adapter = new RandevularimListViewAdapter(this, _doktorRandevulari);
            _listView.Adapter = adapter;


        }
    }
}