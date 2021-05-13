using System;
using Xamarin.Forms;

namespace RadialProgress
{
    internal class ColorInterpolator
    {
        delegate double ComponentSelector(Color color);
        static ComponentSelector _redSelector = color => color.R;
        static ComponentSelector _greenSelector = color => color.G;
        static ComponentSelector _blueSelector = color => color.B;

        public static Color InterpolateBetween(
            Color from,
            Color to,
            double percent)
        {
            if (percent < 0 || percent > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(percent));
            }
            var color = new Color(
                InterpolateComponent(from, to, percent, _redSelector),
                InterpolateComponent(from, to, percent, _greenSelector),
                InterpolateComponent(from, to, percent, _blueSelector)
            );

            return color;
        }

        public static Color InterpolateBetween(
            Color from,
            Color to,
            Color via,
            double percent)
        {
            Color color;
            if (percent < 0 || percent > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(percent));
            }
            if(percent <= 0.5)
            {
                color = InterpolateBetween(from, via, percent * 2);
            }
            else 
            {
                color = InterpolateBetween(via, to, (percent * 2) - 1);
            }

            return color;
        }

        static double InterpolateComponent(
            Color endPoint1,
            Color endPoint2,
            double percent,
            ComponentSelector selector)
        {
            return (double)(selector(endPoint1) + (selector(endPoint2) - selector(endPoint1)) * percent);
        }
    }
}