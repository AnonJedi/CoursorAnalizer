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
       public static List<Point> Glist = new List<Point>();//траектория мышки
       public static List<List<Point>> CordList = new List<List<Point>>(); 
       public static List<List<double>> discCList = new List<List<double>>(); 
       public static List<double> Cmax = new List<double>(); 
       public static List<double> Size = new List<double>();//размер куба
       public static List<double> Len = new List<double>();//расстояние между центрами
       public static List<double> distance = new List<double>();//длинна траектории
       public static List<DateTime> Sec = new List<DateTime>();//время
       public static double V;//средняя скорость
       public static List<double> T = new List<double>();//формула
       public static double mT = 0;
       public static double mCmax = 0;
       public static Dictionary<string, List<double>> Persons = new Dictionary<string, List<double>>();//список пользователей(не доделан)
       public static List<double> CList = new List<double>(); 
       #endregion

       public static void TimeCursor(int Counter, TextBox t, string name)
       {
           if (Counter > 0)
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

           try
           {
               Persons.Add(name, T);

               foreach (string s in Persons.Keys)
               {
                   for (int i = 0; i < Persons[s].Count; i++)
                   {
                       t.Text += s + " " + Persons[s][i] + "\r\n";
                   }
               }
           }
           catch (ArgumentException)
           {
               
           }
                    
           Glist = new List<Point>();
           Size = new List<double>();
           Len = new List<double>();
           Sec = new List<DateTime>();
           T = new List<double>();
       }

       public static void MidV(DateTime t, int Counter)
       {
           double Temp = 0;
           if (Counter > 0)
           {
               for (int i = 1; i < Glist.Count; i++)
               {
                   Temp += Math.Sqrt(Math.Pow(Glist[i].X - Glist[i - 1].X, 2)) + Math.Sqrt(Math.Pow(Glist[i].Y - Glist[i-1].Y, 2));
               }
           }
           V = Temp*10000000 / t.Ticks;
       }

       public static void SaverParam(int w, int x, int y, int Counter)
       {
           double A, C;
           if (Counter > 0)
           {
               Len.Add(Math.Sqrt(Math.Pow(x + w/2 - Coord.X + w/2, 2)) + Math.Sqrt(Math.Pow(y + w/2 - Coord.Y + w/2, 2)));
               Size.Add(w);
             
               A = (y + w/2 - Coord.Y - w/2)/(x - Coord.X - x/2);
               C = Coord.Y - A*(Coord.Y + w/2);

               if (CordList[Counter-1].Count < 64)
               {
                   foreach (Point p in CordList[Counter-1])
                   {
                       CList.Add(Math.Abs(A*p.X + p.Y + C)/Math.Sqrt(A*A+1));
                   }

                   for (int i = 1; i < CordList[Counter - 1].Count; i++)
                   {
                       distance.Add(Math.Sqrt(Math.Pow(CordList[Counter - 1][i].X - CordList[Counter - 1][i - 1].X, 2)) + Math.Sqrt(Math.Pow(CordList[Counter - 1][i].Y - CordList[Counter - 1][i - 1].Y, 2)));
                   }

                   double max = CList[0];
                   foreach (double d in CList)
                   {
                       if (d > max)
                       {
                           max = d;
                       }
                   }                              

                   Cmax.Add(max);
               }
               else
               {
                   for (int i = 0; i < CordList[Counter-1].Count;)
                   {
                       CList.Add(Math.Abs(A * CordList[Counter - 1][i].X + CordList[Counter - 1][i].Y + C) / Math.Sqrt(A * A + 1));
                       i += (int)(CordList[Counter - 1].Count/64);
                   }

                   for (int i = 64; i < CordList[Counter - 1].Count; i+=64)
                   {
                       distance.Add(Math.Sqrt(Math.Pow(CordList[Counter - 1][i].X - CordList[Counter - 1][i - 64].X, 2)) + Math.Sqrt(Math.Pow(CordList[Counter - 1][i].Y - CordList[Counter - 1][i - 64].Y, 2)));
                   }
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
    }
}
