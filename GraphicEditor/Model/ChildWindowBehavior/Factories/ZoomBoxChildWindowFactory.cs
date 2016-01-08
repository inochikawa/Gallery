using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.ToolBehavior;

namespace GraphicEditor.Model.ChildWindowBehavior.Factories
{
    class ZoomBoxChildWindowFactory : IChildWindowFactory
    {
        public ZoomBoxChildWindowFactory()
        {
            ChildWindow = new ZoomBoxChildWindow();
            ChildWindow.Move(-20, 430);
        }

        public IChildWindow ChildWindow { get; set; }

        public GraphicContent GraphicContent { get; set; }
    }
}
