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
            GraphicContent.Command.StartMove(GraphicContent.WorkSpace,
                    GraphicContent.MousePositionOnWindow.X - GraphicContent.DeltaPoint.X,
                    GraphicContent.MousePositionOnWindow.Y - GraphicContent.DeltaPoint.Y);
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                GraphicContent.DeltaPoint = e.GetPosition(GraphicContent.SelectedLayer);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                GraphicContent.Command.Move(GraphicContent.WorkSpace,
                    GraphicContent.MousePositionOnWindow.X - GraphicContent.DeltaPoint.X,
                    GraphicContent.MousePositionOnWindow.Y - GraphicContent.DeltaPoint.Y);
            }
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            GraphicContent.Command.EndMove(GraphicContent.WorkSpace,
                    GraphicContent.MousePositionOnWindow.X - GraphicContent.DeltaPoint.X,
                    GraphicContent.MousePositionOnWindow.Y - GraphicContent.DeltaPoint.Y);
        }
    }
}
