using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace news_FE.library
{
    public static class Mystring
    {
        public static String ToStringNospace(this String s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\";,]", "" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static String ToSlug(this String s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\";,]", "-" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }

        public static String GetFileExtension(this String s)
        {
            string[] Name_extension = s.Split('.');
            int countarray = Name_extension.Count();
            s = Name_extension[countarray - 1];
            return s;
        }
    }
}