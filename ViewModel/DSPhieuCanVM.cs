using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuatThuy.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Diagnostics;
using System.Windows;
using XuatThuy.Controls;
using MaterialDesignThemes.Wpf;
using CommonServiceLocator;

namespace XuatThuy.ViewModel
{
    public class DSPhieuCanVM : ViewModelBase
    {
        XuatHangDataContext db = new XuatHangDataContext();
        public ObservableCollection<PhieuCan> PhieuCans { get; set; }
        public RelayCommand CmdThongKe { get; set; }
        public RelayCommand CmdInPhieuLayMau { get; set; }
        public RelayCommand CmdInA4 { get; set; }
        public RelayCommand CmdInA6 { get; set; }
        public RelayCommand CmdInNhiet { get; set; }
        public RelayCommand CmdNhapThucXuat { get; set; }
        public RelayCommand CmdNhapMaChi { get; set; }

        string _maChiA;
        public string MaChiA
        {
            get => _maChiA;
            set
            {
                if (_maChiA != value)
                {
                    _maChiA = value;
                    RaisePropertyChanged("MaChiA");
                }
            }
        }

        string _maChiB;
        public string MaChiB
        {
            get => _maChiB;
            set
            {
                if (_maChiB != value)
                {
                    _maChiB = value;
                    RaisePropertyChanged("MaChiB");
                }
            }
        }

        PhieuCan _selectedPhieuCan;
        public PhieuCan SelectedPhieuCan
        {
            get => _selectedPhieuCan;
            set
            {
                _selectedPhieuCan = value;
            }
        }

        public void InitDSPhieuCan()
        {
            PhieuCans.Clear();
            var phieucans = db.p_PhieuCan_LayXiMangRoiThuy(null).ToList();

            foreach (var pc in phieucans)
            {
                PhieuCans.Add(new PhieuCan(pc.ID, pc.Order_ID, pc.BienSo1,
                                            pc.BienSo2, pc.ChuPhuongTien,
                                            pc.HinhThucCan, pc.OFFLINE_FLAG,
                                            pc.TLXuat, pc.TLThucXuat, pc.TLDangKy, pc.LoaiGiaoDich,
                                            pc.SoChi, pc.TrangThai, pc.SoPhieuCu, pc.NgayDongBo,
                                            pc.TrangThaiDongBo, pc.SoLoID, pc.PhieuChatLuongID,
                                            pc.DELIVERY_CODE));
            }
        }

        public DSPhieuCanVM()
        {
            PhieuCans = new ObservableCollection<PhieuCan>();

            CmdInPhieuLayMau = new RelayCommand(InPhieuLayMau);
            CmdThongKe = new RelayCommand(ThongKe);
            CmdInA4 = new RelayCommand(InA4);
            CmdInA6 = new RelayCommand(InA6);
            CmdInNhiet = new RelayCommand(InNhiet);
            CmdNhapMaChi = new RelayCommand(OnNhapMaChi);
            CmdNhapThucXuat = new RelayCommand(OnNhapThucXuat);
        }

        async void OnNhapThucXuat()
        {
            ThucXuatVM thucXuatVM = new ThucXuatVM("Nhập trọng lượng thực xuất của phiếu cân", 
                    SelectedPhieuCan.TLThucXuat == null ? null : SelectedPhieuCan.TLThucXuat.ToString());
            var view = new ThucXuatControl
            {
                DataContext = thucXuatVM
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingNhapThucXuatEventHandler);
        }

        private void ClosingNhapThucXuatEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            DialogHost dh = sender as DialogHost;
            ThucXuatControl view = dh.DialogContent as ThucXuatControl;
            ThucXuatVM thucXuatVM = view.DataContext as ThucXuatVM;

            Debug.WriteLine("SAMPLE 1: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));
            Debug.WriteLine("sender == " + sender.GetType());
            Debug.WriteLine("thucXuatVM.ThucXuat == " + thucXuatVM.ThucXuat);

            if ((bool)eventArgs.Parameter)
            {
                double ThucXuat;

                if (double.TryParse(thucXuatVM.ThucXuat, out ThucXuat))
                {
                    int ret = db.p_PhieuCan_ThemThucXuat(null, SelectedPhieuCan.ID, ThucXuat);

                    if (ret == 0)
                    {
                        SelectedPhieuCan.TLThucXuat = ThucXuat;
                    }
                }
                else
                {
                    eventArgs.Cancel();
                }
            }
        }

        async void OnNhapMaChi()
        {
            int ret;

            MaChiVM maChiVM = new MaChiVM(SelectedPhieuCan.ID);
            var view = new MaChiControl
            {
                DataContext = maChiVM
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog");

            if ((bool)result) // lưu
            {
                var machis = db.p_PhieuCan_MaChi_Lay(null, SelectedPhieuCan.ID).ToList();

                // cập nhật
                if (machis.Count >= 2)
                {
                    ret = db.p_PhieuCan_MaChi_Sua(null, machis[0].ID, maChiVM.MaChiA);

                    if (ret == 0)
                    {
                        ret = db.p_PhieuCan_MaChi_Sua(null, machis[1].ID, maChiVM.MaChiB);
                    }
                }
                else // thêm mới
                {
                    long? MaChiID = null;
                    ret = db.p_PhieuCan_MaChi_Them(null, ref MaChiID, SelectedPhieuCan.ID, maChiVM.MaChiA, string.Empty);

                    if (ret == 0)
                    {
                        ret = db.p_PhieuCan_MaChi_Them(null, ref MaChiID, SelectedPhieuCan.ID, maChiVM.MaChiB, string.Empty);

                        if (ret == 0)
                        {
                            SelectedPhieuCan.SoChi = 2;
                        }
                    }
                } 
            }

        }

        private void InPhieuLayMau()
        {
            if (SelectedPhieuCan != null)
            {
                Window window = new Window();
                window.Title = "In phiếu lấy mẫu";
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = SelectedPhieuCan.ID;
                rvControl.ReportType = 5;

                window.Content = rvControl;
                window.ShowDialog(); 
            }
        }

        private void ThongKe()
        {
            if (SelectedPhieuCan != null)
            {
                Window window = new Window();
                window.Title = "Thống kê phiếu cân";
                window.Width = 900;
                //CRControl cRControl = new CRControl();
                //cRControl.PhieuCanID = SelectedPhieuCan.ID;
                //cRControl.ReportType = 1;

                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = SelectedPhieuCan.ID;
                rvControl.ReportType = 1;

                window.Content = rvControl;// cRControl;
                window.ShowDialog(); 
            }
        }

        private void InA4()
        {
            if (SelectedPhieuCan != null)
            {
                Window window = new Window();
                //window.WindowState = WindowState.Maximized;
                window.Width = 900;
                window.Title = "In phiếu xuất A4";
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = SelectedPhieuCan.ID;
                rvControl.ReportType = 4;

                window.Content = rvControl;
                window.ShowDialog(); 
            }
        }

        private void InNhiet()
        {
            if (SelectedPhieuCan != null)
            {
                Window window = new Window();
                //window.WindowState = WindowState.Maximized;
                window.Width =500;
                window.Title = "Phiếu in nhiệt";
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = SelectedPhieuCan.ID;
                rvControl.ReportType = 3;

                window.Content = rvControl;
                window.ShowDialog(); 
            }
        }

        private void InA6()
        {
            if (SelectedPhieuCan != null)
            {
                Window window = new Window();
                //window.WindowState = WindowState.Maximized;
                window.Title = "In phiếu xuất A6";
                window.Width = 900;
                window.Height = 700;
                //CRControl cRControl = new CRControl();
                //cRControl.PhieuCanID = SelectedPhieuCan.ID;
                //cRControl.ReportType = 2;
                RvControl rvControl = new RvControl();
                rvControl.PhieuCanID = SelectedPhieuCan.ID;
                rvControl.ReportType = 2;

                window.Content = rvControl;
                window.ShowDialog();
            }
        }
    }
}
