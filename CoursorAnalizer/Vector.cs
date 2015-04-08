using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace CoursorAnalizer
{
   static class Vector
    {
       public static Point Coord = new Point() ;//коорд.
       public static List <Point> Glist=new List<Point>();//траектория мышки
       public static List<double> Size = new List<double>();//размер куба
       public static List<double> Len = new List<double>();//расстояние между центрами
       public static List<DateTime> Sec = new List<DateTime>();//время
       public static double V;//средняя скорость
       public static List <double> T= new List<double>();//формула
       public static Dictionary<string,List<double>> Persons= new Dictionary<string,List<double>>();//список пользователей(не доделан)

       public static void TimeCursor(int Counter,TextBox t)
       {
           if (Counter > 0)
           {
               for (int i = 0; i < Len.Count; i++)
               {
                   T.Add(V * Math.Log(Len[i] / Size[i] + 1, 2));
               }
           }
           Persons.Add("1", T);
           Glist=new List<Point>();
           Size = new List<double>();
           Len = new List<double>();
           Sec = new List<DateTime>();
           T = new List<double>();
           foreach (string s in Persons.Keys)
           {
               for (int i = 0; i < Persons[s].Count; i++)
               {
                   t.Text += s + " " + Persons[s][i] + "\r\n";
               }
            }
       }


       public static void MidV(DateTime t,int Counter)
       {
           double Temp = 0;
           if (Counter > 0)
           {
               for (int i = 1; i < Glist.Count;i++ )
               {
                   Temp += Math.Sqrt(Math.Pow(Glist[i].X - Glist[i - 1].X, 2)) + Math.Sqrt(Math.Pow(Glist[i].Y - Glist[i-1].Y, 2));
               }
           }
           V = Temp*10000000 / t.Ticks;
       }

       public static void SaverParam(int w,int x,int y,int Counter)
       {
           if (Counter > 0)
           {

               Len.Add(Math.Sqrt(Math.Pow(x+w/2 - Coord.X+w/2, 2)) + Math.Sqrt(Math.Pow(y+w/2 - Coord.Y+w/2, 2)));
               Size.Add(w);
             
           }


           Coord.X = x;
           Coord.Y = y;
        

       }
       public static void Trecker(MouseEventArgs e)
       {
           Coord = e.Location;
           Glist.Add(Coord);

       }
    }
}
