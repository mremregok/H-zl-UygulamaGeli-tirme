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
    [Activity(Label = "Randevu kapatıldı", Theme = "@style/AppTheme")]
    public class AdminRandevuKapandiActivity : AppCompatActivity
    {
        Button btnRandevuKapatDon;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.adminRandevuKapandi_layout);

            btnRandevuKapatDon = FindViewById<Button>(Resource.Id.btnRandevuKapatDon);

            btnRandevuKapatDon.Click += BtnRandevuKapatDon_Click1;
            // Create your application here
        }

        private void BtnRandevuKapatDon_Click1(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AdminRandevuKapatActivity));
            StartActivity(intent);
        }
    }
}