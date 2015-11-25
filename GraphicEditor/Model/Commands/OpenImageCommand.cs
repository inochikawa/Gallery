using System;
using System.Windows.Controls;

namespace GraphicEditor.Model.Commands
{
    class OpenImageCommand : ICommand
    {
        private Layer f_layer;
        private Image f_image;

        public OpenImageCommand(Image image, Layer layer)
        {
            f_image = image;
            f_layer = layer;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
