using System;
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

namespace XuatThuy.ViewModel
{
    public class TongQuanVM : ViewModelBase
    {
        public ObservableCollection<ChiTietCan> ChiTietCans1 { get; set; }
        public ObservableCollection<ChiTietCan> ChiTietCans2 { get; set; }
        XuatHangDataContext db = new XuatHangDataContext();

        public PlcViewModel PlcViewModel => ServiceLocator.Current.GetInstance<PlcViewModel>();      

        bool _ChamMaxKet2;
        public bool ChamMaxKet2
        {
            get { return _ChamMaxKet2; }
            set
            {
                if (_ChamMaxKet2 != value)
                {
                    _ChamMaxKet2 = value;
                    RaisePropertyChanged("ChamMaxKet2");
                }
            }
        }

        bool _ChamMaxKet1;
        public bool ChamMaxKet1
        {
            get { return _ChamMaxKet1; }
            set
            {
                if (_ChamMaxKet1 != value)
                {
                    _ChamMaxKet1 = value;
                    RaisePropertyChanged("ChamMaxKet1");
                }
            }
        }

        bool _ChamMinKet2;
        public bool ChamMinKet2
        {
            get { return _ChamMinKet2; }
            set
            {
                if (_ChamMinKet2 != value)
                {
                    _ChamMinKet2 = value;
                    RaisePropertyChanged("ChamMinKet2");
                }
            }
        }

        bool _ChamMinKet1;
        public bool ChamMinKet1
        {
            get { return _ChamMinKet1; }
            set
            {
                if (_ChamMinKet1 != value)
                {
                    _ChamMinKet1 = value;
                    RaisePropertyChanged("ChamMinKet1");
                }
            }
        }

        public int _LoadData { get; private set; }

        //public RelayCommand CmdShowPhieuCan { get; set; }


        public TongQuanVM()
        {
            // khoi tao PLC
            ChiTietCans1 = new ObservableCollection<ChiTietCan>();
            ChiTietCans2 = new ObservableCollection<ChiTietCan>();


            DispatcherTimer timer;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void LoadChiTietCan()
        {
            ChiTietCans1.Clear();
            ChiTietCans2.Clear();

            var chiTietCans = db.p_PhieuCan_ChiTiet_Can_LayTheoPC(null, (long?)PlcViewModel.Sophieu).OrderByDescending(x => x.ID);

            foreach (var ctc in chiTietCans)
            {
                //Debug.WriteLine(" Ket = " + ctc.Ket + ". Stt = " + ctc.STT);
                if (ctc.Ket == 1)
                {
                    ChiTietCans1.Add(new ChiTietCan(ctc.ID, ctc.PhieuCanChiTietID, ctc.STT, ctc.Ket, ctc.TLCan1, ctc.TLCan2,
                                     ctc.TLHang, ctc.ThoiDiemCan1, ctc.ThoiDiemCan2, ctc.TrangThai));
                }
                else
                {
                    ChiTietCans2.Add(new ChiTietCan(ctc.ID, ctc.PhieuCanChiTietID, ctc.STT, ctc.Ket, ctc.TLCan1, ctc.TLCan2,
                                     ctc.TLHang, ctc.ThoiDiemCan1, ctc.ThoiDiemCan2, ctc.TrangThai));
                }
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            ChamMaxKet1 = PlcViewModel.KLCan1 < PlcViewModel.Max_ket1;
            ChamMaxKet2 = PlcViewModel.KLCan2 < PlcViewModel.Max_ket2;
            ChamMinKet1 = PlcViewModel.KLCan1 > PlcViewModel.Min_ket1;
            ChamMinKet2 = PlcViewModel.KLCan2 > PlcViewModel.Min_ket2;
        }
    }
}
