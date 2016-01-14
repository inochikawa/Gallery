using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace GraphicEditor.Model.ToolBehavior.GraphicBuilderBehavior
{
    public class ImageBuilder : GraphicBuilder
    {
        public ImageBuilder():base()
        {
        }

        public override void Buid(string imageName)
        {
            if(imageName == null) return;

            Panel.Measure(new Size((int)Panel.Width, (int)Panel.Height));
            Panel.Arrange(new Rect(new Size((int)Panel.Width, (int)Panel.Height)));

            int height = (int)Panel.Height;
            int width = (int)Panel.Width;

            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(Panel);

            string extension = Path.GetExtension(imageName)?.ToLower();

            BitmapEncoder encoder;
            if (extension == ".bmp")
                encoder = new BmpBitmapEncoder();
            else if (extension == ".png")
                encoder = new PngBitmapEncoder();
            else if (extension == ".jpg")
                encoder = new JpegBitmapEncoder();
            else
                return;

            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stm = File.Create(imageName))
            {
                encoder.Save(stm);
            }
        }

        public override string FileName()
        {
            // Create OpenFileDialog
            var dlg = new SaveFileDialog()
            {
                Filter =
                    "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|Bmp Files (*.bmp)|*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Save as",
                AddExtension = true
            };

            var result = dlg.ShowDialog();

            if (result != true) return null;

            return dlg.FileName;
        }
    }
}