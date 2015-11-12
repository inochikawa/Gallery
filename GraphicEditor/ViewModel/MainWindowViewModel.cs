using System;
using System.Windows.Input;
using GraphicEditor.Model;
using GraphicEditor.Model.GraphicContentStatePattern;
using GraphicEditor.View.Windows;
using GraphicEditor.View.UserControls.LayersControl;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicEditor.ViewModel
{
    public class MainWindowViewModel
    {
        private ICommand f_pointerToolSelectedCommand;
        private ICommand f_noToolSelectedCommand;
        private ICommand f_brushToolSelectedCommand;
        private ICommand f_lineToolSelectedCommand;
        private GraphicContent f_graphicContent;
        private LayersWindow f_layersWindow;

        public MainWindowViewModel()
        {
            f_pointerToolSelectedCommand = new RelayCommand(PointerToolSelectedExecute);
            f_noToolSelectedCommand = new RelayCommand(NoToolSelectedExecute);
            f_brushToolSelectedCommand = new RelayCommand(BrushToolSelectedExecute);
            f_lineToolSelectedCommand = new RelayCommand(LineToolSelectedExecute);
            f_graphicContent = new GraphicContent();
            f_layersWindow = new LayersWindow();
            
            f_layersWindow.ViewModel.AddLayer(f_graphicContent.SelectedLayer());
            f_layersWindow.ViewModel.OnLayerCreate += LayerWindowViewModel_OnLayerCreate;
            f_layersWindow.ViewModel.OnLayerDelete += LayerWindowViewModel_OnLayerDelete;
            f_layersWindow.ViewModel.OnLayerDublicate += LayerWindowViewModel_OnLayerDublicate;
            f_layersWindow.ViewModel.OnLayerSelectionChanged += LayerWindowViewModel_OnLayerSelectionChanged;

            f_layersWindow.Show();
        }

        private void LayerWindowViewModel_OnLayerSelectionChanged(object obj)
        {
            foreach (var layer in f_graphicContent.Layers)
            {
                if (layer.LayerName == ((LayerItem)(((ListBox)obj).SelectedItem)).LayerName)
                    layer.IsSelected = true;
                else
                    layer.IsSelected = false;
            }
        }

        private void LayerWindowViewModel_OnLayerDublicate()
        {
            Layer cloneLayer = new Layer();
            cloneLayer = f_graphicContent.SelectedLayer().Clone() as Layer;
            f_graphicContent.Layers.Add(cloneLayer);
            f_graphicContent.WorkSpace.Children.Add(cloneLayer);
            f_layersWindow.ViewModel.AddLayer(cloneLayer);
        }

        private void LayerWindowViewModel_OnLayerDelete()
        {
            throw new NotImplementedException();
        }

        private void LayerWindowViewModel_OnLayerCreate()
        {
            f_graphicContent.SelectedLayer().IsSelected = false;
            Layer layer = new Layer("New layer " + f_graphicContent.Layers.Count);
            f_graphicContent.Layers.Add(layer);
            f_graphicContent.WorkSpace.Children.Add(layer);
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

        private void NoToolSelectedExecute(object obj=null)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new NoToolSelected(f_graphicContent);
        }
        
        private void LineToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new LineToolSelected(f_graphicContent);
        }

        #endregion
    }
}
