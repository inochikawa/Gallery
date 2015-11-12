using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace GraphicEditor.Model
{
    public class Layer : Canvas, ILayer, INotifyPropertyChanged
    {
        public Layer()
        {
            IsActive = true;
            IsSelected = true;
            Background = Brushes.Transparent;
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            MouseLeftButtonUp += Layer_MouseLeftButtonUp;
        }
        
        private void Layer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnLayerMouseLeftButtonUp(this);
        }

        public Layer(string name)
            :this()
        {
            LayerName = name;
        }

        public delegate void LayerUpdating(Layer layer);

        public event LayerUpdating OnLayerMouseLeftButtonUp;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public string LayerName { get; set; }

        public Layer Clone()
        {
            var xaml = XamlWriter.Save(this);
            var xamlString = new StringReader(xaml);
            var xmlTextReader = new XmlTextReader(xamlString);
            var deepCopyObject = XamlReader.Load(xmlTextReader) as Layer;
            deepCopyObject.LayerName += " clone";
            IsSelected = false;
            return deepCopyObject;
        }
        
        public BitmapImage Preview()
        {
                return newPreview(this, 1, 20);
        }

        private BitmapImage newPreview(UIElement source, double scale, int quality)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;

            if (actualHeight == 0 || actualWidth == 0)
                return null;

            double renderHeight = actualHeight * scale;
            double renderWidth = actualWidth * scale;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(source);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
            }
            renderTarget.Render(drawingVisual);

            PngBitmapEncoder jpgEncoder = new PngBitmapEncoder();
            jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            
            BitmapImage bitmapImage = new BitmapImage();

            using (var stream = new MemoryStream())
            {
                jpgEncoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        internal void SelectLayer(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }

        internal void UnselectLayer(object sender, RoutedEventArgs e)
        {
            IsSelected = false;
        }

        public void UnactivateLayer()
        {
            IsActive = false;
            base.Opacity = 0;
        }

        public void ActivateLayer()
        {
            IsActive = true;
            base.Opacity = 1;
        }
    }
}
