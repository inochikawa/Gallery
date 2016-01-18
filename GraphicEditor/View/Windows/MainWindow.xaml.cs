using System.Collections.Generic;
using System.ComponentModel;
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
            statusBar.viewModel.UpdateSize((int)f_mainWindowViewModel.GraphicContent.WorkSpace.Width, (int)f_mainWindowViewModel.GraphicContent.WorkSpace.Height);
            f_mainWindowViewModel.StatusBar = statusBar.viewModel;

            // Does not match the MVVM pattern
            GraphicToolPropertiesUserControl.viewModel.Subscribe(f_mainWindowViewModel.GraphicContent.GraphicToolProperties);

            Loaded += MainContainer_Loaded;
            Closing += MainContainer_Unloaded;
        }

        private void MainContainer_Unloaded(object sender, CancelEventArgs e)
        {
            f_mainWindowViewModel.SaveChildWindowsStates();
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
            f_mainWindowViewModel.LoadChildWindowsStates();
        }
    }
}
