using System;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston
{
    public static class Extensions
    {
        public static TEnum RandomEnumValue<TEnum>() where TEnum: struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum)
                throw new Exception("TEnum must be an enum.");
            var v = Enum.GetValues(typeof(TEnum));
            return (TEnum)v.GetValue(UnityEngine.Random.Range(0, v.Length));
        }

        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 1);
        }
        public static void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }
}
