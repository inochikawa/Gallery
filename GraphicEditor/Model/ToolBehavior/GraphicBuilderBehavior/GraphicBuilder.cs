using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace GraphicEditor.Model.ToolBehavior.GraphicBuilderBehavior
{
    public abstract class GraphicBuilder
    {
        public GraphicBuilder()
        {
            Panel = new Canvas();
        }

        public Canvas Panel { get; set; }

        public GraphicBuilder BuildLayer(Layer layer)
        {
            Panel.Children.Add(layer.Clone());
            Panel.Width = layer.Width;
            Panel.Height = layer.Height;
            return this;
        }
        
        public abstract void Buid(string fileName);

        public abstract string FileName();
    }
}