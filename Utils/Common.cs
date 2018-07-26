using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XuatThuy.Utils
{
    public class Common
    {
        public static bool READ_ONLY = false;


        public static string CnnString = @"Data Source=noibo2.hoangthach.vn;Initial Catalog=DEV;Persist Security Info=True;User ID=appl;Password=NSKpxIvJduS5qZksSEyMfCbohcfgqQ";
        //public static string CnnString = @"Data Source=nb11.hoangthach.vn,1435;Initial Catalog=DMZ;Persist Security Info=True;User ID=appl;Password=tpoQnFpqSsLqshQKagxvYsKD8yCPqO";
        //@"Data Source=nb11.hoangthach.vn\DMZ;Initial Catalog=DMZ;Persist Security Info=True;User ID=appl;Password=tpoQnFpqSsLqshQKagxvYsKD8yCPqO";
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}
