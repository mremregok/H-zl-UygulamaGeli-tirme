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
    [Activity(Label = "ProfileActivity", Theme = "@style/AppTheme")]
    public class ProfileActivity : Activity
    {
        IHastaService hastaService;

        private TextView hastaAd, hastaSoyad, hastaTCKN, hastaCinsiyet, hastaAciklama, hastaDogumTarihi;

        private GridView hastaGecmisRandevular;

        private Button hastaProfiliGuncelle;

        public ProfileActivity()
        {
            hastaService = Business.IOCUtil.Container.Resolve<IHastaService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profile_layout);
            // Create your application here

            hastaAd = FindViewById<TextView>(Resource.Id.lblHastaAdi);

            hastaSoyad = FindViewById<TextView>(Resource.Id.lblHastaSoyadi);

            hastaTCKN = FindViewById<TextView>(Resource.Id.lblHastaTCKN);

            hastaCinsiyet = FindViewById<TextView>(Resource.Id.lblHastaCinsiyet);

            hastaAciklama = FindViewById<TextView>(Resource.Id.lblHastaAciklama);

            hastaDogumTarihi = FindViewById<TextView>(Resource.Id.lblHastaDogumTarihi);

            hastaGecmisRandevular = FindViewById<GridView>(Resource.Id.gridGecmisRandevular);

            hastaProfiliGuncelle = FindViewById<Button>(Resource.Id.btnHastaBilgiDuzenle);



        }
    }
}
