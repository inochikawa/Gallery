using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using GraphicEditor.Model.ToolBehavior.ToolProperties;

namespace GraphicEditor.Model.ToolBehavior
{
    public class BrushTool : GraphicTool
    {
        private double f_thickness;
        private double f_opacity;
        private readonly Layer f_layer;
        private double f_softness;
        private Polyline f_polyLine;

        public BrushTool(GraphicContent graphicContent)
            : base(graphicContent)
        {
            f_thickness = 2;
            f_opacity = 1;
            f_softness = 10;
            f_layer = graphicContent.SelectedLayer;
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (f_layer == null)
                return;

            if (!f_layer.IsActive)
                return;

            ConfigurePolyLine();

            f_polyLine.Points.Add(e.GetPosition(f_layer));

            // subscribe events for layer's child
            SubscribeEvents();

            GraphicContent.Command.Insert(f_polyLine, f_layer);
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (f_polyLine == null)
                return;

            if (!f_layer.IsActive)
                return;

            if (e.LeftButton == MouseButtonState.Pressed)
                f_polyLine.Points.Add(e.GetPosition(f_layer));
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
        }

        private void ConfigurePolyLine()
        {
            f_polyLine = new Polyline
            {
                StrokeThickness = (double)GraphicContent.GraphicToolProperties.Thickness,
                Stroke = new SolidColorBrush((Color)GraphicContent.GraphicToolProperties.Color),
                Effect = DropShadowEffect()
            };
        }

        private DropShadowEffect DropShadowEffect()
        {
            // Initialize a new DropShadowEffect 
            DropShadowEffect myDropShadowEffect = new DropShadowEffect();

            // Set the color of the shadow to Black.
            Color myShadowColor = (Color)GraphicContent.GraphicToolProperties.Color;
            myDropShadowEffect.Color = myShadowColor;

            // Set the direction of where the shadow is cast to 320 degrees.
            myDropShadowEffect.Direction = 0;

            // Set the depth of the shadow being cast.
            myDropShadowEffect.ShadowDepth = 0;

            // Set the shadow opacity to half opaque or in other words - half transparent.
            // The range is 0-1.
            myDropShadowEffect.Opacity = 1;

            return myDropShadowEffect;
        }

        private void SubscribeEvents()
        {
            f_polyLine.MouseLeftButtonDown += GraphicContent.ElementMouseLeftButtonDown;
            f_polyLine.MouseLeftButtonUp += GraphicContent.ElementMouseLeftButtonUp;
            f_polyLine.MouseMove += GraphicContent.ElementMouseMove;
        }
    }
}
