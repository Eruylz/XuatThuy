using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuatThuy.Utils
{
    class LogUtils
    {
        private static List<string> buffer = new List<string>();

        // Methods
        public static void PrintLog(string message)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"logs\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                StreamWriter writer = new StreamWriter(path + DateTime.Now.ToString("yy_MM_dd") + ".txt", true, Encoding.UTF8);

                foreach (string str3 in buffer)
                {
                    writer.WriteLine(str3);
                }

                buffer.Clear();
                writer.WriteLine(DateTime.Now.ToString("HH:mm") + ": " + message);
                writer.Close();
            }
            catch (Exception)
            {
                buffer.Add(DateTime.Now.ToString("HH:mm") + ": " + message);
            }
        }

        [Conditional("DEBUG")]
        public static void PrintDebug(string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
