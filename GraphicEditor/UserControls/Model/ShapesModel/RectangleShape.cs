using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.UserControls.Model.ShapesModel
{
    public class RectangleShape : Shapes
    {
        public RectangleShape(double width, double height, Brush background)
        {
            Rectangle = new Rectangle();
            this.Width = width;
            this.Height = height;
            this.Background = background;

            setParametres();
        }

        public Rectangle Rectangle { get; set; }

        public override Shapes Clone()
        {
            RectangleShape cloned = new RectangleShape(Width, Height, Background);
            return cloned as Shapes;
        }

        private void setParametres()
        {
            Rectangle.Width = this.Width;
            Rectangle.Height = this.Height;
            Rectangle.Fill = this.Background;
        }
    }
}
