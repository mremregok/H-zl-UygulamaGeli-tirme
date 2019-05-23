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
    public class HastaneListeleAdapter : BaseAdapter<Hastane>
    {

        private readonly Context context;
        private IHastaneService hastaneService;
        private List<Hastane> hastaneler;


        public HastaneListeleAdapter(Context context, List<Hastane> hastaneler)
        {
            this.context = context;
            this.hastaneler = hastaneler;
            hastaneService = Business.IOCUtil.Container.Resolve<IHastaneService>();

        }
        public override Hastane this[int position]
        {
            get { return hastaneler[position]; }
        }

        public override int Count
        {
            get { return hastaneler.Count; }
        }

        public override long GetItemId(int position)
        {
            return hastaneler[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View satir = convertView;
            if (satir == null) satir = LayoutInflater.From(context).Inflate(Resource.Layout.hastaneListeleItem_layout, null, false);

            TextView txtHastaneSilHastaneAdi = satir.FindViewById<TextView>(Resource.Id.txtHastaneSilHastaneAdi);
            TextView txtHastaneGüncelle = satir.FindViewById<TextView>(Resource.Id.txtHastaneGüncelle);
            TextView txtHastaneSil = satir.FindViewById<TextView>(Resource.Id.txtHastaneSil);

            txtHastaneSil.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;
            txtHastaneGüncelle.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;

            txtHastaneSilHastaneAdi.Text = hastaneler[position].Ad;
            txtHastaneGüncelle.Text = "Güncelle";
            txtHastaneSil.Text = "Sil";

            txtHastaneGüncelle.Click += (sender, e) => txtHastaneGüncelle_Click(sender, e, hastaneler[position].Id);
            txtHastaneSil.Click += (sender, e) => txtHastaneSil_Click(sender, e, hastaneler[position]);


            return satir;
        }

        private void txtHastaneGüncelle_Click(object sender, EventArgs e, int Id)
        {

            Intent intent = new Intent(context, typeof(HastaneGuncelleActivity));
            intent.PutExtra("guncelleHastaneId", Id);
            context.StartActivity(intent);

        }

        private void txtHastaneSil_Click(object sender, EventArgs e, Hastane hastane)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(context);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Dikkat !");
            alert.SetMessage("Hastane silinirken hastaneye ait doktorlar, bölümler ve bütün randevular silinecek.");
            alert.SetButton("ONAYLIYORUM", (c, ev) =>
            {
              
                hastaneService.Sil(hastane.Id);
                Intent intent = new Intent(context, typeof(HastaneListeleActivity));
                context.StartActivity(intent);
            });
            
            alert.SetButton2("İPTAL", (c, ev) => 
            {
                Intent intent = new Intent(context, typeof(HastaneListeleActivity));
                context.StartActivity(intent);
            });
            alert.Show();
            

        }

    }
}
  