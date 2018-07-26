using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.Model
{
    public class SideMenu : ViewModelBase
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        readonly List<SideMenu> _children = new List<SideMenu>();

        public IList<SideMenu> Children
        {
            get { return _children; }
        }

    }
}
