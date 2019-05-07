using PromotionParser.Managers;
using PromotionParser.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PromotionParser.Data;
using static System.String;

namespace PromotionParser
{
    public class PromotionParser
    {
        private List<IPromotion> _promotions = new List<IPromotion>();
        
        public List<IPromotion> Promotions {
            get
            {
                if (_promotions != null && _promotions.Count > 0)
                {
                    return _promotions;
                }
                throw new Exception("Listen er tom, kan ikke hente data.");
            }
        }
        
        // Setup classes
        public void Run()
        {
            try
            {
                var parser = new Parser(); // Initialize the xls file parser
                var storage = new StorageManager(); // Initialize the storage manager
                var importedExcelFile = StorageManager.ImportExcelFile(); // Load the file to memory

                parser.Parse(importedExcelFile); // Parse the xls file
                
                storage.SaveToMemory(parser.Result); // save the List<IPromotion> to the StorageManager.
                if (parser.Result != null)
                {
                    _promotions = parser.Result;
                }
                StorageManager.SaveToFile(parser.Result, new FileInfo("ostekake.xml")); // Save the List to a xml file.
                
                var reportGenerator = new ReportGenerator(parser.Result);
                reportGenerator.Generate();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}{Environment.NewLine}{Environment.NewLine}{e.InnerException?.Message ?? Empty}", "Det oppstod en feil", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}