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

namespace AndroidApp
{
    [Activity(Label = "Mail Onayı", Theme = "@style/AppTheme")]
    public class MailOnayActivity : AppCompatActivity
    {
        private Button btnDogrula;
        private EditText txtKod;
        private ImageButton btnImg;
        ILoginService loginService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mailOnay_layout);

            loginService = Business.IOCUtil.Container.Resolve<ILoginService>();

            btnDogrula = FindViewById<Button>(Resource.Id.btnDogrula);
            txtKod = FindViewById<EditText>(Resource.Id.txtKod);
            btnImg = FindViewById<ImageButton>(Resource.Id.btnImg);

            btnDogrula.Click += BtnDogrula_Click;
            btnImg.Click += BtnImg_Click;


        }

        private void BtnImg_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnDogrula_Click(object sender, EventArgs e)
        {

        }
    }
}