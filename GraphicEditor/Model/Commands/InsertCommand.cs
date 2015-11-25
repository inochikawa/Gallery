using System.Windows;
using System.Windows.Controls;

namespace GraphicEditor.Model.Commands
{
    public class InsertCommand : ICommand
    {
        private UIElement _uiElement;
        private Canvas _container;

        public InsertCommand(UIElement uiElement, Canvas container)
        {
            _uiElement = uiElement;
            _container = container;
        }
        
        public void Execute()
        {
            if (!_container.Children.Contains(_uiElement))
                _container.Children.Add(_uiElement);
        }

        public void Unexecute()
        {
            if (_container.Children.Contains(_uiElement))
                _container.Children.Remove(_uiElement);
        }
    }
}
