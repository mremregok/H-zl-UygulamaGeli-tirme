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
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp
{
    [Activity(Label = "Profilim", Theme = "@style/AppTheme")]
    public class DoktorProfilimActivity : AppCompatActivity
    {
        private TextView lblHosgeldiniz;
        private IDoktorService doktorService;

        public DoktorProfilimActivity()
        {
            doktorService = Business.IOCUtil.Container.Resolve<IDoktorService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktorAnaSayfa_layout);

            lblHosgeldiniz = FindViewById<TextView>(Resource.Id.lblHoşgeldiniz);
            Doktor doktor = new Doktor();
            doktor = doktorService.Getir(Intent.GetStringExtra("tc"));
            lblHosgeldiniz.Text = "Hoşgeldiniz, "+doktor.Unvan+" Dr."+doktor.Ad+" "+doktor.Soyad;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.doktorMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuBtnDoktorProfilim:
                    {
                        //aynı sayfa
                        return true;
                    }
                case Resource.Id.menuBtnDoktorRandevularim:
                    {
                        var intent = new Intent(this, typeof(DoktorRandevularimActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorSifreDegistir:
                    {
                        var intent = new Intent(this, typeof(DoktorSifreDegistirActivity));
                        intent.PutExtra("tc", Intent.GetStringExtra("tc"));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuBtnDoktorCikisYap:
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