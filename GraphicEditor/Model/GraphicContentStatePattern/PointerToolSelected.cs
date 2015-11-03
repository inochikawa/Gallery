using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class PointerToolSelected : GraphicContentState
    {
        public PointerToolSelected(GraphicContent graphicContent)
            : base(graphicContent)
        {

        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.ScrollAll;
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                base.GraphicContent.DeltaPoint = e.GetPosition(base.GraphicContent.SelectedLayer());

            if (e.LeftButton == MouseButtonState.Pressed && base.GraphicContent.MousePositionOnWindow != null)
            {
                moveWorkSpace(base.GraphicContent.MousePositionOnWindow.X - base.GraphicContent.DeltaPoint.X,
                    base.GraphicContent.MousePositionOnWindow.Y - base.GraphicContent.DeltaPoint.Y);
            }
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void moveWorkSpace(double x, double y)
        {
            Canvas.SetTop(base.GraphicContent.WorkSpace, y);
            Canvas.SetLeft(base.GraphicContent.WorkSpace, x);
        }
    }
}
