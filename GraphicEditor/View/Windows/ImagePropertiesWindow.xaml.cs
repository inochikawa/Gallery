using System.Globalization;
using System.Windows;
using Combogallary.Model.ProxyPattern;

namespace GraphicEditor.View.Windows
{
    /// <summary>
    /// Interaction logic for ImagePropertiesWindow
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
            if (picture == null)
                return;
            txtDimension.Text = picture.Dimension;
            txtName.Text = picture.Name;
            txtPath.Text = picture.Location;
            txtSize.Text = picture.Size.ToString(CultureInfo.CurrentCulture);
            previewImage.Source = picture.Preview();
        }
    }
}
