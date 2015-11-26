namespace GraphicEditor.Model
{
    public interface IViewModel
    {
        void Subscribe(ITool observer);
        void Unsubscribe(ITool observer);
        void Notify();
    }
}