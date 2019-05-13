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
                button.LayoutParameters = new ViewGroup.LayoutParams(140, 60);
                button.SetPadding(8, 8, 8, 8);
                button.SetBackgroundColor(Color.Gray);
                button.SetTextColor(Color.Green);
                button.Text = listDates[position].ToShortTimeString();
                button.Click += Button_Click;
            }
            else button = (Button)convertView;

            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            Toast.MakeText(context, button.Text, ToastLength.Long).Show();
        }
    }
}