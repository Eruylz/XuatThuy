using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.ViewModel
{
    public class SideMenuVM : ViewModelBase
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        bool _isSelected;
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.RaisePropertyChanged("IsSelected");
                }
            }
        }

        readonly ObservableCollection<SideMenuVM> _children = new ObservableCollection<SideMenuVM>();

        public ObservableCollection<SideMenuVM> Children
        {
            get { return _children; }
        }
    }
}
