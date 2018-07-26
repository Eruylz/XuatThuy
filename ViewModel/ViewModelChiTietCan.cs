using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuatThuy.Model;
using GalaSoft.MvvmLight;

namespace XuatThuy.ViewModel
{
    class ViewModelChiTietCan : ViewModelBase
    {
        public ObservableCollection<ChiTietCan> ChiTietCans { get; set; }

        public ViewModelChiTietCan(long PhieuCanChiTietID)
        {
            XuatHangDataContext db = new XuatHangDataContext();
            ChiTietCans = new ObservableCollection<ChiTietCan>();

            var chiTietCans = db.p_PhieuCan_ChiTiet_Can_Lay(null, PhieuCanChiTietID).OrderBy(x => x.Ket);

            foreach (var ctc in chiTietCans)
            {
                ChiTietCans.Add(new ChiTietCan(ctc.ID, ctc.PhieuCanChiTietID, ctc.STT, ctc.Ket, ctc.TLCan1, ctc.TLCan2, ctc.TLHang, ctc.ThoiDiemCan1, ctc.ThoiDiemCan2, ctc.TrangThai));
            }
        }
    }
}
