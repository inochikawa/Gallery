using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.GraphicContentStatePattern;
using GraphicEditor.ViewModel;

namespace GraphicEditor.Model.ChildWindowBehavior.Factories
{
    public class ColorPickerChildWindowFactory : IChildWindowFactory
    {
        public ColorPickerChildWindowFactory()
        {
            ChildWindow = new ColorPickerChildWindow() {Header = "Color picker"};
            ChildWindow.Move(590, 210);
        }

        public IChildWindow ChildWindow { get; set; }
        public GraphicContent GraphicContent { get; set; }
    }
}