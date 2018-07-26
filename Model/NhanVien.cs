using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XuatThuy.Model
{
    public class NhanVien : INotifyPropertyChanged
    {
        long _ID;
        public long ID { get => _ID; set => _ID = value; }

        int _CaNhanID;
        public int CaNhanID
        {
            get { return _CaNhanID; }
            set
            {
                if (_CaNhanID != value)
                {
                    _CaNhanID = value;
                    RaisePropertyChanged("CaNhanID");
                }
            }
        }

        int _ToChucID;
        public int ToChucID
        {
            get { return _ToChucID; }
            set
            {
                if (_ToChucID != value)
                {
                    _ToChucID = value;
                    RaisePropertyChanged("ToChucID");
                }
            }
        }

        string _HoVaTen;
        public string HoVaTen
        {
            get { return _HoVaTen; }
            set
            {
                if (_HoVaTen != value)
                {
                    _HoVaTen = value;
                    RaisePropertyChanged("HoVaTen");
                }
            }
        }

        public NhanVien(long iD, int caNhanID, int toChucID, string hoVaTen)
        {
            ID = iD;
            CaNhanID = caNhanID;
            ToChucID = toChucID;
            HoVaTen = hoVaTen;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
}
