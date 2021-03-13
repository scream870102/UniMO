using UnityEngine;

namespace Scream.UniMO.Utils
{
    /// <summary>
    /// utils about color
    /// </summary>
    public class ColorUtils
    {
        /// <summary>
        /// convert rgb formet to hsv format
        /// </summary>
        /// <param name="rgb">value to convert</param>
        /// <returns>hsv value</returns>
        public static Vector3 RGB2HSV(Color rgb)
        {
            var o = new Vector3();
            Color.RGBToHSV(rgb, out o.x, out o.y, out o.z);
            return o;
        }

        /// <summary>
        /// Convert hsv color to rgb
        /// </summary>
        /// <param name="hsv">value to convert</param>
        /// <returns>rgb value as Color</returns>
        public static Color HSV2RGB(Vector3 hsv) => Color.HSVToRGB(hsv.x, hsv.y, hsv.z);

        /// <summary>
        /// Set color's alpha range:0~255
        /// </summary>
        /// <param name="rgb">original color</param>
        /// <param name="alpha">new alpha</param>
        /// <returns>new color</returns>

        public static Color SetAlpha(Color rgb, float alpha)
        {
            float a = alpha / 255f;
            rgb.a = a;
            return rgb;
        }

        /// <summary>
        /// Set colors alpha range:0~1
        /// </summary>
        /// <param name="rgb">original color</param>
        /// <param name="alpha">new alpha</param>
        /// <returns>new color</returns>
        public static Color SetAlpha01(Color rgb, float alpha)
        {
            rgb.a = alpha;
            return rgb;
        }

        /// <summary>
        /// Set alpha of color to half
        /// </summary>
        /// <param name="rgb">original color</param>
        /// <returns>new color</returns>
        public static Color SetAlphaHalf(Color rgb)
        {
            rgb.a *= .5f;
            return rgb;
        }

    }
}
