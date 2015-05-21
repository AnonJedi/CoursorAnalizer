using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace CoursorAnalizer
{
   static class Vector
   {
       #region Var

       private static Point Coord = new Point();//коорд.
       public static List<Point> Glist;//траектория мышки
       public static List<List<Point>> CordList = new List<List<Point>>();
       private static List<List<double>> discCList = new List<List<double>>(); 
       public static List<double> Cmax = new List<double>(); 
       public static List<double> Cmid = new List<double>();
       private static List<double> Size;//размер куба
       public static List<double> Len;//расстояние между центрами
       private static List<double> distance = new List<double>();//длинна траектории
       public static List<DateTime> Sec;//время
       private static double V;//средняя скорость
       public static List<double> T;//формула
       public static double mT;
       public static double mCmax;
       public static double mCmid;
       public static double dT;
       public static double dCmid;
       public static double dCmax;
       public static List<string> users;//список пользователей
       private static List<double> CList = new List<double>(); 
       public static List<float[]> ampList;
       public static float[] ampM;
       public static float[] ampD;
       public static float[] allAmp;
 
       #endregion

       public static void ReadBase()
       {
            users = Saver.ReadDB();
       }

       public static void MathExpectation(int counter)
       {
           if (counter > 0)
           {
               mT = 0;
               mCmax = 0;
               mCmid = 0;
               ampM = new float[10];
               allAmp = new float[2];
               allAmp[0] = 0;
               allAmp[1] = 0;
               for (int i = 0; i < 10; i++) ampM[i] = 0;

               for (int i = 0; i < Len.Count; i++)
               {
                   T.Add(V * Math.Log(Len[i] / Size[i] + 1, 2));
                   mT += T[i];
                   mCmax += Cmax[i];
               }

               foreach (double d in Cmid) mCmid += d;
               foreach (float[] floats in ampList)
               {
                   ampM[0] += floats[0];
                   ampM[1] += floats[1];
                   ampM[2] += floats[2];
                   ampM[3] += floats[3];
                   ampM[4] += floats[4];
                   ampM[5] += floats[5];
                   ampM[6] += floats[6];
                   ampM[7] += floats[7];
                   ampM[8] += floats[8];
                   ampM[9] += floats[9];
               }

               var count = 0;
               foreach (float[] floats in ampList)
               {
                   foreach (float f in floats)
                   {
                       count++;
                       allAmp[0] += f;
                   }
               }
               allAmp[0] = allAmp[0]/count;

               mT = mT/Len.Count;
               mCmax = mCmax/Len.Count;
               mCmid = mCmid/Cmid.Count;

               for (int i = 0; i < 10; i++) ampM[i] = ampM[i]/ampList.Count;
           }      
       }

       public static void Variance(int counter)
       {
           if (counter > 0)
           {
               dCmid = 0;
               for (int i = 1; i < Cmid.Count; i++)
               {
                   dCmid = Math.Sqrt((i - 1) * dCmid * dCmid / i + Math.Pow(Cmid[i] - mCmid, 2));
               }

               dCmax = 0;          
               for (int i = 1; i < Cmax.Count; i++)
               {
                   dCmax = Math.Sqrt((i - 1) * dCmax * dCmax / i + Math.Pow(Cmax[i] - mCmax, 2));
               }

               dT = 0;              
               for (int i = 1; i < T.Count; i++)
               {
                   dT = Math.Sqrt((i - 1) * dT * dT / i + Math.Pow(T[i] - mT, 2));
               }

               List<float> temp = new List<float>();
               foreach (float[] floats in ampList)
               {
                   foreach (float f in floats)
                   {
                       temp.Add(f);
                   }
               }

               for (int i = 1; i < temp.Count; i++)
               {
                   allAmp[1] = (float)Math.Sqrt((i - 1) * allAmp[1] * allAmp[1] / i + Math.Pow(temp[i] - allAmp[0], 2));                   
               }

               ampD = new float[10];
               for (int i = 0; i < 10; i++) ampD[i] = 0;

               for (int i = 1; i < ampList.Count; i++)
               {
                   ampD[0] = (float)Math.Sqrt((i - 1) * ampD[0] * ampD[0] / i + Math.Pow(ampList[i][0] - ampM[0], 2));
                   ampD[1] = (float)Math.Sqrt((i - 1) * ampD[1] * ampD[1] / i + Math.Pow(ampList[i][1] - ampM[1], 2));
                   ampD[2] = (float)Math.Sqrt((i - 1) * ampD[2] * ampD[2] / i + Math.Pow(ampList[i][2] - ampM[2], 2));
                   ampD[3] = (float)Math.Sqrt((i - 1) * ampD[3] * ampD[3] / i + Math.Pow(ampList[i][3] - ampM[3], 2));
                   ampD[4] = (float)Math.Sqrt((i - 1) * ampD[4] * ampD[4] / i + Math.Pow(ampList[i][4] - ampM[4], 2));
                   ampD[5] = (float)Math.Sqrt((i - 1) * ampD[5] * ampD[5] / i + Math.Pow(ampList[i][5] - ampM[5], 2));
                   ampD[6] = (float)Math.Sqrt((i - 1) * ampD[6] * ampD[6] / i + Math.Pow(ampList[i][6] - ampM[6], 2));
                   ampD[7] = (float)Math.Sqrt((i - 1) * ampD[7] * ampD[7] / i + Math.Pow(ampList[i][7] - ampM[7], 2));
                   ampD[8] = (float)Math.Sqrt((i - 1) * ampD[8] * ampD[8] / i + Math.Pow(ampList[i][8] - ampM[8], 2));
                   ampD[9] = (float)Math.Sqrt((i - 1) * ampD[9] * ampD[9] / i + Math.Pow(ampList[i][9] - ampM[9], 2));
               }
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
           Cmax = new List<double>();
           Cmid = new List<double>();
       }
    }
}
