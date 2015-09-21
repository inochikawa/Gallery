using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ActiveRecordPattern;
using ActiveRecordPattern.Attributes;
using System.IO;
using System.Xml.Schema;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Combogallary.Model
{  
    [ActiveRecord]
    [Serializable]
    public class Picture : ActiveRecordBaseGeneric<Picture>, INotifyPropertyChanged
    {
        private Guid _id;
        private BitmapImage _image;
        private DateTime _dateAdded;
        private Guid _idAlbum;
        private string _name;
        private string _location;

        public Picture()
        {

        }
        public Picture(BitmapImage image)
        {
            Id = Guid.NewGuid();
            _image = image;
            _dateAdded = DateTime.Now;
            _name = image.UriSource.Fragment;
        }

        [PropertyKeyRecord]
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }


        [PropertyRecord]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        [PropertyRecord]
        public Guid IdAlbum
        {
            get { return _idAlbum; }
            set 
            {
                if (value != null)
                    _idAlbum = value;
                else System.Windows.Forms.MessageBox.Show("No id of album");
                OnPropertyChanged("IdAlbum");
            }
        }

        public BitmapImage Image
        {
            get 
            {
                if (_image == null)
                {
                    _image = new BitmapImage(new Uri(_location));
                }
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        //public byte[] ImageBytes
        //{
        //    get
        //    {
        //        if (_imageBytes == null && _image != null)
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                BinaryFormatter bf = new BinaryFormatter();
        //                bf.Serialize(ms, this);
        //                _imageBytes = ms.ToArray();
        //            }
        //            return _imageBytes;
        //        }

        //        if (_imageBytes == null && _image == null)
        //        {
        //            List<byte> data = new List<byte>();
        //            StreamReader reader = null;

        //            //Ловим исключения. Если например файл не будет найден, то в консоли выведетс соответствующее вообщение
        //            try
        //            {
        //                reader = new StreamReader(ImageBytesLocation);
        //            }
        //            catch (Exception e) {}

        //            while (!reader.EndOfStream)
        //            {
        //                data.Add(Convert.ToByte(reader.ReadLine()));
        //            }
        //            reader.Close();
        //            _imageBytes = data.ToArray();
        //            return _imageBytes;
        //        }
        //        return _imageBytes;
        //    }
        //    set
        //    {
        //        _imageBytes = value;
        //        OnPropertyChanged("Image");
        //    }
        //}

        //[PropertyRecord]
        //public string ImageBytesLocation
        //{
        //    get
        //    {
        //        return @"Source\ImagesInBytes\" + _id.ToString() + ".iminbt";
        //    }
        //    set { }
        //}

        //public override void Save()
        //{
        //    base.Save();
        //    using (StreamWriter writer = new StreamWriter(ImageBytesLocation))
        //    {
        //        foreach (var item in ImageBytes)
        //        {
        //            writer.WriteLine(item);
        //        }
        //    }
        //}

        //[PropertyRecord(Name = "BytesCount")]
        //public int ImageBytesCount
        //{
        //    get
        //    {
        //        if (_imageBytes == null)
        //        {
        //            _imageBytesCount = ImageBytes.Count();
        //        }
        //        return _imageBytesCount;
        //    }
        //    set
        //    {
        //        _imageBytesCount = value;
        //        OnPropertyChanged("ImageBytesCount");
        //    }
        //}

        //public BitmapImage ImageFromBytes(Byte[] bytes)
        //{
        //    var image = new BitmapImage();
        //    using (var ms = new MemoryStream(bytes))
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        ms.Seek(0, SeekOrigin.Begin);
        //        image = (BitmapImage) formatter.Deserialize(ms);
        //    }
        //    return image;
        //}

        //public Stream GenerateStreamFromString(string s)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write(s);
        //    writer.Flush();
        //    stream.Position = 0;
        //    return stream;
        //}

        [PropertyRecord]
        public string Location
        {
            get
            {
                return _location ?? _image.UriSource.AbsolutePath;
            }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        public string Dimensions
        {
            get
            {
                return _image.Width + "x" + _image.Height;
            }
        }

        public long Size
        {
            get
            {
                return GetFileSizeOnDisk(_image.UriSource.AbsolutePath);
            }
        }

        public static long GetFileSizeOnDisk(string file)
        {
            FileInfo info = new FileInfo(file);
            uint dummy, sectorsPerCluster, bytesPerSector;
            int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
            if (result == 0) throw new Win32Exception();
            uint clusterSize = sectorsPerCluster * bytesPerSector;
            uint hosize;
            uint losize = GetCompressedFileSizeW(file, out hosize);
            long size;
            size = (long)hosize << 32 | losize;
            return ((size + clusterSize - 1) / clusterSize) * clusterSize;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);

        //public List<BitmapImage> NextImage()
        //{ 
        //        List<BitmapImage> img = new List<BitmapImage>();
        //        foreach (var pic in Pictures)
        //        {
        //            img.Add(pic.Image);
        //        }
        //        return img; 
        //}

        [PropertyRecord]
        public DateTime DateAdded
        {
            get { if (_dateAdded != null) return _dateAdded; return new DateTime(1000, 01, 01); }
            set
            {
                if (value != null)
                    _dateAdded = value;
                else _dateAdded = new DateTime(1000, 01, 01);
                OnPropertyChanged("DateAdded");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

       
    }
}
