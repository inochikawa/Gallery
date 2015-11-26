using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicEditor.View.Styles.Helpers
{
    public static class TransparentViewVisualBrush
    {
        public static VisualBrush RenderTransparent()
        {
            return new VisualBrush
            {
                TileMode = TileMode.Tile,
                Viewport = new Rect(0, 0, 20, 20),
                ViewportUnits = BrushMappingMode.Absolute,
                Visual =
                    new Image()
                    {
                        Source =
                            new BitmapImage(
                                new Uri(
                                    @"pack://application:,,,/GraphicEditor;component/View/Resources/Images/transparent.png"))
                    }
            };
        }
    }
}
