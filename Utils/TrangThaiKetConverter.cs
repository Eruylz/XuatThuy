using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XuatThuy.Utils
{
    public class TrangThaiKetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte bTrangThai = 0;

            if (value is byte)
            {
                bTrangThai = (byte)value;
            }

            if (bTrangThai == 1)
            {
                return "Cấp liệu két";
            }
            else if (bTrangThai == 2)
            {
                return "Cân hàng";
            }
            else if (bTrangThai == 3)
            {
                return "Xả hàng";
            }
            else if (bTrangThai == 4)
            {
                return "Cân bì";
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
