using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp.Resources.Adapter
{
    public class HastaFavorilerimAdapter : BaseAdapter<Favori>
    {
        private readonly Context context;
        private List<Favori> favoriler;
        private IHastaService hastaService;
        private IDoktorService doktorService;
        private IFavoriService randevuService;
        private Hasta hasta;

        public HastaFavorilerimAdapter(Context context, List<Favori> favoriler, Hasta hasta)
        {
            this.favoriler = favoriler;
            this.context = context;
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
            this.hasta = hasta;
        }

        public override Favori this[int position]
        {
            get { return favoriler[position]; }
        }

        public override int Count
        {
            get { return favoriler.Count; }
        }

        public override long GetItemId(int position)
        {
            return favoriler[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View satir = convertView;
            if (satir == null) satir = LayoutInflater.From(context).Inflate(Resource.Layout.hastaRandevularimItem_layout, null, false);

            TextView txtFavorilerimDoktorAdi = satir.FindViewById<TextView>(Resource.Id.txtHastaRandevularimDoktorAdi);
            TextView txtFavorilerimBolumAdı = satir.FindViewById<TextView>(Resource.Id.txtHastaRandevularimRandevuTarihi);
            TextView txtFavorilerimDoktorSil = satir.FindViewById<TextView>(Resource.Id.txtHastaRandevularimIslem);

            txtFavorilerimDoktorSil.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;

            Favori favori = hastaService.Getir(favoriler[position].HastaId);

            txtFavorilerimDoktorAdi.Text = hasta.
            txtHastaRandevularimRandevuTarihi.Text = randevular[position].Tarih.Value.ToLongDateString() + " " + randevular[position].Tarih.Value.ToShortTimeString();

            if (randevular[position].Tarih > DateTime.Now)
            {
                txtHastaRandevularimIslem.Text = "IPTAL";
            }
            else
            {
                //fav sorgula. Yoksa fav+ yaz varsa fav- yaz
                if (true) txtHastaRandevularimIslem.Text = "FAV+";
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

    }
}