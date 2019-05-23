using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            
            if (txtAd.Text == null || txtSoyad.Text == null || txtTC.Text == null || txtPass.Text ==null || txtDate.Text == null || txtMail == null)
            {
                Toast.MakeText(Application.Context, "Üyelik oluşturulamadı. Lütfen bilgilerin tamamını doldurduğunuzdan emin olun.", ToastLength.Long).Show();
                return;
            }

            if (txtTC.Text.Length < 11)
            {
                Toast.MakeText(Application.Context, "TC kimlik no 11 hane az olamaz.", ToastLength.Long).Show();
                return;
            }

            if (!txtAd.Text.Contains('1') || !txtAd.Text.Contains('2') || !txtAd.Text.Contains('3') || !txtAd.Text.Contains('4') || !txtAd.Text.Contains('5') || !txtAd.Text.Contains('6') || !txtAd.Text.Contains('7') || !txtAd.Text.Contains('8') || !txtAd.Text.Contains('9') || !txtAd.Text.Contains('0') || !txtAd.Text.Contains('*') || !txtAd.Text.Contains('/') || !txtAd.Text.Contains('.') || !txtAd.Text.Contains('!') || !txtAd.Text.Contains(','))
            {
                Toast.MakeText(Application.Context, "İsimde özel karakter yada rakam kullanılamaz.", ToastLength.Long).Show();
                return;
            }

            if (!txtSoyad.Text.Contains('1') || !txtSoyad.Text.Contains('2') || !txtSoyad.Text.Contains('3') || !txtSoyad.Text.Contains('4') || !txtSoyad.Text.Contains('5') || !txtSoyad.Text.Contains('6') || !txtSoyad.Text.Contains('7') || !txtSoyad.Text.Contains('8') || !txtSoyad.Text.Contains('9') || !txtSoyad.Text.Contains('0') || !txtSoyad.Text.Contains('*') || !txtSoyad.Text.Contains('/') || !txtSoyad.Text.Contains('.') || !txtSoyad.Text.Contains('!') || !txtSoyad.Text.Contains(','))
            {
                Toast.MakeText(Application.Context, "Soyad da özel karakter yada rakam kullanılamaz.", ToastLength.Long).Show();
                return;
            }

            if (txtPass.Text.Length < 6)
            {
                Toast.MakeText(Application.Context, "Oluşturulan şifre 6 karakterden az olamaz.", ToastLength.Long).Show();
                return;
            }
            if (!txtMail.Text.Contains('@') && !txtMail.Text.Contains('.'))
            {
                Toast.MakeText(Application.Context, "Hatalı mail adresi girdiniz.", ToastLength.Long).Show();
                return;
            }
            Hasta hasta = new Hasta();
            hasta.Ad = txtAd.Text;
            hasta.Soyad = txtSoyad.Text;
            hasta.TC = txtTC.Text;
            hasta.Sifre = txtPass.Text;
            hasta.DogumTarihi = Convert.ToDateTime(txtDate.Text);
            hasta.Mail = txtMail.Text;
            hasta.IsMailVerified = false;

            if (rbErkek.Checked) hasta.Cinsiyet = 1;
            else hasta.Cinsiyet = 0;

            bool isRegistered = loginService.UyeOl(hasta);

            if (isRegistered)
            {
                var intent = new Intent(this, typeof(MailOnayActivity));
                intent.PutExtra("mail", txtMail.Text);
                StartActivity(intent);
            }

            else
            {
                Toast.MakeText(Application.Context, "Girilen bilgilere ait üyelik vardır. Lütfen yeni bilgiler giriniz.", ToastLength.Long).Show();
                return;
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
                DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month - 1, currently.Day);

                dialog.DatePicker.MaxDate = (long)(DateTime.Now.Date - new DateTime(1970, 1, 2)).TotalMilliseconds + 1000 * 60 * 60 * 24;
                dialog.DatePicker.MinDate = 1970;
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