using System.Linq;

namespace CodeHelper
{
    using Syncfusion;
    using System;
    using Syncfusion.XlsIO;
    using System.IO;
    using Syncfusion.Drawing;
    using System.Collections.Generic;
    using Syncfusion.XlsIO.Implementation;

    public class XlsExport
    {
        public IWorkbook Workbook { get; set; }
        public IWorksheet Worksheet { get; set; }

        public XlsExport()
        {
        }

        public XlsExport(IWorkbook _workbook)
        {
            this.Workbook = _workbook;
        }
        public void Initialize(string[] paths, string[] values)
        {
            var worksheets = Workbook.Worksheets;
            Worksheet = worksheets[0];

            for (int i = 0; i < paths.ToList().Count; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    string text = File.ReadAllText(paths[i]);

                    Set(i, j, text.Contains(values[i]));
                }
            }
        }

        private void Set(int iRow, int iCol, bool valid)
        {
            Worksheet[iRow, iCol].CellStyle.Color = valid ? Color.Green : Color.Red;
        }
    }
}
