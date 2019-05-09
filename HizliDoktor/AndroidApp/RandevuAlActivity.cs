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
    [Activity(Label = "Randevu Al", Theme = "@style/AppTheme")]
    public class RandevuAlActivity : AppCompatActivity
    {
        IRandevuService randevuService;
        IHastaneService hastaneService;
        IBolumService bolumService;
        IDoktorService doktorService;

        private GridView gridTarihler;
        private Spinner spinnerIller, spinnerIlceler, spinnerHastaneler, spinnerBolumler, spinnerDoktorlar;

        public RandevuAlActivity()
        {
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            bolumService   = Business.IOCUtil.Container.Resolve<IBolumService>();
            doktorService  = Business.IOCUtil.Container.Resolve<IDoktorService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.randevuAl_layout);

            List<string> hastaneOlanIller = hastaneService.HastaneOlanIller();

            gridTarihler = FindViewById<GridView>(Resource.Id.gridTarihler);

            spinnerIller = FindViewById<Spinner>(Resource.Id.spinnerIller);
            spinnerIlceler = FindViewById<Spinner>(Resource.Id.spinnerIlceler);
            spinnerHastaneler = FindViewById<Spinner>(Resource.Id.spinnerHastaneler);
            spinnerBolumler = FindViewById<Spinner>(Resource.Id.spinnerBolumler);
            spinnerDoktorlar = FindViewById<Spinner>(Resource.Id.spinnerDoktorlar);

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneOlanIller);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerIller.Adapter = adapter;

            spinnerIller.ItemSelected += SpinnerIller_ItemSelected;
            spinnerIlceler.ItemSelected += SpinnerIlceler_ItemSelected;
            spinnerHastaneler.ItemSelected += SpinnerHastaneler_ItemSelected;
            spinnerBolumler.ItemSelected += SpinnerBolumler_ItemSelected;
            spinnerDoktorlar.ItemSelected += SpinnerDoktorlar_ItemSelected;            
        }

        private void SpinnerIller_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<string> hastaneOlanIlceler = hastaneService.HastaneOlanIlceler((string)spinnerIller.SelectedItem);

            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneOlanIlceler);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerIlceler.Adapter = adapter;
        }

        private void SpinnerIlceler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<Hastane> hastaneler = hastaneService.Hastaneler((string)spinnerIller.SelectedItem, (string)spinnerIlceler.SelectedItem);

            ArrayAdapter adapter = new ArrayAdapter<Hastane>(this, Android.Resource.Layout.SimpleSpinnerItem, hastaneler);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerHastaneler.Adapter = adapter;
        }

        private void SpinnerHastaneler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var item = spinner.GetItemAtPosition(e.Position);

            List<Bolum> bolumler = bolumService.Bolumler(hastane.Id);

            ArrayAdapter adapter = new ArrayAdapter<Bolum>(this, Android.Resource.Layout.SimpleSpinnerItem, bolumler);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerBolumler.Adapter = adapter;
        }

        private void SpinnerBolumler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            Bolum bolum = (Bolum)(object)spinner.GetItemAtPosition(e.Position);

            List<Doktor> doktorlar = doktorService.Doktorlar(bolum.Id);

            ArrayAdapter adapter = new ArrayAdapter<Doktor>(this, Android.Resource.Layout.SimpleSpinnerItem, doktorlar);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerDoktorlar.Adapter = adapter;
        }

        private void SpinnerDoktorlar_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.hastaMenu, menu);
            return true;
        }

    }
}
