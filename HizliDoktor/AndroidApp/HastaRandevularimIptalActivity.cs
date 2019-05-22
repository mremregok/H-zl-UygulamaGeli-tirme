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
    [Activity(Label = "Randevu İptali", Theme = "@style/AppTheme")]
    public class HastaRandevularimIptalActivity : AppCompatActivity
    {
        Button btnIptal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaRandevularimIptal_layout);

            btnIptal = FindViewById<Button>(Resource.Id.btnRandevuIptalGoruntule);

            btnIptal.Click += BtnIptal_Click;
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HastaRandevularimActivity));
            intent.PutExtra("tc", Intent.GetStringExtra("tc"));
            StartActivity(intent);
        }
    }
}