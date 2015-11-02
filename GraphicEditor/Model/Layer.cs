using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace GraphicEditor.Model
{
    public class Layer
    {
        private DesignerCanvas workCanvas;
        private List<DesignerItem> items;

        public Layer()
        {
            IsActive = true;
            IsSelected = true;
            workCanvas = new DesignerCanvas();
            items = new List<DesignerItem>();

            workCanvas.Background = Brushes.White;
            workCanvas.AllowDrop = true;
        }

        public void AddElement(object element)
        {
            items.Add(new DesignerItem());
            items.Last().Content = element;
            workCanvas.Children.Add(items.Last());
        }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
    }
}
