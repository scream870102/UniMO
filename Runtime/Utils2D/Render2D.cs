using UnityEngine;

namespace Scream.UniMO.Utils2D
{
    /// <summary>
    /// utils for render in 2d
    /// </summary>
    public static class Render2D
    {
        /// <summary>
        /// flip target due to IsFacingRight
        /// </summary>
        /// <param name="IsFacingRight">is target facing at right direction</param>
        /// <param name="target">which transform to change</param>
        /// <param name="IsInvert"></param>
        public static void ChangeDirection(bool IsFacingRight, Transform target, bool IsInvert = false)
        {
            Vector3 tmp = target.localScale;
            tmp.x = Mathf.Abs(tmp.x) * (IsFacingRight ? 1f : -1f) * (IsInvert ? -1 : 1);
            target.localScale = tmp;
        }

        /// <summary>
        /// flip target due to IsFacingRight
        /// </summary>
        /// <param name="IsFacingRight">is target facing at right direction</param>
        /// <param name="target">which transform to change</param>
        /// <param name="IsInvert">invert the result or not</param>
        public static void ChangeDirectionY(bool IsFacingRight, Transform target, bool IsInvert = false)
        {
            Vector3 tmp = target.localScale;
            tmp.y = Mathf.Abs(tmp.y) * (IsFacingRight ? 1f : -1f) * (IsInvert ? -1 : 1);
            target.localScale = tmp;
        }

        /// <summary>
        /// flip target
        /// </summary>
        /// <param name="flipX">flipX or not</param>
        /// <param name="renderer">ref for target renderer</param>
        /// <param name="IsInvert">invert the result or not</param>
        public static void ChangeDirectionXWithSpriteRender(bool flipX, SpriteRenderer renderer, bool IsInvert = false) => renderer.flipX = flipX ^ IsInvert;

        /// <summary>
        /// flip target
        /// </summary>
        /// <param name="flipY">flipY or not</param>
        /// <param name="renderer">ref for target renderer</param>
        /// <param name="IsInvert">invert the result or not</param>
        public static void ChangeDirectionYWithSpriteRender(bool flipY, SpriteRenderer renderer, bool IsInvert = false) => renderer.flipY = flipY ^ IsInvert;

    }
}
