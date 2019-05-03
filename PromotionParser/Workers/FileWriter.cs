using System.IO;
using System.Windows.Forms;

namespace PromotionParser.Workers
{
    /// <summary>
    /// Saves the List to the specified xml-file.
    /// </summary>
    class FileWriter
    {
        private readonly FileInfo _filename;

        public FileWriter(FileInfo filename)
        {
            _filename = filename;
        }

        public void WriteToFile()
        {
            if (File.Exists(_filename.FullName))
            {
                MessageBox.Show("Saving data to xml file.");
                // TODO: Save to XML file.
            }
        }
    }
}