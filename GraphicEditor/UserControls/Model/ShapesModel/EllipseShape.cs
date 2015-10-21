using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.UserControls.Model.ShapesModel
{
    public class EllipseShape : Shapes
    {
        public EllipseShape(double width, double height, Brush background)
        {
            Ellipse = new Ellipse();
            this.Width = width;
            this.Height = height;
            this.Background = background;

            setParametres();
        }

        public Ellipse Ellipse { get; set; }

        public override Shapes Clone()
        {
            EllipseShape cloned = new EllipseShape(Width, Height, Background);
            return cloned as Shapes;
        }

        private void setParametres()
        {
            Ellipse.Width = this.Width;
            Ellipse.Height = this.Height;
            Ellipse.Fill = this.Background;
        }
    }
}
