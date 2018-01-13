using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintBrush
{
    class Square : Rectangle
    {
        public Square(int x1, int x2, int y1, int y2) : base(x1, x2, y1, y2)
        {
            int width = Math.Min(Math.Abs(x2 - x1), Math.Abs(y2 - y1));

            if (x2 > x1)
            {
                if (y2 > y1)
                {
                    calcBounds(x1, y1, width, width);
                }
                else
                    calcBounds(x1, y1 - width, width, width);
            }
            else
            {
                if (y2 > y1)
                    calcBounds(x1 - width, y1, width, width);
                else
                {
                    calcBounds(x1 - width, y1 - width, width, width);
                }
            }

        }
    }
}