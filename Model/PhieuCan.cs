using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.Model
{
    public class PhieuCan : ViewModelBase
    {
        long _ID;
        long? _Order_ID;
        string _deliveryCode;
        string _BienSo1;
        string _BienSo2;
        string _ChuPhuongTien;
        string _HinhThucCan;
        string _OFFLINE_FLAG;
        double? _TLXuat;
        double? _TLThucXuat;
        double? _TLDangKy;
        byte? _LoaiGiaoDich;
        byte? _SoChi;
        byte? _TrangThai;
        long? _SoPhieuCu;
        DateTime? _NgayDongBo;
        byte? _TrangThaiDongBo;
        long? _soLoID;
        long? _phieuChatLuongID;
        bool _isSelected;

        public long ID
        {
            get => _ID;
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }

        public long? Order_ID { get => _Order_ID; set => _Order_ID = value; }
        public string DeliveryCode { get => _deliveryCode; set => _deliveryCode = value; }
        public string BienSo1 { get => _BienSo1; set => _BienSo1 = value; }
        public string BienSo2 { get => _BienSo2; set => _BienSo2 = value; }
        public string ChuPhuongTien { get => _ChuPhuongTien; set => _ChuPhuongTien = value; }
        public string HinhThucCan { get => _HinhThucCan; set => _HinhThucCan = value; }
        public string OFFLINE_FLAG { get => _OFFLINE_FLAG; set => _OFFLINE_FLAG = value; }
        public double? TLXuat { get => _TLXuat; set => _TLXuat = value; }
        public double? TLThucXuat
        {
            get => _TLThucXuat;
            set
            {
                _TLThucXuat = value;
                RaisePropertyChanged("TLThucXuat");
            }
        }
        public double? TLDangKy { get => _TLDangKy; set => _TLDangKy = value; }
        public byte? LoaiGiaoDich { get => _LoaiGiaoDich; set => _LoaiGiaoDich = value; }
        public byte? SoChi
        {
            get => _SoChi;
            set
            {
                _SoChi = value;
                RaisePropertyChanged("SoChi");
            }
        }

        public byte? TrangThai { get => _TrangThai; set => _TrangThai = value; }
        public long? SoPhieuCu { get => _SoPhieuCu; set => _SoPhieuCu = value; }
        public DateTime? NgayDongBo { get => _NgayDongBo; set => _NgayDongBo = value; }
        public byte? TrangThaiDongBo { get => _TrangThaiDongBo; set => _TrangThaiDongBo = value; }
        public long? SoLoID { get => _soLoID; set => _soLoID = value; }
        public long? PhieuChatLuongID { get => _phieuChatLuongID; set => _phieuChatLuongID = value; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public PhieuCan(long iD, long? order_ID, string bienSo1, string bienSo2, string chuPhuongTien, string hinhThucCan, string oFFLINE_FLAG, double? tLXuat, double? tLThucXuat, double? tLDangKy, byte? loaiGiaoDich, byte? soChi, byte? trangThai, long? soPhieuCu, DateTime? ngayDongBo, byte? trangThaiDongBo, long? soLoID, long? phieuChatLuongID, string deliveryCode)
        {
            ID = iD;
            Order_ID = order_ID;
            BienSo1 = bienSo1;
            BienSo2 = bienSo2;
            ChuPhuongTien = chuPhuongTien;
            HinhThucCan = hinhThucCan;
            OFFLINE_FLAG = oFFLINE_FLAG;
            TLXuat = tLXuat;
            TLThucXuat = tLThucXuat;
            TLDangKy = tLDangKy;
            LoaiGiaoDich = loaiGiaoDich;
            SoChi = soChi;
            TrangThai = trangThai;
            SoPhieuCu = soPhieuCu;
            NgayDongBo = ngayDongBo;
            TrangThaiDongBo = trangThaiDongBo;
            SoLoID = soLoID;
            PhieuChatLuongID = phieuChatLuongID;
            DeliveryCode = deliveryCode;
        }
        //public PhieuCan()
        //{
        //    //
        //}

        //public PhieuCan(long iD, long? order_ID, string bienSo1, string bienSo2, string chuPhuongTien, string hinhThucCan, string oFFLINE_FLAG, double? tLXuat, double? tLDangKy, byte? loaiGiaoDich, byte? soChi, byte? trangThai, long? soPhieuCu, DateTime? ngayDongBo, byte? trangThaiDongBo)
        //{
        //    ID = iD;
        //    Order_ID = order_ID;
        //    BienSo1 = bienSo1;
        //    BienSo2 = bienSo2;
        //    ChuPhuongTien = chuPhuongTien;
        //    HinhThucCan = hinhThucCan;
        //    OFFLINE_FLAG = oFFLINE_FLAG;
        //    TLXuat = tLXuat;
        //    TLDangKy = tLDangKy;
        //    LoaiGiaoDich = loaiGiaoDich;
        //    SoChi = soChi;
        //    TrangThai = trangThai;
        //    SoPhieuCu = soPhieuCu;
        //    NgayDongBo = ngayDongBo;
        //    TrangThaiDongBo = trangThaiDongBo;
        //}


        public PhieuCan(Guid? SessionID, long PhieuCanID)
        {
            XuatHangDataContext db = new XuatHangDataContext();

            var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();

            if (pc != null)
            {
                ID = pc.ID;
                Order_ID = pc.Order_ID;
                BienSo1 = pc.BienSo1;
                BienSo2 = pc.BienSo2;
                ChuPhuongTien = pc.ChuPhuongTien;
                HinhThucCan = pc.HinhThucCan;
                OFFLINE_FLAG = pc.OFFLINE_FLAG;
                TLXuat = pc.TLXuat;
                TLDangKy = pc.TLDangKy;
                LoaiGiaoDich = pc.LoaiGiaoDich;
                SoChi = pc.SoChi;
                TrangThai = pc.TrangThai;
                SoPhieuCu = pc.SoPhieuCu;
                NgayDongBo = pc.NgayDongBo;
                TrangThaiDongBo = pc.TrangThaiDongBo;
                SoLoID = pc.SoLoID;
                PhieuChatLuongID = pc.PhieuChatLuongID;
            }
        }

    }
}
