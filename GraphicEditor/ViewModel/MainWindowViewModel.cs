using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GraphicEditor.Model;
using GraphicEditor.Model.GraphicContentStatePattern;
using GraphicEditor.View.UserControls.LayersControl;
using GraphicEditor.View.Windows;
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
        private GraphicContent f_graphicContent;
        private readonly LayersWindow f_layersWindow;

        public MainWindowViewModel()
        {
            f_pointerToolSelectedCommand = new RelayCommand(PointerToolSelectedExecute);
            f_noToolSelectedCommand = new RelayCommand(NoToolSelectedExecute);
            f_brushToolSelectedCommand = new RelayCommand(BrushToolSelectedExecute);
            f_lineToolSelectedCommand = new RelayCommand(LineToolSelectedExecute);
            f_undoCommand = new RelayCommand(UndoExecute);
            f_redoCommand = new RelayCommand(RedoExecute);
            f_openImage = new RelayCommand(OpenImageExecute);
            f_graphicContent = new GraphicContent();
            f_layersWindow = new LayersWindow();

            f_layersWindow.ViewModel.AddLayer(f_graphicContent.SelectedLayer());
            f_layersWindow.ViewModel.OnLayerCreate += LayerWindowViewModel_OnLayerCreate;
            f_layersWindow.ViewModel.OnLayerDelete += LayerWindowViewModel_OnLayerDelete;
            f_layersWindow.ViewModel.OnLayerDublicate += LayerWindowViewModel_OnLayerDublicate;
            f_layersWindow.ViewModel.OnLayerSelectionChanged += LayerWindowViewModel_OnLayerSelectionChanged;

            f_layersWindow.Show();
            f_layersWindow.Topmost = true;
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
            f_graphicContent.Command.Insert(image, f_graphicContent.SelectedLayer());
        }

        private void LayerWindowViewModel_OnLayerSelectionChanged(object obj)
        {
            if (((ListBox)obj).Items.Count == 0)
                return;

            foreach (var layer in f_graphicContent.Layers)
            {
                if (((ListBox)obj).SelectedItem == null)
                    return;

                if (layer.LayerName == ((LayerItem)((ListBox)obj).SelectedItem).LayerName)
                {
                    layer.IsSelected = true;
                    layer.IsHitTestVisible = true;
                }
                else
                {
                    layer.IsSelected = false;
                    layer.IsHitTestVisible = false;
                }
            }
        }

        private void LayerWindowViewModel_OnLayerDublicate()
        {
            var cloneLayer = f_graphicContent.SelectedLayer().Clone();
            if (cloneLayer == null)
                throw new ArgumentNullException(nameof(cloneLayer));
            f_graphicContent.AddLayer(cloneLayer);
            f_layersWindow?.ViewModel.AddLayer(cloneLayer);
        }

        private void LayerWindowViewModel_OnLayerDelete(object obj)
        {
            if (obj == null)
                return;

            LayerItem layerItem = (LayerItem)((ListBox)obj).SelectedItem;
            Layer currentLayer = null;

            foreach (var layer in f_graphicContent.Layers)
            {
                if (layer.LayerName == layerItem.LayerName)
                    currentLayer = layer;
            }

            f_graphicContent.WorkSpace.Children.Remove(currentLayer);
            f_graphicContent.Layers.Remove(currentLayer);
            f_layersWindow.ViewModel.RemoveLayer(layerItem);
        }

        private void LayerWindowViewModel_OnLayerCreate()
        {
            f_graphicContent.SelectedLayer().IsSelected = false;
            Layer layer = new Layer("New layer " + f_graphicContent.Layers.Count);
            f_graphicContent.AddLayer(layer);
            f_layersWindow.ViewModel.AddLayer(layer);
            NoToolSelectedExecute();
        }

        public GraphicContent GraphicContent
        {
            get { return f_graphicContent; }
            set { f_graphicContent = value; }
        }

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

        #endregion

        #region Execute methods

        private void PointerToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new PointerToolSelected(f_graphicContent);
        }

        private void BrushToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new BrushToolSelected(f_graphicContent);
        }

        private void NoToolSelectedExecute(object obj = null)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new NoToolSelected(f_graphicContent);
        }

        private void LineToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new LineToolSelected(f_graphicContent);
        }

        public void UndoExecute(object obj = null)
        {
            f_graphicContent.Command.Undo(1);
        }

        public void RedoExecute(object obj = null)
        {
            f_graphicContent.Command.Redo(1);
        }

        #endregion
    }
}
