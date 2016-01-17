using GraphicEditor.Model.ToolBehavior;

namespace GraphicEditor.Model.ChildWindowBehavior.Interfaces
{
    public interface IChildWindowFactory
    {
        IChildWindow ChildWindow { get; set; }
        GraphicContent GraphicContent { get; set; }
        void SaveState();
        void LoadState();
    }
}