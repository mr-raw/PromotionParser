using System;
using System.Collections.Generic;

namespace PromotionParser.Data
{
    public interface IPromotion
    {
        List<PromotionItem> PromotionItemsList { get; set; }
        string Vendor { get; set; }
        string FromWeek { get; set; }
        string ToWeek { get; set; }
        string Sel { get; set; }
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