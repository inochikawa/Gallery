using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using GraphicEditor.Model.ToolBehavior;

namespace GraphicEditor.Model.Commands
{
    public class CommandReceiver
    {
        private readonly Stack<ICommand> f_undoCommands = new Stack<ICommand>();
        private readonly Stack<ICommand> f_redoCommands = new Stack<ICommand>();
        private ICommand f_command;

        //// public event EventHandler CommandEvent;
        //// private Canvas f_сontainer;
        //// public Canvas Container
        //// {
        ////    get { return f_сontainer; }
        ////    set { f_сontainer = value; }
        //// }

        public void Redo(int levels)
        {
            for (var i = 1; i <= levels; i++)
            {
                if (f_redoCommands.Count != 0)
                {
                    var command = f_redoCommands.Pop();
                    command.Execute();
                    f_undoCommands.Push(command);
                }
            }
        }

        public void Undo(int levels)
        {
            for (var i = 1; i <= levels; i++)
            {
                if (f_undoCommands.Count != 0)
                {
                    var command = f_undoCommands.Pop();
                    command.Unexecute();
                    f_redoCommands.Push(command);
                }
            }
        }
        
        public void Insert(UIElement element, Layer layer)
        {
            f_command = new InsertCommand(element, layer);
            f_undoCommands.Push(f_command);
            f_redoCommands.Clear();
            f_command.Execute();
        }

        public void InsertImage(BitmapImage image, Layer layer)
        {
            f_command = new InsertImageCommand(image, layer);
            f_undoCommands.Push(f_command);
            f_redoCommands.Clear();
            f_command.Execute();
        }

        public void InsertGeFile(string filename, GraphicContent graphicContent)
        {
            f_command = new InsertGeFileCommand(filename, graphicContent);
            f_undoCommands.Push(f_command);
            f_redoCommands.Clear();
            f_command.Execute();
        }

        public void Delete(UIElement element, Layer layer)
        {
            f_command = new DeleteCommand(element, layer);
            f_undoCommands.Push(f_command);
            f_redoCommands.Clear();
            f_command.Execute();
        }
        
        public void StartMove(UIElement element, double x, double y)
        {
            f_command = new MoveCommand(element, x, y);
            f_undoCommands.Push(f_command);
            f_redoCommands.Clear();
        }

        public void Move(UIElement element, double x, double y)
        {
            if (f_command == null)
                return;

            if (f_command.GetType() != typeof(MoveCommand))
                return;

            ((MoveCommand)f_command).Move(element, x, y);
        }

        public void EndMove(UIElement element, double x, double y)
        {
            if(f_command == null)
                return;

            if (f_command.GetType() != typeof(MoveCommand))
                return;

            f_command.Execute();
        }

        public bool IsUndoPossible()
        {
            if (f_undoCommands.Count != 0)
                return true;
            return false;
        }

        public bool IsRedoPossible()
        {
            if (f_redoCommands.Count != 0)
                return true;
            return false;
        }
    }
}
