using System;
using System.IO;
using System.Windows.Forms;

namespace PromotionParser.Workers
{
    public class FileReader
    {
        public FileInfo ReadFromFile()
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) return new FileInfo(ofd.FileName);
            throw new Exception($"Det oppstod en feil ved lesing av fil: {ofd.FileName}");
        }
    }
}