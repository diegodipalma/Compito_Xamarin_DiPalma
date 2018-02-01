using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;

namespace Compito_Xamarin_DiPalma
{
    [Activity(Label = "Home", MainLauncher = true)]
    public class Home : Activity
    {

        BluetoothAdapter btAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Home);

            btAdapter = BluetoothAdapter.DefaultAdapter;
            if (btAdapter != null)
            {
                if (!btAdapter.IsEnabled)
                {
                    Intent intentperbluetooth = new Intent(BluetoothAdapter.ActionRequestEnable);
                    StartActivityForResult(intentperbluetooth, 2);
                }
                else return;
            }
            else
            {
                Toast.MakeText(this, "Il Bluetooth non sembra disponibile...", ToastLength.Long).Show();
                Finish();
                return;
            }
        }
    }
}