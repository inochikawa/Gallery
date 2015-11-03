using System;
using System.Windows.Input;
using GraphicEditor.Model;
using GraphicEditor.Model.GraphicContentStatePattern;

namespace GraphicEditor.ViewModel
{
    public class MainWindowViewModel
    {
        private ICommand f_pointerToolSelectedCommand;
        private ICommand f_noToolSelectedCommand;
        private ICommand f_brushToolSelectedCommand;
        private GraphicContent f_graphicContent;

        public MainWindowViewModel()
        {
            f_pointerToolSelectedCommand = new RelayCommand(PointerToolSelectedExecute);
            f_noToolSelectedCommand = new RelayCommand(NoToolSelectedExecute);
            f_brushToolSelectedCommand = new RelayCommand(BrushToolSelectedExecute);
            f_graphicContent = new GraphicContent();
        }

        public GraphicContent GraphicContent
        {
            get { return f_graphicContent; }
            set { f_graphicContent = value; }
        }

        public ICommand PointerToolSelectedCommand
        {
            get
            {
                return f_pointerToolSelectedCommand;
            }
            set
            {
                f_pointerToolSelectedCommand = value;
            }
        }

        public ICommand NoToolSelectedCommand
        {
            get
            {
                return f_noToolSelectedCommand;
            }
            set
            {
                f_noToolSelectedCommand = value;
            }
        }

        public ICommand BrushToolSelectedCommand
        {
            get
            {
                return f_brushToolSelectedCommand;
            }
            set
            {
                f_brushToolSelectedCommand = value;
            }
        }

        private void PointerToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new PointerToolSelected(f_graphicContent);
        }

        private void BrushToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new BrushToolSelected(f_graphicContent);
        }

        private void NoToolSelectedExecute(object obj)
        {
            f_graphicContent.GraphicContentState.Dispose();
            f_graphicContent.GraphicContentState = new NoToolSelected(f_graphicContent);
        }

    }
}
