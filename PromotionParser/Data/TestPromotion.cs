using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace PromotionParser.Data
{
    public class TestPromotion : IPromotion
    {
        public ExcelRow RawData { get; set; }
        public List<PromotionItem> PromotionItemsList { get; set; }
        public string Vendor { get; set; }
        public string FromWeek { get; set; }
        public string ToWeek { get; set; }
        public string ProductText { get; set; }
        public string Epd { get; set; }
        public int Aip { get; set; }
        public string Type { get; set; }
        public double Discount { get; set; }
        public string DiscountPeriod { get; set; }
        public string HowToOrder { get; set; }
        public string Source { get; set; }
        public DateTime CancelDeadline { get; set; }
        public string Placement { get; set; }
        public string Comments { get; set; }
        public string ResponsiblePerson { get; set; }
        public string AddedWeek { get; set; }
    }
}