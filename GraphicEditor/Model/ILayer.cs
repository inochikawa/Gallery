using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GraphicEditor.Model
{
    public interface ILayer
    {
        string Name { get; set; }
        BitmapImage Preview();
        Layer Clone();
    }
}
