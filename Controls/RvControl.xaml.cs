using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XuatThuy.Utils;

namespace XuatThuy.Controls
{
	/// <summary>
	/// Interaction logic for RvControl.xaml
	/// </summary>
	public partial class RvControl : UserControl
	{
        public long? PhieuCanID { get; set; }
        public long? PhieuCanChiTietID { get; set; }

        public int ReportType { get; set; }
        XuatHangDataContext db = new XuatHangDataContext();

        public RvControl()
		{
			InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // report viewer settings
            //rViewer.Reset();
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();

            rViewer.SetDisplayMode(DisplayMode.PrintLayout);
            rViewer.ZoomMode = ZoomMode.Percent;
            rViewer.ZoomPercent = 100;

            if (ReportType == 1) // chi tiết két
            {
                pg.Margins.Top = 40;
                pg.Margins.Bottom = 40;
                pg.Margins.Left = 0;
                pg.Margins.Right = 0;
                rViewer.SetPageSettings(pg);

                var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();

                if (pc != null)
                {
                    var so = db.p_SALES_ORDERS_LayTheoID(null, pc.Order_ID).FirstOrDefault();
                    var pcctcs = db.p_PhieuCan_ChiTiet_Can_LayTheoPC(null, PhieuCanID).ToList();

                    var ctcs = from ctc in pcctcs
                               select new
                               {
                                   ctc.ID,
                                   ctc.STT,
                                   ctc.Ket,
                                   ctc.TLCan1,
                                   ctc.TLCan2,
                                   ctc.TLHang,
                                   ctc.ThoiDiemCan1,
                                   ctc.ThoiDiemCan2,
                                   ctc.PhieuCanChiTietID
                               };

                    DataTable dt = Common.LINQToDataTable(ctcs);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    rViewer.LocalReport.DataSources.Add(rds);
                    rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptChiTietKet.rdlc";

                    ReportParameter SoPhieu = new ReportParameter("SoPhieu", pc.ID.ToString());
                    rViewer.LocalReport.SetParameters(new ReportParameter[] { SoPhieu });

                    if (so != null)
                    {
                        ReportParameter BenNhan = new ReportParameter("BenNhan", so.CUSTOMER_NAME);
                        ReportParameter Msgh = new ReportParameter("Msgh", so.DELIVERY_CODE);
                        ReportParameter LaiTau = new ReportParameter("LaiTau", so.DRIVER_NAME);
                        ReportParameter LoaiHang = new ReportParameter("LoaiHang", so.TenSanPham);

                        rViewer.LocalReport.SetParameters(new ReportParameter[] { BenNhan, Msgh, LaiTau, LoaiHang });
                    }

                    var pccts = db.p_PhieuCan_ChiTiet_Lay(null, pc.ID).ToList();

                    if (pccts.Count > 0)
                    {
                        var pcct_max = pccts.OrderByDescending(x => x.ID).FirstOrDefault();
                        var pcct_min = pccts.OrderBy(x => x.ID).FirstOrDefault();

                        ReportParameter NvTieuThu = new ReportParameter("NvTieuThu", pcct_max.NVXuatHang);
                        ReportParameter NvBaoVe = new ReportParameter("NvBaoVe", pcct_max.NVBaoVe);


                        DateTime NgayThongKeMax = pcct_max.NgayThongKe != null ? (DateTime)pcct_max.NgayThongKe : DateTime.MaxValue;
                        ReportParameter DenNgay = new ReportParameter("DenNgay",
                            pcct_max.NgayThongKe != null ? String.Format("{0:dd/MM/yyyy}", NgayThongKeMax) : string.Empty);

                        DateTime NgayThongKeMin = pcct_min.NgayThongKe != null ? (DateTime)pcct_min.NgayThongKe : DateTime.MinValue;
                        ReportParameter TuNgay = new ReportParameter("TuNgay",
                            pcct_min.NgayThongKe != null ? String.Format("{0:dd/MM/yyyy}", NgayThongKeMin) : string.Empty);

                        rViewer.LocalReport.SetParameters(new ReportParameter[] { DenNgay, TuNgay });

                    }

                    rViewer.RefreshReport();
                } 
            }
            else if (ReportType == 2) // phiếu cân a6
            {
                pg.Margins.Top = 0;
                pg.Margins.Bottom = 0;
                pg.Margins.Left = 0;
                pg.Margins.Right = 0;
                rViewer.SetPageSettings(pg);

                var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();

                if (pc != null)
                {
                    var so = db.p_SALES_ORDERS_LayTheoID(null, pc.Order_ID).FirstOrDefault();
                    var pccts = db.p_PhieuCan_ChiTiet_Lay(null, pc.ID).ToList();

                    rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptPhieuXuat.rdlc";

                    ReportParameter SoPhieu = new ReportParameter("SoPhieu", pc.ID.ToString());
                    ReportParameter TLHang = new ReportParameter("TLHang", pc.TLXuat.ToString());
                    rViewer.LocalReport.SetParameters(new ReportParameter[] { SoPhieu, TLHang });

                    if (so != null)
                    {
                        ReportParameter BenNhan = new ReportParameter("BenNhan", so.CUSTOMER_NAME);
                        ReportParameter MaSoGiao = new ReportParameter("MaSoGiao", so.DELIVERY_CODE);
                        ReportParameter SoTau = new ReportParameter("SoTau", so.VEHICLE_CODE);
                        ReportParameter LoaiXM = new ReportParameter("LoaiXM", so.TenSanPham);
                        ReportParameter NguoiNhan = new ReportParameter("NguoiNhan", so.DRIVER_NAME);
                        ReportParameter DiaBan = new ReportParameter("DiaBan", so.AREA_NAME);
                        
                        rViewer.LocalReport.SetParameters(new ReportParameter[] { BenNhan, MaSoGiao, SoTau, LoaiXM, NguoiNhan, DiaBan });
                    }


                    if (pccts.Count > 0)
                    {
                        var pcct_min = pccts.OrderBy(x => x.ID).FirstOrDefault();
                        var pcct_max = pccts.OrderByDescending(x => x.ID).FirstOrDefault();

                        ReportParameter TongBatDau = new ReportParameter("TongBatDau", pcct_min.SoTongPLC_TruocCan.ToString());
                        ReportParameter NguoiGiao = new ReportParameter("NguoiGiao", pcct_max.NVXuatHang);
                        ReportParameter Ca = new ReportParameter("Ca", pcct_max.Ca.ToString());
                        ReportParameter GioBatDau = new ReportParameter("GioBatDau", pcct_min.NgayTao == null ? string.Empty : ((DateTime)pcct_min.NgayTao).ToString("HH:mm:ss"));

                        rViewer.LocalReport.SetParameters(new ReportParameter[] { TongBatDau, NguoiGiao, Ca, GioBatDau });

                        if (pcct_max.SoTongKetThuc != null)
                        {
                            ReportParameter TongKetThuc = new ReportParameter("TongKetThuc", pcct_max.SoTongKetThuc.ToString());
                            ReportParameter GioKetThuc = new ReportParameter("GioKetThuc", pcct_max.NgayThongKe == null ? string.Empty : ((DateTime)pcct_max.NgayThongKe).ToString("HH:mm:ss"));

                            rViewer.LocalReport.SetParameters(new ReportParameter[] { TongKetThuc, GioKetThuc });
                        }

                    }
                }

                rViewer.RefreshReport();
            }
            else if (ReportType == 4) // phiếu cân a4
            {
                pg.Margins.Top = 0;
                pg.Margins.Bottom = 0;
                pg.Margins.Left = 0;
                pg.Margins.Right = 0;
                rViewer.SetPageSettings(pg);

                var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();
                var pccts = db.p_PhieuCan_ChiTiet_Lay(null, pc.ID).ToList();
                if (pc != null)
                {
                    rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptPhieuCan.rdlc";

                    var so = db.p_SALES_ORDERS_LayTheoID(null, pc.Order_ID).FirstOrDefault();

                    ReportParameter SoPhieu = new ReportParameter("SoPhieu", pc.ID.ToString());
                    ReportParameter KLGiao = new ReportParameter("KLGiao", pc.TLThucXuat.ToString());
                    ReportParameter SoPCL = new ReportParameter("SoPCL", pc.PhieuChatLuong.ToString());
                    ReportParameter SoLo = new ReportParameter("SoLo", pc.SoLo.ToString());
                    //ReportParameter SoChi = new ReportParameter("SoChi", pc.SoChi.ToString());
                    ReportParameter SoChi = new ReportParameter("SoChi", "8"); // fix = 8
                    ReportParameter NgayCapPCL = new ReportParameter("NgayCapPCL", pc.NgayCapPCL == null ? null : ((DateTime)pc.NgayCapPCL).ToString("dd/MM/yyyy"));
                    rViewer.LocalReport.SetParameters(new ReportParameter[] { SoPhieu, KLGiao, NgayCapPCL, SoPCL, SoLo, SoChi});

                    if (so != null)
                    {
                        ReportParameter BenNhan = new ReportParameter("BenNhan", so.CUSTOMER_NAME);
                        ReportParameter NguoiNhanXM = new ReportParameter("NguoiNhanXM", so.DRIVER_NAME);
                        ReportParameter LaiXe = new ReportParameter("LaiXe", so.DRIVER_NAME);
                        ReportParameter LoaiXM = new ReportParameter("LoaiXM", so.TenSanPham);
                        ReportParameter HoaDonGTGT = new ReportParameter("HoaDonGTGT", so.DELIVERY_CODE);
                        ReportParameter SoTau = new ReportParameter("SoTau", so.VEHICLE_CODE);
                        ReportParameter NoiTraXM = new ReportParameter("NoiTraXM", so.AREA_NAME);
                        rViewer.LocalReport.SetParameters(new ReportParameter[] {
                            BenNhan, NguoiNhanXM, LaiXe, LoaiXM, HoaDonGTGT, SoTau, NoiTraXM });

                    }


                    if (pccts.Count > 0)
                    {
                        var pcct_max = pccts.OrderByDescending(x => x.ID).FirstOrDefault();

                        ReportParameter NvTieuThu = new ReportParameter("NvTieuThu", pcct_max.NVXuatHang);
                        ReportParameter NvBaoVe = new ReportParameter("NvBaoVe", pcct_max.NVBaoVe);

                        rViewer.LocalReport.SetParameters(new ReportParameter[] {NvTieuThu, NvBaoVe});

                        if (pcct_max.NgayThongKe != null)
                        {
                            DateTime dtNgayThongKe = (DateTime)pcct_max.NgayThongKe;

                            ReportParameter GioThongKe = new ReportParameter("GioThongKe", dtNgayThongKe.ToString("HH:mm:ss"));
                            ReportParameter NgayThongKe = new ReportParameter("NgayThongKe", dtNgayThongKe.ToString("dd/MM/yyyy"));
                            rViewer.LocalReport.SetParameters(new ReportParameter[] { GioThongKe, NgayThongKe });

                        }
                    }

                    var mcs = db.p_PhieuCan_MaChi_Lay(null, pc.ID).ToList();

                    if (mcs.Count >= 2)
                    {
                        ReportParameter MaChi1 = new ReportParameter("MaChi1", mcs[0].MaChi);
                        ReportParameter MaChi2 = new ReportParameter("MaChi2", mcs[1].MaChi);
                        rViewer.LocalReport.SetParameters(new ReportParameter[] { MaChi1, MaChi2 });
                    }
                }

                rViewer.RefreshReport();
            }
            else if (ReportType == 5) // phiếu mẫu
            {
                pg.Margins.Top = 0;
                pg.Margins.Bottom = 0;
                pg.Margins.Left = 0;
                pg.Margins.Right = 0;
                rViewer.SetPageSettings(pg);
                var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();

                if (pc != null)
                {
                    rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptPhieuMau.rdlc";

                    ReportParameter SoPhieu = new ReportParameter("SoPhieu", pc.ID.ToString());
                    ReportParameter PhieuChatLuong = new ReportParameter("PhieuChatLuong", pc.PhieuChatLuong.ToString());
                    ReportParameter NgayCapPCL = new ReportParameter("NgayCapPCL", pc.NgayCapPCL == null ? null : ((DateTime)pc.NgayCapPCL).ToString("dd/MM/yyyy"));
                    rViewer.LocalReport.SetParameters(new ReportParameter[] { SoPhieu, PhieuChatLuong, NgayCapPCL });

                    var so = db.p_SALES_ORDERS_LayTheoID(null, pc.Order_ID).FirstOrDefault();
                    var pccts = db.p_PhieuCan_ChiTiet_Lay(null, pc.ID).ToList();

                    if (so != null)
                    {
                        ReportParameter SoTau = new ReportParameter("SoTau", so.VEHICLE_CODE);
                        ReportParameter BenNhan = new ReportParameter("BenNhan", so.CUSTOMER_NAME);
                        ReportParameter LoaiHang = new ReportParameter("LoaiHang", so.TenSanPham);
                        ReportParameter LaiXe = new ReportParameter("LaiXe", so.DRIVER_NAME);
                        ReportParameter Msgh = new ReportParameter("Msgh", so.DELIVERY_CODE);
                        rViewer.LocalReport.SetParameters(new ReportParameter[] { SoTau, BenNhan, LoaiHang, LaiXe, Msgh });
                    }

                    if (pccts.Count > 0)
                    {
                        var pcct_max = pccts.OrderByDescending(x => x.ID).FirstOrDefault();
                        ReportParameter NvTieuThu = new ReportParameter("NvTieuThu", pcct_max.NVXuatHang);
                        rViewer.LocalReport.SetParameters(new ReportParameter[] { NvTieuThu });

                    }

                    var mcs = db.p_PhieuCan_MaChi_Lay(null, pc.ID).ToList();

                    if (mcs.Count >= 2)
                    {
                        ReportParameter MaChiA = new ReportParameter("MaChiA", mcs[0].MaChi);
                        ReportParameter MaChiB = new ReportParameter("MaChiB", mcs[1].MaChi);
                        ReportParameter NgayGioLayMau = new ReportParameter("NgayGioLayMau", 
                            mcs[1].NgayTao != null ? ((DateTime)mcs[1].NgayTao).ToString("dd/MM/yyyy HH:mm:ss") : null);
                        ReportParameter NgayLayMau = new ReportParameter("NgayLayMau",
                            mcs[1].NgayTao != null ? ((DateTime)mcs[1].NgayTao).ToString("dd/MM/yyyy") : null);
                        ReportParameter GioLayMau = new ReportParameter("GioLayMau",
                            mcs[1].NgayTao != null ? ((DateTime)mcs[1].NgayTao).ToString("HH:mm:ss") : null);
                        rViewer.LocalReport.SetParameters(new ReportParameter[] { MaChiA, MaChiB, NgayGioLayMau, NgayLayMau, GioLayMau });

                    }
                }

                rViewer.RefreshReport();
            }
            else if (ReportType == 3) // phiếu in nhiệt
            {
                pg.Margins.Top = 0;
                pg.Margins.Bottom = 0;
                pg.Margins.Left = 0;
                pg.Margins.Right = 0;
                rViewer.SetPageSettings(pg);
                var pc = db.p_PhieuCan_LayTheoID(null, PhieuCanID).FirstOrDefault();

                if (pc != null)
                {
                    rViewer.LocalReport.ReportEmbeddedResource = "XuatThuy.Reports.rptPhieuNho.rdlc";

                    var so = db.p_SALES_ORDERS_LayTheoID(null, pc.Order_ID).FirstOrDefault();
                    var pccts = db.p_PhieuCan_ChiTiet_Lay(null, pc.ID).ToList();

                    ReportParameter LuongXuat = new ReportParameter("LuongXuat", pc.TLXuat.ToString());
                    ReportParameter SoPhieu = new ReportParameter("SoPhieu", pc.ID.ToString());
                    rViewer.LocalReport.SetParameters(new ReportParameter[] { LuongXuat, SoPhieu });

                    if (so != null)
                    {
                        ReportParameter SoTau = new ReportParameter("SoTau", so.VEHICLE_CODE);
                        ReportParameter KhachHang = new ReportParameter("KhachHang", so.CUSTOMER_NAME);
                        ReportParameter LoaiHang = new ReportParameter("LoaiHang", so.TenSanPham);
                        ReportParameter SoHoaDon = new ReportParameter("SoHoaDon", so.DELIVERY_CODE);
                        ReportParameter LuongDat = new ReportParameter("LuongDat", so.ORDER_QUANTITY.ToString());
                        rViewer.LocalReport.SetParameters(new ReportParameter[] { SoTau, KhachHang, LoaiHang, SoHoaDon, LuongDat });
                    }

                    if (pccts.Count > 0)
                    {
                        var pcct_max = pccts.OrderByDescending(x => x.ID).FirstOrDefault();

                        ReportParameter NgayCan = new ReportParameter("NgayCan",
                            pcct_max.NgayThongKe != null ? ((DateTime)pcct_max.NgayThongKe).ToString("dd:MM:yyyy") : null);
                    }
                }

                rViewer.RefreshReport();
            }
        }
    }
}
