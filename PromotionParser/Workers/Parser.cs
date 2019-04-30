using OfficeOpenXml;
using PromotionParser.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PromotionParser.Workers
{
    public class Parser
    {
        private int _startRow;
        private int _endRow;
        private readonly List<RawPromotion> _rawPromotionList = new List<RawPromotion>(); 

        public List<IPromotion> Result = new List<IPromotion>();

        public void Parse(FileInfo filename)
        {
            using (var p = new ExcelPackage(filename))
            {
                try
                {
                    _startRow = p.Workbook.Worksheets["Pivot"].Dimension.Start.Row;
                    _endRow = p.Workbook.Worksheets["Pivot"].Dimension.End.Row;
                    for (var i = _startRow + 1; i <= _endRow; i++)
                    {
                        var cellContent = p.Workbook.Worksheets["Pivot"].Cells[i, 6].Text;
                        // First find the rows where the promotions start
                        if (cellContent != string.Empty)
                        {
                            _rawPromotionList.Add(new RawPromotion()
                            {
                                StartRow = i,
                                EndRow = i - 1 // This will not work
                                // TODO: Add the real magic code here.
                            });
                        }
                    }
                    MessageBox.Show($"Number of promotions: {_rawPromotionList.Count}"); // This should be removed

                    Result = new List<IPromotion> {new TestPromotion {Discount = 10.0}}; // This should of course be removed as well. 
                }
                catch (Exception ex)
                {
                    throw new Exception("Det oppstod en feil ved parsing av Excel fil", ex);
                }
            }
        }
    }
}