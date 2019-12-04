using System;

namespace Altantu.Core.Extensions
{
    public static class Extension
    {
        #region Methods

        public static string ToStringValue(this int intValue)
        {
            string stringValue = null;

            int absValue = Math.Abs(intValue);

            if (absValue >= 0 && absValue < Math.Pow(10, 3))
            {
                stringValue = intValue.ToString();
            }
            else if (absValue >= Math.Pow(10, 3) && absValue < Math.Pow(10, 6))
            {
                stringValue = string.Format("{0:0.#} K", intValue / Math.Pow(10, 3));
            }
            else if (absValue >= Math.Pow(10, 6) && absValue < Math.Pow(10, 9))
            {
                stringValue = string.Format("{0:0.#} M", intValue / Math.Pow(10, 6));
            }
            else if (absValue >= Math.Pow(10, 9) && absValue < Math.Pow(10, 12))
            {
                stringValue = string.Format("{0:0.#} G", intValue / Math.Pow(10, 9));
            }
            else
            {
                throw new Exception("Number too big");
            }

            return stringValue;
        }

        #endregion
    }
}
