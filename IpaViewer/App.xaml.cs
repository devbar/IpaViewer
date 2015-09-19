using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IpaViewer {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static string OpenFile { get; set; }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0) {
                OpenFile = e.Args[0];
            }
        }
    }
}
