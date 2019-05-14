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
    [Activity(Label = "Doktor Randevularım", Theme = "@style/AppTheme")]
    public class DoktorRandevularimActivity : AppCompatActivity
    {
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

            txtAdSatir = FindViewById<TextView>(Resource.Id.txtAdSatir);
            txtRndSaatSatir = FindViewById<TextView>(Resource.Id.txtRndSaatSatir);
            txtRndTarihSatir = FindViewById<TextView>(Resource.Id.txtRndTarihSatir);

            Doktor doktor = new Doktor();
            //List<Randevu> doktorRandevuları = randevuService.DoktorRandevulari()
        }
    }
}