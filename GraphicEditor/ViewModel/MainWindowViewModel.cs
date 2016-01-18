using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GraphicEditor.Model;
using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Factories;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.ToolBehavior;
using GraphicEditor.Model.ToolBehavior.GraphicBuilderBehavior;
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
        private ICommand f_saveImage;
        private ICommand f_saveGeFile;
        private ICommand f_openGeFile;
        private ICommand f_fillToolSelectedCommand;
        private ICommand f_showOrHideOverviewWindow;
        private ICommand f_showOrHideLayersWindow;
        private ICommand f_showOrHideColorPickerWindow;
        private GraphicBuilder f_graphicBuilderForSave;
        private readonly List<IChildWindowFactory> f_childWindows;
        private readonly IChildWindowFactory f_layersChildWindowFactory;
        private readonly IChildWindowFactory f_colorPickerChildWindowFactory;
        private readonly IChildWindowFactory f_zoomBoxChildWindowFactory;
        private StatusBarViewModel f_statusBar;


        public MainWindowViewModel()
        {
            SubscribeToCommands();
            GraphicContent = new GraphicContent();
            GraphicContent.WorkSpace.MouseMove += GraphicContentWorkSpaceMouseMove;
            ConfigureWorkSpace();
            f_layersChildWindowFactory = new LayersChildWindowFactory(GraphicContent);
            f_colorPickerChildWindowFactory = new ColorPickerChildWindowFactory();
            f_zoomBoxChildWindowFactory = new ZoomBoxChildWindowFactory();
            ((ZoomBoxChildWindow)f_zoomBoxChildWindowFactory.ChildWindow).ScrollViewer = ScrollViewer;
            ((ColorPickerViewModel)f_colorPickerChildWindowFactory.ChildWindow.ViewModel).Subscribe(GraphicContent.GraphicToolProperties);
            f_childWindows = new List<IChildWindowFactory>()
            {
                f_layersChildWindowFactory,
                f_colorPickerChildWindowFactory,
                f_zoomBoxChildWindowFactory
            };
        }

        private void GraphicContentWorkSpaceMouseMove(object sender, MouseEventArgs e)
        {
            f_statusBar.UpdatePosition(e.GetPosition((Canvas)sender));
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

        public ICommand SaveImage
        {
            get { return f_saveImage; }
            set { f_saveImage = value; }
        }

        public ICommand SaveGeFile
        {
            get { return f_saveGeFile; }
            set { f_saveGeFile = value; }
        }

        public ICommand OpenGeFile
        {
            get { return f_openGeFile; }
            set { f_openGeFile = value; }
        }

        public ICommand ShowOrHideOverviewWindow
        {
            get { return f_showOrHideOverviewWindow; }
            set { f_showOrHideOverviewWindow = value; }
        }

        public ICommand ShowOrHideLayersWindow
        {
            get { return f_showOrHideLayersWindow; }
            set { f_showOrHideLayersWindow = value; }
        }

        public ICommand ShowOrHideColorPickerWindow
        {
            get { return f_showOrHideColorPickerWindow; }
            set { f_showOrHideColorPickerWindow = value; }
        }

        #endregion

        public ScrollViewer ScrollViewer { get; set; }

        public Canvas BackCanvas { get; set; }

        public StatusBarViewModel StatusBar
        {
            get { return f_statusBar; }
            set { f_statusBar = value; }
        }
        
        public void SubscribeMenuItemsToChildWindows(List<MenuItem> menuItems)
        {
            for (int i = 0; i < menuItems.Count; i++)
                f_childWindows[i].ChildWindow.ChildWindow.Subscribe(menuItems[i]);
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
            f_openGeFile = new RelayCommand(OpenGeFileExecute);
            f_saveImage = new RelayCommand(SaveImageExecute);
            f_saveGeFile = new RelayCommand(SaveGeFileExecute);
            f_showOrHideColorPickerWindow = new RelayCommand(ShowOrHideColorPickerWindowExecute);
            f_showOrHideLayersWindow = new RelayCommand(ShowOrHideLayersWindowExecute);
            f_showOrHideOverviewWindow = new RelayCommand(ShowOrHideOverviewWindowExecute);
            f_fillToolSelectedCommand = new RelayCommand(FillToolSelectedCommandExecute);
        }

        private void ShowOrHideChildWindow(IChildWindowFactory childWindowFactory, bool isVisible)
        {
            childWindowFactory.ChildWindow.ChildWindow.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        private void ConfigureWorkSpace()
        {
            ScrollViewer = new ScrollViewer
            {
                Background = Brushes.Transparent,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible
            };

            BackCanvas = new Canvas()
            {
                Background = Brushes.Transparent,
                Children = { GraphicContent.WorkSpace },
                MinHeight = 1000,
                MinWidth = 1000
            };

            BackCanvas.MouseMove += BackCanvas_MouseMove;

            ScrollViewer.Content = BackCanvas;
        }

        private void BackCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            GraphicContent.MousePositionOnWindow = e.GetPosition(BackCanvas);
        }

        #region Execute methods

        private void PointerToolSelectedExecute(object obj)
        {
            GraphicContent.CurrentTool.Dispose();
            GraphicContent.CurrentTool = new PointerTool(GraphicContent);
            StatusBar.UpdateTool(GraphicContent.CurrentTool.Name);
        }

        private void FillToolSelectedCommandExecute(object obj)
        {
            GraphicContent.CurrentTool.Dispose();
            GraphicContent.CurrentTool = new FillTool(GraphicContent);
            StatusBar.UpdateTool(GraphicContent.CurrentTool.Name);
        }

        private void BrushToolSelectedExecute(object obj)
        {
            GraphicContent.CurrentTool.Dispose();
            GraphicContent.CurrentTool = new BrushTool(GraphicContent);
            StatusBar.UpdateTool(GraphicContent.CurrentTool.Name);
        }

        private void NoToolSelectedExecute(object obj = null)
        {
            GraphicContent.CurrentTool.Dispose();
            GraphicContent.CurrentTool = new NoTool(GraphicContent);
            StatusBar.UpdateTool(GraphicContent.CurrentTool.Name);
        }

        private void LineToolSelectedExecute(object obj)
        {
            GraphicContent.CurrentTool.Dispose();
            GraphicContent.CurrentTool = new LineTool(GraphicContent);
            StatusBar.UpdateTool(GraphicContent.CurrentTool.Name);
        }

        private void OpenImageExecute(object obj = null)
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
            // Image image = new Image { Source = bitmapImage };
            GraphicContent.Command.InsertImage(bitmapImage, GraphicContent.SelectedLayer);
        }

        private void OpenGeFileExecute(object obj = null)
        {
            // Create OpenFileDialog
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".gef",
                Filter = "Graphic editor files (*.gef)|*.gef"
            };
            var result = dlg.ShowDialog();
            if (result != true) return;

            GraphicContent.Command.InsertGeFile(dlg.FileName, GraphicContent);
        }

        private void SaveImageExecute(object obj = null)
        {
            f_graphicBuilderForSave = new ImageBuilder();
            f_graphicBuilderForSave = GraphicContent.Layers.Aggregate(f_graphicBuilderForSave, (current, layer) => current.BuildLayer(layer));
            f_graphicBuilderForSave.Buid(f_graphicBuilderForSave.FileName());
        }

        private void SaveGeFileExecute(object obj = null)
        {
            f_graphicBuilderForSave = new GeFileBuilder();
            f_graphicBuilderForSave = GraphicContent.Layers.Aggregate(f_graphicBuilderForSave, (current, layer) => current.BuildLayer(layer));
            f_graphicBuilderForSave.Buid(f_graphicBuilderForSave.FileName());
        }

        private void ShowOrHideOverviewWindowExecute(object obj)
        {
            ShowOrHideChildWindow(f_zoomBoxChildWindowFactory, (bool)obj);
        }

        private void ShowOrHideLayersWindowExecute(object obj)
        {
            ShowOrHideChildWindow(f_layersChildWindowFactory, (bool)obj);
        }

        private void ShowOrHideColorPickerWindowExecute(object obj)
        {
            ShowOrHideChildWindow(f_colorPickerChildWindowFactory, (bool)obj);
        }

        #endregion

        public void ShowChildWindows(MainWindow window)
        {
            window.ChildrenContent.Children.Insert(0, ScrollViewer);

            f_layersChildWindowFactory.ChildWindow.Show(window);
            f_colorPickerChildWindowFactory.ChildWindow.Show(window);
            f_zoomBoxChildWindowFactory.ChildWindow.Show(window);
        }

        public void UndoExecute(object obj = null)
        {
            GraphicContent.Command.Undo(1);
        }

        public void RedoExecute(object obj = null)
        {
            GraphicContent.Command.Redo(1);
        }

        public void OpenGeFileOnStartup(string fileName)
        {
            GraphicContent = GraphicContent.InitFromGeFile(fileName);
            GC.Collect();
        }

        public void SaveChildWindowsStates()
        {
            foreach (IChildWindowFactory childWindow in f_childWindows)
            {
                childWindow.SaveState();
            }
        }

        public void LoadChildWindowsStates()
        {
            foreach (IChildWindowFactory childWindow in f_childWindows)
            {
                childWindow.LoadState();
            }
        }
    }
}
