using OfficeOpenXml;
using PromotionParser.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PromotionParser.Workers
{
    public class Parser
    {
        private int _startRow = 0;
        private int _endRow = 0;
        private readonly List<RawPromotion> RawPromotionList = new List<RawPromotion>(); // 

        public List<IPromotion> Result { get; private set; }

        public void Parse(FileInfo _filename)
        {
            using (var p = new ExcelPackage(_filename))
            {
                try
                {
                    _startRow = p.Workbook.Worksheets["Pivot"].Dimension.Start.Row;
                    _endRow = p.Workbook.Worksheets["Pivot"].Dimension.End.Row;
                    for (int i = _startRow + 1; i <= _endRow; i++)
                    {
                        var cellContent = p.Workbook.Worksheets["Pivot"].Cells[i, 6].Text;
                        // First find the rows where the promotions start
                        if (cellContent != string.Empty)
                        {
                            RawPromotionList.Add(new RawPromotion()
                            {
                                StartRow = i,
                                EndRow = i - 1 // This will not work
                                // TODO: Add the real magic code here.
                            });
                        }
                    }
                    MessageBox.Show($"Number of promotions: {RawPromotionList.Count}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}