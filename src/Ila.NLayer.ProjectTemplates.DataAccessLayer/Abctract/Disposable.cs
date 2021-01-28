using System;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract
{
    public class Disposable : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose used for the objects you want to.
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}