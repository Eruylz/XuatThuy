using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace XuatThuy.Utils
{
    public class TrangThaiPhieuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte? bTrangThai = 0;

            if (value is byte?)
            {
                bTrangThai = (byte?)value;
            }

            if (bTrangThai == 10)
            {
                return "Đang xuất";
            }
            else
            {
                return "Kết thúc";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
