using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using GraphicEditor.Model;
using GraphicEditor.View.UserControls.LayersControl;

namespace GraphicEditor.ViewModel
{
    public class LayersWindowViewModel
    {
        private ObservableCollection<LayerItem> f_layerItems;
        private ListBox f_listBox;

        public LayersWindowViewModel()
        {
            f_layerItems = new ObservableCollection<LayerItem>();
            CreateNewLayerCommand = new RelayCommand(CreateNewLayerExecute);
            DublicateSelectedLayerCommand = new RelayCommand(DublicateSelectedLayerExecute);
            DeleteSelectedLayerCommand = new RelayCommand(DeleteSelectedLayerExecute);
        }

        public void LayersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OnLayerSelectionChanged != null)
            {
                OnLayerSelectionChanged(sender);
                f_listBox = sender as ListBox;
            }
        }

        public delegate void LayerProcessing();
        public delegate void ListBoxProcessing(object obj);

        public event ListBoxProcessing OnLayerDelete;
        public event LayerProcessing OnLayerDublicate;
        public event LayerProcessing OnLayerCreate;
        public event ListBoxProcessing OnLayerSelectionChanged;

        private void DeleteSelectedLayerExecute(object obj)
        {
            OnLayerDelete?.Invoke(f_listBox);
        }

        private void DublicateSelectedLayerExecute(object obj)
        {
            OnLayerDublicate?.Invoke();
        }

        private void CreateNewLayerExecute(object obj)
        {
            OnLayerCreate?.Invoke();
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

        public ListBox ListBox
        {
            get
            {
                return f_listBox;
            }

            set
            {
                f_listBox = value;
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

            f_layerItems.Insert(0, layerItem);
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

        internal void RemoveLayer(LayerItem layerItem)
        {
            f_layerItems.Remove(layerItem);
        }
    }
}
