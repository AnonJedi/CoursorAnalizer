using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace CoursorAnalizer
{
   static class Vector
   {
       #region Var
       public static Point Coord = new Point();//коорд.
       public static List<Point> Glist;//траектория мышки
       public static List<List<Point>> CordList = new List<List<Point>>(); 
       public static List<List<double>> discCList = new List<List<double>>(); 
       public static List<double> Cmax = new List<double>(); 
       public static List<double> Cmid = new List<double>(); 
       public static List<double> Size;//размер куба
       public static List<double> Len;//расстояние между центрами
       public static List<double> distance = new List<double>();//длинна траектории
       public static List<DateTime> Sec;//время
       public static double V;//средняя скорость
       public static List<double> T;//формула
       public static double mT = 0;
       public static double mCmax = 0;
       public static List<string> users;//список пользователей
       public static List<double> CList = new List<double>(); 
       public static List<float[]> ampList; 
       #endregion

       public static void ReadBase()
       {
            users = Saver.ReadDB();
       }

       public static void TimeCursor(int counter)
       {
           if (counter > 0)
           {
               for (int i = 0; i < Len.Count; i++)
               {
                   T.Add(V * Math.Log(Len[i] / Size[i] + 1, 2));
                   mT += T[i];

                   mCmax += Cmax[i];
               }

               mT = mT/T.Count;
               mCmax = mCmax/Cmax.Count;
           }      
       }

       public static void MidV(DateTime t, int counter)
       {         
           if (counter > 0)
           {
               double temp = 0;
               for (int i = 1; i < Glist.Count; i++)
               {
                   temp += Math.Sqrt(Math.Pow(Glist[i].X - Glist[i - 1].X, 2)) + Math.Sqrt(Math.Pow(Glist[i].Y - Glist[i-1].Y, 2));
               }
               V = temp * 10000000 / t.Ticks;              
           }
           
       }

       public static void SaverParam(int w, int x, int y, int counter)
       {
           double A, C;
           if (counter > 0)
           {
               Len.Add(Math.Sqrt(Math.Pow(x + w/2 - Coord.X + w/2, 2)) + Math.Sqrt(Math.Pow(y + w/2 - Coord.Y + w/2, 2)));
               Size.Add(w);
               Cmid.Add(0);
             
               A = (y + w/2 - Coord.Y - w/2)/(x - Coord.X - x/2);
               C = Coord.Y - A*(Coord.Y + w/2);

               if (CordList[counter-1].Count < 64)
               {
                   foreach (Point p in CordList[counter-1])
                   {
                       CList.Add(Math.Abs(A*p.X + p.Y + C)/Math.Sqrt(A*A+1));
                       Cmid[Cmid.Count - 1] += CList[CList.Count - 1];
                   }
                   Cmid[Cmid.Count - 1] = Cmid[Cmid.Count - 1]/CList.Count;

                   for (int i = 1; i < CordList[counter - 1].Count; i++)
                   {
                       distance.Add(Math.Sqrt(Math.Pow(CordList[counter - 1][i].X - CordList[counter - 1][i - 1].X, 2)) + Math.Sqrt(Math.Pow(CordList[counter - 1][i].Y - CordList[counter - 1][i - 1].Y, 2)));
                   }
               }
               else
               {
                   for (int i = 0; i < CordList[counter-1].Count;)
                   {
                       CList.Add(Math.Abs(A * CordList[counter - 1][i].X + CordList[counter - 1][i].Y + C) / Math.Sqrt(A * A + 1));
                       i += (int)(CordList[counter - 1].Count/64);
                       Cmid[Cmid.Count - 1] += CList[CList.Count - 1];
                   }
                   Cmid[Cmid.Count - 1] = Cmid[Cmid.Count - 1] / CList.Count;

                   for (int i = 64; i < CordList[counter - 1].Count; i+=64)
                   {
                       distance.Add(Math.Sqrt(Math.Pow(CordList[counter - 1][i].X - CordList[counter - 1][i - 64].X, 2)) + Math.Sqrt(Math.Pow(CordList[counter - 1][i].Y - CordList[counter - 1][i - 64].Y, 2)));
                   }
               }

               double max = CList[0];
               foreach (double d in CList) if (d > max) max = d;              

               Cmax.Add(max);

               int n = 0;

               if (distance.Count >= 32 && distance.Count < 64) n = 64;
               else if (distance.Count >= 64 && distance.Count < 128) n = 128;

               if (n != 0)
               {
                   float[] ar = new float[n];
                   float[] ai = new float[n];
                   float[] amp;

                   for (int i = 0; i < n; i++)
                   {
                       if (i < distance.Count) ar[i] = (float)distance[i];
                       else ar[i] = 0;
                       ai[i] = 0;
                   }

                   FFT.complexToComplex(-1, distance.Count, ar, ai);

                   for (int i = 0; i < distance.Count; i++)
                   {
                       ar[ar.Length - i - 1] = ar[ar.Length - i - 1] - ar[i];
                       ai[ai.Length - i - 1] = ai[ai.Length - i - 1] + ai[i];
                   }

                   double energy = 0;

                   float[] am = new float[distance.Count];

                   for (int i = 0; i < am.Length; i++)
                   {
                       am[i] = ((ar[i] * ar[i] + ai[i] * ai[i]) / am.Length);
                       energy += am[i];
                   }

                   amp = new float[distance.Count];

                   for (int i = 0; i < distance.Count; i++) amp[i] = am[i] / (float)energy;                 

                   ampList.Add(amp);
               }
               
               discCList.Add(CList);
               CList = new List<double>();
           }

           Coord.X = x;
           Coord.Y = y;
       }

       public static void Trecker(MouseEventArgs e)
       {
           Glist.Add(e.Location);
       }

       public static void RefreshList(List<Point> l)
       {
           l = new List<Point>();
       }

       public static void Refresher()
       {
           Glist = new List<Point>();
           Size = new List<double>();
           Len = new List<double>();
           Sec = new List<DateTime>();
           T = new List<double>();
           ampList = new List<float[]>();
       }

       public static void ShowData(TextBox textBox)
       {

           textBox.Text += "Cmid:\r\n";
           foreach (double d in Cmid) textBox.Text += d + "\r\n";
           

           textBox.Text += "T:\r\n";
           foreach (double d in T) textBox.Text += d + "\r\n";

           textBox.Text += "First 3 harmonics:\r\n";
           foreach (float[] floats in ampList) textBox.Text += floats[0] + "  " + floats[1] + "  " + floats[2] + "\r\n";          
       }
    }
}
