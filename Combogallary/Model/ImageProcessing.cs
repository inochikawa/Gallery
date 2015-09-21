using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Combogallary.Model
{
    public static class ImageProcessing
    {
        public static List<BitmapImage> AddImagesToList(string dirPath)
        {
            List<BitmapImage> images = new List<BitmapImage>();
            try
            {
                foreach (var filePath in Directory.GetFiles(dirPath, "*.jpg"))
                    images.Add(new BitmapImage(new Uri(System.IO.Path.GetFullPath(filePath))));
            }
            catch (DirectoryNotFoundException)
            {
                return images;
            }
            
            return images;
        }

        public static void SetBorderBackgroundSlider(ImageBrush image, Border border)
        {
            //Create the fade out animation. 
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(0, TimeSpan.FromMilliseconds(500));
            fadeOutAnimation.AutoReverse = false;

            //wait until the first animation is complete before changing the background, or else it will appear to just "fadeIn" with now fadeout.
            fadeOutAnimation.Completed += delegate(object sender, EventArgs e)
            {
                //once the fadeout is complete set the new back ground and fade back in. 
                //Create a new background brush.
                image.Opacity = 0;

                //Set the grid background to the new brush. 
                border.Background = image;

                //Set the brush...(not the background property) with the animation. 
                DoubleAnimation fadeInAnimation = new DoubleAnimation(1, TimeSpan.FromMilliseconds(1000));
                fadeInAnimation.AutoReverse = false;
                image.BeginAnimation(Brush.OpacityProperty, fadeInAnimation);
            };

            //Fade out..before changing the background.
            var currentBackground = border.Background;
            currentBackground.BeginAnimation(Brush.OpacityProperty, fadeOutAnimation);
        }
    }
}
