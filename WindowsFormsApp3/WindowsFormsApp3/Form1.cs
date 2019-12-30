using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        List<double> coordinates = new List<double>();
        double[,] mas = new double[1600,3];
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();//библиотека и методы Glut работают с ОС и конкретной аппаратурой
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);//для отображения кадра 
            //будет использоваться двойная буферизация, убирает мерцание монитора
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(-2.0, 2.0, -2.0, 2.0);
            //нужна еще одна объектно-видовая матрица для переноса
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Print();

            timer1.Start();

        }
        public void Print()
        {
            mas = new double[1600, 3];
            Random rand = new Random();
            int k = 0;
            for (double i = -2.0; i < 2.0; i += 0.1)
            {
                for (double j = -2.0; j < 2.0; j += 0.1)
                {
                    mas[k, 0] = i;
                    mas[k, 1] = j;
                    mas[k, 2] = rand.NextDouble();
                    k++;
                }
            }
        }
        private void Drawing()
        {
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            Gl.glColor3f(0.0f, 1.0f, 1.0f);
            Gl.glBegin(Gl.GL_POINTS);
            for (int i = 0; i < 1600; i++)
            {
                Gl.glVertex2f((float)mas[i, 0], (float)mas[i, 1]);
            }

            Gl.glEnd();
            AnT.Invalidate();

        }

        private void f()
        {
            /*for (int i = 0; i < 1600; i++)//cедло
            {
                mas[i, 0] += (- (mas[i, 0] * mas[i, 0]) + mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
                mas[i, 1] += (mas[i, 0] * mas[i, 0] - 2.0 * mas[i, 0] *mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
            }*/
            for (int i = 0; i < 1600; i++)//cедло
            {
                mas[i, 0] += (0.0 * mas[i, 0] + 1.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
                mas[i, 1] += (2.0 * (mas[i, 0] - mas[i, 0] * mas[i, 0] * mas[i, 0]) - 1.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
            }
            //for (int i = 0; i < 1600; i++)//неустойчивый фокус
            //{
            //    mas[i, 0] += (3.0 * mas[i, 0] - 4.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
            //    mas[i, 1] += (2.0 * mas[i, 0] - 1.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
            //}

            /*for (int i = 0; i < 1600; i++)//cедло
            {
                mas[i, 0] += (3.0 * mas[i, 0] - 4.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
                mas[i, 1] += (-3.0 * mas[i, 0] - 3.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
            }*/

            /*for (int i = 0; i < 1600; i++)//неустойчивый узел
            {
                mas[i, 0] += (-1.0 * mas[i, 0] - 1.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
                mas[i, 1] += (1.0 * mas[i, 0] - 3.0 * mas[i, 1]) * timer1.Interval / 1000.0 * mas[i, 2];
            }*/
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Drawing();
            f();
        }
        private void AnT_Load(object sender, EventArgs e)
        {

        }

    }
}
