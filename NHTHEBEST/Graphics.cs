using System.Threading;
using System.Drawing;
using System;
using System.Collections.Generic;


namespace NHTHEBEST
{
    namespace Graphics
    {
        public class SortImgs
        {
            private Bitmap bitmap;
            private Color imgcolor;
            public int Resolution { get; private set; }
            public int Height { get; private set; }
            public int Width { get; private set; }
            public Color GetColor(Image image)
            {
                bitmap = new Bitmap(image);
                Thread thread = new Thread(process);
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                thread.Join();
                return imgcolor;
            }
            public List<Color> GetColors(List<Image> images)
            {
                List<Color> buff = new List<Color>();
                foreach (var item in images)
                {
                    buff.Add(GetColor(item));
                }
                return buff;
            }
            public Color[] GetColors(Image[] images)
            {
                List<Color> buff = new List<Color>();
                foreach (var item in images)
                {
                    buff.Add(GetColor(item));
                }
                return buff.ToArray();
            }
            private void process()
            {
                Height = bitmap.Height - 1;
                Width = bitmap.Width - 1;
                Resolution = Height * Width;
                int a = 0;
                int r = 0;
                int g = 0;
                int b = 0;
                int pixals_done = 0;
                int lines_done = 0;
                for (int h = 1; h <= Height; h++)
                {
                    for (int w = 1; w <= Width; w++)
                    {
                        var temp = bitmap.GetPixel(w, h);
                        a += Convert.ToInt32(temp.A);
                        r += Convert.ToInt32(temp.R);
                        g += Convert.ToInt32(temp.G);
                        b += Convert.ToInt32(temp.B);
                        pixals_done++;
                        pixaldone(pixals_done);

                    }
                    lines_done++;
                    linedone(lines_done);
                }
                a = a / Resolution;
                r = r / Resolution;
                g = g / Resolution;
                b = b / Resolution;
                Resolution = 0;
                imgcolor = Color.FromArgb(a, r, g, b);
            }
            public event EventHandler<LineEventArgs> Line_done;
            public event EventHandler<PixalEventArgs> Pixal_done;
            protected virtual void pixaldone(int pixals)
            {
                EventHandler<PixalEventArgs> pixaldone = Pixal_done;
                PixalEventArgs e = new PixalEventArgs();
                e.Pixals = pixals;
                if (pixaldone != null)
                {
                    pixaldone(this, e);
                }
            }
            protected virtual void linedone(int lines)
            {
                EventHandler<LineEventArgs> linedone = Line_done;
                LineEventArgs e = new LineEventArgs();
                e.Lines = lines;
                if (linedone != null)
                {
                    linedone(this, e);
                }
            }

        }
        public class ConsoleFX
        {
            private static int LogNum = 1;
            public static void HashPrecentBar(int Value, int Off)
            {
                string line = "[";
                int width = Console.WindowWidth - 11;
                double p = (double)Value / (double)Off;
                double hash = Math.Round(p * (double)width);
                p = Math.Round(p * (double)100);
                for (int i = 0; i <= width; i++)
                {
                    if (i <= hash)
                    {
                        line += "#";
                    }
                    else
                    {
                        line += " ";
                    }
                }
                line += "]  ";
                if (p <= 9)
                    line += "  " + p.ToString() + "%";
                else if (p <= 99)
                    line += " " + p.ToString() + "%";
                else
                    line += p.ToString() + "%";
                Console.Write(line + "\r");
            }
            public static void ColorLog(object data)
            {
                ColorLog(data, LogStatus.OK);
            }
            public static void ColorLog(object data, LogStatus status)
            {
                string LogText = data.ToString();
                Console.Write(" [  ");

                int LogLenth = LogText.Length;
                int width = Console.WindowWidth - 22;

                if (status == LogStatus.OK)
                {
                    ConsoleColor before = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" OK ");
                    Console.ForegroundColor = before;

                }
                else if (status == LogStatus.Fail)
                {
                    ConsoleColor before = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Fail");
                    Console.ForegroundColor = before;
                }
                else if (status == LogStatus.Warning)
                {
                    ConsoleColor before = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Warn");
                    Console.ForegroundColor = before;
                }
                Console.Write("  ]  ");
                int l = width - LogLenth;
                Console.Write(LogText);
                for (int i = 1; i <= l; i++)
                {
                    Console.Write(" ");
                }
                string num;
                if (LogNum <= 9)
                    num = "  " + LogNum.ToString() + "  ";
                else if (LogNum <= 99)
                    num = "  " + LogNum.ToString() + " ";
                else
                    num = " " + LogNum.ToString() + " ";
                Console.Write(" [" + num + "] ");
                LogNum++;
            }
        }
        public enum LogStatus
        {
            OK, Fail, Warning
        }
        public class PixalEventArgs : EventArgs
        {
            public int Pixals { get; set; }
        }
        public class LineEventArgs : EventArgs
        {
            public int Lines { get; set; }
        }
    }
}
