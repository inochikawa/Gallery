using Combogallary.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Combogallary.Model.ProxyPattern
{
    public class PictureProxy : IPicture
    {
        Picture picture;
        Guid id;
        string dimension;
        public double Width;
        public double Height;
        DateTime dateAdded;
        string name;
        string location;
        long size;

        public PictureProxy(string name, string location)
        {
            this.name = name;
            this.location = location;
            this.dateAdded = DateTime.Now;
        }

        public DateTime DateAdded
        {
            get
            {
                if (picture == null)
                    return dateAdded;
                else
                    return picture.DateAdded;
            }

            set
            {
                if (picture == null)
                    dateAdded = value;
                else
                    picture.DateAdded = value;
            }
        }

        public string Dimension
        {
            get
            {
                if (picture == null)
                    return dimension ?? Width + "x" + Height;
                else
                    return picture.Dimension;
            }

            set
            {
                if (picture == null)
                    dimension = value;
            }
        }

        public Guid Id
        {
            get
            {
                if (picture == null)
                    return id;
                else
                    return picture.Id;
            }

            set
            {
                if (picture == null)
                    id = value;
                else
                    picture.Id = value;
            }
        }

        public string Location
        {
            get
            {
                if (picture == null)
                    return location;
                else
                    return picture.Location;
            }

            set
            {
                if (picture == null)
                    location = value;
                else
                    picture.Location = value;
            }
        }

        public string Name
        {
            get
            {
                if (picture == null)
                    return name;
                else
                    return picture.Name;
            }

            set
            {
                if (picture == null)
                    name = value;
                else
                    picture.Name = value;
            }
        }

        public long Size
        {
            get
            {
                if (picture == null)
                    return size;
                else
                    return picture.Size;
            }

            set
            {
                if (picture == null)
                    size = value;
            }
        }

        public BitmapImage Preview()
        {
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri(location, UriKind.RelativeOrAbsolute);
            bmpImage.DecodePixelWidth = 120;
            bmpImage.DecodePixelHeight = 120;
            bmpImage.EndInit();
            return bmpImage;
        }

        public BitmapImage Open()
        {
            if(picture == null)
                picture = new Picture(location);
            this.id = picture.Id;
            this.dimension = picture.Dimension;
            return picture.Image;
        }
    }
}
