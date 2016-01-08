using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace GraphicEditor.Model.Commands
{
    public class InsertImageCommand : ICommand
    {
        private Image _image;
        private Canvas _container;
        private Size f_imageSize;

        public InsertImageCommand(BitmapImage image, Canvas container)
        {
            _image = new Image() { Source = image };
            f_imageSize = new Size((int)image.Width, (int)image.Height);
            _container = container;
        }

        public void Execute()
        {
            if (!_container.Children.Contains(_image))
            {
                if (_container.ActualWidth < f_imageSize.Width ||
                    _container.ActualHeight < f_imageSize.Height)
                {
                    double scale = 1;

                    if(f_imageSize.Width > f_imageSize.Height)
                        scale = _container.ActualWidth / f_imageSize.Width;
                    else
                        scale = _container.ActualHeight / f_imageSize.Height;

                    ScaleTransform scaleTransform = new ScaleTransform(scale, scale);
                    _image.RenderTransform = scaleTransform;
                }
                _container.Children.Add(_image);
            }
        }

        public void Unexecute()
        {
            if (_container.Children.Contains(_image))
                _container.Children.Remove(_image);
        }
    }
}