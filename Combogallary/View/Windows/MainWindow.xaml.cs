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
using System.Windows.Media.Animation;
using ActiveRecordPattern;
using Combogallary.Themes;
using Combogallary.Model;
using Combogallary.ViewModel;

namespace Combogallary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowVM = new MainWindowViewModel();
            if (mainWindowVM.AlbumCount == 0) btnCreateAlbum.Visibility = System.Windows.Visibility.Visible;
            
        }

        private string welcomeString
        {
            get
            {
                if(DateTime.Now.TimeOfDay <= new TimeSpan(12,00,00))
                { return "Good morning,"; }
                if (DateTime.Now.TimeOfDay <= new TimeSpan(18, 00, 00))
                { return "Good afternoon,"; }
                if (DateTime.Now.TimeOfDay <= new TimeSpan(21, 00, 00))
                { return "Good eaving,"; } 
                if (DateTime.Now.TimeOfDay <= new TimeSpan(23, 59, 59))
                { return "Good night,"; }
                return "";
            }
        }

        private void btnCreateAlbum_Click(object sender, RoutedEventArgs e)
        {
            new CreateAlbumView().ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CreateAlbumView().ShowDialog();
        }

        
    }
}
