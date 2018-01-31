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
    public class Main : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDatabase = Path.Combine(docsFolder, "data.db");
            if (!File.Exists(pathToDatabase))
            {
                SqliteConnection.CreateFile(pathToDatabase);

                using (var conn = new SqliteConnection(pathToDatabase))
                using (var query = new SqliteCommand(@"CREATE TABLE `Utenti` (`IDUtente` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `Username` TEXT NOT NULL UNIQUE, `Password` TEXT NOT NULL, `Soldi` NUMERIC DEFAULT 10.00 CHECK(Soldi > 0));", conn))
                {

                }
            }

        }

    }
}