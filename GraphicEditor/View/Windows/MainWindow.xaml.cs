using System.Windows;
using System.Windows.Input;
using GraphicEditor.ViewModel;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel f_mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            f_mainWindowViewModel = new MainWindowViewModel();
            backgroundCanvas.Children.Add(f_mainWindowViewModel.GraphicContent.WorkSpace);
            DataContext = f_mainWindowViewModel;
            f_mainWindowViewModel.ShowChildWindows(this);
        }

        private void imageProperties_Click(object sender, RoutedEventArgs e)
        {
        }

        private void backgroundCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            f_mainWindowViewModel.GraphicContent.MousePositionOnWindow = e.GetPosition(backgroundCanvas);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z)
                f_mainWindowViewModel.UndoExecute();

            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.Z)
                f_mainWindowViewModel.RedoExecute();
        }
    }
}
