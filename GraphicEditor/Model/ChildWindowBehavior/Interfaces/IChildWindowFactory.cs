using GraphicEditor.Model.GraphicContentStatePattern;

namespace GraphicEditor.Model.ChildWindowBehavior.Interfaces
{
    public interface IChildWindowFactory
    {
        IChildWindow ChildWindow { get; set; }
        GraphicContent GraphicContent { get; set; }
    }
}