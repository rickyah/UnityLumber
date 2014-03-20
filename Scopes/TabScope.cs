namespace Lumber.Scopes
{
    public class TabScope : BaseScope
    {
        private int _oldLevel;

        internal TabScope(RichLogger logger, int newLevel)
        : base(logger)
        {
            _oldLevel = logger.Level;
        
            LoggerRef.SetLevel(newLevel);
        }

        public override void RestoreState()
        {
            LoggerRef.SetLevel(_oldLevel);
        }
    }
}