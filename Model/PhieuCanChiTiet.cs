using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XuatThuy.Model
{
    public class PhieuCanChiTiet : INotifyPropertyChanged
    {
        long _ID;
        long _PhieuCanID;
        int? _MangXuatID;
        string _MangXuat;
        int? _NVXuatHangID;
        string _NVXuatHang;
        int? _NVBaoVeID;
        string _NVBaoVe;
        byte? _Ca;
        byte? _TrangThai;

        public long ID { get => _ID; set => _ID = value; }
        public long PhieuCanID { get => _PhieuCanID; set => _PhieuCanID = value; }
        public int? MangXuatID { get => _MangXuatID; set => _MangXuatID = value; }
        public int? NVXuatHangID { get => _NVXuatHangID; set => _NVXuatHangID = value; }
        public int? NVBaoVeID { get => _NVBaoVeID; set => _NVBaoVeID = value; }
        public byte? Ca { get => _Ca; set => _Ca = value; }
        public byte? TrangThai { get => _TrangThai; set => _TrangThai = value; }
        public string NVXuatHang { get => _NVXuatHang; set => _NVXuatHang = value; }
        public string NVBaoVe { get => _NVBaoVe; set => _NVBaoVe = value; }
        public string MangXuat { get => _MangXuat; set => _MangXuat = value; }
        public double? SoTongPLC_TruocCan { get; set; }
        public double? SoTongKetThuc { get; set; }
        public double? KLHang { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public DateTime? NgayThongKe { get; set; }

        public PhieuCanChiTiet(long iD, long phieuCanID, int? mangXuatID, int? nVXuatHangID, int? nVBaoVeID,
                            byte? ca, DateTime? ngayTao, DateTime? ngaySua, DateTime? ngayThongKe, byte? trangThai, string nVXuatHang, string nVBaoVe, 
                            string mangXuat, double? soTongPLC_TruocCan, double? soTongKetThuc)
        {
            ID = iD;
            PhieuCanID = phieuCanID;
            MangXuatID = mangXuatID;
            NVXuatHangID = nVXuatHangID;
            NVBaoVeID = nVBaoVeID;
            Ca = ca;
            NgayThongKe = ngayThongKe;
            NgayTao = ngayTao;
            NgaySua = ngaySua;
            TrangThai = trangThai;
            NVXuatHang = nVXuatHang;
            NVBaoVe = nVBaoVe;
            MangXuat = mangXuat;
            SoTongPLC_TruocCan = soTongPLC_TruocCan;
            SoTongKetThuc = soTongKetThuc;
            KLHang = soTongKetThuc == null ? null : soTongKetThuc - soTongPLC_TruocCan;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
}
