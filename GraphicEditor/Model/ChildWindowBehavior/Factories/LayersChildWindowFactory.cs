﻿using System;
using System.Windows.Controls;
using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.GraphicContentStatePattern;
using GraphicEditor.View.UserControls.LayersControl;
using GraphicEditor.ViewModel;

namespace GraphicEditor.Model.ChildWindowBehavior.Factories
{
    public class LayersChildWindowFactory : IChildWindowFactory
    {
        public LayersChildWindowFactory(GraphicContent graphicContent)
        {
            GraphicContent = graphicContent;
            ChildWindow = new LayersViewChildWindow {Header = "Layers"};
            ChildWindow.Move(590, 10);
            ((LayersViewViewModel)ChildWindow.ViewModel).AddLayer(GraphicContent.SelectedLayer);
            ((LayersViewViewModel)ChildWindow.ViewModel).OnLayerCreate += LayerWindowViewModel_OnLayerCreate;
            ((LayersViewViewModel)ChildWindow.ViewModel).OnLayerDelete += LayerWindowViewModel_OnLayerDelete;
            ((LayersViewViewModel)ChildWindow.ViewModel).OnLayerDublicate += LayerWindowViewModel_OnLayerDublicate;
        }

        public IChildWindow ChildWindow { get; set; }

        public GraphicContent GraphicContent { get; set; }

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
