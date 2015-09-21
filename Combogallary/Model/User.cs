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
    public class User : ActiveRecordBaseGeneric<User>, INotifyPropertyChanged
    {
        private string _login;
        private string _name;
        private string _surname;
        private string _email;
        private string _password;

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }
        
        [PropertyKeyRecord]
        public string Login 
        { 
            get { return _login ?? "no login"; }
            set
            {
                if (value != null || value != "")
                    _login = value;
                else _login = "null login";
                OnPropertyChanged("Login");
            }
        }
        [PropertyRecord]
        public string Name 
        {
            get { return _name ?? "no name"; }
            set
            {
                if (value != null || value != "")
                    _name = value;
                else _name = "null name";
                OnPropertyChanged("Name");
            }
        }
        [PropertyRecord]
        public string Surname 
        {
            get { return _surname ?? "WTF? =\\"; }
            set
            {
                if (value != null || value != "")
                    _surname = value;
                else _surname = "null surname";
                OnPropertyChanged("Surname");
            }
        }
        [PropertyRecord]
        public string Email
        {
            get { return _email ?? "WTF? =\\"; }
            set
            {
                if (value != null || value != "")
                    _email = value;
                else _email = "null surname";
                OnPropertyChanged("Email");
            }
        }
        [PropertyRecord]
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != null || value != "")
                    _password = value;
                else _password = "null surname";
                OnPropertyChanged("Password");
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
