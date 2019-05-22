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
    [Activity(Label = "Randevunuz onaylandı", Theme = "@style/AppTheme")]
    public class RandevuOnaylandiActivity : AppCompatActivity
    {
        Button btnRandevuOnayGoruntule;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.randevuOnaylandi_layout);

            btnRandevuOnayGoruntule = FindViewById<Button>(Resource.Id.btnRandevuOnayGoruntule);

            btnRandevuOnayGoruntule.Click += BtnRandevuOnayGoruntule_Click;
            // Create your application here
        }

        private void BtnRandevuOnayGoruntule_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HastaRandevularimActivity));
            intent.PutExtra("tc", Intent.GetStringExtra("tc"));
            StartActivity(intent);
        }
    }
}