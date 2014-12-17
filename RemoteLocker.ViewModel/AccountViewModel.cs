using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteLocker.Common.Model;
using System.ComponentModel;

namespace RemoteLocker.ViewModel
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Account account;

        public Account Account
        {
            get { return account; }
            set
            {
                if(!value.Equals(account))
                {
                    account = value;
                    OnPropertyChanged("Account");
                }
            }
        }

        public AccountViewModel(Account account)
        {
            this.account = account;
        }

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
