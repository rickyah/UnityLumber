namespace Lumber.Extensions
{
    public static class ColorExtensions
    {
        public static string ToARGB(this UnityEngine.Color datColor)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", 
                datColor.r.ToRGBNumber(), 
                datColor.g.ToRGBNumber(), 
                datColor.b.ToRGBNumber(),
                datColor.a.ToRGBNumber());
        }

        private static int ToRGBNumber(this float number)
        {
            return (int)(number * 255);
        }
    }
}