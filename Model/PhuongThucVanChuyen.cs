using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XuatThuy.Model
{
    public class PhuongThucVanChuyen : INotifyPropertyChanged
    {
        int _ID;
        public int ID { get => _ID; set => _ID = value; }

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

        public PhuongThucVanChuyen(int iD, string ten)
        {
            ID = iD;
            Ten = ten;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
}
