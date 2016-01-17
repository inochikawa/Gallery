using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.ToolBehavior;
using GraphicEditor.View.UserControls.LayersControl;
using GraphicEditor.ViewModel;

namespace GraphicEditor.Model.ChildWindowBehavior.Factories
{
    public class LayersChildWindowFactory : IChildWindowFactory
    {
        private  string Path = AppDomain.CurrentDomain.BaseDirectory + @"\Parameters\LayersChildWindow.cwd";

        public LayersChildWindowFactory(GraphicContent graphicContent)
        {
            GraphicContent = graphicContent;
            ChildWindow = new LayersViewChildWindow { Header = "Layers" };
            ChildWindow.Move(-20, 10);
            ((LayersViewViewModel)ChildWindow.ViewModel).AddLayer(GraphicContent.SelectedLayer);
            ((LayersViewViewModel)ChildWindow.ViewModel).OnLayerCreate += LayerWindowViewModel_OnLayerCreate;
            ((LayersViewViewModel)ChildWindow.ViewModel).OnLayerDelete += LayerWindowViewModel_OnLayerDelete;
            ((LayersViewViewModel)ChildWindow.ViewModel).OnLayerDublicate += LayerWindowViewModel_OnLayerDublicate;
            GraphicContent.OnLayerCreate += GraphicContentOnOnLayerCreate;
            GraphicContent.OnLayerRemove += GraphicContentOnOnLayerRemove;
        }

        private void GraphicContentOnOnLayerRemove(Layer layer)
        {
            if (layer == null) return;

            GraphicContent.WorkSpace.Children.Remove(layer);
            GraphicContent.Layers.Remove(layer);
            ((LayersViewViewModel)ChildWindow.ViewModel).RemoveLayer(layer);
        }

        private void GraphicContentOnOnLayerCreate(Layer layer)
        {
            ((LayersViewViewModel)ChildWindow.ViewModel).AddLayer(layer);
            GraphicContent.AddLayer(layer);
        }

        public IChildWindow ChildWindow { get; set; }

        public GraphicContent GraphicContent { get; set; }

        public void SaveState()
        {
            WindowParameters windowParameters = new WindowParameters()
            {
                Height = (int)ChildWindow.ChildWindow.Height,
                Width = (int)ChildWindow.ChildWindow.Width,
                X = (int)((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).X,
                Y = (int)((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).Y,
                IsVisible = ChildWindow.ChildWindow.IsVisible
            };

            windowParameters.Save(Path);
        }

        public void LoadState()
        {
            WindowParameters windowParameters = WindowParameters.Load(Path);

            if (windowParameters == null) return;

            ChildWindow.ChildWindow.Width = windowParameters.Width;
            ChildWindow.ChildWindow.Height = ChildWindow.ChildWindow.Height;
            ((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).Y = windowParameters.Y;
            ((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).X = windowParameters.X;

            ChildWindow.ChildWindow.Visibility = windowParameters.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }

        private void LayerWindowViewModel_OnLayerCreate()
        {
            Layer layer = new Layer("New layer " + GraphicContent.Layers.Count);
            ((LayersViewViewModel)ChildWindow.ViewModel).AddLayer(layer);
            GraphicContent.AddLayer(layer);
        }

        private void LayerWindowViewModel_OnLayerDublicate()
        {
            var cloneLayer = GraphicContent.SelectedLayer.Clone();
            if (cloneLayer == null)
                throw new ArgumentNullException(nameof(cloneLayer));
            GraphicContent.AddLayer(cloneLayer);
            ((LayersViewViewModel)ChildWindow.ViewModel).AddLayer(cloneLayer);
        }

        private void LayerWindowViewModel_OnLayerDelete(object obj)
        {
            if (obj == null)
                return;

            LayerItem layerItem = (LayerItem)((ListBox)obj).SelectedItem;
            Layer currentLayer = null;

            foreach (var layer in GraphicContent.Layers)
            {
                if (layer.LayerName == layerItem.LayerName)
                    currentLayer = layer;
            }

            GraphicContent.WorkSpace.Children.Remove(currentLayer);
            GraphicContent.Layers.Remove(currentLayer);
            ((LayersViewViewModel)ChildWindow.ViewModel).RemoveLayer(layerItem);
        }
    }
}
