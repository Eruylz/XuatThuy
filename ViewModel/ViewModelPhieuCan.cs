using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuatThuy.ViewModel;
using System.Windows.Threading;
using System.Diagnostics;
using XuatThuy.PLCs;
using XuatThuy.Model;
using XuatThuy.Utils;
using System.Windows;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using S7.Net;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CommonServiceLocator;
using XuatThuy.Controls;
using System.Windows.Controls;
using Microsoft.Reporting.WinForms;

namespace XuatThuy.ViewModel
{
    public class ViewModelPhieuCan : ViewModelBase
    {
        PlcViewModel plcViewModel = ServiceLocator.Current.GetInstance<PlcViewModel>();

        SalesOrder _selectedSalesOrder;
        public SalesOrder SelectedSalesOrder
        {
            get { return _selectedSalesOrder; }
            set
            {
                if (_selectedSalesOrder != value)
                {
                    _selectedSalesOrder = value;
                    RaisePropertyChanged("SelectedSalesOrder");

                    if (_selectedSalesOrder != null)
                    {
                        var pcs = db.p_PhieuCan_LayTheoOrder(null, _selectedSalesOrder.ORDER_ID).Where(x => x.TrangThai != 10).ToList();

                        if (pcs.Count > 0) // đã có phiếu xuất
                        {
                            PLCSetPoint = Convert.ToDouble(_selectedSalesOrder.ORDER_QUANTITY * 1000);

                            foreach (var pc in pcs)
                            {
                                PLCSetPoint = PLCSetPoint - pc.TLXuat;
                            }
                        }
                        else // chưa có phiếu xuất setpoint = tl đăng ký của đơn hàng
                        {
                            PLCSetPoint = Convert.ToDouble(_selectedSalesOrder.ORDER_QUANTITY * 1000);
                        } 
                    }
                }
            }
        }

        public RelayCommand CmdSalesOrdersOpening { get; set; }
        public RelayCommand CmdTamDung { get; set; }
        public RelayCommand CmdKetThuc { get; set; }
        public RelayCommand CmdKetThucChuDong { get; set; }
        public RelayCommand CmdChotLuong { get; set; }
        public RelayCommand CmdInA6 { get; set; }
        public RelayCommand CmdInA4 { get; set; }
        public RelayCommand CmdInNhiet { get; set; }
        public RelayCommand<PhieuCanChiTiet> CmdShowCTKet { get; set; }
        public RelayCommand<PhieuCanChiTiet> CmdInChiTiet { get; set; }

        public ObservableCollection<SanPham> SanPhams { get; set; }
        public ObservableCollection<Area> Areas { get; set; }
        public ObservableCollection<NhaPhanPhoi> NhaPhanPhois { get; set; }
        public ObservableCollection<NhanVien> NVXuatHangs { get; set; }
        public ObservableCollection<NhanVien> NVBaoVes { get; set; }
        public ObservableCollection<PhuongThucVanChuyen> PTVCs { get; set; }
        public ObservableCollection<PhieuCanChiTiet> PhieuCanChiTiets { get; set; }
        public ObservableCollection<ChiTietCan> ChiTietCans { get; set; }
        public ObservableCollection<SalesOrder> SalesOrders { get; set; }

        XuatHangDataContext db = new XuatHangDataContext();

        SoLo _selectedSoLo;
        public SoLo SelectedSoLo
        {
            get => _selectedSoLo;
            set
            {
                _selectedSoLo = value;
                RaisePropertyChanged("SelectedSoLo");
            }
        }

        PhieuChatLuong _selectedPhieuChatLuong;
        public PhieuChatLuong SelectedPhieuChatLuong
        {
            get => _selectedPhieuChatLuong;
            set
            {
                _selectedPhieuChatLuong = value;
                RaisePropertyChanged("SelectedPhieuChatLuong");
            }
        }

        #region property 
        private bool _CbxSalesOrderEnable;
        public bool CbxSalesOrderEnable
        {
            get
            {
                return _CbxSalesOrderEnable;
            }
            set
            {
                if (_CbxSalesOrderEnable != value)
                {
                    _CbxSalesOrderEnable = value;
                    RaisePropertyChanged("CbxSalesOrderEnable");
                }
            }
        }

        string _BtnTamDungName;
        public string BtnTamDungName
        {
            get
            {
                return _BtnTamDungName;
            }
            set
            {
                if (_BtnTamDungName != value)
                {
                    _BtnTamDungName = value;
                    RaisePropertyChanged("BtnTamDungName");
                }
            }
        }

        bool _BtnTaoPhieuEnable;
        public bool BtnTaoPhieuEnable
        {
            get
            {
                return _BtnTaoPhieuEnable;
            }
            set
            {
                if (_BtnTaoPhieuEnable != value)
                {
                    _BtnTaoPhieuEnable = value;
                    RaisePropertyChanged("BtnTaoPhieuEnable");
                }
            }
        }

        bool _BtnTamDungEnable;
        public bool BtnTamDungEnable
        {
            get
            {
                return _BtnTamDungEnable;
            }
            set
            {
                if (_BtnTamDungEnable != value)
                {
                    _BtnTamDungEnable = value;
                    RaisePropertyChanged("BtnTamDungEnable");
                }
            }
        }

        bool _BtnChotLuongEnable;
        public bool BtnChotLuongEnable
        {
            get
            {
                return _BtnChotLuongEnable;
            }
            set
            {
                if (_BtnChotLuongEnable != value)
                {
                    _BtnChotLuongEnable = value;
                    RaisePropertyChanged("BtnChotLuongEnable");
                }
            }
        }

        bool _BtnKetThucEnable;
        public bool BtnKetThucEnable
        {
            get
            {
                return _BtnKetThucEnable;
            }
            set
            {
                _BtnKetThucEnable = value;
                RaisePropertyChanged("BtnKetThucEnable");
            }
        }

        bool _BtnKetThucChuDongEnable;
        public bool BtnKetThucChuDongEnable
        {
            get
            {
                return _BtnKetThucChuDongEnable;
            }
            set
            {
                if (_BtnKetThucChuDongEnable != value)
                {
                    _BtnKetThucChuDongEnable = value;
                    RaisePropertyChanged("BtnKetThucChuDongEnable");
                }
            }
        }

        double? _PLCSetPoint;
        public double? PLCSetPoint
        {
            get
            {
                return _PLCSetPoint;
            }
            set
            {
                if (_PLCSetPoint != value)
                {
                    _PLCSetPoint = value;
                    RaisePropertyChanged("PLCSetPoint");
                }
            }
        }

        SanPham _SelectedSanPham;
        public SanPham SelectedSanPham
        {
            get
            {
                return _SelectedSanPham;
            }
            set
            {
                if (_SelectedSanPham != value)
                {
                    _SelectedSanPham = value;
                    RaisePropertyChanged("SelectedSanPham");
                }
            }
        }

        NhanVien _SelectedNhanVienBaoVe;
        public NhanVien SelectedNhanVienBaoVe
        {
            get
            {
                return _SelectedNhanVienBaoVe;
            }
            set
            {
                if (_SelectedNhanVienBaoVe != value)
                {
                    _SelectedNhanVienBaoVe = value;
                    RaisePropertyChanged("SelectedNhanVienBaoVe");
                }
            }
        }

        NhanVien _SelectedNhanVienXuatHang;
        public NhanVien SelectedNhanVienXuatHang
        {
            get
            {
                return _SelectedNhanVienXuatHang;
            }
            set
            {
                if (_SelectedNhanVienXuatHang != value)
                {
                    _SelectedNhanVienXuatHang = value;
                    RaisePropertyChanged("SelectedNhanVienXuatHang");

                    var ct = PhieuCanChiTiets.Where(x => x.TrangThai == 10).FirstOrDefault();

                    if (ct != null)
                    {
                        int ret = db.p_PhieuCan_ChiTiet_Sua(null, ct.ID, _SelectedNhanVienXuatHang.CaNhanID,
                                                SelectedNhanVienBaoVe == null ? null : (int?)SelectedNhanVienBaoVe.CaNhanID);
                    }

                    
                }
            }
        }

        PhieuCanChiTiet _SelectedPhieuCanChiTiet;
        public PhieuCanChiTiet SelectedPhieuCanChiTiet
        {
            get
            {
                return _SelectedPhieuCanChiTiet;
            }
            set
            {
                //if (_SelectedPhieuCanChiTiet != value)
                //{
                    _SelectedPhieuCanChiTiet = value;
                    RaisePropertyChanged("SelectedPhieuCanChiTiet");

                    //ShowChiTietCan(value);
                //}
            }
        }

        public RelayCommand CmdTaoPhieu { get; set; }
        public RelayCommand CmdShowChiTietCan { get; set; }

        #endregion


        public ViewModelPhieuCan()
        {
            PhieuCanChiTiets = new ObservableCollection<PhieuCanChiTiet>();
            ChiTietCans = new ObservableCollection<ChiTietCan>();
            SalesOrders = new ObservableCollection<SalesOrder>();

            DispatcherTimer timer;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            Debug.WriteLine("======ViewModelPhieuCan========????????????????????");

            //new PaletteHelper().SetLightDark(true);
            //new PaletteHelper().ReplacePrimaryColor(Swatch);

            CmdSalesOrdersOpening = new RelayCommand(OnSalesOrdersOpening);
            CmdTamDung = new RelayCommand(TamDung);
            CmdInA6 = new RelayCommand(InA6);
            CmdInA4 = new RelayCommand(InA4);
            CmdInNhiet = new RelayCommand(InNhiet);
            CmdKetThuc = new RelayCommand(() => KetThucAsync());
            CmdKetThucChuDong = new RelayCommand(() => KetThucChuDong());
            CmdInChiTiet = new RelayCommand<PhieuCanChiTiet>(OnInChiTiet);
            CmdShowCTKet = new RelayCommand<PhieuCanChiTiet>(OnShowCTKet);
            //CmdChotLuong = new RelayCommand(() => ChotLuong());
            CmdTaoPhieu = new RelayCommand(() => TaoPhieuAsync());
            CmdShowChiTietCan = new RelayCommand(() => ShowChiTietCan(this));

            SalesOrders = new ObservableCollection<SalesOrder>();

            PTVCs = new ObservableCollection<PhuongThucVanChuyen>();
            var ptvcs = db.p_PhuongThucVanChuyen_Lay(null);

            foreach (var ptvc in ptvcs)
            {
                PTVCs.Add(new PhuongThucVanChuyen((int)ptvc.ID, ptvc.Ten));
            }

            SanPhams = new ObservableCollection<SanPham>();
            var sanphams = db.p_SanPham_Lay(null);

            foreach (var sanpham in sanphams)
            {
                SanPhams.Add(new SanPham(sanpham.ID, sanpham.ItemID, sanpham.Ten));
            }

            NhaPhanPhois = new ObservableCollection<NhaPhanPhoi>();
            var npps = db.p_SMS_NPP_Lay();

            foreach (var npp in npps)
            {
                NhaPhanPhois.Add(new NhaPhanPhoi(npp.ID, npp.CUSTOMER_ID, npp.CUSTOMER_NAME));
            }

            Areas = new ObservableCollection<Area>();
            var areas = db.p_AREAS_Lay(null);

            foreach (var area in areas)
            {
                Areas.Add(new Area(area.ID, area.area_id, area.name));
            }

            NVBaoVes = new ObservableCollection<NhanVien>();
            NVXuatHangs = new ObservableCollection<NhanVien>();
            var nhanviens = db.p_NhanVien_Lay(null);

            foreach (var nhanvien in nhanviens)
            {
                if (nhanvien.ToChucID == 1027)
                {
                    NVXuatHangs.Add(new NhanVien(nhanvien.ID, nhanvien.CaNhanID, nhanvien.ToChucID, nhanvien.HoVaTen));
                }
                else if (nhanvien.ToChucID == 1013)
                {
                    NVBaoVes.Add(new NhanVien(nhanvien.ID, nhanvien.CaNhanID, nhanvien.ToChucID, nhanvien.HoVaTen));
                }
            }

            SelectedNhanVienBaoVe = NVBaoVes.FirstOrDefault();
            SelectedNhanVienXuatHang = NVXuatHangs.FirstOrDefault();

            //InitPhieuCan();
        }

        private void OnShowCTKet(PhieuCanChiTiet obj)
        {
            //throw new NotImplementedException();
            ShowChiTietCan(obj);
        }

        private void OnInChiTiet(PhieuCanChiTiet ct)
        {
            if (SelectedSalesOrder != null)
            {
                Window window = new Window();
                window.Title = "In phiếu xuất A6";
                window.Width = 900;
                window.Height = 700;
                RvControl rvControl = new RvControl();


                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.Margins.Top = 0;
                pg.Margins.Bottom = 0;
                pg.Margins.Left = 0;
                pg.Margins.Right = 0;
                rvControl.rViewer.SetPageSettings(pg);

                var so = db.p_SALES_ORDERS_LayTheoID(null, SelectedSalesOrder.ORDER_ID).FirstOrDefault();

                rvControl.rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptPhieuXuat.rdlc";

                ReportParameter SoPhieu = new ReportParameter("SoPhieu", plcViewModel.Sophieu.ToString());
                ReportParameter TLHang = new ReportParameter("TLHang", ct.KLHang.ToString());
                ReportParameter TongBatDau = new ReportParameter("TongBatDau", ct.SoTongPLC_TruocCan.ToString());
                ReportParameter NguoiGiao = new ReportParameter("NguoiGiao", ct.NVXuatHang);
                ReportParameter Ca = new ReportParameter("Ca", ct.Ca.ToString());
                ReportParameter TongKetThuc = new ReportParameter("TongKetThuc", ct.SoTongKetThuc.ToString());
                ReportParameter GioBatDau = new ReportParameter("GioBatDau", ct.NgayTao == null ? string.Empty : ((DateTime)ct.NgayTao).ToString("HH:mm:ss"));
                ReportParameter GioKetThuc = new ReportParameter("GioKetThuc", ct.NgaySua == null ? string.Empty : ((DateTime)ct.NgaySua).ToString("HH:mm:ss"));
                ReportParameter NgayThongKe = new ReportParameter("NgayThongKe", ct.NgayThongKe == null ? string.Empty : ((DateTime)ct.NgayThongKe).ToString("dd-MM-yyyy"));

                rvControl.rViewer.LocalReport.SetParameters(new ReportParameter[] { SoPhieu, TLHang, TongKetThuc,
                TongBatDau, NguoiGiao, Ca, GioBatDau, GioKetThuc, NgayThongKe });

                if (so != null)
                {
                    ReportParameter BenNhan = new ReportParameter("BenNhan", so.CUSTOMER_NAME);
                    ReportParameter MaSoGiao = new ReportParameter("MaSoGiao", so.DELIVERY_CODE);
                    ReportParameter SoTau = new ReportParameter("SoTau", so.VEHICLE_CODE);
                    ReportParameter LoaiXM = new ReportParameter("LoaiXM", so.TenSanPham);
                    ReportParameter NguoiNhan = new ReportParameter("NguoiNhan", so.DRIVER_NAME);
                    ReportParameter DiaBan = new ReportParameter("DiaBan", so.AREA_NAME);

                    rvControl.rViewer.LocalReport.SetParameters(new ReportParameter[] { BenNhan,
                    MaSoGiao, SoTau, LoaiXM, NguoiNhan, DiaBan });
                }

                rvControl.rViewer.RefreshReport();
                window.Content = rvControl;
                window.ShowDialog(); 
            }
        }

        private void InNhiet()
        {
            if (plcViewModel.Sophieu != 0)
            {
                Window window = new Window();
                window.Width = 500;
                window.Title = "Phiếu in nhiệt";
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = plcViewModel.Sophieu;
                rvControl.ReportType = 3;

                window.Content = rvControl;
                window.ShowDialog();
            }
        }

        private void InA4()
        {
            if (plcViewModel.Sophieu != 0)
            {
                Window window = new Window();
                window.Width = 900;
                window.Title = "In phiếu xuất A4";
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = plcViewModel.Sophieu;
                rvControl.ReportType = 4;

                window.Content = rvControl;
                window.ShowDialog();
            }
        }

        private void InA6()
        {
            if (plcViewModel.Sophieu != 0)
            {
                Window window = new Window();
                window.Title = "In phiếu xuất A6";
                window.Width = 900;
                window.Height = 700;
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanChiTietID = plcViewModel.Sophieu;
                rvControl.ReportType = 2;

                window.Content = rvControl;
                window.ShowDialog();
            }
        }

        public void LoadPhieuCanChiTietDgr()
        {
            long? PhieuCanID = (long?)plcViewModel.Sophieu;

            if (PhieuCanID != 0)
            {
                PhieuCanChiTiets.Clear();
                var chitiets = db.p_PhieuCan_ChiTiet_Lay(null, PhieuCanID);

                foreach (var ct in chitiets)
                {
                    PhieuCanChiTiets.Add(new PhieuCanChiTiet(ct.ID, (long)PhieuCanID, ct.MangXuatID,
                                        ct.NV_XuatHang_ID, ct.NV_BaoVe_ID, ct.Ca, ct.NgayTao, ct.NgaySua, ct.NgayThongKe, ct.TrangThai, ct.NVXuatHang,
                                        ct.NVBaoVe, ct.MangXuat, ct.SoTongPLC_TruocCan, ct.SoTongKetThuc));

                    var ctcans = db.p_PhieuCan_ChiTiet_Can_Lay(null, ct.ID);

                    foreach (var ctcan in ctcans)
                    {
                        ChiTietCans.Add(new ChiTietCan(ctcan.ID, ctcan.PhieuCanChiTietID, ctcan.STT, ctcan.Ket, 
                                                        ctcan.TLCan1, ctcan.TLCan2, ctcan.TLHang,
                                                        ctcan.ThoiDiemCan1, ctcan.ThoiDiemCan2, ctcan.TrangThai));
                    }
                }
            }
        }
        
        void OnSalesOrdersOpening()
        {

            Debug.WriteLine("aaaaaaaaaaaaaaaaaaaaa");
            InitPhieuCan();

        }
        async void TamDung()
        {
            var view = new NotifyControl
            {
                DataContext = new DialogNotifyVM(plcViewModel.HMI_Tamdung ? "Chạy lại" : "Tạm dừng" + " thiết bị!")
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            if ((bool)result)
            {
                long? PhieuCanID = (long?)plcViewModel.Sophieu;
                //long? SoTongPLC = tong_can;

                if (plcViewModel.HMI_Tamdung) // đang chạy
                {
                    //BtnTamDungName = "Tạm dừng";
                    PLC.Write_DB300_HMI_Tamdung(false);
                }
                else // tạm dừng xuất
                {
                    PLC.Write_DB300_HMI_Tamdung(true);
                }
            }

        }

        async void KetThucAsync()
        {
            var view = new NotifyControl
            {
                DataContext = new DialogNotifyVM("Kết thúc phiếu cân?")
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            if ((bool)result)
            {
                ErrorCode errorCode = PLC.Write_DB300_HMI_Nhapphieu(false);

                if (errorCode == ErrorCode.NoError)
                {
                    long PhieuCanID = (long)plcViewModel.Sophieu;
                    // ket thuc trong db
                    int ret = db.p_PhieuCan_KetThuc(null, PhieuCanID, plcViewModel.KLTongPhieu);

                    if (ret == 0)
                    {
                        var chitiet = db.p_PhieuCan_ChiTiet_Lay(null, PhieuCanID).Where(x => x.TrangThai == 10).FirstOrDefault();

                        if (chitiet != null)
                        {
                            ret = db.p_PhieuCan_ChiTiet_KetThuc(null, chitiet.ID, plcViewModel.tong_can);

                            if (ret == 0)
                            {
                                ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().LoadPhieuCanChiTietDgr();
                                ServiceLocator.Current.GetInstance<TongQuanVM>().LoadChiTietCan();
                            }
                        }
                    }
                }

                var pc = db.p_PhieuCan_LayTheoID(null, (long?)plcViewModel.Sophieu).FirstOrDefault();

                if (pc != null)
                {
                    ServiceLocator.Current.GetInstance<DSPhieuCanVM>().PhieuCans
                            .Add(new PhieuCan(pc.ID, pc.Order_ID, pc.BienSo1,
                                    pc.BienSo2, pc.ChuPhuongTien,
                                    pc.HinhThucCan, pc.OFFLINE_FLAG,
                                    pc.TLXuat, pc.TLThucXuat, pc.TLDangKy, pc.LoaiGiaoDich,
                                    pc.SoChi, pc.TrangThai, pc.SoPhieuCu, pc.NgayDongBo,
                                    pc.TrangThaiDongBo, pc.SoLoID, pc.PhieuChatLuongID,
                                    pc.MSGH)); 
                }
            }
        }

        async void KetThucChuDong()
        {
            // kết thúc phiếu trong plc
            //PLC.Write_DB300_HMI_Nhapphieu(false);

            var view = new NotifyPassControl
            {
                DataContext = new DialogNotifyVM("Nhập mật khẩu để kết thúc chủ động phiếu cân!")
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingKetThucChuDongEventHandler);

            if (result != null)
            {
                var passwordBox = result as PasswordBox;

                if (passwordBox.Password == "123456")
                {
                    PLC.Write_DB300_HMI_chay_rut_lieu(true);
                }
            }
        }

        private void ClosingKetThucChuDongEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            //var passwordBox = eventArgs.Parameter as PasswordBox;

        }


        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            //Debug.WriteLine("eventArgs.GetType() ==== " + eventArgs.GetType());
            //Debug.WriteLine("eventArgs.Session() ==== " + eventArgs.Session.ToString());
            //Debug.WriteLine("You can intercept the closing event, and cancel here.  eventArgs.Source === " + eventArgs.Source.ToString());
        }

        void ShowChiTietCan(object obj)
        {
            if (obj != null)
            {
                var chitiet = obj as PhieuCanChiTiet;
                ChiTietCanWindow chiTietCan = new ChiTietCanWindow();
                chiTietCan.DataContext = new ViewModelChiTietCan(chitiet.ID);
                chiTietCan.ShowDialog();
            }
        }

        async void TaoPhieuAsync()
        {
            if (SelectedSalesOrder != null)
            {
                var npp = NhaPhanPhois.Where(x => x.CustomerID == SelectedSalesOrder.CUSTOMER_ID).FirstOrDefault();

                string msg = "Tạo phiếu cân với mã số giao hàng '" + SelectedSalesOrder.DELIVERY_CODE.ToString() +
                                    "'. Bên nhận hàng '" + npp.CustomerName + "'";
                var view = new NotifyControl
                {
                    DataContext = new DialogNotifyVM(msg)
                };

                //show the dialog
                var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

                if ((bool)result)
                {
                    long? PhieuCanID = null;
                    long? PhieuCanChiTietID = 0;
                    //int SetPointKg = Convert.ToInt32(PLCSetPoint * 1000);

                    int ret = db.p_PhieuCan_Them(null,
                                                ref PhieuCanID,
                                                SelectedSalesOrder.ORDER_ID,
                                                SelectedSalesOrder.VEHICLE_CODE,// txtSoTau.Text,
                                                "",//txtSoDuoiTau.Text,
                                                SelectedSalesOrder.DRIVER_NAME,// txtLaiTau.Text,
                                                "cân theo mẻ",// cbxHinhThucCan.Text,
                                                null,//"Offline_flag",
                                                PLCSetPoint,// double.Parse(txtTLXuat.Text), TLDangKy
                                                            //SelectedSalesOrder.ORDER_QUANTITY,// TLDangKy,
                                                1, // trang thai dong bo
                                                SelectedSoLo.ID,
                                                SelectedPhieuChatLuong.ID);

                    if (ret == 0)
                    {
                        // tạo phiếu chi tiết
                        ret = db.p_PhieuCan_ChiTiet_Them(null,
                                                        ref PhieuCanChiTietID,
                                                        PhieuCanID,
                                                        1033, // mang xuat ID
                                                        SelectedNhanVienXuatHang.CaNhanID,
                                                        SelectedNhanVienBaoVe.CaNhanID,
                                                        plcViewModel.tong_can, // tong plc truoc can
                                                        plcViewModel.CurrentCa
                                                        );

                        // view phieu chi tiet
                        if (ret == 0)
                        {
                            LoadPhieuCanChiTietDgr();
                            ErrorCode error = PLC.Write_DB300_Setpoint(Convert.ToInt32(PLCSetPoint));

                            //Debug.WriteLine("============Chay lenh nhap phieu===========");
                            error = PLC.Write_DB300_HMI_Nhapphieu(true);
                            error = PLC.Write_DB300_Sophieu(PhieuCanID == null ? 0 : (int)PhieuCanID);

                            if (error == ErrorCode.NoError)
                            {
                                TongQuanVM tongQuanVM = ServiceLocator.Current.GetInstance<TongQuanVM>();
                                tongQuanVM.ChiTietCans1.Clear();
                                tongQuanVM.ChiTietCans2.Clear();
                                ServiceLocator.Current.GetInstance<SoLoVM>().SoLos.
                                    Where(x => x.ID == SelectedSoLo.ID).FirstOrDefault().CanDelete = false;
                                ServiceLocator.Current.GetInstance<PhieuChatLuongVM>().PhieuChatLuongs.
                                    Where(x => x.ID == SelectedPhieuChatLuong.ID).FirstOrDefault().CanDelete = false;

                                //ret = db.p_SALES_ORDERS_SuaTrangThai(null, SelectedSalesOrder.ID, "RECEIVING");
                            }
                            else
                            {
                                // todo rollback db
                            }
                            // view phieu chi tiet
                        }
                    }
                }
            }
        }

        public void InitPhieuCan()
        {
            DateTime d1 = DateTime.Now;

            SalesOrders.Clear();
            TimeSpan timeSpan = DateTime.Now - d1;

            Debug.WriteLine("timeSpan222222  == " + timeSpan.Milliseconds.ToString());


            var sos = db.p_SALES_ORDERS_LayXuatThuyXMRoi(null);

            foreach (var so in sos)
            {
                SalesOrders.Add(new SalesOrder(so.ID, so.ORDER_ID, so.ORDER_QUANTITY, so.UOM_CODE, so.MacSP, so.CUSTOMER_ID, so.AREA_ID,
                                        so.INVENTORY_ITEM_ID, so.TRANSPORT_METHOD_ID, so.DELIVERY_CODE, so.VEHICLE_CODE, so.DRIVER_NAME, so.STATUS));
            }

            if (plcViewModel.Sophieu != 0)
            {
                // lay thong tin phieu can hien tai
                PhieuCan phieuCan = new PhieuCan(null, (long)plcViewModel.Sophieu);

                if (phieuCan.ID != 0)
                {
                    PLCSetPoint = phieuCan.TLDangKy;

                    SalesOrder so = SalesOrder.GetSalesOrderByID(null, phieuCan.Order_ID);

                    if(SalesOrders.IndexOf(so) < 0)
                    {
                        SalesOrders.Insert(0, so);                        
                    }
                    
                    SelectedSalesOrder = so;

                    SelectedSoLo = ServiceLocator.Current.GetInstance<SoLoVM>().SoLos.Where(x => x.ID == phieuCan.SoLoID).FirstOrDefault();
                    SelectedPhieuChatLuong = ServiceLocator.Current.GetInstance<PhieuChatLuongVM>().PhieuChatLuongs.Where(x => x.ID == phieuCan.PhieuChatLuongID).FirstOrDefault();


                    // load phieu can chi tiet + chi tiet ket
                    LoadPhieuCanChiTietDgr();
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Debug.WriteLine(" =====================  Start timer =============================");
            //Debug.WriteLine("SelectedSalesOrder.STATUS === " + SelectedSalesOrder.STATUS);
            BtnTamDungEnable = PLC.PLCConnected && plcViewModel.HMI_Nhapphieu && !plcViewModel.DieuKienKetThuc;
            BtnTaoPhieuEnable = PLC.PLCConnected && (SelectedNhanVienXuatHang != null &&
                                    SelectedNhanVienBaoVe != null &&
                                    SelectedPhieuChatLuong != null &&
                                    SelectedSoLo != null &&
                                    !plcViewModel.DieuKienKetThuc &&
                                    SelectedSalesOrder.STATUS != "RECEIVED" &&
                                    plcViewModel.HMI_Nhapphieu == false);// && SelectedSalesOrder.STATUS == "BOOKED");
            BtnKetThucChuDongEnable = PLC.PLCConnected && plcViewModel.HMI_Nhapphieu &&
                                        !plcViewModel.DieuKienKetThuc &&
                                        !plcViewModel.HMI_chay_rut_lieu;
            BtnChotLuongEnable = PLC.PLCConnected && plcViewModel.HMI_Nhapphieu &&
                                !plcViewModel.DieuKienKetThuc && (SelectedNhanVienXuatHang != null) &&
                                (SelectedNhanVienBaoVe != null);
            BtnTamDungName = plcViewModel.HMI_Tamdung ? "Tạm dừng" : "Tiếp tục";
        }
    }
}
