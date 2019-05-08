using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PromotionParser.Data;
using PromotionParser.Data.PromotionTypes;

namespace PromotionParser.Workers
{
    class ReportGenerator
    {
        private readonly List<IPromotion> _parserResult;

        public ReportGenerator(List<IPromotion> parserResult)
        {
            _parserResult = parserResult;
        }

        public void Generate() // This will generate the weekly Promotion report
        {
            if (_parserResult != null)
            {
                GenerateWeeklyReport();
            } else throw new Exception("Det ble ikke generert noe data.");
        }

        private void GenerateWeeklyReport()
        {
            var currentWeek = GetWeekNumber();
            var nextWeek = currentWeek + 1;
            var inTwoWeeks = currentWeek + 2;
            
            var thisWeekEndediskPromos = new List<IPromotion>();
            var nextWeekEndediskPromos = new List<IPromotion>();
            
            var thisWeekPallebordPromos = new List<IPromotion>();
            var nextWeekPallebordPromos = new List<IPromotion>();
            var pallebord2NextWeekPromos = new List<IPromotion>();
            var pallebord2InTwoWeeksPromos = new List<IPromotion>();
            var pallebord3NextWeekPromos = new List<IPromotion>();
            var pallebord3InTwoWeeksPromos = new List<IPromotion>();
            
            var endedisk1CoolNextWeekPromotions = new List<IPromotion>();
            var endedisk1CoolInTwoWeeksPromotions = new List<IPromotion>();
            var endedisk2CoolNextWeekPromotions = new List<IPromotion>();
            var endedisk2CoolInTwoWeeksPromotions = new List<IPromotion>();

            var endedisk1FreezeNextWeekPromotions = new List<IPromotion>();
            var endedisk1FreezeInTwoWeeksPromotions = new List<IPromotion>();
            var endedisk2FreezeNextWeekPromotions = new List<IPromotion>();
            var endedisk2FreezeInTwoWeeksPromotions = new List<IPromotion>();
            
            thisWeekPallebordPromos.AddRange(_parserResult.Where(c => c is PallebordPromotion && IsInRange(currentWeek, c.FromWeek, c.ToWeek)));
            thisWeekEndediskPromos.AddRange(_parserResult.Where(c => c is EndediskPromotion && IsInRange(currentWeek, c.FromWeek, c.ToWeek)));
            
            nextWeekPallebordPromos.AddRange(_parserResult.Where(c => c is PallebordPromotion && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            nextWeekEndediskPromos.AddRange(_parserResult.Where(c => c is EndediskPromotion && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            
            pallebord2NextWeekPromos.AddRange(_parserResult.Where(c => c.Comments.StartsWith("PALLEBORD KASSE 2") && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            pallebord2InTwoWeeksPromos.AddRange(_parserResult.Where(c => c.Comments.StartsWith("PALLEBORD KASSE 2") && IsInRange(inTwoWeeks, c.FromWeek, c.ToWeek)));
            
            pallebord3NextWeekPromos.AddRange(_parserResult.Where(c => c.Comments.StartsWith("PALLEBORD KASSE 3") && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            pallebord3InTwoWeeksPromos.AddRange(_parserResult.Where(c => c.Comments.StartsWith("PALLEBORD KASSE 3") && IsInRange(inTwoWeeks, c.FromWeek, c.ToWeek)));
            
            // Generate endedisk promotions. This is a bit tricky because the naming is not consistent. So Im going for separating the "endedisk" based on the responsible person.
            // Harald = "Kjøl" only. Brit and Trond have "Frys".
            
            // It is tricky when there is two placements in one promotions, not able to parse this of yet. If for example the main line contains a "Frys" item, the whole promotion is parsed as "Frys". 
            // I have to iterate through every line to solve this, TODO: Look at this at a later time. Maybe add a dialog to manually choose the location if there is doubt.
            
            endedisk1CoolNextWeekPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("ENDEDISK 1") && c.ResponsiblePerson.Equals("HARALD") && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            endedisk1CoolInTwoWeeksPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("ENDEDISK 1") && c.ResponsiblePerson.Equals("HARALD") && IsInRange(inTwoWeeks, c.FromWeek, c.ToWeek)));
            
            endedisk2CoolNextWeekPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("ENDEDISK 2") && c.ResponsiblePerson.Equals("HARALD") && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            endedisk2CoolInTwoWeeksPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("ENDEDISK 2") && c.ResponsiblePerson.Equals("HARALD") && IsInRange(inTwoWeeks, c.FromWeek, c.ToWeek)));
            
            endedisk1FreezeNextWeekPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("FRYS")
                                                                                && c.Comments.Contains("ENDEDISK")
                                                                                && !c.Comments.Contains("ENDEDISK 3")
                                                                                && !c.Comments.Contains("ENDEDISK 2")
                                                                                && (c.ResponsiblePerson.Equals("BRIT") || c.ResponsiblePerson.Equals("TROND"))
                                                                                && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            endedisk1FreezeInTwoWeeksPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("FRYS")
                                                                                  && c.Comments.Contains("ENDEDISK")
                                                                                  && !c.Comments.Contains("ENDEDISK 3")
                                                                                  && !c.Comments.Contains("ENDEDISK 2")
                                                                                  && (c.ResponsiblePerson.Equals("BRIT") || c.ResponsiblePerson.Equals("TROND"))
                                                                                  && IsInRange(inTwoWeeks, c.FromWeek, c.ToWeek)));
            endedisk2FreezeNextWeekPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("FRYS") && c.Comments.Contains("ENDEDISK 2")  && (c.ResponsiblePerson.Equals("BRIT") || c.ResponsiblePerson.Equals("TROND")) && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            endedisk2FreezeInTwoWeeksPromotions.AddRange(_parserResult.Where(c => c.Comments.Contains("FRYS") && c.Comments.Contains("ENDEDISK 2")  && (c.ResponsiblePerson.Equals("BRIT") || c.ResponsiblePerson.Equals("TROND")) && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
        }
        
        private static int GetWeekNumber()
        {
            var time = DateTime.Now;
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private static bool IsInRange(int week, string from, string to)
        {
            return week >= int.Parse(from) && week <= int.Parse(to);
        }
    }
}