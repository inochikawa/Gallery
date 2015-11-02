using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GraphicEditor.Model
{
    public class ZoomBox : Control
    {
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ZoomBox));

        private Thumb f_zoomThumb;
        private Canvas f_zoomCanvas;
        private Slider f_zoomSlider;
        private ScaleTransform f_scaleTransform;
        private DesignerCanvas f_designerCanvas;

        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ScrollViewer == null)
                return;

            this.f_designerCanvas = this.ScrollViewer.Content as DesignerCanvas;

            ////if (this.designerCanvas == null)
            ////    throw new Exception("DesignerCanvas must not be null!");

            this.f_zoomThumb = Template.FindName("PART_ZoomThumb", this) as Thumb;

            ////if (this.zoomThumb == null)
            ////   throw new Exception("PART_ZoomThumb template is missing!");

            this.f_zoomCanvas = Template.FindName("PART_ZoomCanvas", this) as Canvas;

            ////if (this.zoomCanvas == null)
            ////    throw new Exception("PART_ZoomCanvas template is missing!");

            this.f_zoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;

            ////if (this.zoomSlider == null)
            ////    throw new Exception("PART_ZoomSlider template is missing!");

            this.f_designerCanvas.LayoutUpdated += new EventHandler(this.DesignerCanvas_LayoutUpdated);

            this.f_zoomThumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);

            this.f_zoomSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.ZoomSlider_ValueChanged);

            this.f_scaleTransform = new ScaleTransform();
            this.f_designerCanvas.LayoutTransform = this.f_scaleTransform;
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double scale = e.NewValue / e.OldValue;

            double halfViewportHeight = this.ScrollViewer.ViewportHeight / 2;
            double newVerticalOffset = ((ScrollViewer.VerticalOffset + halfViewportHeight) * scale) - halfViewportHeight;

            double halfViewportWidth = this.ScrollViewer.ViewportWidth / 2;
            double newHorizontalOffset = ((this.ScrollViewer.HorizontalOffset + halfViewportWidth) * scale) - halfViewportWidth;

            this.f_scaleTransform.ScaleX *= scale;
            this.f_scaleTransform.ScaleY *= scale;

            this.ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
            this.ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double scale, xOffset, yOffset;
            this.InvalidateScale(out scale, out xOffset, out yOffset);

            this.ScrollViewer.ScrollToHorizontalOffset(this.ScrollViewer.HorizontalOffset + (e.HorizontalChange / scale));
            this.ScrollViewer.ScrollToVerticalOffset(this.ScrollViewer.VerticalOffset + (e.VerticalChange / scale));
        }

        private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            double scale, xOffset, yOffset;
            this.InvalidateScale(out scale, out xOffset, out yOffset);

            this.f_zoomThumb.Width = this.ScrollViewer.ViewportWidth * scale;
            this.f_zoomThumb.Height = this.ScrollViewer.ViewportHeight * scale;

            Canvas.SetLeft(this.f_zoomThumb, xOffset + (this.ScrollViewer.HorizontalOffset * scale));
            Canvas.SetTop(this.f_zoomThumb, yOffset + (this.ScrollViewer.VerticalOffset * scale));
        }

        private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
        {
            // designer canvas size
            double w = this.f_designerCanvas.ActualWidth * this.f_scaleTransform.ScaleX;
            double h = this.f_designerCanvas.ActualHeight * this.f_scaleTransform.ScaleY;

            // zoom canvas size
            double x = this.f_zoomCanvas.ActualWidth;
            double y = this.f_zoomCanvas.ActualHeight;

            double scaleX = x / w;
            double scaleYos = y / h;

            scale = (scaleX < scaleYos) ? scaleX : scaleYos;

            xOffset = (x - (scale * w)) / 2;
            yOffset = (y - (scale * h)) / 2;
        }
    }
}
