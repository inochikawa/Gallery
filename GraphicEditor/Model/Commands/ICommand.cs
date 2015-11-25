namespace GraphicEditor.Model.Commands
{
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }
}
