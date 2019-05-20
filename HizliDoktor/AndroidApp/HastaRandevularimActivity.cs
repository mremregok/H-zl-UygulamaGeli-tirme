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
    [Activity(Label = "Randevularım", Theme = "@style/AppTheme")]
    public class HastaRandevularimActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaRandevularim_layout);

            // Create your application here
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.hastaMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnHastaProfilim:
                    {
                        var intent = new Intent(this, typeof(HastaProfilimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent); return true;
                    }
                case Resource.Id.menuBtnHastaRandevuAl:
                    {
                        var intent = new Intent(this, typeof(RandevuAlActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaRandevuListele:
                    {
                        return true;
                    }
                case Resource.Id.menuBtnHastaFavorilerim:
                    {
                        var intent = new Intent(this, typeof(HastaFavoriDoktorlarimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaCikisYap:
                    {
                        var intent = new Intent(this, typeof(GirisYapActivity));
                        StartActivity(intent);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}