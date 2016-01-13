using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using GraphicEditor.Model.Commands;
using GraphicEditor.Model.ToolBehavior.ToolProperties;
using GraphicEditor.View.Styles.Helpers;
using GraphicEditor.ViewModel;

namespace GraphicEditor.Model.ToolBehavior
{
    public class GraphicContent
    {
        /// <summary>
        /// Container for main work space.
        /// </summary>
        private Canvas f_workSpace;

        /// <summary>
        /// Delta point. Difference between start mouse (on down) point and current mouse point.
        /// </summary>
        private Point f_dPoint;

        /// <summary>
        /// Window size.
        /// </summary>
        private Size f_windowSize;

        /// <summary>
        /// State of graphic content. Defends selected tool in now moment.
        /// </summary>
        private Tool f_currentTool;

        private CommandReceiver f_command;
        
        private GraphicToolProperties f_graphicToolProperties;

        public GraphicContent()
        {
            Command = new CommandReceiver();
            f_workSpace = new Canvas();
            f_graphicToolProperties = new GraphicToolProperties()
            {
                Color = Colors.White,
                Softness = 1,
                Thickness = 10
            };
            ConfigureWorkSpace();
            Layers = new List<Layer>();
            AddLayer(new Layer("New layer " + Layers.Count));

            // Set current tool is Pointer
            f_currentTool = new PointerTool(this);
        }

        public delegate void LayerProcessing(Layer layer);

        public event LayerProcessing OnLayerCreate;

        public List<Layer> Layers { get; set; }

        public Canvas WorkSpace
        {
            get { return f_workSpace; }
            set { f_workSpace = value; }
        }

        public Point DeltaPoint
        {
            get { return f_dPoint; }
            set { f_dPoint = value; }
        }

        public Point MousePositionOnWindow { get; set; }

        public Tool CurrentTool
        {
            get { return f_currentTool; }
            set { f_currentTool = value; }
        }

        public Size WindowSize
        {
            get { return f_windowSize; }
            set { f_windowSize = value; }
        }
        
        public CommandReceiver Command
        {
            get
            {
                return f_command;
            }

            set
            {
                f_command = value;
            }
        }

        /// <summary>
        /// Returns selected layer for now
        /// </summary>
        /// <returns>Layer</returns>
        public Layer SelectedLayer
        {
            get { return Layers.FirstOrDefault(layer => layer.IsSelected); }
        }
        
        public GraphicToolProperties GraphicToolProperties
        {
            get { return f_graphicToolProperties; }
            set { f_graphicToolProperties = value; }
        }

        public void AddLayer(Layer layer)
        {
            // Unselect all layers
            foreach (var item in Layers)
                item.IsSelected = false;

            // Unselect all layers
            foreach (Layer item in f_workSpace.Children)
                item.IsSelected = false;

            Layers.Add(layer);
            f_workSpace.Children.Add(layer);
            layer.Width = f_workSpace.Width;
            layer.Height = f_workSpace.Height;
        }

        public void AddLayerEventHandler(Layer layer)
        {
            OnLayerCreate?.Invoke(layer);
        }

        public BitmapImage ConvertToRasterImage(UIElement source, double scale, int quality)
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

            PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
            pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));

            BitmapImage bitmapImage = new BitmapImage();

            using (var stream = new MemoryStream())
            {
                pngBitmapEncoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        public void ElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public void ElementMouseMove(object sender, MouseEventArgs e)
        {
        }

        public void ElementMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void ConfigureWorkSpace()
        {
            f_workSpace.Background = TransparentViewVisualBrush.RenderTransparent();
            f_workSpace.Width = 500;
            f_workSpace.Height = 300;
            f_workSpace.Name = "PART_WorkSpaceCanvas";

            // Initialize a new DropShadowEffect 
            DropShadowEffect myDropShadowEffect = new DropShadowEffect();

            // Set the color of the shadow to Black.
            var myShadowColor = new Color
            {
                ScA = 1,
                ScB = 0,
                ScG = 0,
                ScR = 0
            };
            myDropShadowEffect.Color = myShadowColor;

            // Set the direction of where the shadow is cast to 320 degrees.
            myDropShadowEffect.Direction = -45;

            // Set the depth of the shadow being cast.
            myDropShadowEffect.ShadowDepth = 7;

            // Set the shadow opacity to half opaque or in other words - half transparent.
            // The range is 0-1.
            myDropShadowEffect.Opacity = 0.3;

            f_workSpace.Effect = myDropShadowEffect;
        }

    }
}
