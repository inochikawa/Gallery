﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.UserControls.Model.Shapes
{
    public class EllipseShape : Shapes
    {
        public Ellipse Ellipse;

        public EllipseShape(double width, double height, Brush background)
        {
            Ellipse = new Ellipse();
            this.Width = width;
            this.Height = height;
            this.Bckground = background;

            setParametres();
        }

        private void setParametres()
        {
            Ellipse.Width = this.Width;
            Ellipse.Height = this.Height;
            Ellipse.Fill = this.Bckground;
        }

        public override Shapes Clone()
        {
            EllipseShape cloned = new EllipseShape(Width, Height, Bckground);
            return cloned as Shapes;
        }
    }
}