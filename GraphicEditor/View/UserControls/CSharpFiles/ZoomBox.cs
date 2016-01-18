using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GraphicEditor.View.UserControls.CSharpFiles
{
    public class ZoomBox: Control, INotifyPropertyChanged
    {
        private Thumb f_zoomThumb;
        private Canvas f_zoomCanvas;
        private Slider f_zoomSlider;
        private ScaleTransform f_scaleTransform;
        private Canvas f_designerCanvas;

        public event PropertyChangedEventHandler PropertyChanged;

        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public Canvas BindedCanvas
        {
            get { return (Canvas)GetValue(CanvasProperty); }
            set
            {
                SetValue(CanvasProperty, value);
                NotifyPropertyChanged("BindedCanvas");
            }
        }

        public static readonly DependencyProperty CanvasProperty =
            DependencyProperty.Register("Canvas", typeof(Canvas), typeof(ZoomBox));

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ZoomBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ScrollViewer == null)
                return;

            BindedCanvas = f_designerCanvas = ((Canvas)ScrollViewer.Content).Children[0] as Canvas;
            f_designerCanvas = ScrollViewer.Content as Canvas;
            if (f_designerCanvas == null)
                throw new Exception("Canvas must not be null!");

            //f_zoomThumb = Template.FindName("PART_ZoomThumb", this) as Thumb;
            //if (f_zoomThumb == null)
            //    throw new Exception("PART_ZoomThumb template is missing!");

            f_zoomCanvas = Template.FindName("PART_ZoomCanvas", this) as Canvas;
            if (f_zoomCanvas == null)
                throw new Exception("PART_ZoomCanvas template is missing!");

            f_zoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;
            if (f_zoomSlider == null)
                throw new Exception("PART_ZoomSlider template is missing!");

            f_designerCanvas.LayoutUpdated += DesignerCanvas_LayoutUpdated;

            //f_zoomThumb.DragDelta += Thumb_DragDelta;

            f_zoomSlider.ValueChanged += ZoomSlider_ValueChanged;

            f_scaleTransform = new ScaleTransform();
            f_designerCanvas.LayoutTransform = f_scaleTransform;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double scale = e.NewValue / e.OldValue;

            double halfViewportHeight = ScrollViewer.ViewportHeight / 2;
            double newVerticalOffset = ((ScrollViewer.VerticalOffset + halfViewportHeight) * scale - halfViewportHeight);

            double halfViewportWidth = ScrollViewer.ViewportWidth / 2;
            double newHorizontalOffset = ((ScrollViewer.HorizontalOffset + halfViewportWidth) * scale - halfViewportWidth);

            f_scaleTransform.ScaleX *= scale;
            f_scaleTransform.ScaleY *= scale;

            ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
            ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double scale, xOffset, yOffset;
            InvalidateScale(out scale, out xOffset, out yOffset);

            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + e.HorizontalChange / scale);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + e.VerticalChange / scale);
        }

        private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            double scale, xOffset, yOffset;
            InvalidateScale(out scale, out xOffset, out yOffset);

            //f_zoomThumb.Width = ScrollViewer.ViewportWidth * scale;
            //f_zoomThumb.Height = ScrollViewer.ViewportHeight * scale;

            //Canvas.SetLeft(f_zoomThumb, xOffset + ScrollViewer.HorizontalOffset * scale);
            //Canvas.SetTop(f_zoomThumb, yOffset + ScrollViewer.VerticalOffset * scale);
        }

        private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
        {
            // designer canvas size
            double w = f_designerCanvas.ActualWidth * f_scaleTransform.ScaleX;
            double h = f_designerCanvas.ActualHeight * f_scaleTransform.ScaleY;

            // zoom canvas size
            double x = f_zoomCanvas.ActualWidth;
            double y = f_zoomCanvas.ActualHeight;

            double scaleX = x / w;
            double scaleY = y / h;

            scale = (scaleX < scaleY) ? scaleX : scaleY;

            xOffset = (x - scale * w) / 2;
            yOffset = (y - scale * h) / 2;
        }
    }
}
