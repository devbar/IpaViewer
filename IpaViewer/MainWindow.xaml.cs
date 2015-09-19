using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IpaLib;
using Microsoft.Win32;

namespace IpaViewer {

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            if (!string.IsNullOrEmpty(App.OpenFile)) {
                LoadIpa(App.OpenFile);
            }
        }

        private void MenuItemOpen_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "IPA-Files (*.ipa)|*.ipa"};

            if (openFileDialog.ShowDialog() == true) {
                LoadIpa(openFileDialog.FileName);
            }
        }

        private void LoadIpa(string path)
        {
            using (var ipaService = new IpaService(new ZipArchiveFactory())) {
                var ipaFile = ipaService.FromFile(path);
                MainGrid.DataContext = ipaFile;
            }
        }
    }
}
