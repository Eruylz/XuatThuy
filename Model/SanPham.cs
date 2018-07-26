using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XuatThuy.Model
{
   public class SanPham : INotifyPropertyChanged
    {
        long _ID;
        public long ID { get => _ID; set => _ID = value; }

        int? _ItemID;
        public int? ItemID
        {
            get { return _ItemID; }
            set
            {
                if (_ItemID != value)
                {
                    _ItemID = value;
                    RaisePropertyChanged("ItemID");
                }
            }
        }

        string _Ten;
        public string Ten
        {
            get { return _Ten; }
            set
            {
                if (_Ten != value)
                {
                    _Ten = value;
                    RaisePropertyChanged("Ten");
                }
            }
        }

        public SanPham(long iD, int? itemID, string ten)
        {
            ID = iD;
            ItemID = itemID;
            Ten = ten;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
}
