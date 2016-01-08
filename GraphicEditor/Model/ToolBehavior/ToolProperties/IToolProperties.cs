using System.Windows.Media;

namespace GraphicEditor.Model.ToolBehavior.ToolProperties
{
    public interface IToolProperties
    {
        Color? Color { get; set; }
        string ToolKey { get; set; }

        void UpdateProperties(IToolProperties toolProperties);
    }
}