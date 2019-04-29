using PromotionParser.Data;
using PromotionParser.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionParser.Managers
{
    class StorageManager
    {
        private List<IPromotion> PromoList = new List<IPromotion>();
        private readonly FileInfo filename;

        public StorageManager(FileInfo filename)
        {
            this.filename = filename;
        }

        /// <summary>
        /// Replace the entire list of promotions
        /// </summary>
        /// <param name="promotions">The list of promotions to be saved.</param>
        public void SaveToMemory(List<IPromotion> promotions)
        {
            PromoList = promotions;
        }

        public void SaveToFile(List<IPromotion> result, FileInfo fileInfo)
        {
            if (result.Count > 0)
            {
                using (var f = new FileWriter(fileInfo))
                {
                    f.WriteToFile();
                }
            }            
        }

        /// <summary>
        /// Load the entire list of promotions
        /// </summary>
        /// <returns>The entire list of promotions.</returns>
        public List<IPromotion> Load()
        {
            return null;
        }
    }
}