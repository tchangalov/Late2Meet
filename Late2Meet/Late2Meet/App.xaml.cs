using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Late2Meet.Services;
using Late2Meet.Views;
using Late2Meet.Data;
using SQLite;
using System.IO;

namespace Late2Meet
{
    public partial class App : Application
    {
        static L2MDatabase database;

        public static L2MDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new L2MDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "L2MDB.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
