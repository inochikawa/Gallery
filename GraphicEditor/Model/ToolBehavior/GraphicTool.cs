using System.Windows.Media;
using GraphicEditor.Model.ToolBehavior.ToolProperties;

namespace GraphicEditor.Model.ToolBehavior
{
    public abstract class GraphicTool : Tool
    {
        public GraphicTool(GraphicContent graphicContent) : base(graphicContent)
        {
        }
    }
}