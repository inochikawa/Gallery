using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace GraphicEditor.Model.ToolBehavior
{
    public class FillTool : GraphicTool
    {
        public FillTool(GraphicContent graphicContent) : base(graphicContent)
        {
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (GraphicContent.SelectedLayer == null)
                return;

            Rectangle rect = new Rectangle()
            {
                Width = GraphicContent.WorkSpace.ActualWidth,
                Height = GraphicContent.WorkSpace.ActualHeight,
                Fill = new SolidColorBrush((Color)GraphicContent.GraphicToolProperties.Color)
            };
            GraphicContent.Command.Insert(rect, GraphicContent.SelectedLayer);
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
