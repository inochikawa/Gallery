using System.Windows.Controls;

namespace GraphicEditor.Model.ToolBehavior.GraphicBuilderBehavior
{
    /// <summary>
    /// Graphic image builder 
    /// </summary>
    public abstract class GraphicBuilder
    {
        protected GraphicBuilder()
        {
            Panel = new Canvas();
        }

        protected Canvas Panel { get; }

        public GraphicBuilder BuildLayer(Layer layer)
        {
            Panel.Children.Add(layer.Clone(false));
            Panel.Width = layer.Width;
            Panel.Height = layer.Height;
            return this;
        }
        
        public abstract void Buid(string fileName);

        public abstract string FileName();
    }
}