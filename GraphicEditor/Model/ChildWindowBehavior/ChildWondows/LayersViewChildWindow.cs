using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.View.UserControls;

namespace GraphicEditor.Model.ChildWindowBehavior.ChildWondows
{
    public class LayersViewChildWindow: IChildWindow
    {
        public LayersViewChildWindow()
        {
            ChildWindow = new ChildWindow()
            {
                Child = new LayersView(),
                Width = 190,
                Height = 210
            };
        }
        
        public ChildWindow ChildWindow { get; set; }
        
        public object ViewModel => ((LayersView) ChildWindow.Child).ViewModel;

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