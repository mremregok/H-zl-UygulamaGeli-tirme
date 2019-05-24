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
   [Activity(Label = "Eklendi", Theme = "@style/AppTheme")]
    public class DoktorEkleOnayActivity : AppCompatActivity
    {
        Button btnDoktorEkleOnay;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktorEkleOnay_layout);

            btnDoktorEkleOnay = FindViewById<Button>(Resource.Id.btnDoktorEkleOnay);

            btnDoktorEkleOnay.Click += BtnDoktorEkleOnay_Click;
            // Create your application here
        }

        private void BtnDoktorEkleOnay_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AdminAnaSayfaActivity));
            StartActivity(intent);
        }
    }
}