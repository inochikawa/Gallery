using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public class NoToolSelected : GraphicContentState
    {
        public NoToolSelected(GraphicContent graphicContent)
            : base(graphicContent)
        {

        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
