using GraphicEditor.Model;
using GraphicEditor.View.UserControls.LayersControl;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace GraphicEditor.ViewModel
{
    public class LayersWindowViewModel
    {
        private ObservableCollection<LayerItem> f_layerItems;

        public LayersWindowViewModel()
        {
            f_layerItems = new ObservableCollection<LayerItem>();
            CreateNewLayerCommand = new RelayCommand(createNewLayerExecute);
            DublicateSelectedLayerCommand = new RelayCommand(dublicateSelectedLayerExecute);
            DeleteSelectedLayerCommand = new RelayCommand(deleteSelectedLayerExecute);
        }

        public void LayersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OnLayerSelectionChanged != null)
                OnLayerSelectionChanged(sender);
        }

        public delegate void LayerProcessing();
        public delegate void ListBoxProcessing(object obj);

        public event LayerProcessing OnLayerDelete;
        public event LayerProcessing OnLayerDublicate;
        public event LayerProcessing OnLayerCreate;
        public event ListBoxProcessing OnLayerSelectionChanged;

        private void deleteSelectedLayerExecute(object obj)
        {
            OnLayerDelete();
        }

        private void dublicateSelectedLayerExecute(object obj)
        {
            OnLayerDublicate();
        }

        private void createNewLayerExecute(object obj)
        {
            OnLayerCreate();
        }

        public ICommand CreateNewLayerCommand { get; set; }
        public ICommand DublicateSelectedLayerCommand { get; set; }
        public ICommand DeleteSelectedLayerCommand { get; set; }
        
        public ObservableCollection<LayerItem> LayerItems
        {
            get
            {
                return f_layerItems;
            }

            set
            {
                f_layerItems = value;
            }
        }

        public void AddLayer(Layer layer)
        {
            LayerItem layerItem = new LayerItem();
            layerItem.IsChecked = layer.IsActive;
            layerItem.IsSelected = layer.IsSelected;
            layerItem.LayerName = layer.LayerName;
            layerItem.Preview = layer.Preview();
            layerItem.OnCheckBoxChecked += layer.ActivateLayer;
            layerItem.OnCheckBoxUnchecked += layer.UnactivateLayer;
            layer.OnLayerMouseLeftButtonUp += layerItem.LayerMouseLeftButtonUp;

            f_layerItems.Add(layerItem);
        }
        public void UpdateLayers(List<Layer> layers)
        {
            if (layers.Count != f_layerItems.Count)
                return;

            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].IsActive = f_layerItems[i].IsChecked;
                layers[i].IsSelected = f_layerItems[i].IsSelected;
            }
        }
    }
}
