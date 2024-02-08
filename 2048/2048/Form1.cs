using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace _2048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        public class cell
        {
            public int x;
            public int y;
            public int val;

            public cell(int pozx, int pozy, int value)
            {
                x = pozx;
                y = pozy;
                val = value;
            }
        }

        cell[] celule = new cell[16];
        int loaded = 0;
        SolidBrush brush;

        public void generare_playground()
        {
            int pozx = 50, pozy = 50;
            int k = 0;
            for(int i=0; i<4; i++)
            {
                for(int j=0; j<4; j++)
                {
                    cell aux = new cell(pozx, pozy, 0);
                    celule[k] = aux;
                    k++;
                    pozx += 50;
                }
                pozx = 50;
                pozy += 50;
            }
        }

        public void first_values()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 16);
            celule[i].val = 2;
            Thread.Sleep(200);
            rnd = new Random();
            i = rnd.Next(0, 16);
            celule[i].val = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generare_playground();
            first_values();
            loaded = 1;
            pictureBox1.Refresh();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(loaded==1)
            {
                for(int cnt=0; cnt<16; cnt++)
                {
                    Font font = new Font("Arial", 16);
                    Rectangle rect = new Rectangle(celule[cnt].x, celule[cnt].y, 40, 40);
                    if (celule[cnt].val != 0)
                    {
                        if (celule[cnt].val == 2)
                            brush = new SolidBrush(Color.Green);
                        if (celule[cnt].val == 4)
                            brush = new SolidBrush(Color.Yellow);
                        if (celule[cnt].val == 8)
                            brush = new SolidBrush(Color.Orange);
                        if (celule[cnt].val == 16)
                            brush = new SolidBrush(Color.OrangeRed);
                        if (celule[cnt].val == 32)
                            brush = new SolidBrush(Color.DarkRed);
                        if (celule[cnt].val == 64)
                            brush = new SolidBrush(Color.Blue);
                        e.Graphics.FillRectangle(brush, rect);
                        SolidBrush black = new SolidBrush(Color.Black);
                        e.Graphics.DrawString(celule[cnt].val.ToString(), font, black, rect);
                    }
                    else
                    {
                        SolidBrush grey= new SolidBrush(Color.DarkGray);
                        e.Graphics.FillRectangle(grey, rect);
                    }
                    
                }
            }
        }

        public void generare_nou()
        {
            int i;
            Random rnd;
            int ok = 0;
            for(int cnt=0; cnt<=15; cnt++)
            {
                if(celule[cnt].val==0)
                {
                    ok = 1;
                }
            }
            if (ok == 1)
            {
                do
                {
                    rnd = new Random();
                    i = rnd.Next(0, 16);
                    Thread.Sleep(100);
                } while (celule[i].val != 0);
                rnd = new Random();
                int pow = rnd.Next(1, 4);
                int value = 1;
                for (int cnt = 1; cnt <= pow; cnt++)
                {
                    value *= 2;
                }
                celule[i].val = value;
            }
            else
            {
                MessageBox.Show("Ai pierdut!");
                Application.Exit();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if(e.KeyValue=='W')
            {
                for(int i=4; i<=15; i++)
                {
                    while(i>= 4 && celule[i].val != 0 && celule[i - 4].val == 0)
                    {

                       celule[i - 4].val = celule[i].val;
                       celule[i].val = 0;     

                        i = i - 4;
                    }
                    if(i >= 4 && celule[i].val != 0 && celule[i - 4].val != 0 && celule[i].val == celule[i - 4].val)
                    {
                        celule[i - 4].val += celule[i].val;
                        celule[i].val = 0;

                        i = i - 4;
                    }
                }
            }
            if (e.KeyValue == 'A')
            {
                for (int i = 0; i <=15; i++)
                {
                    while (i - 1 >= 0 && i%4!=0 && celule[i].val != 0 && celule[i - 1].val == 0)
                    {
                            celule[i - 1].val = celule[i].val;
                            celule[i].val = 0;
                        i = i - 1;
                    }
                    if(i - 1 >= 0 && i % 4 != 0 && celule[i].val != 0 && celule[i - 1].val != 0 && celule[i].val == celule[i - 1].val)
                    {
                        celule[i - 1].val += celule[i].val;
                        celule[i].val = 0;
                        i = i - 1;
                    }
                }
            }
            if (e.KeyValue == 'S')
            {
                for (int i = 11; i >=0; i--)
                {
                    while (i <=11 && celule[i].val != 0 && celule[i + 4].val == 0)
                    { 
                            celule[i + 4].val = celule[i].val;
                            celule[i].val = 0;
                        i = i + 4;
                    }
                    if(i <= 11 && celule[i].val != 0 && celule[i + 4].val != 0 && celule[i].val == celule[i + 4].val)
                    {
                        celule[i + 4].val += celule[i].val;
                        celule[i].val = 0;
                        i = i + 4;
                    }    
                }
            }
            if (e.KeyValue == 'D')
            {
                for (int i = 15; i >=0; i--)
                {
                    while (i +1 <= 15 && i%4 !=3 && celule[i].val != 0 && celule[i +1].val == 0)
                    {
                        celule[i +1].val = celule[i].val;
                        celule[i].val = 0;
                        i++;
                    }
                    if(i + 1 <= 15 && i % 4 != 3 && celule[i].val != 0 && celule[i + 1].val !=0 && celule[i].val ==celule[i + 1].val)
                    {
                        celule[i + 1].val = celule[i].val+celule[i].val;
                        celule[i].val = 0;
                        i++;
                    }
                }
            }
            generare_nou();
            pictureBox1.Refresh();
        }
    }
}
