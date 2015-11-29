using System.Windows;
using System.Windows.Controls;
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
           // backgroundCanvas = new Canvas();
            f_mainWindowViewModel = new MainWindowViewModel(); 
            DataContext = f_mainWindowViewModel;
            f_mainWindowViewModel.ShowChildWindows(this);
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
