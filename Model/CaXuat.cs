using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuatThuy.Model
{
    public class CaXuat
    {
        public byte ID { get; set; }
        public string Ca { get; set; }

        public CaXuat(byte iD, string ca)
        {
            ID = iD;
            Ca = ca;
        }
    }
}
