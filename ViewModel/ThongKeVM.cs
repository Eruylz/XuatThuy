using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuatThuy.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using XuatThuy.Controls;
using System.Diagnostics;
using XuatThuy.Utils;
using System.Data;
using XuatThuy.Reports;
using Microsoft.Reporting.WinForms;

namespace XuatThuy.ViewModel
{
    public class ThongKeVM : ViewModelBase
    {
        public RelayCommand CmdThongKe { get; set; }

        #region property
        DateTime? _tuNgay;
        public DateTime? TuNgay
        {
            get { return _tuNgay; }
            set
            {
                _tuNgay = value;
                RaisePropertyChanged("TuNgay");
            }
        }

        DateTime? _denNgay;
        public DateTime? DenNgay
        {
            get { return _denNgay; }
            set
            {
                _denNgay = value;
                RaisePropertyChanged("DenNgay");
            }
        }

        long? _tuSoPhieu;
        public long? TuSoPhieu
        {
            get => _tuSoPhieu;
            set
            {
                _tuSoPhieu = value;
                RaisePropertyChanged("TuSoPhieu");
            }
        }

        long? _denSoPhieu;
        public long? DenSoPhieu
        {
            get => _denSoPhieu;
            set
            {
                _denSoPhieu = value;
                RaisePropertyChanged("DenSoPhieu");
            }
        }

        CaXuat _selectedCa;
        public CaXuat SelectedCa
        {
            get => _selectedCa;
            set
            {
                _selectedCa = value;
                RaisePropertyChanged("SelectedCa");
            }
        }

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

        Area _selectedArea;
        public Area SelectedArea
        {
            get => _selectedArea;
            set
            {
                _selectedArea = value;
                RaisePropertyChanged("SelectedArea");
            }
        }

        NhaPhanPhoi _selectedNhaPhanPhoi;
        public NhaPhanPhoi SelectedNhaPhanPhoi
        {
            get => _selectedNhaPhanPhoi;
            set
            {
                _selectedNhaPhanPhoi = value;
                RaisePropertyChanged("SelectedNhaPhanPhoi");
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
                }
            }
        }
        #endregion

        public ThongKeVM()
        {
            CmdThongKe = new RelayCommand(ThongKe);
        }

        private void ThongKe()
        {
            XuatHangDataContext db = new XuatHangDataContext();

            Window window = new Window();
            window.WindowState = WindowState.Maximized;
            window.Title = "Thống kê phiếu cân";

            RvControl rvControl = new RvControl();

            var pcs = db.p_PhieuCan_ThongKe(null, TuSoPhieu, DenSoPhieu, TuNgay, DenNgay,
                                            SelectedNhaPhanPhoi != null ? (int?)SelectedNhaPhanPhoi.CustomerID : null,
                                            SelectedArea != null ? (int?)SelectedArea.AreaID : null,
                                            SelectedSoLo != null ? (long?)SelectedSoLo.ID : null,
                                            SelectedPhieuChatLuong != null ? (long?)SelectedPhieuChatLuong.ID : null,
                                            SelectedNhanVienXuatHang != null ? (int?)SelectedNhanVienXuatHang.CaNhanID : null,
                                            SelectedSanPham != null ? (int?)SelectedSanPham.ItemID : null,
                                            SelectedCa != null ? (byte?)SelectedCa.ID : null
                                            );
            DataTable dt = Common.LINQToDataTable(pcs);

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 40;
            pg.Margins.Bottom = 40;
            pg.Margins.Left = 0;
            pg.Margins.Right = 0;
            rvControl.rViewer.SetPageSettings(pg);
            rvControl.rViewer.LocalReport.DataSources.Add(rds);
            rvControl.rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptThongKe.rdlc";

            rvControl.rViewer.RefreshReport();

            window.Content = rvControl;
            window.ShowDialog();
        }
    }
}
