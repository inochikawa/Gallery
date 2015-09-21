using ActiveRecordPattern;
using Combogallary.Model;
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
using System.Windows.Shapes;


namespace Combogallary
{
    /// <summary>
    /// Interaction logic for CreateAlbumWindow.xaml
    /// </summary>
    public partial class CreateAlbumView : Window
    {
        List<Picture> _pictres = new List<Picture>();
        public CreateAlbumView()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            foreach (var bitmapImage in ImageProcessing.AddImagesToList(dialog.SelectedPath))
            {
                _pictres.Add(new Picture(bitmapImage));
            }

            foreach (var pic in _pictres)
            {
                lsbPics.Items.Add(pic);
            }

            txtPicCount.Text = _pictres.Count.ToString();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Album album = new Album(txtAlbumName.Text);
            album.Initialize();
            album.Save();
            foreach (var pic in _pictres)
            {
                pic.IdAlbum = album.Id;
                pic.Initialize();
                pic.Save();
            }
        }

        private void lsbPics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPicDimensions.Text = ((Picture)(lsbPics.SelectedItem)).Dimensions;
            txtPicLocation.Text = ((Picture)(lsbPics.SelectedItem)).Location;
            txtPicSize.Text = ((Picture)(lsbPics.SelectedItem)).Size.ToString();
        }
    }
}
