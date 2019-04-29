using System;
using System.IO;
using System.Windows.Forms;

namespace PromotionParser.Workers
{
    /// <summary>
    /// Saves the List<IPromotion> to the specified xml-file.
    /// </summary>
    class FileWriter : IDisposable
    {
        private readonly FileInfo filename;

        public FileWriter(FileInfo filename)
        {
            this.filename = filename;
        }

        public void WriteToFile()
        {
            if (File.Exists(filename.ToString()))
            {
                MessageBox.Show("Saving data to xml file.");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FileWriter()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}