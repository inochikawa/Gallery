using System;
using System.Windows.Media.Imaging;
using ActiveRecordPattern;
using ActiveRecordPattern.Attributes;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Combogallary.Model.Interfaces;

namespace Combogallary.Model
{
    [ActiveRecord]
    [Serializable]
    public class Picture : ActiveRecordBaseGeneric<Picture>, IPicture, INotifyPropertyChanged
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
        public Picture(int location)
        {
            Id = Guid.NewGuid();
            this._location = location;
            _image = Image;
            _dateAdded = DateTime.Now;
            _name = Image.UriSource.Fragment;
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

        public string Dimension
        {
            get
            {
                return _image.Width + "x" + _image.Height;
            }
            set { }
        }

        public long Size
        {
            get
            {
                return GetFileSizeOnDisk(_image.UriSource.AbsolutePath);
            }
            set { }
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

        public BitmapImage Open()
        {
            return _image;
        }
        #endregion


    }
}
