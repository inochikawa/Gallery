using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveRecordPattern;
using ActiveRecordPattern.Attributes;
using System.ComponentModel;

namespace Combogallary.Model
{
    [ActiveRecord]
    public class MetaDataBase<T> : ActiveRecordBaseGeneric<T>, INotifyPropertyChanged
        where T : MetaDataBase<T>
    {
        private Guid _id;
        private Guid _idPricture;

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
        public Guid IdPicture 
        {
            get { return _idPricture; }
            set 
            {
                _idPricture = value;
                OnPropertyChanged("IdPicture");
            } 
        }
        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
