using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public abstract class GraphicContentState
    {
        private GraphicContent f_graphicContent;

        public GraphicContentState(GraphicContent graphicContent)
        {
            f_graphicContent = graphicContent;
            Mouse.AddMouseUpHandler(f_graphicContent.SelectedLayer(), MouseUpHandler);
            Mouse.AddMouseDownHandler(f_graphicContent.SelectedLayer(), MouseDownHandler);
            Mouse.AddMouseMoveHandler(f_graphicContent.SelectedLayer(), MouseMoveHandler);
        }

        public GraphicContent GraphicContent
        {
            get { return f_graphicContent; }
            set { f_graphicContent = value; }
        }

        public abstract void MouseMoveHandler(object sender, MouseEventArgs e);

        public abstract void MouseDownHandler(object sender, MouseButtonEventArgs e);

        public abstract void MouseUpHandler(object sender, MouseButtonEventArgs e);

        public void Dispose()
        {
            Mouse.RemoveMouseUpHandler(f_graphicContent.SelectedLayer(), MouseUpHandler);
            Mouse.RemoveMouseDownHandler(f_graphicContent.SelectedLayer(), MouseDownHandler);
            Mouse.RemoveMouseMoveHandler(f_graphicContent.SelectedLayer(), MouseMoveHandler);
        }
    }
}
