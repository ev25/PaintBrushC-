using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintBrush
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        Graphics g;
        Shapes shapes = new Shapes();
        bool fillAction = false;
        bool borderAction = false;
        bool startPaint = false;
        //nullable int for storing Null value  
        int? initX = null;
        int? initY = null;
        bool drawRectangle = false;
        bool drawSquare = false;
        bool drawEllipse = false;
        Shape currentlyDrawn;
        Pen pen;
        private void button1_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drawEllipse = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pen = new Pen(Color.Black, 0); //default
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button4.BackColor = colorDialog1.Color;
                borderAction = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDialog1.Color;
                fillAction = true;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            
            foreach (Shape s in shapes.shapesArray)//לוקח טיפוס מסויים בתוך רשימה ועובר על כל האיברים ברשימה
            {
                s.Draw(e.Graphics);
                
            }
            if (currentlyDrawn != null)//הצורה שכרגע מצוירת, שנראה תוך כדי גרירה
            {
                currentlyDrawn.outerColor = pen.Color;
                currentlyDrawn.lineWidth = (int)pen.Width;
                currentlyDrawn.pen.DashStyle = pen.DashStyle;
                currentlyDrawn.Draw(e.Graphics);
            }
        }

        private void pictureBox1_MouseClick(Object sender, MouseEventArgs e)
        {

            if (startPaint == false)
            {
                if(borderAction)
                {
                    Shape s = shapes.GetInside(e.X, e.Y);
                    if(s != null)
                    {
                        shapes.shapesArray[shapes.Index(s)].outerColor = button4.BackColor;
                    }
                    borderAction = false;
                    pictureBox1.Invalidate();
                }
                if(fillAction)
                {
                    Shape s = shapes.GetInside(e.X, e.Y);
                    if (s != null)
                    {
                        shapes.shapesArray[shapes.Index(s)].innerColor = button5.BackColor;
                    }
                    fillAction = false;
                    pictureBox1.Invalidate();
                }

                initX = e.X;
                initY = e.Y;

                if (drawSquare)
                {
                    startPaint = true; //inside because only painting if pressed on button first
                    currentlyDrawn = new Square((int)initX, e.X, (int)initY, e.Y);
                }
                if (drawRectangle)
                {
                    startPaint = true;
                    currentlyDrawn = new Rectangle((int)initX, e.X, (int)initY, e.Y);
                }
                if (drawEllipse)
                {
                    startPaint = true;
                    currentlyDrawn = new Ellipse((int)initX, e.X, (int)initY, e.Y);
                }
            }
            else
            {
                shapes.Insert(currentlyDrawn);
                currentlyDrawn = null;
                startPaint = false;

                if (drawSquare == true)
                {
                    drawSquare = false;
                }
                if (drawRectangle == true)
                {
                    drawRectangle = false;
                }
                if (drawEllipse == true)
                {
                    drawEllipse = false;
                }
                pictureBox1.Invalidate();//קורא לפונקציית הציור של הקונטרול  
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint == true)
            {
                if (drawSquare == true)
                {
                    currentlyDrawn = new Square((int)initX, e.X, (int)initY, e.Y);
                }
                if (drawRectangle == true)
                {
                    currentlyDrawn = new Rectangle((int)initX, e.X, (int)initY, e.Y);
                }
                if (drawEllipse == true)
                {
                    currentlyDrawn = new Ellipse((int)initX, e.X, (int)initY, e.Y);
                }
                pictureBox1.Invalidate();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)numericUpDown1.Value;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (Shape s in shapes.shapesArray)
            {
                if(s.isSelected == true)
                {
                    s.isSelected = false;
                    s.pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
            }
            Shape selected = shapes.GetInside(e.X, e.Y);
            if (selected != null)
            {
                shapes.shapesArray[shapes.Index(selected)].isSelected = true;
                shapes.shapesArray[shapes.Index(selected)].pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            pictureBox1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dropdownlist!
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Color color;
            if (comboBox1.SelectedItem != null)
            {
                color = Color.FromName(comboBox1.SelectedItem.ToString());
                foreach (Shape s in shapes.shapesArray)
                {
                    if (s.isSelected)
                    {
                        s.innerColor = color;
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            while(shapes.shapesArray.Count != 0)
            {
                shapes.Delete(shapes.shapesArray[0]);
            }
            Dispose();
        }
    }
}