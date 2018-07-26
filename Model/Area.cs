using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XuatThuy.Model
{
    public class Area : INotifyPropertyChanged
    {
        long _ID;
        public long ID { get => _ID; set => _ID = value; }

        int _AreaID;
        public int AreaID
        {
            get { return _AreaID; }
            set
            {
                if (_AreaID != value)
                {
                    _AreaID = value;
                    RaisePropertyChanged("AreaID");
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public Area(long iD, int areaID, string name)
        {
            ID = iD;
            AreaID = areaID;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
    }
}
