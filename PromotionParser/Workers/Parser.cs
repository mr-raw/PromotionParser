using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using PromotionParser.Data;
using PromotionParser.Data.PromotionTypes;
using static System.Text.RegularExpressions.Regex;

namespace PromotionParser.Workers
{
    public class Parser
    {
        private readonly List<PromotionStartStopPoints> _rawPromotionList = new List<PromotionStartStopPoints>(); // The RawPromotion contains the start line and end line of the promotions.
        public readonly List<IPromotion> Result = new List<IPromotion>();

        public void Parse(FileInfo filename) // Consider to move the FileInfo to a constructor.
        {
            using (var p = new ExcelPackage(filename))
            {
                try
                {
                    var worksheet = p.Workbook.Worksheets["Pivot"]; 
                    InsertStartRow(worksheet); // Set the StartRow for all the promotions.
                    InsertEndRow(worksheet); // Set the EndRow for all the promotion.
                    AddDataToList(p);
                }
                catch (Exception ex)
                {
                    throw new Exception("Det oppstod en feil ved analysering av Excel fil", ex);
                }
            }
        }

        private void AddDataToList(ExcelPackage package)
        {
            if (_rawPromotionList.Count == 0) throw new Exception("Listen med rådata er tom, kan ikke fortsette");
            foreach (var promotion in _rawPromotionList)
            {
                var promo = ReturnCorrectPromotion(package, promotion); // First generate the Promotion, then add all the PromotionItems

                for (var i = promotion.StartRow; i <= promotion.EndRow; i++) // Add all the promotion items to the promotion.
                {
                    var date = DateTime.MinValue;
                    double discount;
                    try
                    {
                        if (!DateTime.TryParse(GetCellData(package, i, 17), out date))
                        {
                            date = DateTime.MinValue;
                        }

                        if (!double.TryParse(GetCellData(package, i, 13).Replace("%", String.Empty).Trim(), out discount))
                        {
                            discount = double.MinValue;
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception($"Kunne ikke dekode dato. {date.ToString(CultureInfo.InvariantCulture)}");
                    }
                    
                    var promoItem = new PromotionItem
                    {
                        Vendor = promo.Vendor,
                        Sel = GetCellData(package, i, 8),
                        ProductText = GetCellData(package, i, 9),
                        Epd = GetCellData(package, i, 11), // Column 7 should not be used I think.
                        Aip = GetCellData(package, i, 10),
                        Type = GetCellData(package, i, 12),
                        Discount = discount,
                        HowToOrder = GetCellData(package, i, 15),
                        CancelDeadline = date,
                        Placement = GetCellData(package, i, 18),
                        Comments = GetCellData(package, i, 19),
                        ResponsiblePerson = GetCellData(package, i, 20),
                        AddedWeek = GetCellData(package, i, 21)
                    };

                    promo.PromotionItemsList.Add(promoItem);
                }
                Result.Add(promo);
            }
            Debug.WriteLine($"Antall aktiviteter lagret: {Result.Count}");
        }

        private static IPromotion ReturnCorrectPromotion(ExcelPackage package, PromotionStartStopPoints line) // Should maybe change the name of this method?
        {
            var rawData = string.Empty; // Initialize here so that the variable can be read in the catch scope.

            try
            {
                rawData = GetCellData(package, line.StartRow, 6);
                var (fromWeek, toWeek, vendor) = SplitRawData();
                
                DateTime date;
                double discount;
                try
                {
                    if (!DateTime.TryParse(GetCellData(package, line.StartRow, 17), out date))
                    {
                        date = DateTime.MinValue; // Fallback is 01.01.0001 (I think)
                    }

                    if (!double.TryParse(GetCellData(package, line.StartRow, 13).Replace("%", string.Empty).Trim(), out discount))
                    {
                        discount = 0.0; // Fallback is 0.0
                    }
                }
                catch (Exception)
                {
                    throw new Exception($"Det oppstod en feil ved analysering av inndata.");
                }

                if (true) // Insert logic here to choose the correct PromotionType. Maybe switch statement would be better?
                {
                    
                }
                return new FallBackPromotion // Test. We have to generate the right promotion here at a later time.
                {
                    PromotionItemsList = new List<PromotionItem>(), // Initializes the list of PromotionItem
                    FromWeek = fromWeek, // Fetched from the local method
                    ToWeek = toWeek, // Fetched from the local method
                    Vendor = vendor, // Fetched from the local method
                    Sel = GetCellData(package, line.StartRow, 8),
                    Type = GetCellData(package, line.StartRow, 12),
                    Discount = discount, // discount is parsed from column 13
                    HowToOrder = GetCellData(package, line.StartRow, 15),
                    Source = GetCellData(package, line.StartRow, 16),
                    CancelDeadline = date, // The date is parsed from column 17
                    Placement = GetCellData(package, line.StartRow, 18),
                    Comments = GetCellData(package, line.StartRow, 19),
                    ResponsiblePerson = GetCellData(package, line.StartRow, 20),
                    AddedWeek = GetCellData(package, line.StartRow, 21)
                };
            
                (string FromWeek, string ToWeek, string Vendor) SplitRawData() // This is a local function. How cool is that?
                {
                    const RegexOptions options = RegexOptions.Multiline;

                    var twoWeeksMatches = Matches(rawData, @"Uke (.*?)-(.*?) - (.*)", options);
                    if (twoWeeksMatches.Count > 0)
                        return (twoWeeksMatches[0].Groups[1].Value, twoWeeksMatches[0].Groups[2].Value,
                            twoWeeksMatches[0].Groups[3].Value);
                    
                    var oneWeeksMatches = Matches(rawData, "Uke (.*?) - (.*)", options); // Fallback if there is only one week. Different Regex Pattern.
                    return (oneWeeksMatches[0].Groups[1].Value, oneWeeksMatches[0].Groups[1].Value, oneWeeksMatches[0].Groups[2].Value);
                }
            }
            catch (Exception)
            {
                throw new Exception($"Kunne ikke kjøre oppdeling av tekst. \n\rRÅDATA: {rawData}");
            }
        }

        private static string GetCellData(ExcelPackage p, int row, int column)
        {
            return p.Workbook.Worksheets["Pivot"].Cells[row, column].Text;
        }

        private void InsertStartRow(ExcelWorksheet worksheet)
        {
            var startRow = worksheet.Dimension.Start.Row + 1;
            var endRow = worksheet.Dimension.End.Row - 1;
            
            for (var i = startRow; i <= endRow; i++)
            {
                var cellContent = worksheet.Cells[i, 6].Text;
                // First find the rows where the promotions start

                if (cellContent != string.Empty)
                {
                    _rawPromotionList.Add(new PromotionStartStopPoints
                    {
                        StartRow = i // EndRow is set in the second pass
                    });
                }
            }
        }
        
        private void InsertEndRow(ExcelWorksheet worksheet)
        {
            for (var promo = 0; promo <= _rawPromotionList.Count - 1; promo++)
            {
                try
                {
                    _rawPromotionList[promo].EndRow = _rawPromotionList[promo + 1].StartRow - 2; // Go to the next promo and subtract 2 lines.
                    // Debug.WriteLine($"Promotion #{promo}, StartRow: {_rawPromotionList[promo].StartRow}, EndRow: {_rawPromotionList[promo].EndRow}");
                }
                catch (ArgumentOutOfRangeException) // TODO: Don't throw an error message, just insert the last line in the document. This is a bit hacky, maybe change this at a later time?
                {
                    _rawPromotionList[promo].EndRow = worksheet.Dimension.End.Row - 1;
                    // Debug.WriteLine($"Promotion #{promo}, StartRow: {_rawPromotionList[promo].StartRow}, EndRow: {_rawPromotionList[promo].EndRow}");
                }
            }
        }
    }
}