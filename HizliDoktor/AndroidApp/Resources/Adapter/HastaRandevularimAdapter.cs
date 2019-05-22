using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp.Resources.Adapter
{
    class HastaRandevularimAdapter : BaseAdapter<Randevu>
    {
        private readonly Context context;
        private List<Randevu> randevular;
        private IDoktorService doktorService;
        private IRandevuService randevuService;
        private IFavoriService favoriService;
        private Hasta hasta;

        public HastaRandevularimAdapter(Context context, List<Randevu> randevular, Hasta hasta)
        {
            this.randevular = randevular;
            this.context = context;
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
            randevuService = Business.IOCUtil.Container.Resolve<IRandevuService>();
            favoriService = Business.IOCUtil.Container.Resolve<IFavoriService>();
            this.hasta = hasta;
        }

        public override Randevu this[int position]
        {
            get { return randevular[position]; }
        }

        public override int Count
        {
            get { return randevular.Count; }
        }

        public override long GetItemId(int position)
        {
            return randevular[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View satir = convertView;
            if (satir == null) satir = LayoutInflater.From(context).Inflate(Resource.Layout.hastaRandevularimItem_layout, null, false);

            TextView txtHastaRandevularimDoktorAdi     = satir.FindViewById<TextView>(Resource.Id.txtHastaRandevularimDoktorAdi);
            TextView txtHastaRandevularimRandevuTarihi = satir.FindViewById<TextView>(Resource.Id.txtHastaRandevularimRandevuTarihi);
            TextView txtHastaRandevularimIslem         = satir.FindViewById<TextView>(Resource.Id.txtHastaRandevularimIslem);
            
            txtHastaRandevularimIslem.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;

            Doktor doktor = doktorService.Getir(randevular[position].DoktorId);

            txtHastaRandevularimDoktorAdi.Text = doktor.Unvan + " " + doktor.Ad + " " + doktor.Soyad;
            txtHastaRandevularimRandevuTarihi.Text = randevular[position].Tarih.Value.ToLongDateString() + " " + randevular[position].Tarih.Value.ToShortTimeString();

            if (randevular[position].Tarih > DateTime.Now)
            {
                txtHastaRandevularimIslem.Text = "IPTAL";
            }
            else
            {
                //fav sorgula. Yoksa fav+ yaz varsa fav- yaz
                Favori favori = favoriService.Getir(hasta.Id, doktor.Id);
                if (favori == null) txtHastaRandevularimIslem.Text = "FAV+";
                else txtHastaRandevularimIslem.Text = "FAV-";
            }

            txtHastaRandevularimIslem.Click += (sender, e) => TxtHastaRandevularimIslem_Click(sender, e, randevular[position], doktor);
            satir.Click += (sender, e) => Satir_Click(sender, e, randevular[position]);

            return satir;
        }

        private void Satir_Click(object sender, EventArgs e, Randevu randevu)
        {
            Intent intent = new Intent(context, typeof(HastaRandevularimDetayActivity));
            intent.PutExtra("randevuTarihi", randevu.Tarih.Value.ToString());
            intent.PutExtra("hastaneId", randevu.HastaneId);
            intent.PutExtra("bolumId", randevu.BolumId);
            intent.PutExtra("doktorId", randevu.DoktorId);
            intent.PutExtra("hastaTc", hasta.TC);
            context.StartActivity(intent);
        }

        private void TxtHastaRandevularimIslem_Click(object sender, EventArgs e, Randevu randevu, Doktor doktor)
        {
            TextView thisButton = (TextView)sender;

            if (randevu.Tarih > DateTime.Now)
            {
                randevuService.Sil(randevu.Id);

                Intent intent = new Intent(context, typeof(HastaRandevularimIptalActivity));
                intent.PutExtra("tc", hasta.TC);
                context.StartActivity(intent);

                string mesaj = "Sayın " + hasta.Ad + " " + hasta.Soyad + ", " + System.Environment.NewLine + System.Environment.NewLine +
                                randevu.Tarih.Value.ToLongDateString() + " " + randevu.Tarih.Value.ToShortTimeString() +
                                " tarihli randevunuz iptal edilmiştir." + System.Environment.NewLine + System.Environment.NewLine +
                                "Sağlıklı günler dileriz.";

                randevuService.RandevuMailiGonder(hasta.Mail, mesaj);
            }
            else
            {
                //favori sorgula. Yoksa ekle, varsa çıkart.
                Favori favori = favoriService.Getir(hasta.Id, doktor.Id);
                if (favori == null)
                {
                    favori = new Favori();
                    favori.DoktorId = doktor.Id;
                    favori.HastaId = hasta.Id;
                    favori.OlusturulmaTarihi = DateTime.Now;

                    favoriService.Ekle(favori);

                    Toast.MakeText(Application.Context, doktor.Unvan + " " + doktor.Ad + " " + doktor.Soyad + " favorilere eklendi.", ToastLength.Short).Show();
                    thisButton.Text = "FAV-";
                }
                else
                {
                    favoriService.Sil(favori.Id);
                    Toast.MakeText(Application.Context, doktor.Unvan + " " + doktor.Ad + " " + doktor.Soyad + " favorilerden çıkarıldı.", ToastLength.Short).Show();
                    thisButton.Text = "FAV+";
                }
            }
        }
    }
}
