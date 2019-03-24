﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTController.Common
{
    public class LedColorGradient
    {
        private readonly List<LedColorGradientPoint> _points;

        public IReadOnlyList<LedColorGradientPoint> Points => _points;
        public LedColorGradientPoint Start => _points.First();
        public LedColorGradientPoint End => _points.Last();

        public LedColorGradient(IEnumerable<LedColorGradientPoint> points)
        {
            _points = points.OrderBy(p => p.Location).ToList();
        }

        public LedColor GetColor(double location)
        {
            var (r, g, b) = ColorAtDeconstruct(location);
            return new LedColor((byte)r, (byte)g, (byte)b);
        }

        public (double, double, double) ColorAtDeconstruct(double location)
        {
            var i = 0;
            LedColorGradientPoint start, end;
            do
            {
                start = _points[i++];
                if (i >= _points.Count)
                    return (start.Color.R, start.Color.G, start.Color.B);

                end = _points[i];
            } while (!(location > start.Location && location < end.Location));

            var correctedLocation = (location - start.Location) / (end.Location - start.Location);
            return LedColor.LerpDeconstruct(correctedLocation, start.Color, end.Color);
        }
    }

    public class LedColorGradientPoint
    {
        public double Location { get; }
        public LedColor Color { get; }

        public LedColorGradientPoint(double location, LedColor color)
        {
            Location = location;
            Color = color;
        }
    }
}