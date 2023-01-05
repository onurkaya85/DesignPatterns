using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPattern.Web.Commands
{
    public class ExcelFile<T> 
    {
        public readonly List<T> _list;

        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public MemoryStream Create()
        {
            var workbook = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable());
            workbook.Worksheets.Add(ds);
            var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms;
        }

        private DataTable GetTable()
        {
            var table = new DataTable();
            var type = typeof(T);
            type.GetProperties().ToList().ForEach(c =>
            {
                table.Columns.Add(c.Name, c.PropertyType);
            });

            _list.ForEach(x =>
            {
                var values = typeof(T).GetProperties().Select(pi => pi.GetValue(x, null)).ToArray();
                table.Rows.Add(values);
            });

            return table;
        }
    }
}
