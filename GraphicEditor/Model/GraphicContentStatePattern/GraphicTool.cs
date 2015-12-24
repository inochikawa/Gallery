using System.Windows.Media;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public abstract class GraphicTool : Tool
    {
        protected GraphicTool(GraphicContent graphicContent) : base(graphicContent)
        {
            Color = new Color();
            Color = Colors.White;
        }

        public Color Color { get; set; }

        public abstract void UpdateColor(Color color);
    }
}