using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.UserControls.Model.Shapes
{
    public class RectangleShape : Shapes
    {
        public RectangleShape(double width, double height, Brush background)
        {
            Rectangle = new Rectangle();
            this.Width = width;
            this.Height = height;
            this.Bckground = background;

            setParametres();
        }

        public Rectangle Rectangle { get; set; }

        public override Shapes Clone()
        {
            RectangleShape cloned = new RectangleShape(Width, Height, Bckground);
            return cloned as Shapes;
        }

        private void setParametres()
        {
            Rectangle.Width = this.Width;
            Rectangle.Height = this.Height;
            Rectangle.Fill = this.Bckground;
        }
    }
}
