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
    [Activity(Label = "Anasayfa", Theme = "@style/AppTheme")]
    public class AdminAnaSayfaActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.adminAnaSayfa_layout);

            // Create your application here
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.adminMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnAnasayfa:
                    var intent = new Intent(this, typeof(AdminAnaSayfaActivity));
                    StartActivity(intent);
                    return true;
                case Resource.Id.menuBtnHastaneEkle:
                    var intent1 = new Intent(this, typeof(HastaneEkleActivity));
                    StartActivity(intent1);
                    return true;
                case Resource.Id.menuBtnBolumEkle:
                    var intent2 = new Intent(this, typeof(BolumEkleActivity));
                    StartActivity(intent2);
                    return true;
                case Resource.Id.menuBtnDoktorEkle:
                    var intent3 = new Intent(this, typeof(DoktorEkleActivity));
                    StartActivity(intent3);
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }
    }
}