using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintBrush
{
    class Rectangle : Shape
    {
        public Rectangle(int x1, int x2, int y1, int y2) : base(x1, x2, y1, y2)
        {
            calcBounds(Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x2 - x1), Math.Abs(y2 - y1));
        }
    }
}
