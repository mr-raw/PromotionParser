using System;
using System.Collections.Generic;

namespace PromotionParser.Data.PromotionTypes
{
    public class TrappesjokkerPromotion : IPromotion
    {
        public List<PromotionItem> PromotionItemsList { get; set; }
        public string Vendor { get; set; }
        public string FromWeek { get; set; }
        public string ToWeek { get; set; }
        public string Sel { get; set; }
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