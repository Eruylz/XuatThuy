using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuatThuy.Model
{
    public class Session
    {
        public static Guid? ID { get; set; }
        public static string UserName { get; set; }
        public static string FullName { get; set; }

        public static void Cleanup()
        {
            ID = null;
            UserName = null;
            FullName = null;
        }
    }
}
