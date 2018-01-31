using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Mono.Data.Sqlite;
using Android.Content;

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

        public void CopyDatabase(string dataBaseName)
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dataBaseName);

            if (!File.Exists(dbPath))
            {
                var dbAssetStream = Assets.Open(dataBaseName);
                var dbFileStream = new FileStream(dbPath, FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
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
                Toast.MakeText(this, "Uno o entrambi i campi sono vuoti!", ToastLength.Short).Show();
            }
            else
            {
                CopyDatabase("data.db");
                var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data.db");

                using (var conn = new SqliteConnection("Data Source=" + dbPath))
                using (var query = new SqliteCommand("select count(*) from Users where Username=@user and Password=@pass", conn)
                )
                {
                    query.Parameters.AddWithValue("@user", txtMail.Text);
                    query.Parameters.AddWithValue("@pass", txtPass.Text);
                    conn.Open();
                    var risultato = query.ExecuteScalar();
                    if ((int)risultato == 0)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Attenzione");
                        alert.SetMessage("Non è stato trovato nessun utente con questa combinazione di credenziali!");
                        alert.Show();
                        txtMail.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        Intent intent = new Intent(this, typeof(Messaggi));
                        Bundle b = new Bundle();
                        b.PutString("user", txtMail.Text);
                        intent.PutExtras(b);
                        StartActivity(intent);
                    }
                }







            }
        }

        private void BtnRecover_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Funzione non implementata");
            alert.SetMessage("Ti chiedo scusa ma questa funzione non è ancora implementata! Resta in attesa di una seconda versione.");
            alert.Show();
        }

        //public static string CodificaPassword(string password, bool isBase64)
        //{
        //    if (isBase64)
        //    {
        //        var base64EncodedBytes = Convert.FromBase64String(password);
        //        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //    }
        //    else
        //    {
        //        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
        //        return Convert.ToBase64String(plainTextBytes);
        //    }
        //}

    }

}

