using PromotionParser.Managers;
using PromotionParser.Workers;
using System;
using System.IO;
using System.Windows.Forms;
using static System.String;

namespace PromotionParser
{
    public class PromotionParser
    {
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
                StorageManager.SaveToFile(parser.Result, new FileInfo("ostekake.xml")); // Save the List to a xml file.
            
                // Generate report.
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}{Environment.NewLine}{Environment.NewLine}{e.InnerException?.Message ?? Empty}", "Det oppstod en feil", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}