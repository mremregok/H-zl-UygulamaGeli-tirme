using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Autofac;
using Business.Abstract;
using Entities.Concrete;

namespace AndroidApp
{
    [Activity(Label = "Üye Ol", Theme = "@style/AppTheme")]
    public class UyeOlActivity : AppCompatActivity
    {
        private Button btnUyeOl, btnTarihSec;
        private EditText txtTC, txtAd, txtSoyad, txtPass, txtDate, txtMail;
        private RadioButton rbErkek, rbKadin;
        ILoginService loginService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.uyeOl_layout);

            loginService = Business.IOCUtil.Container.Resolve<ILoginService>();

            btnUyeOl = FindViewById<Button>(Resource.Id.btnUyeOl);
            btnTarihSec = FindViewById<Button>(Resource.Id.btnTarihSec);
            txtTC = FindViewById<EditText>(Resource.Id.txtTC);
            txtAd = FindViewById<EditText>(Resource.Id.txtAd);
            txtSoyad = FindViewById<EditText>(Resource.Id.txtSoyad);
            txtPass = FindViewById<EditText>(Resource.Id.txtPass);
            txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            txtMail = FindViewById<EditText>(Resource.Id.txtMail);
            rbErkek = FindViewById<RadioButton>(Resource.Id.rbErkek);
            rbKadin = FindViewById<RadioButton>(Resource.Id.rbKadin);

            btnTarihSec.Click += BtnTarihSec_Click;
            btnUyeOl.Click += BtnUyeOl_Click;
        }

        private void BtnTarihSec_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                txtDate.Text = time.ToLongDateString();
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtnUyeOl_Click(object sender, EventArgs e)
        {
            Hasta hasta = new Hasta();
            hasta.Ad = txtAd.Text;
            hasta.Soyad = txtSoyad.Text;
            hasta.TC = txtTC.Text;
            hasta.Sifre = txtPass.Text;
            hasta.DogumTarihi = Convert.ToDateTime(txtDate.Text);
            hasta.Mail = txtMail.Text;

            if (rbErkek.Checked)
            {
                hasta.Cinsiyet = 1;
            }
            else hasta.Cinsiyet = 0;

            //hasta.IsMailVerified = false;
            bool isRegistered = loginService.UyeOl(hasta);

            if (isRegistered)
            {
                //loginService.SendVerificationMail(txtMail.Text);
                var intent = new Intent(this, typeof(MailOnayActivity));
                intent.PutExtra("mail", txtMail.Text);
                StartActivity(intent);
            }
        }

        public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
        {
            // TAG can be any string of your choice.  
            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
            // Initialize this value to prevent NullReferenceExceptions.  
            Action<DateTime> _dateSelectedHandler = delegate { };
            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }
            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month, currently.Day);
                return dialog;
            }
            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!  
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
        }
    }
}