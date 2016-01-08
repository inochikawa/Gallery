using System.Windows.Input;

namespace GraphicEditor.Model.ToolBehavior
{
    public class NoTool : Tool
    {
        public NoTool(GraphicContent graphicContent)
            : base(graphicContent)
        {
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
