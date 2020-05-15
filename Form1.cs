using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //midPointCircleDraw(0, 0, 3);
        }
        void PutPixel(Graphics g, int x, int y, Color c)

        {

            Bitmap bm = new Bitmap(1, 1);

            bm.SetPixel(0, 0, Color.Red);

            g.DrawImageUnscaled(bm, x, y);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics myGraphics = e.Graphics;



            //myGraphics.Clear(Color.White);

            //double radius = 5;

            //for (int j = 1; j <= 25; j++)

            //{

            //    radius = (j + 1) * 5;

            //    for (double i = 0.0; i < 360.0; i += 0.1)

            //    {

            //        double angle = i * System.Math.PI / 180;

            //        int x = (int)(150 + radius * System.Math.Cos(angle));

            //        int y = (int)(150 + radius * System.Math.Sin(angle));



            //        PutPixel(myGraphics, x, y, Color.Red);

            //        //System.Threading.Thread.Sleep(1); // If you want to draw circle very slowly.

            //    }

            //}

            //myGraphics.Dispose();
        }

        static void midPointCircleDraw(int x_centre,
                           int y_centre, int r)
        {

            int x = r, y = 0;

            // Printing the initial point on the 
            // axes after translation 
            Console.Write("(" + (x + x_centre)
                    + ", " + (y + y_centre) + ")");

            // When radius is zero only a single 
            // point will be printed 
            if (r > 0)
            {

                Console.Write("(" + (x + x_centre)
                    + ", " + (-y + y_centre) + ")");

                Console.Write("(" + (y + x_centre)
                    + ", " + (x + y_centre) + ")");

                Console.WriteLine("(" + (-y + x_centre)
                    + ", " + (x + y_centre) + ")");
            }

            // Initialising the value of P 
            int P = 1 - r;
            while (x > y)
            {

                y++;

                // Mid-point is inside or on the perimeter 
                if (P <= 0)
                    P = P + 2 * y + 1;

                // Mid-point is outside the perimeter 
                else
                {
                    x--;
                    P = P + 2 * y - 2 * x + 1;
                }

                // All the perimeter points have already  
                // been printed 
                if (x < y)
                    break;

                // Printing the generated point and its  
                // reflection in the other octants after 
                // translation 
                Console.Write("(" + (x + x_centre)
                        + ", " + (y + y_centre) + ")");

                Console.Write("(" + (-x + x_centre)
                        + ", " + (y + y_centre) + ")");

                Console.Write("(" + (x + x_centre) +
                        ", " + (-y + y_centre) + ")");

                Console.WriteLine("(" + (-x + x_centre)
                        + ", " + (-y + y_centre) + ")");

                // If the generated point is on the  
                // line x = y then the perimeter points 
                // have already been printed 
                if (x != y)
                {
                    Console.Write("(" + (y + x_centre)
                        + ", " + (x + y_centre) + ")");

                    Console.Write("(" + (-y + x_centre)
                        + ", " + (x + y_centre) + ")");

                    Console.Write("(" + (y + x_centre)
                        + ", " + (-x + y_centre) + ")");

                    Console.WriteLine("(" + (-y + x_centre)
                        + ", " + (-x + y_centre) + ")");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] genom_files = Directory.GetFiles(@"C:\COVID\extract4\", "*.genom");


            foreach (string file_name in genom_files)
            {

            
            string text = System.IO.File.ReadAllText(file_name);
            char[] arr;

            arr = text.ToCharArray();

            int say = 0;
            Bitmap bmp = new Bitmap(200, 200);

            int r = 98; // radius
            int ox = 100, oy = 100; // origin

            for (int x = -r; x < r; x++)
            {
                int height = (int)Math.Sqrt(r * r - x * x);

                for (int y = -height; y < height; y++)
                {
                    try
                    {
                        string cc = arr[say].ToString();
                        if (cc == "A")
                            bmp.SetPixel(x + ox, y + oy, Color.Blue);
                        if (cc == "T")
                            bmp.SetPixel(x + ox, y + oy, Color.Yellow);
                        if (cc == "C")
                            bmp.SetPixel(x + ox, y + oy, Color.Red);
                        if (cc == "G")
                            bmp.SetPixel(x + ox, y + oy, Color.Green);
                        if (cc == "N")
                            bmp.SetPixel(x + ox, y + oy, Color.White);
                        }
                    catch { bmp.SetPixel(x + ox, y + oy, Color.White); }
                    say += 1;
                }
            }
                int c = 8000 + int.Parse(Path.GetFileNameWithoutExtension(file_name));
                bmp.Save(@"c:\COVID\picto4-austr\" + c.ToString() + ".png");
            Console.WriteLine(say.ToString());

            }
        }

        
    

    private void button2_Click(object sender, EventArgs e)
        {
            Regex MyRegex = new Regex("(>.*\\|\\scomplete genome)(.*?)", RegexOptions.Compiled);

            int satir = 1;
            string text = System.IO.File.ReadAllText(@"C:\COVID\sequences_2.fasta");

            //// Replace the matched text in the InputText using the replacement pattern
            // string result = MyRegex.Replace(InputText,MyRegexReplace);

            //// Split the InputText wherever the regex matches
            string[] results = MyRegex.Split(text);
            int line_n = 0;
            string my_genome = "";
            foreach (string g in results)
            {
                if (g != "")
                {
                    if (g.StartsWith(">") == false)
                    {
                        my_genome = g.Replace("\r\n", "").Replace("\n","");
                        File.WriteAllText(@"C:\COVID\extract1\" + satir.ToString() +".genom", my_genome);
// textBox1.Text += satir.ToString() + "_" + results[line_n - 2].ToString().Substring(1, results[line_n - 2].ToString().IndexOf(" |"))+"\r\n";
                        textBox1.Text += satir.ToString() + ";" + results[line_n - 2].ToString().Replace("|",";") + "\r\n";
                        satir += 1;
                    }
                }
                line_n += 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Regex MyRegex = new Regex("(>.*\\s\\|\\s.*)(.*?)", RegexOptions.Compiled);

            int satir = 1;
            string text = System.IO.File.ReadAllText(@"C:\COVID\all.fasta");

            //// Replace the matched text in the InputText using the replacement pattern
            // string result = MyRegex.Replace(InputText,MyRegexReplace);

            //// Split the InputText wherever the regex matches
            string[] results = MyRegex.Split(text);
            int line_n = 0;
            string my_genome = "";
            foreach (string g in results)
            {
                if (g != "")
                {
                    if (g.StartsWith(">") == false)
                    {
                        my_genome = g.Replace("\r\n", "").Replace("\n", "");
                        if (my_genome.Length > 1000)
                        {
                            if (my_genome.Contains("NNNNNNNN") == false)
                            {
                                File.WriteAllText(@"C:\COVID\extract1\" + satir.ToString() + ".genom", my_genome);
                                // textBox1.Text += satir.ToString() + "_" + results[line_n - 2].ToString().Substring(1, results[line_n - 2].ToString().IndexOf(" |"))+"\r\n";
                                textBox1.Text += satir.ToString() + ";" + results[line_n - 2].ToString().Replace("|", ";") + "\r\n";
                                satir += 1;
                            }
                        }
                    }
                }
                line_n += 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] aa = { 6, 7, 8, 9, 10, 16, 17, 18, 19, 20, 21, 22, 23, 24, 27, 28, 29, 30, 31, 32, 35, 40, 41, 42, 43, 70, 71, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 125, 137, 138, 139, 140, 141, 142, 143, 156, 157, 190, 191, 192, 193, 194, 195, 288, 289, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 641, 642, 643, 644, 645, 646, 647, 648, 649, 650, 1054, 1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150, 1151, 1152, 1153, 1154, 1155, 1156 };

            foreach(int sayy in aa)
            {
                string text = System.IO.File.ReadAllText(@"C:\COVID\extract1\" + sayy.ToString() + ".genom");
                char[] arr;

                arr = text.ToCharArray();

                int say = 0;
                Bitmap bmp = new Bitmap(200, 200);

                int r = 98; // radius
                int ox = 100, oy = 100; // origin

                for (int x = -r; x < r; x++)
                {
                    int height = (int)Math.Sqrt(r * r - x * x);

                    for (int y = -height; y < height; y++)
                    {
                        try
                        {
                            string cc = arr[say].ToString();
                            if (cc == "A")
                                bmp.SetPixel(x + ox, y + oy, Color.Blue);
                            if (cc == "T")
                                bmp.SetPixel(x + ox, y + oy, Color.Yellow);
                            if (cc == "C")
                                bmp.SetPixel(x + ox, y + oy, Color.Red);
                            if (cc == "G")
                                bmp.SetPixel(x + ox, y + oy, Color.Green);
                            if (cc == "N")
                                bmp.SetPixel(x + ox, y + oy, Color.White);
                        }
                        catch { bmp.SetPixel(x + ox, y + oy, Color.White); }
                        say += 1;
                    }
                }

                int c = 2000 + sayy;
                bmp.Save(@"c:\DOKTORA\COVID\Picto2\" + c.ToString() + ".png");
                Console.WriteLine(say.ToString());

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Regex MyRegex = new Regex("(>.*\\|.*)(.*?)", RegexOptions.Compiled);

            int satir = 1;
            string text = System.IO.File.ReadAllText(@"C:\COVID\asia_son_.fasta");

            //// Replace the matched text in the InputText using the replacement pattern
            // string result = MyRegex.Replace(InputText,MyRegexReplace);

            //// Split the InputText wherever the regex matches
            string[] results = MyRegex.Split(text);
            int line_n = 0;
            string my_genome = "";
            foreach (string g in results)
            {
                if (g != "")
                {
                    if (g.StartsWith(">") == false)
                    {
                        my_genome = g.Replace("\r\n", "").Replace("\n", "");
                        if (my_genome.Length > 1000)
                        {
                            if (my_genome.ToUpper().Contains("NNNN") == false)
                            {
                                File.WriteAllText(@"C:\COVID\extract3\" + satir.ToString() + ".genom", my_genome.ToUpper());
                                // textBox1.Text += satir.ToString() + "_" + results[line_n - 2].ToString().Substring(1, results[line_n - 2].ToString().IndexOf(" |"))+"\r\n";
                                textBox1.Text += satir.ToString() + ";" + results[line_n - 2].ToString().Replace("|", ";") + "\r\n";
                                satir += 1;
                            }
                        }
                    }
                }
                line_n += 1;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Regex MyRegex = new Regex("(>.*\\|.*)(.*?)", RegexOptions.Compiled);

            int satir = 1;
            string text = System.IO.File.ReadAllText(@"C:\COVID\all_australia.fasta");

            //// Replace the matched text in the InputText using the replacement pattern
            // string result = MyRegex.Replace(InputText,MyRegexReplace);

            //// Split the InputText wherever the regex matches
            string[] results = MyRegex.Split(text);
            int line_n = 0;
            string my_genome = "";
            foreach (string g in results)
            {
                if (g != "")
                {
                    if (g.StartsWith(">") == false)
                    {
                        
                        
                            my_genome = g.Replace("\r\n", "").Replace("\n", "");
                            if (my_genome.Length > 1000 && results[line_n - 2].ToString().Contains("Australia") == true)
                            {
                                if (my_genome.ToUpper().Contains("NNNN") == false)
                                {
                                    File.WriteAllText(@"C:\COVID\extract4\" + satir.ToString() + ".genom", my_genome.ToUpper());
                                    // textBox1.Text += satir.ToString() + "_" + results[line_n - 2].ToString().Substring(1, results[line_n - 2].ToString().IndexOf(" |"))+"\r\n";
                                    textBox1.Text += satir.ToString() + ";" + results[line_n - 2].ToString().Replace("|", ";") + "\r\n";
                                    satir += 1;
                                }
                            }
                        
                    }
                }
                line_n += 1;
            }
        }
    }
     
}
