using System.Collections.Generic;
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
            f_mainWindowViewModel = new MainWindowViewModel();
            DataContext = f_mainWindowViewModel;
            f_mainWindowViewModel.ShowChildWindows(this);
            f_mainWindowViewModel.SubscribeMenuItemsToChildWindows(new List<MenuItem>()
            {
                layersMenuItem,
                colorPickerMenuItem,
                overviewMenuItem
            });
            f_mainWindowViewModel.StatusBar = statusBar.viewModel;

            Loaded += MainContainer_Loaded;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z)
                f_mainWindowViewModel.UndoExecute();

            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.Z)
                f_mainWindowViewModel.RedoExecute();
        }

        void MainContainer_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["ArbitraryArgName"] != null)
            {
                string fname = Application.Current.Properties["ArbitraryArgName"].ToString();
                f_mainWindowViewModel.OpenGeFileOnStartup(fname);
            }
        }
    }
}
