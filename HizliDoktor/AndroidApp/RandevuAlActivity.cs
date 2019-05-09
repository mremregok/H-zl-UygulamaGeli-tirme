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
        private GridView gridTarihler;
        private Spinner spinnerIller, spinnerIlceler, spinnerHastaneler, spinnerBolumler, spinnerDoktorlar;

        public RandevuAlActivity()
        {
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.randevuAl_layout);

            gridTarihler = FindViewById<GridView>(Resource.Id.gridTarihler);

            spinnerIller = FindViewById<Spinner>(Resource.Id.spinnerIller);
            spinnerIlceler = FindViewById<Spinner>(Resource.Id.spinnerIlceler);
            spinnerHastaneler = FindViewById<Spinner>(Resource.Id.spinnerHastaneler);
            spinnerBolumler = FindViewById<Spinner>(Resource.Id.spinnerBolumler);
            spinnerDoktorlar = FindViewById<Spinner>(Resource.Id.spinnerDoktorlar);

            spinnerIller.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, )

            spinnerIller.ItemSelected += SpinnerIller_ItemSelected;
            spinnerIlceler.ItemSelected += SpinnerIlceler_ItemSelected;
            spinnerHastaneler.ItemSelected += SpinnerHastaneler_ItemSelected;
            spinnerBolumler.ItemSelected += SpinnerBolumler_ItemSelected;
            spinnerDoktorlar.ItemSelected += SpinnerDoktorlar_ItemSelected;
            
        }

        private void SpinnerBolumler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void SpinnerHastaneler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void SpinnerIlceler_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void SpinnerIller_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
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
