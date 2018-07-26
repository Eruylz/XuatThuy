using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.Model
{
    public class PhieuChatLuong : ViewModelBase
    {
        long _id;
        string _ten;
        DateTime? _ngayCap;
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

        public DateTime? NgayCap
        {
            get
            {
                return _ngayCap;
            }
            set
            {
                _ngayCap = value;
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
        public PhieuChatLuong(long iD, string ten, DateTime? ngayCap, bool canDelete)
        {
            ID = iD;
            Ten = ten;
            NgayCap = ngayCap;
            CanDelete = canDelete;
        }
    }
}
