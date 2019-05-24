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
    class BolumListeleAdapter : BaseAdapter<Bolum>
    {
        private readonly Context context;
        private IBolumService bolumService;
        private List<Bolum> bolumler;
        
        public BolumListeleAdapter(Context context, List<Bolum> bolumler)
        {
            this.context = context;
            this.bolumler = bolumler;
            bolumService = Business.IOCUtil.Container.Resolve<IBolumService>();
        }

        public override int Count
        {
            get { return bolumler.Count; }
        }


        public override long GetItemId(int position)
        {
            return bolumler[position].Id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View satir = convertView;
            if (satir == null) satir = LayoutInflater.From(context).Inflate(Resource.Layout.bolumListeleItem_layout, null, false);

            TextView txtBolumSilBolumAdi = satir.FindViewById<TextView>(Resource.Id.txtBolumSilBolumAdi);
            TextView txtBolumGüncelle = satir.FindViewById<TextView>(Resource.Id.txtBolumGüncelle);
            TextView txtBolumSil = satir.FindViewById<TextView>(Resource.Id.txtBolumSil);

            txtBolumSil.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;
            txtBolumGüncelle.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;
            
            txtBolumSilBolumAdi.Text = bolumler[position].Ad;
            txtBolumGüncelle.Text = "Güncelle";
            txtBolumSil.Text = "Sil";

            txtBolumGüncelle.Click += (sender, e) => TxtBolumGüncelle_Click(sender, e, bolumler[position].Id);
            txtBolumSil.Click += (sender, e) => TxtBolumSil_Click(sender, e, bolumler[position]);


            return satir;
        }

        private void TxtBolumSil_Click(object sender, EventArgs e, Bolum bolum)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Dikkat !");
            alert.SetMessage("Bölüm silinirken bölüme ait doktorlar ve bütün randevular silinecek.");
            alert.SetButton("ONAYLIYORUM", (c, ev) =>
            {

                bolumService.Sil(bolum.Id);
                Intent intent = new Intent(context, typeof(BolumListeleActivity));
                context.StartActivity(intent);
            });

            alert.SetButton2("İPTAL", (c, ev) =>
            {
                Intent intent = new Intent(context, typeof(BolumListeleActivity));
                context.StartActivity(intent);
            });
            alert.Show();
        }

        private void TxtBolumGüncelle_Click(object sender, EventArgs e, int Id)
        {
            Intent intent = new Intent(context, typeof(BolumGuncelleActivity));
            intent.PutExtra("guncelleBolumId", Id);
            context.StartActivity(intent);
        }

        public override Bolum this[int position]
        {
            get { return bolumler[position]; }
        }
    }

    class BolumListeleAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}