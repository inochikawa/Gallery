using System.Windows.Controls;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.View.UserControls;
using GraphicEditor.View.UserControls.CSharpFiles;

namespace GraphicEditor.Model.ChildWindowBehavior.ChildWondows
{
    public class ZoomBoxChildWindow : IChildWindow
    {
        public ZoomBoxChildWindow()
        {
            ChildWindow = new ChildWindow()
            {
                Child = new ZoomBox(),
                Width = 190,
                Height = 210,
                MinHeight = 63
            };
            Header = "Overview";
        }

        public ChildWindow ChildWindow { get; set; }

        public object ViewModel => null;

        public ScrollViewer ScrollViewer
        {
            get { return ((ZoomBox)ChildWindow.Child).ScrollViewer; }
            set { ((ZoomBox)ChildWindow.Child).ScrollViewer = value; }
        }

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
