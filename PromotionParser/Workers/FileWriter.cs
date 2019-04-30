using System;
using System.IO;
using System.Windows.Forms;

namespace PromotionParser.Workers
{
    /// <summary>
    /// Saves the List<IPromotion> to the specified xml-file.
    /// </summary>
    class FileWriter
    {
        private readonly FileInfo filename;

        public FileWriter(FileInfo filename)
        {
            this.filename = filename;
        }

        public void WriteToFile()
        {
            if (File.Exists(filename.FullName))
            {
                MessageBox.Show("Saving data to xml file.");
            }
        }
    }
}