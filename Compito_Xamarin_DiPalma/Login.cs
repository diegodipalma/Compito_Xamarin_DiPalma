using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Mono.Data.Sqlite;

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

            #region data binding e variabili
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
            var query = "";
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
                Toast.MakeText(this, "Uno o entrambi i campi sono vuoti!", ToastLength.Short).Show();
            }
            else
            {
                var nomedb = "data.db";
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nomedb);
                var connStr = @"Data Source=" + path + ";Version=3;";
                SqliteConnection connessione = new SqliteConnection(connStr);

                try
                {
                    connessione.Open();
                }
                catch (Exception ex)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Eccezione");
                    alert.SetMessage(ex.ToString());
                    alert.Show();
                }

                query = "select count(*) from users where username=@user and password=@pass";
                SqliteCommand queryLogin = new SqliteCommand(query, connessione);
                queryLogin.Parameters.AddWithValue("@user", txtMail.Text);
                queryLogin.Parameters.AddWithValue("@pass", CodificaPassword(txtPass.Text, true));
                var risultato = (Int32)queryLogin.ExecuteScalar();

                if (risultato == 0)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Attenzione");
                    alert.SetMessage("Non è stato trovato nessun utente con questa combinazione di credenziali!");
                    alert.Show();
                    txtMail.Text = "";
                    txtPass.Text = "";
                }
                else

            }
        }

        private void BtnRecover_Click(object sender, EventArgs e)
        {

        }

        public static string CodificaPassword(string password, bool isBase64)
        {
            if (isBase64)
            {
                var base64EncodedBytes = Convert.FromBase64String(password);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            else
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(plainTextBytes);
            }
        }

    }

}

