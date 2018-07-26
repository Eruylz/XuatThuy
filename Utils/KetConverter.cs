using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XuatThuy.Utils
{
    public class KetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte? bKet= 0;

            if (value is byte?)
            {
                bKet = (byte?)value;
            }

            if (bKet == 1)
            {
                return "B03";
            }
            else if (bKet == 2)
            {
                return "B04";
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
