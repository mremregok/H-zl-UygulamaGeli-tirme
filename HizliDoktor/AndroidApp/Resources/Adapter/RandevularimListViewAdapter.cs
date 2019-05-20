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
    public class RandevularimListViewAdapter : BaseAdapter<Randevu>
    {
        readonly Context _context;
        private List<Randevu> _doktorRandevulari;
        private IHastaService hastaService;

        public RandevularimListViewAdapter(Context context, List<Randevu> doktorlar)
        {
            _doktorRandevulari = doktorlar;
            _context = context;
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
        }

        public override Randevu this[int position]
        {
            get { return _doktorRandevulari[position]; }
        }

        public override int Count
        {
            get { return _doktorRandevulari.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View satir = convertView;
            if (satir == null)
                satir = LayoutInflater.From(_context).Inflate(Resource.Layout.doktorRandevularimItem_layout, null, false);

            TextView txtAdSatir = satir.FindViewById<TextView>(Resource.Id.txtAdSatir);
            Hasta hasta = hastaService.Getir(_doktorRandevulari[position].HastaId);
            txtAdSatir.Text = hasta.Ad + " " + hasta.Soyad;

            DateTime tarih = Convert.ToDateTime(_doktorRandevulari[position].Tarih);
            TextView txtRndSaatSatir = satir.FindViewById<TextView>(Resource.Id.txtRndSaatSatir);
            txtRndSaatSatir.Text = tarih.ToShortTimeString();

            TextView txtRndTarihSatir = satir.FindViewById<TextView>(Resource.Id.txtRndTarihSatir);
            txtRndTarihSatir.Text = tarih.ToShortDateString();

            return satir;
        }
    }
}