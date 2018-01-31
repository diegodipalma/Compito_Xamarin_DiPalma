using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLite.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Compito_Xamarin_DiPalma
{
    [Activity(Label = "APP", MainLauncher = true)]
    public class Login : Activity
    {
        Button btnLogin, btnRecover;
        TextView txtMail, txtPass;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            #region data binding
            btnLogin = FindViewById<Button>(Resource.Id._login);
            btnRecover = FindViewById<Button>(Resource.Id._recoverPass);
            txtMail = FindViewById<TextView>(Resource.Id._userMail);
            txtPass = FindViewById<TextView>(Resource.Id._userPass);
            #endregion

            btnLogin.Click += BtnLogin_Click;
            btnRecover.Click += BtnRecover_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            #region check password con regex
            //// check sul campo password se è vuoto o meno

            //var pswd = txtPass.Text;
            //var regexpswd = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

            //if (!Regex.IsMatch(pswd, regexpswd))
            //{
            //    Toast.MakeText(this, "La password non è sicura!", ToastLength.Short);
            //    txtPass.Text = "";
            //} 
            #endregion

            if (String.IsNullOrEmpty(txtMail.Text) ||
                string.IsNullOrWhiteSpace(txtMail.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text))
            {
                Toast.MakeText(this, "Uno o entrambi i campi\nsono vuoti!", ToastLength.Short);
            }
            else
            {
                var nomedb = "data.db";
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nomedb);

                var connString = new SQLiteConnectionString(path, false);
                var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path, true)

                try
                {
                    conn.open()
                }
                catch (Exception ex)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Eccezione");
                    alert.SetMessage(ex.ToString());
                    alert.Show();
                }
            }
        }

        private void BtnRecover_Click(object sender, EventArgs e)
        {

        }


    }

}

