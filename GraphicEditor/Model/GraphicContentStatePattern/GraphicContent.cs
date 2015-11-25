using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using GraphicEditor.Model.Commands;

namespace GraphicEditor.Model.GraphicContentStatePattern
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
        private GraphicContentState f_graphicContentState;

        private CommandReceiver f_command;

        public GraphicContent()
        {
            Command = new CommandReceiver();
            f_workSpace = new Canvas();
            ConfigureWorkSpace();
            Layers = new List<Layer>();
            AddLayer(new Layer("New layer " + Layers.Count));
            // Set current tool is Pointer
            f_graphicContentState = new PointerToolSelected(this);
        }

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

        public GraphicContentState GraphicContentState
        {
            get { return f_graphicContentState; }
            set { f_graphicContentState = value; }
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
        public Layer SelectedLayer()
        {
            return Layers.FirstOrDefault(w => w.IsSelected);
        }

        public void AddLayer(Layer layer)
        {
            // Unselect all layers
            foreach (var item in Layers)
            {
                item.IsSelected = false;
            }
            Layers.Add(layer);
            f_workSpace.Children.Add(layer);
            Panel.SetZIndex(layer, Layers.Count - 1);
            layer.Width = f_workSpace.Width;
            layer.Height = f_workSpace.Height;
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
            var visualBrush = new VisualBrush
            {
                TileMode = TileMode.Tile,
                Viewport = new Rect(0, 0, 20, 20),
                ViewportUnits = BrushMappingMode.Absolute,
                Visual =
                    new Image()
                    {
                        Source =
                            new BitmapImage(
                                new Uri(
                                    @"pack://application:,,,/GraphicEditor;component/View/Resources/Images/transparent.png"))
                    }
            };


            f_workSpace.Background = visualBrush;
            f_workSpace.Width = 500;
            f_workSpace.Height = 300;

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
