using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class GraphicContent
    {
        /// <summary>
        /// Container for main work space
        /// </summary>
        private Grid f_workSpace;

        /// <summary>
        /// 
        /// </summary>
        private Point f_dPoint;

        private Size f_windowSize;

        private GraphicContentState f_graphicContentState;

        public GraphicContent()
        {
            f_workSpace = new Grid();
            Layers = new List<Layer>();
            Layers.Add(new Layer());
            // Set current tool is Pointer
            f_graphicContentState = new PointerToolSelected(this);
            // Add new layer to the work space
            f_workSpace.Children.Add(Layers.Last());
            configureWorkSpace();
        }

        public List<Layer> Layers { get; set; }
        
        public Grid WorkSpace
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
        
        private void configureWorkSpace()
        {
            f_workSpace.Background = Brushes.White;
            f_workSpace.Width = 500;
            f_workSpace.Height = 300;

            // Initialize a new DropShadowEffect 
            DropShadowEffect myDropShadowEffect = new DropShadowEffect();
            // Set the color of the shadow to Black.
            Color myShadowColor = new Color();
            myShadowColor.ScA = 1;
            myShadowColor.ScB = 0;
            myShadowColor.ScG = 0;
            myShadowColor.ScR = 0;
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

        /// <summary>
        /// Returns selected layer for now
        /// </summary>
        /// <returns></returns>
        public Layer SelectedLayer()
        {
            return Layers.FirstOrDefault(w => w.IsSelected);
        }
    }
}
