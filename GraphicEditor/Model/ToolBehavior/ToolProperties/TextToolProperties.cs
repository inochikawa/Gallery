using System;
using System.Windows.Media;

namespace GraphicEditor.Model.ToolBehavior.ToolProperties
{
    public class TextToolProperties : IToolProperties
    {
        public Color? Color { get; set; }
        public string ToolKey { get; set; }

        public void UpdateProperties(IToolProperties toolProperties)
        {
            throw new NotImplementedException();
        }
    }
}