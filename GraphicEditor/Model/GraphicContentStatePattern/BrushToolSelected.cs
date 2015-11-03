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
using System.Windows.Shapes;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class BrushToolSelected : GraphicContentState
    {
        private Brush f_color;
        private double f_thickness;
        private double f_opacity;
        private Layer f_layer;
        private double f_softness;

        public BrushToolSelected(GraphicContent graphicContent)
            : base(graphicContent)
        {
            f_color = Brushes.Blue;
            f_thickness = 10;
            f_opacity = 1;
            f_softness = 10;
            f_layer = graphicContent.SelectedLayer();
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            drawEllipse(e.GetPosition(base.GraphicContent.WorkSpace), f_layer);
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                drawEllipse(e.GetPosition(base.GraphicContent.WorkSpace), f_layer);
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void drawEllipse(Point position, Layer layer)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = f_color;
            ellipse.Width = f_thickness;
            ellipse.Height = f_thickness;
            ellipse.Opacity = f_opacity;

            layer.Children.Add(ellipse);
            Canvas.SetTop(ellipse, position.Y - f_thickness / 2);
            Canvas.SetLeft(ellipse, position.X - f_thickness / 2);
        }

        private DropShadowEffect dropShadowEffect()
        {
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
            myDropShadowEffect.Direction = 0;

            // Set the depth of the shadow being cast.
            myDropShadowEffect.ShadowDepth = f_softness;

            // Set the shadow opacity to half opaque or in other words - half transparent.
            // The range is 0-1.
            myDropShadowEffect.Opacity = 0.3;

            return myDropShadowEffect;
        }
    }
}
