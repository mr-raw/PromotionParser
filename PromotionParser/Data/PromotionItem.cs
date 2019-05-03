using System;

namespace PromotionParser.Data
{
    public class PromotionItem
    {
        public string Vendor { get; set; }
        public string ProductText { get; set; }
        public string Epd { get; set; }
        public string Sel { get; set; }
        public string Aip { get; set; } 
        public string Type { get; set; } // "The "Aktivitetstype" might be different on the different lines.
        public double Discount { get; set; } // "RABATT "The discount may be different on the various lines.
        public string HowToOrder { get; set; } // "BESTILLING"
        public DateTime CancelDeadline { get; set; } // "AVBESTILLINGSFRIST" // Observed that the date was not always on the first line.
        public string Placement { get; set; } // "PLASSERING"
        public string Comments { get; set; } // "KOMMENTARER"
        public string ResponsiblePerson { get; set; } // "ANSV."  
        public string AddedWeek { get; set; } // "INN UKE" This has to be per item because there were multiple "answers"
    }
}