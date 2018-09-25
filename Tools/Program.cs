using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static IWorkbook BuildWorkbook(DataSet ds, string sPath = "", string sTitle = "", int rowBegin = 0, int colBegin = 0, bool bSummary = true)
        {
            IWorkbook book = null;

            using (FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.ReadWrite))
            {
                book = WorkbookFactory.Create(fs);
            }

            foreach (DataTable dt in ds.Tables)
            {
                ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? book.GetSheet("Sheet1") : book.GetSheet(dt.TableName);

                sheet = sheet != null ? sheet : (string.IsNullOrEmpty(dt.TableName) ? book.CreateSheet("Sheet1") : book.CreateSheet(dt.TableName));

                int nRowCount = dt.Rows.Count;
                int nColCount = dt.Columns.Count;

                IRow header = sheet.GetRow(0);

                if (header == null)
                {
                    header = sheet.CreateRow(0);
                    header.CreateCell(0);
                }

                header.GetCell(0).SetCellValue(sTitle);

                //Data Rows
                for (int i = 0; i < nRowCount; i++)
                {
                    IRow drow = sheet.CreateRow(i + rowBegin);
                    for (int j = 0; j < nColCount; j++)
                    {
                        var celltype = GetValueType(dt.Columns[j].DataType);
                        ICell cell = drow.CreateCell(j + colBegin, celltype);

                        if (bSummary && i == nRowCount - 1 && j == 0)
                        {
                            cell.SetCellValue("汇总");
                            continue;
                        }

                        switch (celltype)
                        {
                            case CellType.Numeric: cell.SetCellValue(Math.Round(Convert.ToDouble(dt.Rows[i][j]), 4)); break;
                            case CellType.String: cell.SetCellValue(dt.Rows[i][j].ToString() == "0001.1.1 0:00:00" ? string.Empty : dt.Rows[i][j].ToString()); break;
                            default: cell.SetCellValue(dt.Rows[i][j].ToString() == "0001.1.1 0:00:00" ? string.Empty : dt.Rows[i][j].ToString()); break;
                        }
                        if (dt.Columns[j].ColumnName.ToLower().Contains("rate"))
                        {
                            ICellStyle style = book.CreateCellStyle();
                            style.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                            cell.CellStyle = style;
                        }
                    }
                }
                //自动列宽
                for (int i = 0; i <= dt.Columns.Count; i++)
                    sheet.AutoSizeColumn(i, true);
            }
            return book;
        }
    }

    public class OffOnlineAdvertis
    {
        public BaseInfo BaseInfo { get; set; }
        public AdvertisingData AdvertisingData { get; set; }
        public HourlyAnalysis HourlyAnalysis { get; set; }
        public AreaAnalysis AreaAnalysis { get; set; }
    }

    public class BaseInfo
    {
        public long AdsId { get; set; }

        public long EngineAdsId { get; set; }

        public long DealId { get; set; }

        public DateTime SchedulingStartDate { get; set; }

        public DateTime SchedulingEndDate { get; set; }

    }

    public class AdvertisingData {

        public long CreateId { get; set; }

        public DateTime Date { get; set; }

        public long ExposureNum { get; set; }

        public long ClickNum { get; set; }

        public long UV { get; set; }
    }

    public class HourlyAnalysis {

        public DateTime Date { get; set; }

        public int Hour { get; set; }

        public long ExposureNum { get; set; }

        public long ClickNum { get; set; }

        public long UV { get; set; }

    }

    public class AreaAnalysis
    {

        public string CityName { get; set; }

        public DateTime Date { get; set; }

        public long ExposureNum { get; set; }

        public long ClickNum { get; set; }

        public long UV { get; set; }

    }

}
