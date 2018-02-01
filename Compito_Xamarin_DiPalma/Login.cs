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
        string pathDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bluechat.sqlite3");
        TextView user, pswd;
        Button btnlogin, btnregistra, btncontatti;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            //mostro istruzioni
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Istruzioni");
            alert.SetMessage("Utenti registrati: inserire le credenziali e premere \"Login\"\nUtenti non registrati: inserire le credenziali desiderate e premere \"Registra\"!");
            alert.Show();

            user = FindViewById<TextView>(Resource.Id.txtUser);
            pswd = FindViewById<TextView>(Resource.Id.txtPswd);
            btnlogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnregistra = FindViewById<Button>(Resource.Id.btnRegistra);
            btncontatti = FindViewById<Button>(Resource.Id.btnContatti);

            //creazione metodi evento
            btnregistra.Click += Btnregistra_Click;
            btncontatti.Click += Btncontatti_Click;

        }

        private void Btnregistra_Click(object sender, EventArgs e)
        {
            if (iControlliSonoVuoti())
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Attenzione!");
                alert.SetMessage("I campi Username e Password devono contenere un valore!");
                alert.Show();
            }
            else
            {
                //da eseguire per copiare il db da assets alla personal folder
                /*SOLO UNA VOLTA!!!*/
                CopyDatabase("bluechat.sqlite3");

                //connessione al database
                using (SqliteConnection cn = new SqliteConnection(@"Data Source=" + pathDB + @";Version=3;"))
                {
                    cn.Open();
                    //using (SqliteCommand cmd = new SqliteCommand("select count(*) from users where user='@user'", cn))
                    using (SqliteCommand cmd = new SqliteCommand("select count(*) from `users` where user=`@user`", cn))
                    {
                        cmd.Parameters.AddWithValue("@user", user.Text);

                        if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Attenzione!");
                            alert.SetMessage("C'è già un utente con questo Username!");
                            alert.Show();
                            user.Text = "";
                        }
                        else
                        {
                            SqliteCommand inserisciutente = new SqliteCommand("insert into users(username,password) values('@user','@pswd')", cn);
                            inserisciutente.Parameters.AddWithValue("@user", user.Text);
                            inserisciutente.Parameters.AddWithValue("@pswd", pswd.Text);
                        }
                    }
                    Toast.MakeText(this, "Benvenuto " + user + "!", ToastLength.Short);
                }
            }
        }

        private bool iControlliSonoVuoti()
        {
            if (user.Text == "" || user.Text == null || pswd.Text == "" || pswd.Text == null)
            {
                return true;
            }
            else return false;
        }

        private void Btncontatti_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionSendto);
            intent.SetType("plain/text");
            intent.PutExtra(Intent.ExtraEmail, new String[] { "diegodipalma@outlook.com" });
            intent.PutExtra(Intent.ExtraSubject, "Contatti");
            intent.PutExtra(Intent.ExtraText, "Ciao,");
            StartActivity(Intent.CreateChooser(intent, ""));
        }

        private void CopyDatabase(string dataBaseName)
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

    }
}