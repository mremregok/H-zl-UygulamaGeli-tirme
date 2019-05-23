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
    class DoktorListeleAdapter : BaseAdapter<Doktor>
    {
        private readonly Context context;
        private IDoktorService doktorService;
        private List<Doktor> doktorlar;

        public DoktorListeleAdapter(Context context, List<Doktor> doktorlar)
        {
            this.context = context;
            this.doktorlar = doktorlar;
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return doktorlar[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View satir = convertView;
            if (satir == null) satir = LayoutInflater.From(context).Inflate(Resource.Layout.doktorListeleItem_layout, null, false);

            TextView txtDoktorSilDoktorAdi = satir.FindViewById<TextView>(Resource.Id.txtDoktorSilHDoktorAdi);
            TextView txtDoktorGüncelle = satir.FindViewById<TextView>(Resource.Id.txtDoktorGüncelle);
            TextView txtDoktorSil = satir.FindViewById<TextView>(Resource.Id.txtDoktorSil);

            txtDoktorSil.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;
            txtDoktorGüncelle.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;

            txtDoktorSilDoktorAdi.Text = doktorlar[position].Unvan+ "." + doktorlar[position].Ad +" "+ doktorlar[position].Soyad;
            txtDoktorGüncelle.Text = "Güncelle";
            txtDoktorSil.Text = "Sil";

            txtDoktorGüncelle.Click += (sender, e) => TxtDoktorGüncelle_Click(sender, e, doktorlar[position].Id);
            txtDoktorSil.Click += (sender, e) => TxtDoktorSil_Click(sender, e, doktorlar[position]);


            return satir;
        }

        private void TxtDoktorSil_Click(object sender, EventArgs e, Doktor doktor)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Dikkat !");
            alert.SetMessage("Doktor Hakkında Bütün Bilgiler Silinecek");
            alert.SetButton("ONAYLIYORUM", (c, ev) =>
            {

                doktorService.Sil(doktor.Id);
                Intent intent = new Intent(context, typeof(DoktorListeleActivity));
                context.StartActivity(intent);
            });

            alert.SetButton2("İPTAL", (c, ev) =>
            {
                Intent intent = new Intent(context, typeof(DoktorListeleActivity));
                context.StartActivity(intent);
            });
            alert.Show();
        }

        private void TxtDoktorGüncelle_Click(object sender, EventArgs e, int Id)
        {
            Intent intent = new Intent(context, typeof(DoktorGuncelleActivity));
            intent.PutExtra("guncelleDoktorId", Id);
            context.StartActivity(intent);
        }

        public override int Count
        {
            get { return doktorlar.Count; }
        }

        public override Doktor this[int position]
        {
            get { return doktorlar[position]; }
        }
    }
}