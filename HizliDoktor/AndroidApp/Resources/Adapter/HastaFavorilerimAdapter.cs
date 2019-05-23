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
        private IFavoriService favoriService;
        private IBolumService bolumService;
        Hasta hasta;


        public HastaFavorilerimAdapter(Context context, List<Favori> favoriler, Hasta hasta)
        {
            this.favoriler = favoriler;
            this.context = context;
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
            favoriService = Business.IOCUtil.Container.Resolve<IFavoriService>();
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
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
            if (satir == null) satir = LayoutInflater.From(context).Inflate(Resource.Layout.hastaFavoriDoktorlariItem_layout, null, false);

            TextView txtFavorilerimDoktorAdi = satir.FindViewById<TextView>(Resource.Id.txtFavorilerimDoktorAdi);
            TextView txtFavorilerimBolumAdı = satir.FindViewById<TextView>(Resource.Id.txtFavorilerimBolumAdi);
            TextView txtFavorilerimDoktorSil = satir.FindViewById<TextView>(Resource.Id.txtFavorilerimDoktorSil);

            txtFavorilerimDoktorSil.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;

            Doktor doktor =  doktorService.Getir(favoriler[position].DoktorId);

            if(doktor != null)
            {
                Bolum bolum = bolumService.Getir(doktor.BolumId);

                txtFavorilerimDoktorAdi.Text = doktor.Ad + " " + doktor.Soyad;
                txtFavorilerimBolumAdı.Text = bolum.Ad;
                txtFavorilerimDoktorSil.Text = "Çıkar";

                txtFavorilerimDoktorSil.Click += (sender, e) => txtFavorilerimDoktorSil_Click(sender, e, favoriler[position]);
            }

            return satir;
        }


        private void txtFavorilerimDoktorSil_Click(object sender, EventArgs e, Favori favori)
        {
            favoriService.Sil(favori.Id);

            Intent intent = new Intent(context, typeof(HastaFavoriSilActivity));
            intent.PutExtra("tc", hasta.TC);
            context.StartActivity(intent);
           
        }

    }
}