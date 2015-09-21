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

        #region WinResizeble
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                Window mainWindow = senderRect.Tag as Window;
                if (senderRect != null)
                {
                    double width = e.GetPosition(mainWindow).X;
                    double height = e.GetPosition(mainWindow).Y;
                    senderRect.CaptureMouse();
                    if (senderRect.Name.ToLower().Contains("right"))
                    {
                        width += 5;
                        if (width > 0)
                            mainWindow.Width = width;
                    }
                    if (senderRect.Name.ToLower().Contains("left"))
                    {
                        width -= 5;
                        mainWindow.Left += width;
                        width = mainWindow.Width - width;
                        if (width > 0)
                        {
                            mainWindow.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                            mainWindow.Height = height;
                    }
                    if (senderRect.Name.ToLower().Contains("top"))
                    {
                        height -= 5;
                        mainWindow.Top += height;
                        height = mainWindow.Height - height;
                        if (height > 0)
                        {
                            mainWindow.Height = height;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
