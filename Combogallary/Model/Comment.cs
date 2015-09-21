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
    class Comment : MetaData<Comment>
    {
        private string _text;

        [PropertyRecord]
        public string Text
        {
            get { return _text; }
            set 
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public Comment(Guid idUser, Guid idPicture, string text)
        {
            this.IdUser = idUser;
            this.IdPicture = idPicture;
            _text = text;
            this.Id = Guid.NewGuid();
        }
    }
}
