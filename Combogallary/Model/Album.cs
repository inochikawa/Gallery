using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveRecordPattern;
using ActiveRecordPattern.Attributes;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace Combogallary.Model
{
    [ActiveRecord]
    public class Album : ActiveRecordBaseGeneric<Album>, INotifyPropertyChanged
    {
        private Guid _id;
        private string _name;
        private DateTime _dateCreated;
        private bool _publicAccess;
        public Album(int name)
        {
            _name = name;
            _dateCreated = DateTime.Now;
            this.Id = Guid.NewGuid();
            this.PublicAccess = false;
        }

        public Album() { }

        public BitmapImage Background
        {
            get
            {
                Random random = new Random();
                List<Picture> pictures = new List<Picture>(Picture.LoadSpecificPictures(_id));
                return pictures[random.Next(0, pictures.Count)].Image;
            }
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
        public bool PublicAccess 
        {
            get { return _publicAccess; }
            set
            {
                _publicAccess = value;
                OnPropertyChanged("PublicAccess");
            }
        }

        [PropertyRecord]
        public string Name
        {
            get { return _name ?? "no name"; }
            set 
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ShortName
        {
            get
            {
                if (_name.Length > 22)
                    return _name.Substring(0, 18) + "...";
                else
                    return _name;
            }
        }

        public string VeryShortName
        {
            get
            {
                if (_name.Length > 12)
                    return _name.Substring(0, 11) + "...";
                else
                    return _name;
            }
        }

        [PropertyRecord]
        public DateTime DateCreated
        {
            get { if (_dateCreated != null) return _dateCreated; return new DateTime(1000, 01, 01); }
            set
            {
                if (value != null)
                    _dateCreated = value;
                else _dateCreated = new DateTime(1000, 01, 01);
                OnPropertyChanged("DateCreated");
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
