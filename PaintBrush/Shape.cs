using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PaintBrush
{
    interface IShape
    {
        void Draw(Graphics g);
        void Fill(Graphics g);
        bool Inside(int x, int y);
        bool Equal(Shape shape);
    }

    class Shape : IShape
    {
        public Color outerColor { get; set; }
        public Color innerColor { get; set; }
        public int lineWidth { get; set; }
        public bool isSelected { get; set; }

        public Pen pen { get; set; }

        public System.Drawing.Rectangle bounds;
        
        public Shape(int x1, int x2, int y1, int y2)
        {
            pen = new Pen(Color.Blue);
        }

        protected void calcBounds(int x, int y, int width, int height)
        {
            bounds = new System.Drawing.Rectangle(x, y, width, height);
        }

        public void Draw(Graphics g)
        {
            pen.Color = outerColor;
            pen.Width = lineWidth;
            if(this is Ellipse)
                g.DrawEllipse(pen, bounds);
            else
            g.DrawRectangle(pen, bounds);
            Fill(g);
        }

        public void Fill(Graphics g)
        {
            SolidBrush b = new SolidBrush(innerColor);
            if (this is Ellipse)
                g.FillEllipse(b, bounds);
            else
                g.FillRectangle(b, bounds);
        }

        public virtual bool Inside(int x, int y)
        {
            if (bounds.Contains(x, y))
                return true;
            else
                return false;
        }

        public bool Equal(Shape shape)
        {
            if (innerColor == shape.innerColor && outerColor == shape.outerColor && lineWidth == shape.lineWidth && bounds == shape.bounds)
                return true;
            else
                return false;
        }
    }
}