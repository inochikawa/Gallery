using System.Windows.Media;

namespace GraphicEditor.Model.ShapesModel
{
    public abstract class Shapes
    {
        private double f_width;
        private double f_height;
        private Brush f_background;

        public double Width
        {
            get { return this.f_width; }
            set { this.f_width = value; }
        }

        public double Height
        {
            get { return this.f_height; }
            set { this.f_height = value; }
        }

        public Brush Background
        {
            get { return this.f_background; }
            set { this.f_background = value; }
        }

        public abstract Shapes Clone();
    }
}
