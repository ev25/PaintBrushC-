using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintBrush
{
    class Shapes
    {
        public List<Shape> shapesArray { get; set; }

        public Shapes()
        {
            shapesArray = new List<Shape>();
        }

        public void Insert(Shape shape)
        {
            shapesArray.Add(shape);
        }

        public void Delete(Shape shape)
        {
            shapesArray.Remove(shape);
        }
        public int Index(Shape shape)
        {
            return shapesArray.FindIndex(a => a.Equal(shape));//הפונקצייה רוצה לקבל חוק. בתוך החוק קובעים איך ייקראו לאובייקט.
        }
        public Shape GetInside(int x, int y)
        { //take last, because if there are two or more colliding shapes, it should select the top one, so the last one drawn/added
            return shapesArray.FindLast(s => s.Inside(x, y));
        }
    }
}
