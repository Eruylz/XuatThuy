using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuatThuy.Model
{
    public class ChiTietCan
    {
        long _ID;
        
        double? _TLCan1;
        double? _TLCan2;
        double? _TLHang;
        DateTime? _ThoiDiemCan1;
        DateTime? _ThoiDiemCan2;
        byte? _TrangThai;

        public long ID { get => _ID; set => _ID = value; }
        long? PhieuCanChiTietID { get; set; }
        public double? TLCan1 { get => _TLCan1; set => _TLCan1 = value; }
        public double? TLCan2 { get => _TLCan2; set => _TLCan2 = value; }
        public double? TLHang { get => _TLHang; set => _TLHang = value; }
        public DateTime? ThoiDiemCan1 { get => _ThoiDiemCan1; set => _ThoiDiemCan1 = value; }
        public DateTime? ThoiDiemCan2 { get => _ThoiDiemCan2; set => _ThoiDiemCan2 = value; }
        public byte? TrangThai { get => _TrangThai; set => _TrangThai = value; }
        public byte? Ket { get; set ; }
        public int? Stt { get; set; }

        public ChiTietCan(long iD, long? phieuCanChiTietID, int? stt, byte? ket, double? tLCan1, double? tLCan2, double? tLHang,
            DateTime? thoiDiemCan1, DateTime? thoiDiemCan2, byte? trangThai)
        {
            ID = iD;
            PhieuCanChiTietID = phieuCanChiTietID;
            Stt = stt;
            Ket = ket;
            TLCan1 = tLCan1;
            TLCan2 = tLCan2;
            TLHang = tLHang;
            ThoiDiemCan1 = thoiDiemCan1;
            ThoiDiemCan2 = thoiDiemCan2;
            TrangThai = trangThai;
        }

        public ChiTietCan(int? stt, byte? ket, double? tLCan1, double? tLCan2, double? tLHang)
        {
            Stt = stt;
            Ket = ket;
            TLCan1 = tLCan1;
            TLCan2 = tLCan2;
            TLHang = tLHang;
        }
    }

    public enum KetCan
    {
        KET_1 = 1,
        KET_2 = 2
    }
}
