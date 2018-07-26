using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using XuatThuy.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight.Command;

namespace XuatThuy.ViewModel
{
    public class SoLoVM : ViewModelBase
    {
        public ObservableCollection<SoLo> SoLos { get; set; }
        public RelayCommand<SoLo> CmdEditSoLo { get; set; }
        public RelayCommand<SoLo> CmdDeleteSoLo { get; set; }
        public RelayCommand CmdAddSoLo { get; set; }
        XuatHangDataContext db = new XuatHangDataContext();

        string _tenMoi;
        public string TenMoi
        {
            get { return _tenMoi; }
            set
            {
                _tenMoi = value;
                RaisePropertyChanged("TenMoi");
            }
        }

        SoLo _selectedSoLo;
        public SoLo SelectedSoLo
        {
            get { return _selectedSoLo; }
            set
            {
                _selectedSoLo = value;
                RaisePropertyChanged("SelectedSoLo");
            }
        }

        void EditSoLo(SoLo soLo)
        {
            int ret = db.p_SoloXMRoi_Sua(null, _selectedSoLo.ID, _selectedSoLo.Ten);
        }

        void DeleteSoLo(SoLo soLo)
        {
            int ret = db.p_SoloXMRoi_Xoa(null, _selectedSoLo.ID);

            if (ret == 0)
            {
                SoLos.Remove(SoLos.Where(x => x.ID == _selectedSoLo.ID).FirstOrDefault());
            }
        }

        void AddSoLo()
        {
            long? SoLoID = 0;
            int ret = db.p_SoloXMRoi_Them(null, ref SoLoID, TenMoi);

            if (ret == 0)
            {
                SoLos.Insert(0, new SoLo((long)SoLoID, TenMoi, true));
                TenMoi = string.Empty;
            }
        }

        //public Action<SoLo> EditSoLo = new Action<SoLo>(x => x.Ten = "aaaa");

        public void LoadSoLos()
        {
            SoLos.Clear();
            var solos = db.p_SoloXMRoi_Lay(null).ToList();

            foreach (var solo in solos)
            {
                SoLos.Add(new SoLo(solo.ID, solo.Ten, solo.TrangThai == 10));
            }
        }

        public SoLoVM()
        {
            //RelayCommand 
            SoLos = new ObservableCollection<SoLo>();

            CmdEditSoLo = new RelayCommand<SoLo>(EditSoLo);
            CmdDeleteSoLo = new RelayCommand<SoLo>(DeleteSoLo);
            CmdAddSoLo = new RelayCommand(AddSoLo);

            LoadSoLos();
        }

    }
}
