using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

namespace XuatThuy.Model
{
    public class SoLo : ViewModelBase
    {
        long _id;
        string _ten;
        bool _canDelete;
        public long ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Ten
        {
            get
            {
                return _ten;
            }
            set
            {
                _ten = value;
            }
        }

        public bool CanDelete
        {
            get
            {
                return _canDelete;
            }
            set
            {
                _canDelete = value;
                RaisePropertyChanged("CanDelete");
            }
        }

        public SoLo(long iD, string ten, bool canDelete)
        {
            ID = iD;
            Ten = ten;
            CanDelete = canDelete;
        }
    }
}
