using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.View.UserControls;

namespace GraphicEditor.Model.ChildWindowBehavior.ChildWondows
{
    public class ColorPickerChildWindow : IChildWindow
    {
        public ColorPickerChildWindow()
        {
            ChildWindow = new ChildWindow() { Child = new ColorPicker() };
        }

        public ChildWindow ChildWindow { get; set; }

        public object ViewModel => ((ColorPicker) ChildWindow.Child).ColorPickerViewModel;

        public string Header
        {
            get { return ChildWindow.Header; }
            set { ChildWindow.Header = value; }
        }

        public void Show(MainWindow window)
        {
            window.ChildrenContent.Children.Add(ChildWindow);
        }

        public void Move(int x, int y)
        {
            ChildWindow.Move(x, y);
        }
    }
}