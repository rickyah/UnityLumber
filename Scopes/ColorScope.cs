namespace Lumber.Scopes
{
    public class ColorScope : BaseScope
    {
        private UnityEngine.Color _oldColor;

        internal ColorScope(RichLogger logger, UnityEngine.Color color)
        : base(logger)
        {
        
            _oldColor = LoggerRef.Color;
        
            LoggerRef.SetColor(color);
        
        }

        public override void RestoreState()
        {
            LoggerRef.SetColor(_oldColor);
        }
    }
}
