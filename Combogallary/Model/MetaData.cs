using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveRecordPattern.Attributes;

namespace Combogallary.Model
{
    [ActiveRecord]
    public class MetaData<T> : MetaDataBase<T>
        where T: MetaData<T>
    {
        private Guid _idUser;

        [PropertyRecord]
        public Guid IdUser
        {
            get { return _idUser; }
            set
            {
                _idUser = value;
                OnPropertyChanged("IdUser");
            }
        }
    }
}
