using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using CommonServiceLocator;
using XuatThuy.Controls;
using System.Windows;

namespace XuatThuy.ViewModel
{
    public class MaChiVM : ViewModelBase
    {
        XuatHangDataContext db = new XuatHangDataContext();
        public long PhieuCanID { get; set; }

        string _MaChiA;
        public string MaChiA
        {
            get => _MaChiA;
            set
            {
                if (_MaChiA != value)
                {
                    _MaChiA = value;
                    RaisePropertyChanged("MaChiA");
                }
            }
        }

        string _MaChiB;
        public string MaChiB
        {
            get => _MaChiB;
            set
            {
                if (_MaChiB != value)
                {
                    _MaChiB = value;
                    RaisePropertyChanged("MaChiB");
                }
            }
        }

        public MaChiVM(long phieuCanID)
        {
            PhieuCanID = phieuCanID;

            var machis = db.p_PhieuCan_MaChi_Lay(null, phieuCanID).ToList();

            if (machis.Count >= 2)
            {
                MaChiA = machis[0].MaChi;
                MaChiB = machis[1].MaChi;
            }
        }

    }
}
