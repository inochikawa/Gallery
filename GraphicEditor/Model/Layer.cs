using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicEditor.Model
{
    public class Layer : Canvas
    {
        public Layer()
        {
            IsActive = true;
            IsSelected = true;
            Background = Brushes.Transparent;
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        }
        
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
    }
}
