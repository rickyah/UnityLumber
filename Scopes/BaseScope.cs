using System;

namespace Lumber.Scopes
{
    public abstract class BaseScope : IDisposable
    {
        protected RichLogger LoggerRef { get; set; }

        internal BaseScope(RichLogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            
            LoggerRef = logger;
        }

        public void Dispose()
        {
            this.RestoreState();
        }

        public abstract void RestoreState();
    }
}