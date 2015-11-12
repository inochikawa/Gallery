using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using Combogallary.Model.ProxyPattern;
using GraphicEditor.Model;
using GraphicEditor.View.Windows;
using GraphicEditor.ViewModel;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            backgroundCanvas.Children.Add(f_viewModel.GraphicContent.WorkSpace);
        }

        private void openFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter =
                "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                PictureProxy picture = new PictureProxy(dlg.SafeFileName, dlg.FileName);
                picture.Width = image.Width;
                picture.Height = image.Height;
                return;
            }
        }

        private void imageProperties_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void pictureTabView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void backgroundCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            f_viewModel.GraphicContent.MousePositionOnWindow = e.GetPosition(backgroundCanvas);
        }
    }
}
