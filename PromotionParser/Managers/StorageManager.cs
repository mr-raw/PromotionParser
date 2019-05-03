using PromotionParser.Data;
using PromotionParser.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PromotionParser.Managers
{
    public class StorageManager
    {
        private List<IPromotion> _promoList = new List<IPromotion>();

        /// <summary>
        /// Replace the entire list of promotions
        /// </summary>
        /// <param name="promotions">The list of promotions to be saved.</param>
        public void SaveToMemory(List<IPromotion> promotions)
        {
            if (_promoList.Count <= 0 || _promoList == null) return;
            if (MessageBox.Show("Vil du overskrive listen med aktiviteter som allerede ligger i minnet?", "Overskrive", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _promoList = promotions;
            }
            else
            {
                throw new Exception("Kunne ikke lagre aktiviteter til minnet");
            }
        }
        
        public static void SaveToFile(List<IPromotion> resultToBeSaved, FileInfo fileInfo)
        {
            if (MessageBox.Show(@"Vil du lagre resultatet som en xml-fil?", "Lagre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (resultToBeSaved.Count > 0)
                {
                    try
                    {
                        var f = new FileWriter(fileInfo);
                        f.WriteToFile();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Load the entire list of promotions
        /// </summary>
        /// <returns>The entire list of promotions.</returns>
        public static FileInfo ImportExcelFile()
        {
            try
            {
                var fReader = new FileReader();
                return fReader.ReadFromFile();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}