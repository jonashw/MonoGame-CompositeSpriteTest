using System;

namespace CompositeSpriteTest
{
    public static class MathUtil
    {
        /// <summary>
        /// Clamps a value between two extremes.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="current"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(float min, float current, float max)
        {
            return Math.Min(max, Math.Max(min, current));
        }
    }
}