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
    [Activity(Label = "Randevu Onayla", Theme = "@style/AppTheme")]
    public class RandevuOnaylaActivity : AppCompatActivity
    {
        TextView txtIl, txtIlce, txtHastane, txtBolum, txtDoktor, txtTarih;
        Button btnRandevuOnay, btnRandevuIptal;
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

        public RandevuOnaylaActivity()
        {
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();
            bolumService   = Business.IOCUtil.Container.Resolve<IBolumService>();
            doktorService  = Business.IOCUtil.Container.Resolve<IDoktorService>();
            hastaService   = Business.IOCUtil.Container.Resolve<IHastaService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.randevuOnayla_layout);

            txtIl = FindViewById<TextView>(Resource.Id.txtRandOnayIl);
            txtIlce = FindViewById<TextView>(Resource.Id.txtRandOnayIlce);
            txtHastane = FindViewById<TextView>(Resource.Id.txtRandOnayHastane);
            txtBolum = FindViewById<TextView>(Resource.Id.txtRandOnayBolum);
            txtDoktor = FindViewById<TextView>(Resource.Id.txtRandOnayDoktor);
            txtTarih = FindViewById<TextView>(Resource.Id.txtRandOnayTarih);
            btnRandevuOnay = FindViewById<Button>(Resource.Id.btnRandevuOnay);
            btnRandevuIptal = FindViewById<Button>(Resource.Id.btnRandevuIptal);

            btnRandevuOnay.Click += BtnRandevuOnay_Click;
            btnRandevuIptal.Click += BtnRandevuIptal_Click;

            hastane = hastaneService.Getir(Intent.GetIntExtra("hastaneId", 0));
            bolum   = bolumService.Getir(Intent.GetIntExtra("bolumId", 0));
            doktor  = doktorService.Getir(Intent.GetIntExtra("doktorId", 0));
            hasta   = hastaService.Getir(Intent.GetStringExtra("hastaTc"));
            randevuTarihi = Convert.ToDateTime(Intent.GetStringExtra("randevuTarihi"));

            txtIl.Text      = "İl: " + hastane.Il;
            txtIlce.Text    = "İlçe: " + hastane.Ilce;
            txtHastane.Text = "Hastane: " + hastane.Ad;
            txtBolum.Text   = "Bölüm: " + bolum.Ad;
            txtDoktor.Text  = "Doktor: " + doktor.Ad + " " + doktor.Soyad;
            txtTarih.Text   = "Tarih: " + randevuTarihi.ToLongDateString() + " " + randevuTarihi.ToShortTimeString();
        }

        private void BtnRandevuIptal_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HastaProfilimActivity));
            intent.PutExtra("tc", Intent.GetStringExtra("hastaTc"));
            StartActivity(intent);
        }

        private void BtnRandevuOnay_Click(object sender, EventArgs e)
        {
            Randevu randevu = new Randevu();

            randevu.BolumId = bolum.Id;
            randevu.HastaneId = hastane.Id;
            randevu.DoktorId = doktor.Id;
            randevu.Tarih = randevuTarihi;
            randevu.HastaId = hasta.Id;

            randevuService.Ekle(randevu);

            var intent = new Intent(this, typeof(randevuOnaylandiActivity));
            intent.PutExtra("tc", Intent.GetStringExtra("hastaTc"));
            StartActivity(intent);

            string mesaj = "Sayın " + hasta.Ad + " " + hasta.Soyad + ", " + System.Environment.NewLine +
                 "Randevu detaylarınız aşağıda yer almaktadır;" + System.Environment.NewLine + System.Environment.NewLine +
                 "İl: " + hastane.Il + System.Environment.NewLine +
                 "İlçe: " + hastane.Ilce + System.Environment.NewLine +
                 "Hastane: " + hastane.Ad + System.Environment.NewLine +
                 "Bölüm: " + bolum.Ad + System.Environment.NewLine +
                 "Doktor: " + doktor.Ad + " " + doktor.Soyad + System.Environment.NewLine +
                 "Tarih: " + randevuTarihi.ToLongDateString() + " " + randevuTarihi.ToShortTimeString() + System.Environment.NewLine + System.Environment.NewLine +
                 "Sağlıklı günler dileriz.";

            randevuService.RandevuMailiGonder(hasta.Mail, mesaj);
        }
    }
}