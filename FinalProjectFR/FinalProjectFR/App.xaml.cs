using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProjectFR
{
    public partial class App : Application
    {
        private static OpDataBase dataBase;
        public static OpDataBase OpDataBase
        {
            get
            {
                if (dataBase == null)
                {
                    dataBase = new OpDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "openings.db3"));
                }
                return dataBase;
            }
        }
        public NavigationPage m { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            m = MainPage as NavigationPage;
            
        //MainPage = new Opening();
    }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
