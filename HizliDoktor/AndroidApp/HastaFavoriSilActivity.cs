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

namespace AndroidApp
{
    [Activity(Label = "Hasta Favorilerim Sil", Theme = "@style/AppTheme")]
    public class HastaFavoriSilActivity : AppCompatActivity
    {
        Button btnFavoriSilGoruntule;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaFavoriSil_layout);

            btnFavoriSilGoruntule = FindViewById<Button>(Resource.Id.btnFavoriSilGoruntule);

            btnFavoriSilGoruntule.Click += BtnFavoriSilGoruntule_Click;

            // Create your application here
        }

        private void BtnFavoriSilGoruntule_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HastaFavoriDoktorlarimActivity));
            intent.PutExtra("tc", Intent.GetStringExtra("tc"));
            StartActivity(intent);
        }
    }
}