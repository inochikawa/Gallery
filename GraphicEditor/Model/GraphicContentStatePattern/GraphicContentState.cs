using System.Windows.Input;

namespace GraphicEditor.Model.GraphicContentStatePattern
{
    public abstract class GraphicContentState
    {
        private GraphicContent f_graphicContent;

        public GraphicContentState(GraphicContent graphicContent)
        {
            f_graphicContent = graphicContent;
            f_graphicContent.WorkSpace.MouseMove += MouseMoveHandler;
            f_graphicContent.WorkSpace.MouseLeftButtonDown += MouseDownHandler;
            f_graphicContent.WorkSpace.MouseLeftButtonUp += MouseUpHandler;
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
            f_graphicContent.WorkSpace.MouseMove -= MouseMoveHandler;
            f_graphicContent.WorkSpace.MouseLeftButtonDown -= MouseDownHandler;
            f_graphicContent.WorkSpace.MouseLeftButtonUp -= MouseUpHandler;
        }
    }
}
