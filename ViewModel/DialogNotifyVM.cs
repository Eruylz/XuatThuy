using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.ViewModel
{
    class DialogNotifyVM : ViewModelBase
    {
        string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public DialogNotifyVM(string message)
        {
            Message = message;
        }

        public DialogNotifyVM(string message, string password)
        {
            Message = message;
            Password = password;
        }
    }
}
