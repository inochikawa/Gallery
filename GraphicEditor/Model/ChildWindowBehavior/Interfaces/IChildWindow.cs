using GraphicEditor.View.UserControls;

namespace GraphicEditor.Model.ChildWindowBehavior.Interfaces
{
    public interface IChildWindow
    {
        ChildWindow ChildWindow { get; set; }
        object ViewModel { get; }
        string Header { get; set; }
        void Show(MainWindow window);
        void Move(int x, int y);
    }
}