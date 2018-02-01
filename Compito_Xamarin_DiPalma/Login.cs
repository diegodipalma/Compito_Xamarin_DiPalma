using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Mono.Data.Sqlite;
using Android.Content;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Compito_Xamarin_DiPalma
{
    [Activity(Label = "BlueChat", MainLauncher = true)]
    public class Log : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            var user = FindViewById<TextView>(Resource.Id.txtUser);
            var pswd = FindViewById<TextView>(Resource.Id.txtPswd);
            var btnlogin = FindViewById<Button>(Resource.Id.btnLogin);
            var btnregistra = FindViewById<Button>(Resource.Id.btnRegistra);
            var btncontatti = FindViewById<Button>(Resource.Id.btnContatti);

            //creazione metodi evento
            btncontatti.Click += Btncontatti_Click;

        }

        private void Btncontatti_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionSend);
            intent.setType("plain/text");
            intent.putExtra(Intent.ExtraEmail, new String[] { "diegodipalma@outlook.com" });
            intent.putExtra(Intent.ExtraSubject, "Contatti");
            intent.putExtra(Intent.ExtraText, "Ciao,");
            startActivity(Intent.createChooser(intent, ""));
        }

        private void copyAssetDataBase(string fromfile, string tofile)
        {
            using (var source = Assets.Open(fromfile))
            using (var dest = OpenFileOutput(tofile, FileCreationMode.Private))
            {
                source.CopyTo(dest);
            }
        }

    }
}