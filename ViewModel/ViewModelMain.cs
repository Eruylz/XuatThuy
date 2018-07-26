using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using XuatThuy.Utils;
using XuatThuy.PLCs;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CommonServiceLocator;
using XuatThuy.Model;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using XuatThuy.Controls;
using MaterialDesignThemes.Wpf;

namespace XuatThuy.ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        public string Title { get; set; }
        public RelayCommand<SideMenuVM> MenuItemChangedCmd { get; set; }
        public RelayCommand<object> CmdDangXuat { get; set; }
        private PLC _PlcInstance;
        public PLC PlcInstance { get => _PlcInstance; set => _PlcInstance = value; }

        public ObservableCollection<CaXuat> CaXuats { get; set; }
        public ObservableCollection<SanPham> SanPhams { get; set; }
        public ObservableCollection<Area> Areas { get; set; }
        public ObservableCollection<NhaPhanPhoi> NhaPhanPhois { get; set; }
        public ObservableCollection<NhanVien> NVXuatHangs { get; set; }
        public ObservableCollection<NhanVien> NVBaoVes { get; set; }
        public ObservableCollection<PhuongThucVanChuyen> PTVCs { get; set; }

        //ObservableCollection<SideMenu> Menus;
        private CaXuat _selectedCaXuat;
        public CaXuat SelectedCaXuat
        {
            get
            {
                return _selectedCaXuat;
            }
            set
            {
                if (_selectedCaXuat != value)
                {
                    _selectedCaXuat = value;
                    RaisePropertyChanged("SelectedCaXuat");
                }
            }
        }

        private DateTime _ngayXuat;
        public DateTime NgayXuat
        {
            get
            {
                return _ngayXuat;
            }
            set
            {
                if (_ngayXuat != value)
                {
                    _ngayXuat = value;
                    RaisePropertyChanged("NgayXuat");
                }
            }
        }

        private object _SelectedControl;
        public object SelectedControl
        {
            get
            {
                return _SelectedControl;
            }
            set
            {
                if (_SelectedControl != value)
                {
                    
                    _SelectedControl = value;
                    RaisePropertyChanged("SelectedControl");
                }
            }
        }

        async void SetMainContent()
        {
            if (_selectedSideMenu.Index == 1)
            {
                SelectedControl = ServiceLocator.Current.GetInstance<TongQuanVM>();
            }
            else if (_selectedSideMenu.Index == 2)
            {
                SelectedControl = ServiceLocator.Current.GetInstance<ViewModelPhieuCan>();
                    //ServiceLocator.Current.GetInstance<ViewModelPhieuCan>().InitPhieuCan();
            }
            else if (_selectedSideMenu.Index == 3)
            {
                SelectedControl = ServiceLocator.Current.GetInstance<ThongKeVM>();
            }
            else if (_selectedSideMenu.Index == 5)
            {
                var view = new NotifyPassControl
                {
                    DataContext = new DialogNotifyVM("Nhập mật khẩu để vào cài đặt thông số cho két cân!")
                };

                //show the dialog
                var result = await DialogHost.Show(view, "RootDialog");

                if (result != null)
                {
                    var passwordBox = result as PasswordBox;

                    if (passwordBox.Password == "cntt1980")
                    {
                        SelectedControl = ServiceLocator.Current.GetInstance<CaiDatVM>();
                    }
                }
            }
            else if (_selectedSideMenu.Index == 6)
            {
                SelectedControl = ServiceLocator.Current.GetInstance<SoLoVM>();
            }
            else if (_selectedSideMenu.Index == 7)
            {
                SelectedControl = ServiceLocator.Current.GetInstance<PhieuChatLuongVM>();
            }
            else if (_selectedSideMenu.Index == 8)
            {
                SelectedControl = ServiceLocator.Current.GetInstance<DSPhieuCanVM>();
                ServiceLocator.Current.GetInstance<DSPhieuCanVM>().InitDSPhieuCan();
            }
        }

        SideMenuVM _selectedSideMenu;
        public SideMenuVM SelectedSideMenu
        {
            get { return _selectedSideMenu; }
            set
            {
                _selectedSideMenu = value;
                //RaisePropertyChanged("SelectedSideMenu");
                SetMainContent();
            }
        }

        void InitCollection()
        {
            XuatHangDataContext db = new XuatHangDataContext();

            CaXuats = new ObservableCollection<CaXuat>
            {
                new CaXuat(1, "1"),
                new CaXuat(2, "2"),
                new CaXuat(3, "3"),
            };

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
        }


        public ViewModelMain()
        {
            //Debug.WriteLine("ViewModelMain ======================");

            // khởi tạo plc
            PlcViewModel PlcViewModel = ServiceLocator.Current.GetInstance<PlcViewModel>();

            SelectedControl = ServiceLocator.Current.GetInstance<TongQuanVM>();
            MenuItemChangedCmd = new RelayCommand<SideMenuVM>(c => SelectedSideMenu = c);
            CmdDangXuat = new RelayCommand<object>(OnDangXuat);
            SideMenus = GetSideMenu();

            // tạo danh sách
            InitCollection();

            TreeViewItem a = new TreeViewItem();
            //Debug.WriteLine("khởi tạo bảng điện tử ======================");
            //bangDT = new BangDTu("10.68.51.250", 1024);
            //bangDT = new BangDTu("10.68.51.200", 1024);

            DispatcherTimer timer;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            ////LoginAsync();
        }

        private void OnDangXuat(object param)
        {
            XuatHangDataContext db = new XuatHangDataContext();
            int ret = db.p_TaiKhoan_DangXuat(Session.ID);
            Session.Cleanup();

            MainWindow mainWindow = param as MainWindow;
            Login wLogin = new Login();
            wLogin.Show();
            mainWindow.Close();
        }

        void OnStartup()
        {
            //bangDT.Line1_Text = KLCan1.ToString() + "/" + KLTong1.ToString();
            //bangDT.Line2_Text = KLCan2.ToString() + "/" + KLTong2.ToString();
            //bangDT.SetTextLine(4);
        }

        int Index = 0;

        private void timer_Tick(object sender, EventArgs e)
        {
            NgayXuat = DateTime.Now;

            int Hour = DateTime.Now.Hour;
            if (Hour >= 6 && Hour < 14)
            {
                SelectedCaXuat = CaXuats.Where(x => x.ID == 1).FirstOrDefault(); //CaXuat = 1;
            }
            else if (Hour >= 14 && Hour < 22)
            {
                SelectedCaXuat = CaXuats.Where(x => x.ID == 2).FirstOrDefault(); //CaXuat = 2;
            }
            else
            {
                SelectedCaXuat = CaXuats.Where(x => x.ID == 3).FirstOrDefault();
            }

            if (Index < 10)
            {
                Index++;

                if (Index == 2)
                {
                    OnStartup();
                    Index = 20;
                }
            }
        }

        public ObservableCollection<SideMenuVM> SideMenus { get; set; }

        ObservableCollection<SideMenuVM> GetSideMenu()
        {
            return new ObservableCollection<SideMenuVM>
            {
                new SideMenuVM { Index = 1, Name = "Tổng quan", Icon = "Home"},
                new SideMenuVM
                {
                    Index = 2,
                    Name = "Phiếu cân",
                    Icon = "TruckDelivery",
                    Children =
                    {
                        new SideMenuVM { Index = 8, Name = "Danh sách phiếu cân", Icon = "TruckDelivery"},
                    }
                },
                new SideMenuVM {Index = 3, Name = "Thống kê", Icon = "BookMultiple"},
                new SideMenuVM
                {
                    Index = 4,
                    Name = "Cài đặt",
                    Icon = "Settings",
                    Children =
                    {
                        new SideMenuVM {Index = 6, Name = "Lô xi măng", Icon = "Settings"},
                        new SideMenuVM {Index = 7, Name = "Phiếu chất lượng", Icon = "Settings"},
                        new SideMenuVM { Index = 5, Name = "Cài đặt chung", Icon = "Settings"},
                    }
                }
            };
        }
    }
}
