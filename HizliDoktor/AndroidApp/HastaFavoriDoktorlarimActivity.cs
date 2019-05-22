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
using AndroidApp.Resources.Adapter;
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp
{
    [Activity(Label = "Favori Doktorlarım", Theme = "@style/AppTheme")]
    public class HastaFavoriDoktorlarimActivity : AppCompatActivity
    {
        ListView listView;
        List<Favori> favoriler;
        IFavoriService favoriService;
        IHastaService hastaService;
        Hasta hasta;

        public HastaFavoriDoktorlarimActivity()
        {
            favoriService = Business.IOCUtil.Container.Resolve<IFavoriService>();
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hastaFavoriDoktorlarim_layout);

            listView = FindViewById<ListView>(Resource.Id.customFavorilerimListView);

            hasta = hastaService.Getir(Intent.GetStringExtra("tc"));
            favoriler = favoriService.Favoriler(hasta.Id);

            HastaFavorilerimAdapter adapter = new HastaFavorilerimAdapter(this, favoriler, hasta);
            listView.Adapter = adapter;

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
                        var intent = new Intent(this, typeof(HastaRandevularimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnHastaFavorilerim:
                    {
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