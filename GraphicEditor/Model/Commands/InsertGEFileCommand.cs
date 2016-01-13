using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Markup;
using GraphicEditor.Model.ToolBehavior;

namespace GraphicEditor.Model.Commands
{
    public class InsertGeFileCommand : ICommand
    {
        private List<Layer> f_layers;
        private Canvas f_container;
        private GraphicContent f_graphicContent;

        public InsertGeFileCommand(string fileName, GraphicContent graphicContent)
        {
            f_layers = new List<Layer>();
            Canvas loadedCanvas;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                loadedCanvas = XamlReader.Load(fileStream) as Canvas;
            }

            if (loadedCanvas == null) return;

            foreach (Layer layer in loadedCanvas.Children)
                f_layers.Add(layer.Clone());

            f_graphicContent = graphicContent;
            f_container = f_graphicContent.WorkSpace;
        }

        public void Execute()
        {
            foreach (Layer layer in f_layers)
                f_graphicContent.AddLayerEventHandler(layer);
        }

        public void Unexecute()
        {
            foreach (Layer layer in f_layers)
                if (f_container.Children.Contains(layer))
                    f_container.Children.Remove(layer);
        }
    }
}