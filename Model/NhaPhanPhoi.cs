using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XuatThuy.Model
{
    public class NhaPhanPhoi : INotifyPropertyChanged
    {
        long _ID;
        public long ID { get => _ID; set => _ID = value; }

        int _CustomerID;
        public int CustomerID
        {
            get { return _CustomerID; }
            set
            {
                if (_CustomerID != value)
                {
                    _CustomerID = value;
                    RaisePropertyChanged("CustomerID");
                }
            }
        }

        string _CustomerName;
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    RaisePropertyChanged("CustomerName");
                }
            }
        }

        public NhaPhanPhoi(long iD, int customerID, string customerName)
        {
            ID = iD;
            CustomerID = customerID;
            CustomerName = customerName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
}
