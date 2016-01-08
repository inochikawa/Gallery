using System.Windows.Controls;

namespace GraphicEditor.Model.ToolBehavior
{
    public class GraphicBuilder
    {
        private Canvas f_panel;

        public GraphicBuilder()
        {
            f_panel = new Canvas();
        }

        public GraphicBuilder BuildLayer(Layer layer)
        {
            f_panel.Children.Add(layer.Clone());
            f_panel.Width = layer.Width;
            f_panel.Height = layer.Height;
            return this;
        }

        public Canvas Buid()
        {
            return f_panel;
        }
    }
}