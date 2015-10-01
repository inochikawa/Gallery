using Combogallary.Model.ProxyPattern;
using System.Windows;

namespace GraphicEditor.View.Windows
{
    /// <summary>
    /// Interaction logic for ImagePropertiesWindow.xaml
    /// </summary>
    public partial class ImagePropertiesWindow : Window
    {
        public ImagePropertiesWindow()
        {
            InitializeComponent();
        }

        public ImagePropertiesWindow(PictureProxy picture)
        {
            InitializeComponent();

            txtDimension.Text = picture.Dimension;
            txtName.Text = picture.Name;
            txtPath.Text = picture.Location;
            txtSize.Text = picture.Size.ToString();
            previewImage.Source = picture.Preview();
        }
    }
}
