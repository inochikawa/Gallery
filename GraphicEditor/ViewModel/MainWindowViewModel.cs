using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GraphicEditor.Model;
using GraphicEditor.Model.ChildWindowBehavior.Factories;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.GraphicContentStatePattern;
using Microsoft.Win32;

namespace GraphicEditor.ViewModel
{
    public class MainWindowViewModel
    {
        private ICommand f_pointerToolSelectedCommand;
        private ICommand f_noToolSelectedCommand;
        private ICommand f_brushToolSelectedCommand;
        private ICommand f_lineToolSelectedCommand;
        private ICommand f_undoCommand;
        private ICommand f_redoCommand;
        private ICommand f_openImage;
        private ICommand f_fillToolSelectedCommand;
        private IChildWindowFactory f_layersChildWindowFactory;
        private IChildWindowFactory f_colorPickerChildWindowFactory;

        public MainWindowViewModel()
        {
            SubscribeToCommands();
            GraphicContent = new GraphicContent();
            f_layersChildWindowFactory = new LayersChildWindowFactory(GraphicContent);
            f_colorPickerChildWindowFactory = new ColorPickerChildWindowFactory();
        }

        public GraphicContent GraphicContent { get; set; }

        #region Commands

        public ICommand PointerToolSelectedCommand
        {
            get
            {
                return f_pointerToolSelectedCommand;
            }
            set
            {
                f_pointerToolSelectedCommand = value;
            }
        }

        public ICommand NoToolSelectedCommand
        {
            get
            {
                return f_noToolSelectedCommand;
            }
            set
            {
                f_noToolSelectedCommand = value;
            }
        }

        public ICommand BrushToolSelectedCommand
        {
            get
            {
                return f_brushToolSelectedCommand;
            }
            set
            {
                f_brushToolSelectedCommand = value;
            }
        }

        public ICommand LineToolSelectedCommand
        {
            get
            {
                return f_lineToolSelectedCommand;
            }
            set
            {
                f_lineToolSelectedCommand = value;
            }
        }

        public ICommand RedoCommand
        {
            get
            {
                return f_redoCommand;
            }

            set
            {
                f_redoCommand = value;
            }
        }

        public ICommand UndoCommand
        {
            get
            {
                return f_undoCommand;
            }

            set
            {
                f_undoCommand = value;
            }
        }

        public ICommand OpenImage
        {
            get
            {
                return f_openImage;
            }

            set
            {
                f_openImage = value;
            }
        }

        public ICommand FillToolSelectedCommand
        {
            get { return f_fillToolSelectedCommand; }
            set { f_fillToolSelectedCommand = value; }
        }

        #endregion

        public void UndoExecute(object obj = null)
        {
            GraphicContent.Command.Undo(1);
        }

        public void RedoExecute(object obj = null)
        {
            GraphicContent.Command.Redo(1);
        }

        private void SubscribeToCommands()
        {
            f_pointerToolSelectedCommand = new RelayCommand(PointerToolSelectedExecute);
            f_noToolSelectedCommand = new RelayCommand(NoToolSelectedExecute);
            f_brushToolSelectedCommand = new RelayCommand(BrushToolSelectedExecute);
            f_lineToolSelectedCommand = new RelayCommand(LineToolSelectedExecute);
            f_undoCommand = new RelayCommand(UndoExecute);
            f_redoCommand = new RelayCommand(RedoExecute);
            f_openImage = new RelayCommand(OpenImageExecute);
            f_fillToolSelectedCommand = new RelayCommand(FillToolSelectedCommandExecute);
        }

        #region Execute methods

        private void PointerToolSelectedExecute(object obj)
        {
            GraphicContent.GraphicContentState.Dispose();
            GraphicContent.GraphicContentState = new PointerToolSelected(GraphicContent);
        }

        private void FillToolSelectedCommandExecute(object obj)
        {
            GraphicContent.GraphicContentState.Dispose();
            GraphicContent.GraphicContentState = new FillToolSelected(GraphicContent);
            ((ColorPickerViewModel)f_colorPickerChildWindowFactory.ChildWindow.ViewModel).Subscribe((FillToolSelected)GraphicContent.GraphicContentState);
        }

        private void BrushToolSelectedExecute(object obj)
        {
            GraphicContent.GraphicContentState.Dispose();
            GraphicContent.GraphicContentState = new BrushToolSelected(GraphicContent);
            ((ColorPickerViewModel)f_colorPickerChildWindowFactory.ChildWindow.ViewModel).Subscribe((BrushToolSelected)GraphicContent.GraphicContentState);
        }

        private void NoToolSelectedExecute(object obj = null)
        {
            GraphicContent.GraphicContentState.Dispose();
            GraphicContent.GraphicContentState = new NoToolSelected(GraphicContent);
        }

        private void LineToolSelectedExecute(object obj)
        {
            GraphicContent.GraphicContentState.Dispose();
            GraphicContent.GraphicContentState = new LineToolSelected(GraphicContent);
            ((ColorPickerViewModel)f_colorPickerChildWindowFactory.ChildWindow.ViewModel).Subscribe((LineToolSelected)GraphicContent.GraphicContentState);
        }

        private void OpenImageExecute(object obj)
        {
            // Create OpenFileDialog
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter =
                    "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|All files (*.*)|*.*"
            };
            var result = dlg.ShowDialog();
            if (result != true) return;
            BitmapImage bitmapImage = new BitmapImage(new Uri(dlg.FileName));
            Image image = new Image { Source = bitmapImage };
            GraphicContent.Command.Insert(image, GraphicContent.SelectedLayer);
        }

        #endregion

        public void ShowChildWindows(MainWindow window)
        {
            f_layersChildWindowFactory.ChildWindow.Show(window);
            f_colorPickerChildWindowFactory.ChildWindow.Show(window);
        }
    }
}
