using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using XuatThuy.ViewModel;
using XuatThuy.PLCs;
using GalaSoft.MvvmLight;
using XuatThuy.Model;
using CommonServiceLocator;
using System.Data;
using XuatThuy.Utils;
using XuatThuy.Controls;
using MaterialDesignThemes.Wpf;

namespace XuatThuy.ViewModel
{
    public class PlcViewModel : ViewModelBase
    {
        XuatHangDataContext db = new XuatHangDataContext();

        #region property
        int _SoPhieu;
        public int Sophieu
        {
            get { return _SoPhieu; }
            set
            {
                if (_SoPhieu != value)
                {
                    _SoPhieu = value;
                    RaisePropertyChanged("Sophieu");

                    ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                    if (!Common.READ_ONLY)
                    {
                        WriteLine1Bang2((long?)_SoPhieu);
                    }
                }
            }
        }

        int _Setpoint;
        public int Setpoint
        {
            get { return _Setpoint; }
            set
            {
                if (_Setpoint != value)
                {
                    _Setpoint = value;
                    RaisePropertyChanged("Setpoint");
                }
            }
        }

        int _KLTongPhieu;
        public int KLTongPhieu
        {
            get { return _KLTongPhieu; }
            set
            {
                if (_KLTongPhieu != value)
                {
                    _KLTongPhieu = value;
                    RaisePropertyChanged("KLTongPhieu");

                    if (!Common.READ_ONLY)
                    {
                        bangDT2.Line2_Text = KLTongPhieu.ToString();
                        bangDT2.SetTextLine(2); 
                    }
                }
            }
        }

        int _tong_can;
        public int tong_can
        {
            get { return _tong_can; }
            set
            {
                if (_tong_can != value)
                {
                    _tong_can = value;
                    RaisePropertyChanged("tong_can");
                }
            }
        }

        int _KLCan1;
        public int KLCan1
        {
            get { return _KLCan1; }
            set
            {
                if (_KLCan1 != value)
                {
                    _KLCan1 = value;
                    RaisePropertyChanged("KLCan1");
                    //Debug.WriteLine("RaisePropertyChanged. KLCan1.ToString ===== " + KLCan1.ToString());
                    //Đặt giá trị dòng 1(nếu cần cập nhật)

                    if (WriteBangDT1)
                    {
                        bangDT1.Line1_Text = KLCan1.ToString() + "/" + KLTong1.ToString();
                        bangDT1.SetTextLine(1);
                    }
                }
            }
        }

        int _KLTong1;
        public int KLTong1
        {
            get { return _KLTong1; }
            set
            {
                if (_KLTong1 != value)
                {
                    _KLTong1 = value;
                    RaisePropertyChanged("KLTong1");

                    if (WriteBangDT1)
                    {
                        bangDT1.Line1_Text = KLCan1.ToString() + "/" + KLTong1.ToString();
                        bangDT1.SetTextLine(1); 
                    }
                }
            }
        }

        int _Min_ket1;
        public int Min_ket1
        {
            get { return _Min_ket1; }
            set
            {
                if (_Min_ket1 != value)
                {
                    _Min_ket1 = value;
                    RaisePropertyChanged("Min_ket1");
                }
            }
        }

        int _Max_ket1;
        public int Max_ket1
        {
            get { return _Max_ket1; }
            set
            {
                if (_Max_ket1 != value)
                {
                    _Max_ket1 = value;
                    RaisePropertyChanged("Max_ket1");
                }
            }
        }

        int _Ncham_ket1;
        public int Ncham_ket1
        {
            get { return _Ncham_ket1; }
            set
            {
                if (_Ncham_ket1 != value)
                {
                    _Ncham_ket1 = value;
                    RaisePropertyChanged("Ncham_ket1");
                }
            }
        }

        short _Buoc_chay_1;
        public short Buoc_chay_1
        {
            get { return _Buoc_chay_1; }
            set
            {
                if (_Buoc_chay_1 != value)
                {
                    _Buoc_chay_1 = value;
                    RaisePropertyChanged("Buoc_chay_1");
                }
            }
        }

        byte _Trangthai_1;
        public byte Trangthai_1
        {
            get { return _Trangthai_1; }
            set
            {
                if (_Trangthai_1 != value)
                {
                    _Trangthai_1 = value;
                    RaisePropertyChanged("Trangthai_1");
                }
            }
        }

        int _KLCan2;
        public int KLCan2
        {
            get { return _KLCan2; }
            set
            {
                if (_KLCan2 != value)
                {
                    _KLCan2 = value;
                    RaisePropertyChanged("KLCan2");

                    if (WriteBangDT1)
                    {
                        bangDT1.Line2_Text = KLCan2.ToString() + "/" + KLTong2.ToString();
                        bangDT1.SetTextLine(2); 
                    }
                }
            }
        }

        int _KLTong2;
        public int KLTong2
        {
            get { return _KLTong2; }
            set
            {
                if (_KLTong2 != value)
                {
                    _KLTong2 = value;
                    RaisePropertyChanged("KLTong2");

                    if (WriteBangDT1)
                    {
                        bangDT1.Line2_Text = KLCan2.ToString() + "/" + KLTong2.ToString();
                        bangDT1.SetTextLine(2); 
                    }
                }
            }
        }

        int _Min_ket2;
        public int Min_ket2
        {
            get { return _Min_ket2; }
            set
            {
                if (_Min_ket2 != value)
                {
                    _Min_ket2 = value;
                    RaisePropertyChanged("Min_ket2");
                }
            }
        }

        int _Max_ket2;
        public int Max_ket2
        {
            get { return _Max_ket2; }
            set
            {
                if (_Max_ket2 != value)
                {
                    _Max_ket2 = value;
                    RaisePropertyChanged("Max_ket2");
                }
            }
        }

        int _Ncham_ket2;
        public int Ncham_ket2
        {
            get { return _Ncham_ket2; }
            set
            {
                if (_Ncham_ket2 != value)
                {
                    _Ncham_ket2 = value;
                    RaisePropertyChanged("Ncham_ket2");
                }
            }
        }

        short _Buoc_chay_2;
        public short Buoc_chay_2
        {
            get { return _Buoc_chay_2; }
            set
            {
                if (_Buoc_chay_2 != value)
                {
                    _Buoc_chay_2 = value;
                    RaisePropertyChanged("Buoc_chay_2");
                }
            }
        }

        byte _Trangthai_2;
        public byte Trangthai_2
        {
            get { return _Trangthai_2; }
            set
            {
                if (_Trangthai_2 != value)
                {
                    _Trangthai_2 = value;
                    RaisePropertyChanged("Trangthai_2");
                }
            }
        }

        bool _HMI_Nhapphieu;
        public bool HMI_Nhapphieu
        {
            get { return _HMI_Nhapphieu; }
            set
            {
                if (_HMI_Nhapphieu != value)
                {
                    _HMI_Nhapphieu = value;
                    RaisePropertyChanged("HMI_Nhapphieu");
                }
            }
        }

        bool _HMI_Modechay;
        public bool HMI_Modechay
        {
            get { return _HMI_Modechay; }
            set
            {
                if (_HMI_Modechay != value)
                {
                    _HMI_Modechay = value;
                    RaisePropertyChanged("HMI_Modechay");
                }
            }
        }

        bool _HMI_Tamdung;
        public bool HMI_Tamdung
        {
            get { return _HMI_Tamdung; }
            set
            {
                if (_HMI_Tamdung != value)
                {
                    _HMI_Tamdung = value;
                    RaisePropertyChanged("HMI_Tamdung");
                }
            }
        }

        bool _HMI_chay_rut_lieu;
        public bool HMI_chay_rut_lieu
        {
            get { return _HMI_chay_rut_lieu; }
            set
            {
                if (_HMI_chay_rut_lieu != value)
                {
                    _HMI_chay_rut_lieu = value;
                    RaisePropertyChanged("HMI_chay_rut_lieu");
                }
            }
        }

        bool _Caplieuchay;
        public bool Caplieuchay
        {
            get { return _Caplieuchay; }
            set
            {
                if (_Caplieuchay != value)
                {
                    _Caplieuchay = value;
                    RaisePropertyChanged("Caplieuchay");
                }
            }
        }

        bool _Thaolieuchay;
        public bool Thaolieuchay
        {
            get { return _Thaolieuchay; }
            set
            {
                if (_Thaolieuchay != value)
                {
                    _Thaolieuchay = value;
                    RaisePropertyChanged("Thaolieuchay");
                }
            }
        }

        bool _Baodongchung;
        public bool Baodongchung
        {
            get { return _Baodongchung; }
            set
            {
                if (_Baodongchung != value)
                {
                    _Baodongchung = value;
                    RaisePropertyChanged("Baodongchung");
                }
            }
        }

        bool _Yeu_cau_luu1;
        public bool Yeu_cau_luu1
        {
            get { return _Yeu_cau_luu1; }
            set
            {
                if (_Yeu_cau_luu1 != value)
                {
                    //  yêu cầu lưu 1 lật trạng thái false => true : chốt được hàng két 1
                    // Đồng thời không phải là khởi tạo chương trình

                    if (!_Yeu_cau_luu1 && value)
                    {
                        if (Startuped && !Common.READ_ONLY) // không phải khởi tạo ctrinh
                        {
                            Debug.WriteLine("Yêu cầu lưu 1 lật từ " + _Yeu_cau_luu1.ToString() + " đến " + value.ToString());
                            Debug.WriteLine("Bước chạy két 1 ==== " + Buoc_chay_1.ToString());

                            var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, (long?)Sophieu).Where(x => x.TrangThai == 10).FirstOrDefault();

                            if (chitiet != null)
                            {
                                long? LanCanID = null;

                                ChiTietCan ctc = ChiTietCan_Lay(KetCan.KET_1, Buoc_chay_1);

                                Debug.WriteLine("Thêm chi tiết cân cho bước " + Buoc_chay_1.ToString() + ". Khoi luong hang: " + ctc.TLCan1.ToString());

                                int ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, Buoc_chay_1, 1,
                                                                    chitiet.ID, DateTime.Now, ctc.TLCan1, null, null, 10);

                                if (ret == 0)
                                {
                                    ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
                                    ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                                }
                            }
                        }

                        if (!Common.READ_ONLY)
                        {

                            PLC.Write_DB300_Yeu_cau_luuhang1(false);
                        }
                    }

                    _Yeu_cau_luu1 = value;
                    RaisePropertyChanged("Yeu_cau_luu1");
                }
            }
        }

        bool _Yeu_cau_luu2;
        public bool Yeu_cau_luu2
        {
            get { return _Yeu_cau_luu2; }
            set
            {
                if (_Yeu_cau_luu2 != value)
                {
                    //  yêu cầu lưu 2 lật trạng thái false => true : chốt được hàng két 2
                    // Đồng thời không phải là khởi tạo chương trình
                    if (!_Yeu_cau_luu2 && value)
                    {
                        if (Startuped && !Common.READ_ONLY)
                        {
                            var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, (long?)Sophieu).Where(x => x.TrangThai == 10).FirstOrDefault();

                            if (chitiet != null)
                            {
                                long? LanCanID = null;
                                ChiTietCan ctc = ChiTietCan_Lay(KetCan.KET_2, Buoc_chay_2);

                                Debug.WriteLine("Thêm chi tiết cân cho bước " + Buoc_chay_2.ToString() + ". Khoi luong hang: " + ctc.TLCan1.ToString());
                                int ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, Buoc_chay_2, 2,
                                                                    chitiet.ID, DateTime.Now, ctc.TLCan1, null, null, 10);

                                if (ret == 0)
                                {
                                    ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
                                    ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                                }
                            }
                        }

                        if (!Common.READ_ONLY)
                        {
                            PLC.Write_DB300_Yeu_cau_luuhang2(false);
                        }
                    }

                    _Yeu_cau_luu2 = value;
                    RaisePropertyChanged("Yeu_cau_luu2");
                }
            }
        }

        bool _Yeu_cau_luubi1;
        public bool Yeu_cau_luubi1
        {
            get { return _Yeu_cau_luubi1; }
            set
            {
                if (_Yeu_cau_luubi1 != value)
                {
                    //  yêu cầu lưu 1 lật trạng thái false => true : chốt được hàng két 1
                    // Đồng thời không phải là khởi tạo chương trình
                    if (!_Yeu_cau_luubi1 && value)
                    {
                        if (Startuped) // không phải khởi tạo ctrinh
                        {
                            var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, (long?)Sophieu).Where(x => x.TrangThai == 10).FirstOrDefault();

                            if (chitiet != null)
                            {
                                var ctcans = db.p_PhieuCan_ChiTiet_Can_Lay(null, chitiet.ID);

                                var ctcan_cuoi = ctcans.Where(x => x.Ket == 1 && x.STT == Buoc_chay_1).FirstOrDefault();

                                // da co chi tiet can cuoi : ket thuc chi tiet can
                                if (ctcan_cuoi != null)
                                {
                                    ChiTietCan ctc = ChiTietCan_Lay(KetCan.KET_1, Buoc_chay_1);

                                    int ret = db.p_PhieuCan_ChiTiet_Can_KetThuc(null, ctcan_cuoi.ID,
                                                                            ctc.TLCan2, 
                                                                            ctcan_cuoi.TLCan1 - ctc.TLCan2, // tl hang
                                                                            DateTime.Now);
                                    if (ret == 0)
                                    {
                                        ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
                                        ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                                    }
                                }

                            }
                        }
                        if (!Common.READ_ONLY)
                        {
                            PLC.Write_DB300_Yeu_cau_luubi1(false);
                        }
                    }

                    _Yeu_cau_luubi1 = value;
                    RaisePropertyChanged("Yeu_cau_luubi1");
                }
            }
        }

        bool _Yeu_cau_luubi2;
        public bool Yeu_cau_luubi2
        {
            get { return _Yeu_cau_luubi2; }
            set
            {
                if (_Yeu_cau_luubi2 != value)
                {
                    //  yêu cầu lưu 1 lật trạng thái false => true : chốt được hàng két 1
                    // Đồng thời không phải là khởi tạo chương trình
                    if (!_Yeu_cau_luubi2 && value)
                    {
                        if (Startuped) // không phải khởi tạo ctrinh
                        {
                            var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, (long?)Sophieu).Where(x => x.TrangThai == 10).FirstOrDefault();

                            if (chitiet != null)
                            {
                                var ctcans = db.p_PhieuCan_ChiTiet_Can_Lay(null, chitiet.ID);

                                var ctcan_cuoi = ctcans.Where(x => x.Ket == 2 && x.STT == Buoc_chay_2).FirstOrDefault();

                                // da co chi tiet can cuoi : ket thuc chi tiet can
                                if (ctcan_cuoi != null)
                                {
                                    ChiTietCan ctc = ChiTietCan_Lay(KetCan.KET_2, Buoc_chay_2);

                                    int ret = db.p_PhieuCan_ChiTiet_Can_KetThuc(null, ctcan_cuoi.ID,
                                                                            ctc.TLCan2,
                                                                            ctcan_cuoi.TLCan1 - ctc.TLCan2,
                                                                            DateTime.Now);

                                    if (ret == 0)
                                    {
                                        ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
                                        ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                                    }
                                }

                            }
                        }

                        if (!Common.READ_ONLY)
                        {
                            PLC.Write_DB300_Yeu_cau_luubi2(false);
                        }
                    }

                    _Yeu_cau_luubi2 = value;
                    RaisePropertyChanged("Yeu_cau_luubi2");

                }
            }
        }

        // điều kiện kết thúc
        bool _DieuKienKetThuc;
        public bool DieuKienKetThuc
        {
            get { return _DieuKienKetThuc; }
            set
            {
                if (_DieuKienKetThuc != value)
                {
                    // sườn lên điều kiện kết thúc
                    if (!_DieuKienKetThuc && value)
                    {
                        //long PhieuCanID = (long)Sophieu;
                        //// ket thuc trong db
                        //int ret = db.p_PhieuCan_KetThuc(null, PhieuCanID, KLTongPhieu);

                        //if (ret == 0)
                        //{
                        //    var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, PhieuCanID).Where(x => x.TrangThai == 10).FirstOrDefault();

                        //    if (chitiet != null)
                        //    {
                        //        ret = db.p_PhieuCan_ChiTiet_KetThuc(null, chitiet.ID, tong_can);

                        //        if (ret == 0)
                        //        {
                        //            ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                        //            ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
                        //        }
                        //    }
                        //}
                    }

                    _DieuKienKetThuc = value;
                    RaisePropertyChanged("DieuKienKetThuc");
                }
            }
        }

        int _Tongbd;
        public int Tongbd
        {
            get { return _Tongbd; }
            set
            {
                if (_Tongbd != value)
                {
                    _Tongbd = value;
                    RaisePropertyChanged("Tongbd");
                }
            }
        }

        int _Tongkt;
        public int Tongkt
        {
            get { return _Tongkt; }
            set
            {
                if (_Tongkt != value)
                {
                    _Tongkt = value;
                    RaisePropertyChanged("Tongkt");
                }
            }
        }

        bool _PlcIsConnected;
        public bool PlcIsConnected
        {
            get { return _PlcIsConnected; }
            set
            {
                if (_PlcIsConnected != value)
                {
                    // kết nối lại PLC
                    if (Startuped && !_PlcIsConnected && value)
                    {
                        // kết nối lại PLC => coi như chạy lại
                        //index = 0;
                        Startuped = false;
                    }

                    _PlcIsConnected = value;
                    RaisePropertyChanged("PlcIsConnected");
                }
            }
        }
        #endregion

        bool _CapLieu1;
        public bool CapLieu1
        {
            get { return _CapLieu1; }
            set
            {
                if (_CapLieu1 != value)
                {
                    _CapLieu1 = value;
                    RaisePropertyChanged("CapLieu1");
                }
            }
        }

        bool _CapLieu2;
        public bool CapLieu2
        {
            get { return _CapLieu2; }
            set
            {
                if (_CapLieu2 != value)
                {
                    _CapLieu2 = value;
                    RaisePropertyChanged("CapLieu2");
                }
            }
        }

        bool _XaLieu1;
        public bool XaLieu1
        {
            get { return _XaLieu1; }
            set
            {
                if (_XaLieu1 != value)
                {
                    _XaLieu1 = value;
                    RaisePropertyChanged("XaLieu1");
                }
            }
        }

        bool _XaLieu2;
        public bool XaLieu2
        {
            get { return _XaLieu2; }
            set
            {
                if (_XaLieu2 != value)
                {
                    _XaLieu2 = value;
                    RaisePropertyChanged("XaLieu2");
                }
            }
        }

        bool _LocbuiP40_Run;
        public bool LocbuiP40_Run
        {
            get { return _LocbuiP40_Run; }
            set
            {
                if (_LocbuiP40_Run != value)
                {
                    _LocbuiP40_Run = value;
                    RaisePropertyChanged("LocbuiP40_Run");
                }
            }
        }

        bool _LocbuiP41_Run;
        public bool LocbuiP41_Run
        {
            get { return _LocbuiP41_Run; }
            set
            {
                if (_LocbuiP41_Run != value)
                {
                    _LocbuiP41_Run = value;
                    RaisePropertyChanged("LocbuiP41_Run");
                }
            }
        }

        bool _Yc_caplieuK1;
        public bool Yc_caplieuK1
        {
            get { return _Yc_caplieuK1; }
            set
            {
                if (_Yc_caplieuK1 != value)
                {
                    _Yc_caplieuK1 = value;
                    RaisePropertyChanged("Yc_caplieuK1");
                }
            }
        }

        bool _Yc_caplieuK2;
        public bool Yc_caplieuK2
        {
            get { return _Yc_caplieuK2; }
            set
            {
                if (_Yc_caplieuK2 != value)
                {
                    _Yc_caplieuK2 = value;
                    RaisePropertyChanged("Yc_caplieuK2");
                }
            }
        }

        bool _Yc_xalieuK1;
        public bool Yc_xalieuK1
        {
            get { return _Yc_xalieuK1; }
            set
            {
                if (_Yc_xalieuK1 != value)
                {
                    _Yc_xalieuK1 = value;
                    RaisePropertyChanged("Yc_xalieuK1");
                }
            }
        }

        bool _Yc_xalieuK2;
        public bool Yc_xalieuK2
        {
            get { return _Yc_xalieuK2; }
            set
            {
                if (_Yc_xalieuK2 != value)
                {
                    _Yc_xalieuK2 = value;
                    RaisePropertyChanged("Yc_xalieuK2");
                }
            }
        }

        void WriteLine1Bang2(long? PhieuCanID)
        {
            var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();

            if (pc != null)
            {
                var so = db.p_SALES_ORDERS_LayTheoID(null, pc.Order_ID).FirstOrDefault();

                if (so != null)
                {

                    string unicode_str = so.DELIVERY_CODE + "  " + so.CUSTOMER_NAME + "  " + pc.BienSo1 + "  " +
                                        so.TenSanPham + "  " + pc.TLXuat.ToString() + "Kg";
                    //bangDT2.Line1_Text = so.DELIVERY_CODE + "  " + so.CUSTOMER_NAME + "  " + pc.BienSo1 + "  " +
                    //                    so.TenSanPham + "  " + pc.TLXuat.ToString() + "Kg";

                    if (!Common.READ_ONLY)
                    {
                        bangDT2.Line1_Text = Unicode2TCVN3.Convert(unicode_str);
                        bangDT2.Line2_Text = KLTongPhieu.ToString();
                        bangDT2.SetTextLine(4);

                    }
                    //bangDT2.SetTextLine(2);
                }
            }
        }


        //int index;
        bool Startuped;
        BangDTu bangDT1;
        BangDTu bangDT2;
        bool WriteBangDT1 = false;

        public PlcViewModel()
        {
            //index = 0;
            Buoc_chay_1 = 0;
            Buoc_chay_2 = 0;
            Startuped = false;

            PLC pLC = PLC.GetInstance(db);

            DispatcherTimer timer;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            DispatcherTimer timer_chot;
            timer_chot = new DispatcherTimer();
            timer_chot.Interval = TimeSpan.FromMinutes(2);
            timer_chot.Tick += new EventHandler(timer_chot_Tick);
            timer_chot.Start();

            if (WriteBangDT1)
            {
                bangDT1 = new BangDTu("10.68.51.250", 1024);
            }

            bangDT2 = new BangDTu("10.68.51.6", 1024);
        }

        public byte? CurrentCa { get; set; }

        public void LayCa()
        {
            TimeSpan GiaoCa1 = new TimeSpan(13, 30, 0);
            TimeSpan GiaoCa2 = new TimeSpan(21, 30, 0);
            TimeSpan GiaoCa3 = new TimeSpan(5, 30, 0);

            TimeSpan currentTimeSpan = DateTime.Now.TimeOfDay;

            //Debug.WriteLine("currentTimeSpan === " + currentTimeSpan.)

            if (currentTimeSpan >= GiaoCa3 && currentTimeSpan < GiaoCa1)
            {
                CurrentCa = 1;
            }
            else if (currentTimeSpan >= GiaoCa1 && currentTimeSpan < GiaoCa2)
            {
                CurrentCa = 2;
            }
            else
            {
                CurrentCa = 3;
            }
            //Debug.WriteLine("CurrentCa === " + CurrentCa.ToString());
        }

        private void timer_chot_Tick(object sender, EventArgs e)
        {
            if (Startuped && HMI_Nhapphieu && !DieuKienKetThuc)
            {
                ChotLuong(CurrentCa);
            }
            
        }

        void LoadParameterFromPLC()
        {
            Buoc_chay_1 = PLC.DS_Tag_DB300.Buoc_chay_1;
            Buoc_chay_2 = PLC.DS_Tag_DB300.Buoc_chay_2;

            Sophieu = PLC.DS_Tag_DB300.Sophieu;
            KLTongPhieu = PLC.DS_Tag_DB300.KLTongPhieu;
            tong_can = PLC.DS_Tag_DB300.tong_can;
            PlcIsConnected = PLC.PLCConnected;
            HMI_Nhapphieu = PLC.DS_Tag_DB300.HMI_Nhapphieu;
            HMI_Tamdung = PLC.DS_Tag_DB300.HMI_Tamdung;
            HMI_chay_rut_lieu = PLC.DS_Tag_DB300.HMI_chay_rut_lieu;

            CapLieu1 = PLC.DS_Tag_DB300.Ket1_Caplieu;
            XaLieu1 = PLC.DS_Tag_DB300.Ket1_thaolieu;
            CapLieu2 = PLC.DS_Tag_DB300.Ket2_Caplieu;
            XaLieu2 = PLC.DS_Tag_DB300.Ket2_thaolieu;

            KLCan1 = PLC.DS_Tag_DB300.KLCan1;
            KLCan2 = PLC.DS_Tag_DB300.KLCan2;
            KLTong1 = PLC.DS_Tag_DB300.KLTong1;
            KLTong2 = PLC.DS_Tag_DB300.KLTong2;
            Trangthai_1 = PLC.DS_Tag_DB300.Trangthai_1;
            Trangthai_2 = PLC.DS_Tag_DB300.Trangthai_2;
            Min_ket1 = PLC.DS_Tag_DB300.Min_ket1;
            Min_ket2 = PLC.DS_Tag_DB300.Min_ket2;
            Max_ket1 = PLC.DS_Tag_DB300.Max_ket1;
            Max_ket2 = PLC.DS_Tag_DB300.Max_ket2;
            LocbuiP40_Run = PLC.DS_Tag_DB300.LocbuiP40_Run;
            LocbuiP41_Run = PLC.DS_Tag_DB300.LocbuiP41_Run;
            Yc_caplieuK1 = PLC.DS_Tag_DB300.Yc_caplieuK1;
            Yc_caplieuK2 = PLC.DS_Tag_DB300.Yc_caplieuK2;
            Yc_xalieuK1 = PLC.DS_Tag_DB300.Yc_xalieuK1;
            Yc_xalieuK2 = PLC.DS_Tag_DB300.Yc_xalieuK2;


            Yeu_cau_luu1 = PLC.DS_Tag_DB300.Yeu_cau_luuhang1;
            Yeu_cau_luu2 = PLC.DS_Tag_DB300.Yeu_cau_luuhang2;
            Yeu_cau_luubi1 = PLC.DS_Tag_DB300.Yeu_cau_luubi1;
            Yeu_cau_luubi2 = PLC.DS_Tag_DB300.Yeu_cau_luubi2;

            // load dữ liệu xong mới lấy tín hiệu kết thúc
            if (Startuped)
            {
                DieuKienKetThuc = PLC.DS_Tag_DB300.Chophepketthuc;
            }

            //Debug.WriteLine("So phieu ====== " + Sophieu.ToString());
            //Debug.WriteLine("HMI_Nhapphieu ====== " + HMI_Nhapphieu.ToString());
            //Debug.WriteLine("DieuKienKetThuc ====== " + DieuKienKetThuc.ToString());
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            // biến PLC
            LoadParameterFromPLC();

            if (!Startuped && PLC.ReadSuccess)
            {
                OnStarup();
                Startuped = true;
            }

            LayCa();


        }

        private void OnStarup()
        {
            //Debug.WriteLine("OnStarup PLCVM");
            LoadChiTietCanFromPLC();
            TongQuanVM TongQuanVM = ServiceLocator.Current.GetInstance<TongQuanVM>();

            TongQuanVM.LoadChiTietCan();

            // write lại các bit yêu cầu lưu
            if (!Common.READ_ONLY)
            {
                PLC.Write_DB300_Yeu_cau_luuhang1(false);
                PLC.Write_DB300_Yeu_cau_luuhang2(false);
                PLC.Write_DB300_Yeu_cau_luubi1(false);
                PLC.Write_DB300_Yeu_cau_luubi2(false);
            }

            ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().InitPhieuCan();
        }

        void ChotLuong(byte? ca)
        {
            int ret;
            long? PhieuCanID = (long?)Sophieu;
            double? TongCan = (double?)tong_can;
            double? xKLCan1;
            double? xKLCan2;

            if (PhieuCanID != 0)
            {
                //var ct = PhieuCanChiTiets.Where(x => x.TrangThai == 10).FirstOrDefault();

                // kết thúc phiếu chi tiết trong db
                var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, PhieuCanID).OrderByDescending(x => x.ID).FirstOrDefault();

                if (chitiet != null && chitiet.TrangThai == 10 && chitiet.Ca != ca)
                {
                    ret = db.p_PhieuCan_ChiTiet_KetThuc(null, chitiet.ID, TongCan);

                    if (ret == 0) // kết thúc chi tiết cân
                    {
                        //tao them phieu can chi tiet
                        long? PhieuCanChiTietID = null;

                        ret = db.p_PhieuCan_ChiTiet_Them(null,
                                                        ref PhieuCanChiTietID,
                                                        PhieuCanID,
                                                        1033, // mang xuat ID
                                                        null, // nv tieu thu
                                                        null, // nv bao ve
                                                        TongCan, // tong plc truoc can
                                                        ca);

                        var ctcs = db.p_PhieuCan_ChiTiet_Can_Lay(null, chitiet.ID).ToList();

                        if (ctcs.Count > 0)
                        {
                            var ctc1_max = ctcs.Where(x => x.Ket == 1).OrderByDescending(x => x.ID).FirstOrDefault();
                            var ctc2_max = ctcs.Where(x => x.Ket == 2).OrderByDescending(x => x.ID).FirstOrDefault();

                            if (ctc1_max != null)
                            {
                                ChiTietCan ctc = ChiTietCan_Lay(KetCan.KET_1, (int)ctc1_max.STT);

                                if (ctc.TLCan1 != 0 && ctc.TLHang == 0)
                                {
                                    xKLCan1 = KLCan1;
                                    ret = db.p_PhieuCan_ChiTiet_Can_KetThuc(null, ctc1_max.ID,
                                                                            xKLCan1,        // tl cân 2 
                                                                            ctc1_max.TLCan1 - xKLCan1,// TLHang, 
                                                                            DateTime.Now);

                                    if (ret == 0) // tạo thêm chi tiêt cân
                                    {
                                        long? LanCanID = null;
                                        ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, ctc1_max.STT,
                                                                            ctc1_max.Ket, PhieuCanChiTietID, DateTime.Now,
                                                                            xKLCan1, // trọng lượng cân 1 của chi tiết cân sau
                                                                            0, 0, 10);
                                    }
                                }
                            }

                            if (ctc2_max != null)
                            {
                                ChiTietCan ctc = ChiTietCan_Lay(KetCan.KET_2, (int)ctc2_max.STT);

                                if (ctc.TLCan1 != 0 && ctc.TLHang == 0)
                                {
                                    xKLCan2 = KLCan2;
                                    ret = db.p_PhieuCan_ChiTiet_Can_KetThuc(null, ctc2_max.ID,
                                                                            xKLCan2,        // tl cân 2 
                                                                            ctc2_max.TLCan1 - xKLCan2,// TLHang, 
                                                                            DateTime.Now);

                                    if (ret == 0) // tạo thêm chi tiêt cân
                                    {
                                        long? LanCanID = null;
                                        ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, ctc2_max.STT,
                                                                            ctc2_max.Ket, PhieuCanChiTietID, DateTime.Now,
                                                                            xKLCan2, // trọng lượng cân 1 của chi tiết cân sau
                                                                            0, 0, 10);
                                    }
                                }
                            }
                        }

                    }
                }

                ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
            }
         
        }

        public void LoadChiTietCanFromPLC()
        {
            int ret;

            if (Sophieu != 0)
            {
                // load du lieu ket can tu plc len db
                var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, (long?)Sophieu).OrderByDescending(x => x.ID).FirstOrDefault();

                // phiếu chi tiết chưa kết thúc mới cập nhật
                if (chitiet != null)
                {
                    var ctcans = db.p_PhieuCan_ChiTiet_Can_LayTheoPC(null, (long?)Sophieu).ToList();

                    // lấy bước chạy nhỏ nhất két của chi tiết cân
                    var buoccanket1_min = ctcans.Where(x => x.Ket == 1).OrderBy(x => x.STT).FirstOrDefault();
                    var buoccanket1_max = ctcans.Where(x => x.Ket == 1).OrderByDescending(x => x.STT).FirstOrDefault();

                    for (int b1 = 1; b1 <= Buoc_chay_1; b1++)
                    {
                        ChiTietCan current_ctc = ChiTietCan_Lay(KetCan.KET_1, b1);

                        // bỏ qua bước cân của chi tiết trước
                        if ((buoccanket1_max != null && b1 >= buoccanket1_max.STT) || (buoccanket1_max == null))
                        {
                            long? LanCanID = null;

                            var ctc1 = ctcans.Where(x => x.Ket == 1 && x.STT == b1).OrderByDescending(x => x.ID).FirstOrDefault();

                            // chưa có ctc trong db & có tl hàng chưa có tl bì và tl giao
                            if (ctc1 == null && current_ctc.TLCan1 != 0 && current_ctc.TLHang == 0)
                            {
                                // tạo mới ctc trạng thái 10
                                ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, b1, 1, chitiet.ID,
                                                                    DateTime.Now,
                                                                    current_ctc.TLCan1,
                                                                    null,
                                                                    null, 10);
                            } // chưa có ctc trong db & đã có tl hàng, tl bì và tl giao
                            else if (ctc1 == null && current_ctc.TLCan1 != 0 && current_ctc.TLHang != 0)
                            {
                                // tạo mới ctc đã kết thúc - trang thái 20
                                ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, b1, 1, chitiet.ID,
                                                                    DateTime.Now,
                                                                    current_ctc.TLCan1,
                                                                    current_ctc.TLCan2,
                                                                    current_ctc.TLHang, 20);
                            } // đã có ctc trg db và trạng thái = 10 - đã có tl hang và tl bi tl giao
                            else if (ctc1 != null && current_ctc.TLCan1 != 0 && current_ctc.TLHang != 0)
                            {
                                ret = db.p_PhieuCan_ChiTiet_Can_KetThuc(null, ctc1.ID,
                                                                        current_ctc.TLCan2,
                                                                        ctc1.TLCan1 - current_ctc.TLCan2,
                                                                        DateTime.Now);
                            }
                        }

                        // cập nhật lại dữ liệu bị lỗi
                        if (buoccanket1_max != null && b1 <= buoccanket1_max.STT)
                        {
                            var ctc1s = ctcans.Where(x => x.Ket == 1 && x.STT == b1).ToList();

                            foreach (var ctc1 in ctc1s)
                            {
                                bool CapNhat = false;
                                double? TLCan1 = ctc1.TLCan1;
                                double? TLCan2 = ctc1.TLCan2;

                                if (ctc1.TLCan1 == 0)
                                {
                                    CapNhat = true;
                                    TLCan1 = current_ctc.TLCan1;
                                }

                                if (ctc1.TLCan2 == 0 && b1 != buoccanket1_max.STT) // khong phai buoc cuoi cung
                                {
                                    CapNhat = true;
                                    TLCan2 = current_ctc.TLCan2;
                                }

                                if (CapNhat)
                                {
                                    ret = db.p_PhieuCan_ChiTiet_Can_CapNhat(null, ctc1.ID, TLCan1, TLCan2, TLCan1 - TLCan2);
                                }
                            }
                        }
                    }

                    // lấy bước chạy nhỏ nhất két của chi tiết cân
                    var buoccanket2_min = ctcans.Where(x => x.Ket == 2).OrderBy(x => x.STT).FirstOrDefault();
                    var buoccanket2_max = ctcans.Where(x => x.Ket == 2).OrderByDescending(x => x.STT).FirstOrDefault();

                    for (int b2 = 1; b2 <= Buoc_chay_2; b2++)
                    {
                        ChiTietCan current_ctc = ChiTietCan_Lay(KetCan.KET_2, b2);

                        // bỏ qua bước cân của chi tiết trước
                        if ((buoccanket2_max != null && b2 >= buoccanket2_max.STT) || (buoccanket2_max == null))
                        {
                            long? LanCanID = null;

                            var ctc2 = ctcans.Where(x => x.Ket == 2 && x.STT == b2).OrderByDescending(x => x.ID).FirstOrDefault();

                            // chưa có ctc trong db & có tl hàng chưa có tl giao
                            if (ctc2 == null && current_ctc.TLCan1 != 0 && current_ctc.TLHang == 0)
                            {
                                // tạo mới ctc trạng thái 10
                                ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, b2, 2, chitiet.ID,
                                                                    DateTime.Now,
                                                                    current_ctc.TLCan1,
                                                                    null,
                                                                    null, 10);
                            } // chưa có ctc trong db & đã có tl hàng và tl giao
                            else if (ctc2 == null && current_ctc.TLCan1 != 0 && current_ctc.TLHang != 0)
                            {
                                // tạo mới ctc đã kết thúc - trang thái 20
                                ret = db.p_PhieuCan_ChiTiet_Can_Them(null, ref LanCanID, b2, 2, chitiet.ID,
                                                                    DateTime.Now,
                                                                    current_ctc.TLCan1,
                                                                    current_ctc.TLCan2,
                                                                    current_ctc.TLHang, 20);
                            } // đã có ctc trg db và trạng thái = 10 - đã có tl hang và tl bi tl giao
                            else if (ctc2 != null && current_ctc.TLCan1 != 0 && current_ctc.TLHang != 0)
                            {
                                ret = db.p_PhieuCan_ChiTiet_Can_KetThuc(null, ctc2.ID,
                                                                        current_ctc.TLCan2,
                                                                        ctc2.TLCan1 - current_ctc.TLCan2,
                                                                        DateTime.Now);
                            }
                        }

                        // cập nhật lại dữ liệu bị lỗi
                        if (buoccanket2_max != null && b2 <= buoccanket2_max.STT)
                        {
                            var ctc2s = ctcans.Where(x => x.Ket == 2 && x.STT == b2).ToList();

                            foreach (var ctc2 in ctc2s)
                            {
                                bool CapNhat = false;
                                double? TLCan1 = ctc2.TLCan1;
                                double? TLCan2 = ctc2.TLCan2;

                                if (ctc2.TLCan1 == 0)
                                {
                                    CapNhat = true;
                                    TLCan1 = current_ctc.TLCan1;
                                }

                                if (ctc2.TLCan2 == 0 && b2 != buoccanket2_max.STT)
                                {
                                    CapNhat = true;
                                    TLCan2 = current_ctc.TLCan2;
                                }

                                if (CapNhat)
                                {
                                    ret = db.p_PhieuCan_ChiTiet_Can_CapNhat(null, ctc2.ID, TLCan1, TLCan2, TLCan1 - TLCan2);
                                }
                            }
                        }
                    }
                }
            }
        }


        public ChiTietCan ChiTietCan_Lay(KetCan ketCan, int BuocChay)
        {
            int KLHang = 0;
            int KLBi = 0;
            int KLGiao = 0;

            switch (BuocChay)
            {
                case 1:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang1 : PLC.DS_Tag_DB305.KLhang1;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi1 : PLC.DS_Tag_DB305.KLbi1;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao1 : PLC.DS_Tag_DB305.KLGiao1;
                        break;
                    }
                case 2:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang2 : PLC.DS_Tag_DB305.KLhang2;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi2 : PLC.DS_Tag_DB305.KLbi2;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao2 : PLC.DS_Tag_DB305.KLGiao2;
                        break;
                    }
                case 3:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang3 : PLC.DS_Tag_DB305.KLhang3;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi3 : PLC.DS_Tag_DB305.KLbi3;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao3 : PLC.DS_Tag_DB305.KLGiao3;
                        break;
                    }
                case 4:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang4 : PLC.DS_Tag_DB305.KLhang4;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi4 : PLC.DS_Tag_DB305.KLbi4;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao4 : PLC.DS_Tag_DB305.KLGiao4;
                        break;
                    }
                case 5:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang5 : PLC.DS_Tag_DB305.KLhang5;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi5 : PLC.DS_Tag_DB305.KLbi5;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao5 : PLC.DS_Tag_DB305.KLGiao5;
                        break;
                    }
                case 6:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang6 : PLC.DS_Tag_DB305.KLhang6;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi6 : PLC.DS_Tag_DB305.KLbi6;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao6 : PLC.DS_Tag_DB305.KLGiao6;
                        break;
                    }
                case 7:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang7 : PLC.DS_Tag_DB305.KLhang7;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi7 : PLC.DS_Tag_DB305.KLbi7;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao7 : PLC.DS_Tag_DB305.KLGiao7;
                        break;
                    }
                case 8:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang8 : PLC.DS_Tag_DB305.KLhang8;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi8 : PLC.DS_Tag_DB305.KLbi8;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao8 : PLC.DS_Tag_DB305.KLGiao8;
                        break;
                    }
                case 9:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang9 : PLC.DS_Tag_DB305.KLhang9;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi9 : PLC.DS_Tag_DB305.KLbi9;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao9 : PLC.DS_Tag_DB305.KLGiao9;
                        break;
                    }
                case 10:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang10 : PLC.DS_Tag_DB305.KLhang10;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi10 : PLC.DS_Tag_DB305.KLbi10;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao10 : PLC.DS_Tag_DB305.KLGiao10;
                        break;
                    }
                case 11:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang11 : PLC.DS_Tag_DB305.KLhang11;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi11 : PLC.DS_Tag_DB305.KLbi11;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao11 : PLC.DS_Tag_DB305.KLGiao11;
                        break;
                    }
                case 12:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang12 : PLC.DS_Tag_DB305.KLhang12;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi12 : PLC.DS_Tag_DB305.KLbi12;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao12 : PLC.DS_Tag_DB305.KLGiao12;
                        break;
                    }
                case 13:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang13 : PLC.DS_Tag_DB305.KLhang13;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi13 : PLC.DS_Tag_DB305.KLbi13;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao13 : PLC.DS_Tag_DB305.KLGiao13;
                        break;
                    }
                case 14:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang14 : PLC.DS_Tag_DB305.KLhang14;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi14 : PLC.DS_Tag_DB305.KLbi14;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao14 : PLC.DS_Tag_DB305.KLGiao14;
                        break;
                    }
                case 15:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang15 : PLC.DS_Tag_DB305.KLhang15;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi15 : PLC.DS_Tag_DB305.KLbi15;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao15 : PLC.DS_Tag_DB305.KLGiao15;
                        break;
                    }
                case 16:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang16 : PLC.DS_Tag_DB305.KLhang16;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi16 : PLC.DS_Tag_DB305.KLbi16;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao16 : PLC.DS_Tag_DB305.KLGiao16;
                        break;
                    }
                case 17:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang17 : PLC.DS_Tag_DB305.KLhang17;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi17 : PLC.DS_Tag_DB305.KLbi17;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao17 : PLC.DS_Tag_DB305.KLGiao17;
                        break;
                    }
                case 18:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang18 : PLC.DS_Tag_DB305.KLhang18;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi18 : PLC.DS_Tag_DB305.KLbi18;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao18 : PLC.DS_Tag_DB305.KLGiao18;
                        break;
                    }
                case 19:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang19 : PLC.DS_Tag_DB305.KLhang19;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi19 : PLC.DS_Tag_DB305.KLbi19;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao19 : PLC.DS_Tag_DB305.KLGiao19;
                        break;
                    }
                case 20:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang20 : PLC.DS_Tag_DB305.KLhang20;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi20 : PLC.DS_Tag_DB305.KLbi20;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao20 : PLC.DS_Tag_DB305.KLGiao20;
                        break;
                    }
                case 21:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang21 : PLC.DS_Tag_DB305.KLhang21;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi21 : PLC.DS_Tag_DB305.KLbi21;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao21 : PLC.DS_Tag_DB305.KLGiao21;
                        break;
                    }
                case 22:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang22 : PLC.DS_Tag_DB305.KLhang22;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi22 : PLC.DS_Tag_DB305.KLbi22;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao22 : PLC.DS_Tag_DB305.KLGiao22;
                        break;
                    }
                case 23:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang23 : PLC.DS_Tag_DB305.KLhang23;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi23 : PLC.DS_Tag_DB305.KLbi23;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao23 : PLC.DS_Tag_DB305.KLGiao23;
                        break;
                    }
                case 24:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang24 : PLC.DS_Tag_DB305.KLhang24;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi24 : PLC.DS_Tag_DB305.KLbi24;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao24 : PLC.DS_Tag_DB305.KLGiao24;
                        break;
                    }
                case 25:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang25 : PLC.DS_Tag_DB305.KLhang25;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi25 : PLC.DS_Tag_DB305.KLbi25;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao25 : PLC.DS_Tag_DB305.KLGiao25;
                        break;
                    }
                case 26:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang26 : PLC.DS_Tag_DB305.KLhang26;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi26 : PLC.DS_Tag_DB305.KLbi26;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao26 : PLC.DS_Tag_DB305.KLGiao26;
                        break;
                    }
                case 27:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang27 : PLC.DS_Tag_DB305.KLhang27;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi27 : PLC.DS_Tag_DB305.KLbi27;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao27 : PLC.DS_Tag_DB305.KLGiao27;
                        break;
                    }
                case 28:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang28 : PLC.DS_Tag_DB305.KLhang28;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi28 : PLC.DS_Tag_DB305.KLbi28;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao28 : PLC.DS_Tag_DB305.KLGiao28;
                        break;
                    }
                case 29:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang29 : PLC.DS_Tag_DB305.KLhang29;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi29 : PLC.DS_Tag_DB305.KLbi29;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao29 : PLC.DS_Tag_DB305.KLGiao29;
                        break;
                    }
                case 30:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang30 : PLC.DS_Tag_DB305.KLhang30;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi30 : PLC.DS_Tag_DB305.KLbi30;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao30 : PLC.DS_Tag_DB305.KLGiao30;
                        break;
                    }
                case 31:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang31 : PLC.DS_Tag_DB305.KLhang31;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi31 : PLC.DS_Tag_DB305.KLbi31;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao31 : PLC.DS_Tag_DB305.KLGiao31;
                        break;
                    }
                case 32:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang32 : PLC.DS_Tag_DB305.KLhang32;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi32 : PLC.DS_Tag_DB305.KLbi32;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao32 : PLC.DS_Tag_DB305.KLGiao32;
                        break;
                    }
                case 33:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang33 : PLC.DS_Tag_DB305.KLhang33;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi33 : PLC.DS_Tag_DB305.KLbi33;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao33 : PLC.DS_Tag_DB305.KLGiao33;
                        break;
                    }
                case 34:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang34 : PLC.DS_Tag_DB305.KLhang34;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi34 : PLC.DS_Tag_DB305.KLbi34;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao34 : PLC.DS_Tag_DB305.KLGiao34;
                        break;
                    }
                case 35:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang35 : PLC.DS_Tag_DB305.KLhang35;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi35 : PLC.DS_Tag_DB305.KLbi35;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao35 : PLC.DS_Tag_DB305.KLGiao35;
                        break;
                    }
                case 36:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang36 : PLC.DS_Tag_DB305.KLhang36;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi36 : PLC.DS_Tag_DB305.KLbi36;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao36 : PLC.DS_Tag_DB305.KLGiao36;
                        break;
                    }
                case 37:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang37 : PLC.DS_Tag_DB305.KLhang37;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi37 : PLC.DS_Tag_DB305.KLbi37;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao37 : PLC.DS_Tag_DB305.KLGiao37;
                        break;
                    }
                case 38:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang38 : PLC.DS_Tag_DB305.KLhang38;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi38 : PLC.DS_Tag_DB305.KLbi38;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao38 : PLC.DS_Tag_DB305.KLGiao38;
                        break;
                    }
                case 39:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang39 : PLC.DS_Tag_DB305.KLhang39;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi39 : PLC.DS_Tag_DB305.KLbi39;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao39 : PLC.DS_Tag_DB305.KLGiao39;
                        break;
                    }
                case 40:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang40 : PLC.DS_Tag_DB305.KLhang40;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi40 : PLC.DS_Tag_DB305.KLbi40;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao40 : PLC.DS_Tag_DB305.KLGiao40;
                        break;
                    }
                case 41:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang41 : PLC.DS_Tag_DB305.KLhang41;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi41 : PLC.DS_Tag_DB305.KLbi41;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao41 : PLC.DS_Tag_DB305.KLGiao41;
                        break;
                    }
                case 42:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang42 : PLC.DS_Tag_DB305.KLhang42;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi42 : PLC.DS_Tag_DB305.KLbi42;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao42 : PLC.DS_Tag_DB305.KLGiao42;
                        break;
                    }
                case 43:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang43 : PLC.DS_Tag_DB305.KLhang43;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi43 : PLC.DS_Tag_DB305.KLbi43;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao43 : PLC.DS_Tag_DB305.KLGiao43;
                        break;
                    }
                case 44:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang44 : PLC.DS_Tag_DB305.KLhang44;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi44 : PLC.DS_Tag_DB305.KLbi44;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao44 : PLC.DS_Tag_DB305.KLGiao44;
                        break;
                    }
                case 45:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang45 : PLC.DS_Tag_DB305.KLhang45;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi45 : PLC.DS_Tag_DB305.KLbi45;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao45 : PLC.DS_Tag_DB305.KLGiao45;
                        break;
                    }
                case 46:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang46 : PLC.DS_Tag_DB305.KLhang46;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi46 : PLC.DS_Tag_DB305.KLbi46;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao46 : PLC.DS_Tag_DB305.KLGiao46;
                        break;
                    }
                case 47:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang47 : PLC.DS_Tag_DB305.KLhang47;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi47 : PLC.DS_Tag_DB305.KLbi47;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao47 : PLC.DS_Tag_DB305.KLGiao47;
                        break;
                    }
                case 48:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang48 : PLC.DS_Tag_DB305.KLhang48;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi48 : PLC.DS_Tag_DB305.KLbi48;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao48 : PLC.DS_Tag_DB305.KLGiao48;
                        break;
                    }
                case 49:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang49 : PLC.DS_Tag_DB305.KLhang49;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi49 : PLC.DS_Tag_DB305.KLbi49;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao49 : PLC.DS_Tag_DB305.KLGiao49;
                        break;
                    }
                case 50:
                    {
                        KLHang = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLhang50 : PLC.DS_Tag_DB305.KLhang50;
                        KLBi = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLbi50 : PLC.DS_Tag_DB305.KLbi50;
                        KLGiao = (ketCan == KetCan.KET_1) ? PLC.DS_Tag_DB304.KLGiao50 : PLC.DS_Tag_DB305.KLGiao50;
                        break;
                    }
                default:
                    break;
            }
            //Debug.WriteLine("===============================================");
            //Debug.WriteLine("Lấy dữ liệu két " + ketCan.ToString() + " bước chạy thứ " + BuocChay.ToString());
            //Debug.WriteLine("KLHang ===== " + KLHang.ToString());
            //Debug.WriteLine("KLBi   ===== " + KLBi.ToString());
            //Debug.WriteLine("KLGiao ===== " + KLGiao.ToString());
            //Debug.WriteLine("===============================================");
            return new ChiTietCan(BuocChay, (byte?)ketCan, KLHang, KLBi, KLGiao);
        }
    }
}
