using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuatThuy.PLCs;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using CommonServiceLocator;

namespace XuatThuy.ViewModel
{
    public class CaiDatVM : ViewModelBase
    {
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

        public RelayCommand CmdSaveConfig { get; set; }

        void SaveConfig()
        {
            PLC.Write_DB300_Min_ket1(Min_ket1);
            PLC.Write_DB300_Min_ket2(Min_ket2);
            PLC.Write_DB300_Max_ket1(Max_ket1);
            PLC.Write_DB300_Max_ket2(Max_ket2);
        }

        public CaiDatVM()
        {
            CmdSaveConfig = new RelayCommand(() => SaveConfig());

            //PlcViewModel plcVM = ServiceLocator.Current.GetInstance<PlcViewModel>();
            //System.Threading.Thread.Sleep(1000);
            Min_ket1 = PLC.DS_Tag_DB300.Min_ket1;////plcVM.Min_ket1;
            Max_ket1 = PLC.DS_Tag_DB300.Max_ket1;// plcVM.Max_ket1;
            Min_ket2 = PLC.DS_Tag_DB300.Min_ket2;
            Max_ket2 = PLC.DS_Tag_DB300.Max_ket2;
        }
    }
}
