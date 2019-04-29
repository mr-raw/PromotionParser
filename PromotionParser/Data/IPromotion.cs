using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace PromotionParser.Data
{
    public interface IPromotion
    {
        ExcelRow RawData{ get; set; }
        List<PromotionItem> PromotionItemsList { get; set; }
        string Vendor { get; set; }
        string FromWeek { get; set; }
        string ToWeek { get; set; }
        string ProductText { get; set; }
        string Epd { get; set; }
        int Aip { get; set; }
        string Type { get; set; }
        double Discount { get; set; }
        string DiscountPeriod { get; set; }
        string HowToOrder { get; set; }
        string Source { get; set; }
        DateTime CancelDeadline { get; set; }
        string Placement { get; set; }
        string Comments { get; set; }
        string ResponsiblePerson { get; set; }
        string AddedWeek { get; set; }
    }
}