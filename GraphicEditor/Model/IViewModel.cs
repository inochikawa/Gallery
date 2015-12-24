using GraphicEditor.Model.GraphicContentStatePattern;

namespace GraphicEditor.Model
{
    public interface IViewModel
    {
        void Subscribe(GraphicTool observer);
        void Unsubscribe(GraphicTool observer);
        void Notify();
    }
}