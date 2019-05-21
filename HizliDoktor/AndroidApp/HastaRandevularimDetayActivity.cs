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
    [Activity(Label = "Randevu Detayı", Theme = "@style/AppTheme")]
    public class HastaRandevularimDetayActivity : AppCompatActivity
    {
        TextView txtIl, txtIlce, txtHastane, txtBolum, txtDoktor, txtTarih;
        Button btnRandevuGoruntule;
        Hastane hastane;
        Bolum bolum;
        Doktor doktor;
        Hasta hasta;
        DateTime randevuTarihi;

        IRandevuService randevuService;
        IHastaneService hastaneService;
        IBolumService bolumService;
        IDoktorService doktorService;
        IHastaService hastaService;

        public HastaRandevularimDetayActivity()
        {
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaRandevularimDetay_layout);

            txtIl = FindViewById<TextView>(Resource.Id.txtDetayOnayIl);
            txtIlce = FindViewById<TextView>(Resource.Id.txtDetayOnayIlce);
            txtHastane = FindViewById<TextView>(Resource.Id.txtDetayOnayHastane);
            txtBolum = FindViewById<TextView>(Resource.Id.txtDetayOnayBolum);
            txtDoktor = FindViewById<TextView>(Resource.Id.txtDetayOnayDoktor);
            txtTarih = FindViewById<TextView>(Resource.Id.txtDetayOnayTarih);
            btnRandevuGoruntule = FindViewById<Button>(Resource.Id.btnRandevuDetayGoruntule);

            hastane = hastaneService.Getir(Intent.GetIntExtra("hastaneId", 0));
            bolum = bolumService.Getir(Intent.GetIntExtra("bolumId", 0));
            doktor = doktorService.Getir(Intent.GetIntExtra("doktorId", 0));
            hasta = hastaService.Getir(Intent.GetStringExtra("hastaTc"));
            randevuTarihi = Convert.ToDateTime(Intent.GetStringExtra("randevuTarihi"));

            txtIl.Text = "İl: " + hastane.Il;
            txtIlce.Text = "İlçe: " + hastane.Ilce;
            txtHastane.Text = "Hastane: " + hastane.Ad;
            txtBolum.Text = "Bölüm: " + bolum.Ad;
            txtDoktor.Text = "Doktor: " + doktor.Ad + " " + doktor.Soyad;
            txtTarih.Text = "Tarih: " + randevuTarihi.ToLongDateString() + " " + randevuTarihi.ToShortTimeString();

            btnRandevuGoruntule.Click += BtnRandevuGoruntule_Click;
        }

        private void BtnRandevuGoruntule_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(HastaRandevularimActivity));
            intent.PutExtra("tc", hasta.TC);
            StartActivity(intent);
        }
    }
}