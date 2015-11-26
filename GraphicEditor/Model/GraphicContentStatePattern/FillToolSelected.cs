using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class FillToolSelected : GraphicContentState, ITool
    {
        private Color f_color;

        public FillToolSelected(GraphicContent graphicContent) : base(graphicContent)
        {
            f_color = Colors.White;
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
                Fill = new SolidColorBrush(f_color)
            };
            GraphicContent.Command.Insert(rect, GraphicContent.SelectedLayer);
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
        }

        public void UpdateColor(Color color)
        {
            f_color = color;
        }
    }
}
