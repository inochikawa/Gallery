﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GraphicEditor.Model
{
    public class ResizeThumb : Thumb
    {
        private DesignerItem designerItem;
        private DesignerCanvas designerCanvas;
        private double f_scale;

        public ResizeThumb()
        {
            DragStarted += new DragStartedEventHandler(this.ResizeThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        public double WidthThumb
        {
            get { return 7 / (designerItem.Width / designerItem.ActualWidth); }
            set { }
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.designerItem = DataContext as DesignerItem;

            if (this.designerItem != null)
            {
                this.designerCanvas = VisualTreeHelper.GetParent(this.designerItem) as DesignerCanvas;
            }
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.designerItem != null && this.designerCanvas != null && this.designerItem.IsSelected)
            {
                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;
                double minDeltaHorizontal = double.MaxValue;
                double minDeltaVertical = double.MaxValue;
                double dragDeltaVertical, dragDeltaHorizontal;

                foreach (DesignerItem item in this.designerCanvas.SelectedItems)
                {
                    minLeft = Math.Min(Canvas.GetLeft(item), minLeft);
                    minTop = Math.Min(Canvas.GetTop(item), minTop);

                    minDeltaVertical = Math.Min(minDeltaVertical, item.ActualHeight - item.MinHeight);
                    minDeltaHorizontal = Math.Min(minDeltaHorizontal, item.ActualWidth - item.MinWidth);
                }

                foreach (DesignerItem item in this.designerCanvas.SelectedItems)
                {
                    switch (VerticalAlignment)
                    {
                        case VerticalAlignment.Bottom:
                            dragDeltaVertical = Math.Min(-e.VerticalChange, minDeltaVertical);
                            item.Height = item.ActualHeight - dragDeltaVertical;
                            break;
                        case VerticalAlignment.Top:
                            dragDeltaVertical = Math.Min(Math.Max(-minTop, e.VerticalChange), minDeltaVertical);
                            Canvas.SetTop(item, Canvas.GetTop(item) + dragDeltaVertical);
                            item.Height = item.ActualHeight - dragDeltaVertical;
                            break;
                    }

                    switch (HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            dragDeltaHorizontal = Math.Min(Math.Max(-minLeft, e.HorizontalChange), minDeltaHorizontal);
                            Canvas.SetLeft(item, Canvas.GetLeft(item) + dragDeltaHorizontal);
                            item.Width = item.ActualWidth - dragDeltaHorizontal;
                            break;
                        case HorizontalAlignment.Right:
                            dragDeltaHorizontal = Math.Min(-e.HorizontalChange, minDeltaHorizontal);
                            item.Width = item.ActualWidth - dragDeltaHorizontal;
                            break;
                    }
                }

                e.Handled = true;
            }
        }
    }
}
