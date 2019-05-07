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
            }
        }

        private void GenerateWeeklyReport()
        {
            var thisWeekPallebordPromos = new List<IPromotion>();
            var thisWeekEndediskPromos = new List<IPromotion>();
            var nextWeekPallebordPromos = new List<IPromotion>();
            var nextWeekEndediskPromos = new List<IPromotion>();
            var currentWeek = GetWeekNumber();
            var nextWeek = currentWeek + 1;

            thisWeekPallebordPromos.AddRange(_parserResult.Where(c => c is PallebordPromotion && IsInRange(currentWeek, c.FromWeek, c.ToWeek)));
            thisWeekEndediskPromos.AddRange(_parserResult.Where(c => c is EndediskPromotion && IsInRange(currentWeek, c.FromWeek, c.ToWeek)));
            
            nextWeekPallebordPromos.AddRange(_parserResult.Where(c => c is PallebordPromotion && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));
            nextWeekEndediskPromos.AddRange(_parserResult.Where(c => c is EndediskPromotion && IsInRange(nextWeek, c.FromWeek, c.ToWeek)));

            MessageBox.Show($"Pallebord-aktiviteter denne uken: {thisWeekPallebordPromos.Count} (Neste uke: {nextWeekPallebordPromos.Count})" +
                            $" - Endedisk-aktiviteter denne uken: {thisWeekEndediskPromos.Count} (Neste uke: {nextWeekEndediskPromos.Count})");
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
            var fromWeek = int.Parse(from);
            var toWeek = int.Parse(to);

            return week >= fromWeek && week <= toWeek;
        }
    }
}