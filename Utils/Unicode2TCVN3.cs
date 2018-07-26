using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuatThuy.Utils
{
    public class Unicode2TCVN3
    {
        public static char[] UNI = { 'à', 'á', 'ả', 'ã', 'ạ', 'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ', 'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ', 'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ', 'ì', 'í', 'ỉ', 'ĩ', 'ị', 'ò', 'ó', 'ỏ', 'õ', 'ọ', 'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ', 'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ', 'ù', 'ú', 'ủ', 'ũ', 'ụ', 'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự', 'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ', 'Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'};
        public static char[] TCVN3 = { 'µ', '¸', '¶', '·', '¹', '¨', '»', '¾', '¼', '½', 'Æ', '©', 'Ç', 'Ê', 'È', 'É', 'Ë', '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ', 'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö', '×', 'Ý', 'Ø', 'Ü', 'Þ', 'ß', 'ã', 'á', 'â', 'ä', '«', 'å', 'è', 'æ', 'ç', 'é', '¬', 'ê', 'í', 'ë', 'ì', 'î', 'ï', 'ó', 'ñ', 'ò', 'ô', '­', 'õ', 'ø', 'ö', '÷', 'ù', 'ú', 'ý', 'û', 'ü', 'þ', '¡', '¢', '§', '£', '¤', '¥', '¦' };

        public static string Convert(string input)
        {
            //string res;
            StringBuilder sb = new StringBuilder(input);

            for (int i = 0; i < input.Length; i++)
            {
                int pos = Array.IndexOf(UNI, input[i]);

                if (pos >= 0)
                {
                    sb[i] = TCVN3[pos];
                }

            }


            return sb.ToString();
        }
    }
}
