using System.Windows.Media;

namespace GraphicEditor.Model.ToolBehavior.ToolProperties
{
    public class GraphicToolProperties : IToolProperties
    {
        public GraphicToolProperties(string key)
        {
            ToolKey = key;
        }

        public GraphicToolProperties()
        {
        }

        public Color? Color { get; set; }
        public string ToolKey { get; set; }
        public double? Thickness { get; set; }
        public double? Softness { get; set; }

        public void UpdateProperties(IToolProperties toolProperties)
        {
            if(((GraphicToolProperties)toolProperties).Color != null)
                Color = ((GraphicToolProperties)toolProperties).Color;

            if (((GraphicToolProperties)toolProperties).Thickness != null)
                Thickness = ((GraphicToolProperties)toolProperties).Thickness;

            if (((GraphicToolProperties)toolProperties).Softness != null)
                Softness = ((GraphicToolProperties)toolProperties).Softness;
        }
    }
}
