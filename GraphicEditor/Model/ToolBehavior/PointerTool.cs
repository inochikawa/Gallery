using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace GraphicEditor.Model.ToolBehavior
{
    public class PointerTool : Tool
    {
        List<Point> logList= new List<Point>();
             
        public PointerTool(GraphicContent graphicContent)
            : base(graphicContent)
        {
            Name = "Pointer";
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
                logList.Add(GraphicContent.MousePositionOnWindow);
                GraphicContent.Command.Move(GraphicContent.WorkSpace,
                    GraphicContent.MousePositionOnWindow.X - GraphicContent.DeltaPoint.X,
                    GraphicContent.MousePositionOnWindow.Y - GraphicContent.DeltaPoint.Y);
            }
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            logList.Clear();
            Mouse.OverrideCursor = Cursors.Arrow;
            GraphicContent.Command.EndMove(GraphicContent.WorkSpace,
                    GraphicContent.MousePositionOnWindow.X - GraphicContent.DeltaPoint.X,
                    GraphicContent.MousePositionOnWindow.Y - GraphicContent.DeltaPoint.Y);
        }
    }
}
