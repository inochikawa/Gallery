using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphicEditor.Model
{
    public class GraphicContent
    {
        public GraphicContent()
        {
            Layers = new List<Layer>();
        }

        public List<Layer> Layers { get; set; }
    }
}
