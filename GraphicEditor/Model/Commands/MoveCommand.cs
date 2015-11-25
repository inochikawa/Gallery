using System.Windows;
using System.Windows.Controls;

namespace GraphicEditor.Model.Commands
{
    public class MoveCommand : ICommand
    {
        private UIElement f_element;
        private double f_startX;
        private double f_startY;
        private double f_lastX;
        private double f_lastY;

        public MoveCommand(UIElement element, double x, double y)
        {
            f_element = element;
            f_startX = x;
            f_startY = y;
        }

        /// <summary>
        /// Move element to last point, when mouse left button up
        /// </summary>
        public void Execute()
        {
            Canvas.SetTop(f_element, f_lastY);
            Canvas.SetLeft(f_element, f_lastX);
        }

        /// <summary>
        /// Move element to first point, when mouse left button up
        /// </summary>
        public void Unexecute()
        {
            Canvas.SetTop(f_element, f_startY);
            Canvas.SetLeft(f_element, f_startX);
        }
        
        /// <summary>
        /// Move element, when mouse left button is pressed.
        /// </summary>
        /// <param name="element">UIElement</param>
        /// <param name="x">Double x</param>
        /// <param name="y">Double y</param>
        public void Move(UIElement element, double x, double y)
        {
            Canvas.SetTop(f_element, y);
            Canvas.SetLeft(f_element, x);
            f_lastX = x;
            f_lastY = y;
        }
    }
}
