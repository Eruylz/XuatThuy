using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.ViewModel
{
    public class ThucXuatVM : ViewModelBase
    {
        string _Title;
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                RaisePropertyChanged("Title");
            }
        }

        string _ThucXuat;
        public string ThucXuat
        {
            get => _ThucXuat;
            set
            {
                _ThucXuat = value;
                RaisePropertyChanged("ThucXuat");
            }
        }

        public ThucXuatVM(string title, string thucXuat)
        {
            Title = title;
            ThucXuat = thucXuat;
        }
    }
}
