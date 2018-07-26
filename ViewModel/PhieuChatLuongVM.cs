using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using XuatThuy.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight.Command;

namespace XuatThuy.ViewModel
{
    public class PhieuChatLuongVM : ViewModelBase
    {
        public ObservableCollection<PhieuChatLuong> PhieuChatLuongs { get; set; }
        public RelayCommand<PhieuChatLuong> CmdEditPhieuChatLuong { get; set; }
        public RelayCommand<PhieuChatLuong> CmdDeletePhieuChatLuong { get; set; }
        public RelayCommand CmdAddPhieuChatLuong { get; set; }
        XuatHangDataContext db = new XuatHangDataContext();

        string _tenMoi;
        public string TenMoi
        {
            get { return _tenMoi; }
            set
            {
                _tenMoi = value;
                RaisePropertyChanged("TenMoi");
            }
        }

        DateTime _ngayCapMoi;
        public DateTime NgayCapMoi
        {
            get { return _ngayCapMoi; }
            set
            {
                _ngayCapMoi = value;
                RaisePropertyChanged("NgayCapMoi");
            }
        }

        PhieuChatLuong _selectedPhieuChatLuong;
        public PhieuChatLuong SelectedPhieuChatLuong
        {
            get { return _selectedPhieuChatLuong; }
            set
            {
                _selectedPhieuChatLuong = value;
                RaisePropertyChanged("SelectedPhieuChatLuong");
            }
        }


        void EditPhieuChatLuong(PhieuChatLuong PhieuChatLuong)
        {
            int ret = db.p_PhieuChatLuong_Sua(null, _selectedPhieuChatLuong.ID, _selectedPhieuChatLuong.Ten, _selectedPhieuChatLuong.NgayCap);
        }

        void DeletePhieuChatLuong(PhieuChatLuong PhieuChatLuong)
        {
            int ret = db.p_PhieuChatLuong_Xoa(null, _selectedPhieuChatLuong.ID);

            if (ret == 0)
            {
                PhieuChatLuongs.Remove(PhieuChatLuongs.Where(x => x.ID == _selectedPhieuChatLuong.ID).FirstOrDefault());
            }
        }

        void AddPhieuChatLuong()
        {
            long? PhieuChatLuongID = 0;
            int ret = db.p_PhieuChatLuong_Them(null, ref PhieuChatLuongID, TenMoi, NgayCapMoi);

            if (ret == 0)
            {
                PhieuChatLuongs.Insert(0, new PhieuChatLuong((long)PhieuChatLuongID, TenMoi, NgayCapMoi, true));
                TenMoi = string.Empty;
            }
        }

        //public Action<PhieuChatLuong> EditPhieuChatLuong = new Action<PhieuChatLuong>(x => x.Ten = "aaaa");


        public PhieuChatLuongVM()
        {
            //RelayCommand 
            NgayCapMoi = DateTime.Now;

            PhieuChatLuongs = new ObservableCollection<PhieuChatLuong>();

            CmdEditPhieuChatLuong = new RelayCommand<PhieuChatLuong>(EditPhieuChatLuong);
            CmdDeletePhieuChatLuong = new RelayCommand<PhieuChatLuong>(DeletePhieuChatLuong);
            CmdAddPhieuChatLuong = new RelayCommand(AddPhieuChatLuong);

            var pcls = db.p_PhieuChatLuong_Lay(null).ToList();

            foreach (var pcl in pcls)
            {
                PhieuChatLuongs.Add(new PhieuChatLuong(pcl.ID, pcl.Ten, pcl.NgayCap, pcl.TrangThai == 10));
            }
        }
    }
}
