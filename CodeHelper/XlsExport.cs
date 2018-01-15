namespace CodeHelper
{
    using Syncfusion.XlsIO;
    using System.IO;
    using Syncfusion.Drawing;
    using Syncfusion.XlsIO.Implementation;
    using System.Linq;

    public class XlsExport
    {
        private IWorkbook _workbook;
        private IWorksheet _worksheet;
        private ExcelEngine _excelEngine;

        private const string abc = "ABCDEFGHIJKLMNOPQRSTWXYZ";

        public XlsExport()
        {
            Init();
        }

        private void Init()
        {
            _excelEngine = new ExcelEngine();
            IApplication application = _excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Excel2016;

            _workbook = application.Workbooks.Create(1);
        }

        public void Export(string[] paths, string[] values)
        {
            var worksheets = _workbook.Worksheets;
            _worksheet = worksheets[0];

            PaintValues(paths, values);

            for (int i = 0; i < paths.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    string text = File.ReadAllText(paths[i]);

                    Set(i + 1, j + 1, text.Contains(values[i]));
                }
            }

            Stream stream = new FileStream("Fields.xlsx", FileMode.Create, FileAccess.ReadWrite);
            _workbook.SaveAs(stream);

            Close();
        }

        private void Close()
        {
            _workbook.Close();
            _excelEngine.Dispose();
        }

        private void PaintValues(string[] paths, string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                _worksheet.Range[$"A{i + 2}"].ColumnWidth = values[i].Length * 2;
                _worksheet.Range[$"A{i + 2}"].Text = values[i];
            }

            for (int j = 0; j < paths.Length; j++)
            {
                var charCol = abc[j + 1];
                _worksheet.Range[$"{charCol}1"].ColumnWidth = paths[j].Length * 2;
                _worksheet.Range[$"{charCol}1"].Text = paths[j];
            }
        }

        private void Set(int iRow, int iCol, bool valid)
        {
            var charCol = abc[iCol];
            _worksheet.Range[$"{charCol}{iRow + 1}"].CellStyle.Color = valid ? Color.Green : Color.Red;
        }
    }
}
