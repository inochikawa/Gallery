using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicEditor.UserControls.Model.Shapes
{
    public abstract class Shapes
    {
        double width;
        double height;
        Brush background;

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        public Brush Bckground
        {
            get { return background; }
            set { background = value; }
        }

        public abstract Shapes Clone();
    }
}
