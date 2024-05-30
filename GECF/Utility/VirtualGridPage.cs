using System;
using System.ComponentModel;
using System.Data;

namespace GECF.Utility
{
	public static class VirtualGridPage
	{
        public static DataTable ToDataTable<NDatumV>(this IList<NDatumV> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(NDatumV));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (NDatumV item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}

