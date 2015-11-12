using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class LineToolSelected : GraphicContentState
    {
        private Brush f_color;
        private double f_thickness;
        private double f_opacity;
        private Layer f_layer;
        private double f_softness;
        private Point f_startPoint;
        private Point f_endPoint;
        private Line f_line;

        public LineToolSelected(GraphicContent graphicContent)
            :base(graphicContent)
        {
            f_color = Brushes.Brown;
            f_thickness = 10;
            f_opacity = 1;
            f_softness = 10;
            f_layer = base.GraphicContent.SelectedLayer();
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (!f_layer.IsActive)
                return;

            f_startPoint = e.GetPosition(f_layer);
            // create new line and set first point
            reCreateLine();
            f_line.X1 = f_startPoint.X;
            f_line.Y1 = f_startPoint.Y;
            // set initial value to last point
            f_line.X2 = f_startPoint.X;
            f_line.Y2 = f_startPoint.Y;
            // add new line to the layer
            f_layer.Children.Add(f_line);
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
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
            return;
        }

        private void reCreateLine()
        {
            f_line = new Line();
            f_line.Stroke = f_color;
            f_line.StrokeThickness = f_thickness;
        }
    }
}
