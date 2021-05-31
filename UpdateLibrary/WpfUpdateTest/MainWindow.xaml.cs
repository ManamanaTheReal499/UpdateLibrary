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
using UpdateLibrary;

namespace WpfUpdateTest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UpdateController updateController = new UpdateController("Shabinder", "SpotiFlyer");

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void UI_ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var releases = updateController.GetResponse();
            UI_ComboBoxReleases.Items.Clear();
            Array.ForEach(releases, (x) => { UI_ComboBoxReleases.Items.Add(x.tag_name); });

           // UI_ImageUploaderPP.Source = releases[0].assets[0].uploader.avatar_url.GetUri().GetImageSource();
        }

        private void UI_ComboBoxSelectionChanged(object sender, SizeChangedEventArgs e)
        {
           
        }
    }
}
