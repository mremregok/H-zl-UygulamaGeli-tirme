using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace AndroidApp.Resources.Adapter
{
    public class GridViewAdapter : BaseAdapter
    {
        public override int Count => listDates.Count;

        private Button tempBtn;
        public string selectedTime;
        List<DateTime> listDates;
        Context context;

        public GridViewAdapter(List<DateTime> _list, Context _context)
        {
            listDates = _list;
            context = _context;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Button button = null;

            if (convertView == null)
            {
                button = new Button(context);
                button.LayoutParameters = new ViewGroup.LayoutParams(140, 100);
                button.SetPadding(8, 8, 8, 8);
                button.TextSize = 14;
                button.SetBackgroundColor(Color.ParseColor("#80e27e"));
                button.SetTextColor(Color.ParseColor("#ffffff"));
                button.Text = listDates[position].ToShortTimeString();
                button.Click += Button_Click;
            }
            else button = (Button)convertView;

            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (tempBtn != null)
            {
                tempBtn.SetBackgroundColor(Color.ParseColor("#80e27e"));
                selectedTime = string.Empty;
            }

            if (tempBtn == button)
            {
                tempBtn.SetBackgroundColor(Color.ParseColor("#80e27e"));
                tempBtn = null;
                selectedTime = string.Empty;
            }
            else
            {
                button.SetBackgroundColor(Color.ParseColor("#b2dfdb"));
                tempBtn = button;
                selectedTime = button.Text;
            }
        }

        public DateTime GetSelectedTime()
        {
            if (string.IsNullOrEmpty(selectedTime)) return new DateTime();
            return listDates.SingleOrDefault(x => x.ToShortTimeString() == selectedTime);
        }
    }
}