using GraphicEditor.Model.ToolBehavior.ToolProperties;

namespace GraphicEditor.Model
{
    public interface IViewModel
    {
        void Subscribe(GraphicToolProperties observer);
        void Unsubscribe(GraphicToolProperties observer);
        void Notify();
    }
}