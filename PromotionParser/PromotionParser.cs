using PromotionParser.Managers;
using PromotionParser.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PromotionParser
{
    public class PromotionParser
    {
        // Setup classes
        public void Run()
        {
            var reader = new FileReader(); // Initialize the FileReader
            var parser = new Parser(); // Initialize the xls file parser
            var filename = reader.ReadFromFile(); // Load the file to memory
            var storage = new StorageManager(filename); // Initialize the storage manager                        

            parser.Parse(filename); // Parse the xls file

            storage.SaveToMemory(parser.Result); // save the List<IPromotion> to the StorageManager.

            if (MessageBox.Show("Vil du lagre resultatet som en xml-fil?", "Lagre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                storage.SaveToFile(parser.Result, new FileInfo("ostekake.xml")); // Save the List to a xml file.
            }           
            
            // Generate report.
        }
    }
}