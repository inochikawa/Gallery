using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class LineTool : GraphicTool
    {
        private readonly double f_thickness;
        private double f_opacity;
        private readonly Layer f_layer;
        private double f_softness;
        private Point f_startPoint;
        private Point f_endPoint;
        private Line f_line;

        public LineTool(GraphicContent graphicContent)
            : base(graphicContent)
        {
            f_thickness = 10;
            f_opacity = 1;
            f_softness = 10;
            f_layer = GraphicContent.SelectedLayer;
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if(f_layer == null)
                return;

            if (!f_layer.IsActive)
                return;

            f_startPoint = e.GetPosition(f_layer);

            // create new line and set first point
            ReCreateLine();
            f_line.X1 = f_startPoint.X;
            f_line.Y1 = f_startPoint.Y;

            // set initial value to last point
            f_line.X2 = f_startPoint.X;
            f_line.Y2 = f_startPoint.Y;
            GraphicContent.Command.Insert(f_line, f_layer);
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (f_line == null)
                return;

            if (!f_layer.IsActive)
                return;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (f_line == null)
                    return;

                f_endPoint = e.GetPosition(f_layer);

                // set last point
                f_line.X2 = f_endPoint.X;
                f_line.Y2 = f_endPoint.Y;
            }
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            f_line = null;
        }

        private void ReCreateLine()
        {
            f_line = new Line
            {
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = f_thickness
            };
        }

        public override void UpdateColor(Color color)
        {
            Color = color;
        }
    }
}
