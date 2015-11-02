using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GraphicEditor.Model
{
    public class MoveThumb : Thumb
    {
        private DesignerItem f_designerItem;
        private DesignerCanvas f_designerCanvas;

        public MoveThumb()
        {
            DragStarted += new DragStartedEventHandler(this.MoveThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.f_designerItem = DataContext as DesignerItem;

            if (this.f_designerItem != null)
            {
                this.f_designerCanvas = VisualTreeHelper.GetParent(this.f_designerItem) as DesignerCanvas;
            }
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.f_designerItem != null && this.f_designerCanvas != null && this.f_designerItem.IsSelected)
            {
                double minLeft = 0;
                double minTop = 0;

                foreach (DesignerItem item in this.f_designerCanvas.SelectedItems)
                {
                    minLeft = Canvas.GetLeft(item);
                    minTop = Canvas.GetTop(item);
                }

                double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                double deltaVertical = Math.Max(-minTop, e.VerticalChange);

                foreach (DesignerItem item in this.f_designerCanvas.SelectedItems)
                {
                    Canvas.SetLeft(item, Canvas.GetLeft(item) + deltaHorizontal);
                    Canvas.SetTop(item, Canvas.GetTop(item) + deltaVertical);
                }

                this.f_designerCanvas.InvalidateMeasure();
                e.Handled = true;
            }
        }
    }
}
