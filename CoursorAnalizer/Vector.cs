using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace CoursorAnalizer
{
    public class Vector
    {
        #region Var

        //текущие координаты мыши
        private Point mousePoint;
        public Point MousePoint
        {
            get { return mousePoint; }
            set { mousePoint = value; }
        }

        //траектория мышки
        private List<Point> mouseTrack;
        public List<Point> MouseTrack
        {
            get { return mouseTrack; }
            set { mouseTrack = value; }
        }

        //хранилище траекторий мыши
        private List<List<Point>> mouseTracksContainer;
        public List<List<Point>> MouseTracksContainer
        {
            get { return mouseTracksContainer; }
            set { mouseTracksContainer = value; }
        }

        //хранилище текущего параметра C
        private List<List<double>> tracksDiffContainer;
        public List<List<double>> TracksDiffContainer
        {
            get { return tracksDiffContainer; }
            set { tracksDiffContainer = value; }
        }

        //хранилище максимальных отклонений траекторий
        private List<double> maxDiffTracks;
        public List<double> MaxDiffTracks
        {
            get { return maxDiffTracks; }
            set { maxDiffTracks = value; }
        }

        //хранилище средних отклонений траекторий
        private List<double> midDiffTracks;
        public List<double> MidDiffTracks
        {
            get { return midDiffTracks; }
            set { midDiffTracks = value; }
        }

        //размер стороны куба
        private List<double> shapeSize;
        public List<double> ShapeSize
        {
            get { return shapeSize; }
            set { shapeSize = value; }
        }

        //хранилище расстояний между центрами фигур
        private List<double> lensContainer;
        public List<double> LensContainer
        {
            get { return lensContainer; }
            set { lensContainer = value; }
        }

        //длинна траектории мыши
        private List<double> distanceLen;
        public List<double> DistanceLen
        {
            get { return distanceLen; }
            set { distanceLen = value; }
        }

        //хранилище времён между кликами
        private List<DateTime> clickTimeContainer;
        public List<DateTime> ClickTimeContainer
        {
            get { return clickTimeContainer; }
            set { clickTimeContainer = value; }
        }

        //средняя скорость движения мыши
        private double midMouseSpeed;
        public double MidMouseSpeed
        {
            get { return midMouseSpeed; }
            set { midMouseSpeed = value; }
        }

        //хранилище Т
        private List<double> t;
        public List<double> T
        {
            get { return t; }
            set { t = value; }
        }

        //математическое ожидание значений Т
        private double expirationT;
        public double ExpirationT
        {
            get { return expirationT; }
            set { expirationT = value; }
        }

        //математическое ожидание максимальных отклонений траектории мыши от идеальной траектории
        private double maxDiffTracksExpiration;
        public double MaxDiffTracksExpiration
        {
            get { return maxDiffTracksExpiration; }
            set { maxDiffTracksExpiration = value; }
        }

        //математическое ожидание средних отклонений траектории мыши от идеальной траектории
        private double midDiffTracksExpiration;
        public double MidDiffTracksExpiration
        {
            get { return midDiffTracksExpiration; }
            set { midDiffTracksExpiration = value; }
        }

        //среднеквадратичное отклонение параметра T
        private double tDispertion;
        public double TDispertion
        {
            get { return tDispertion; }
            set { tDispertion = value; }
        }

        //среднеквадратичное отклонение среднего отклонения траекторий
        private double midDiffTracksDispertion;
        public double MidDiffTracksDispertion
        {
            get { return midDiffTracksDispertion; }
            set { midDiffTracksDispertion = value; }
        }

        //среднеквадратичное отклонение максимального отклонения траекторий
        private double maxDiffTracksDispersion;
        public double MaxDiffTracksDispersion
        {
            get { return maxDiffTracksDispersion; }
            set { maxDiffTracksDispersion = value; }
        }

        // TODO: перетащить в локальную облость
        public static List<string> users;

        //хранилище отклонений координаты мыши от идеальной координаты
        private List<double> diffContainer;
        public List<double> DiffContainer
        {
            get { return diffContainer; }
            set { diffContainer = value; }
        }

        //хранилище амплитуд ряда распределения отклонений мыши
        private List<float[]> ampContainer;
        public List<float[]> AmpContainer
        {
            get { return ampContainer; }
            set { ampContainer = value; }
        }

        //математическое ожидание амплитуд отклонения
        private float[] ampExpiration;
        public float[] AmpExpiration
        {
            get { return ampExpiration; }
            set { ampExpiration = value; }
        }

        //дисперсия амплитуд отклонения
        private float[] ampDispertion;
        public float[] AmpDispertion
        {
            get { return ampDispertion; }
            set { ampDispertion = value; }
        }

        //
        public static float[] allAmp;
        public static List<double> energyList = new List<double>();
        public static List<DateTime> timeList;
        public static List<double> V = new List<double>();
 
        #endregion

        public static void ReadBase()
        {
            users = Saver.ReadDB();
        }

        public void MathExpectation(int counter)
       {
           if (counter > 0)
           {
               expirationT = 0;
               maxDiffTracksExpiration = 0;
               midDiffTracksExpiration = 0;
               ampExpiration = new float[10];
               allAmp = new float[2];
               allAmp[0] = 0;
               allAmp[1] = 0;
               for (int i = 0; i < 10; i++) ampExpiration[i] = 0;

               for (int i = 0; i < lensContainer.Count; i++)
               {
                   t.Add(midMouseSpeed * Math.Log(lensContainer[i] / shapeSize[i] + 1, 2));
                   expirationT += t[i];
                   maxDiffTracksExpiration += maxDiffTracks[i];
               }

               foreach (double d in MidDiffTracks) midDiffTracksExpiration += d;
               foreach (float[] floats in ampContainer)
               {
                   ampExpiration[0] += floats[0];
                   ampExpiration[1] += floats[1];
                   ampExpiration[2] += floats[2];
                   ampExpiration[3] += floats[3];
                   ampExpiration[4] += floats[4];
                   ampExpiration[5] += floats[5];
                   ampExpiration[6] += floats[6];
                   ampExpiration[7] += floats[7];
                   ampExpiration[8] += floats[8];
                   ampExpiration[9] += floats[9];
               }

               var count = 0;
               foreach (float[] floats in ampContainer)
               {
                   foreach (float f in floats)
                   {
                       count++;
                       allAmp[0] += f;
                   }
               }
               allAmp[0] = allAmp[0]/count;

               expirationT = expirationT/lensContainer.Count;
               maxDiffTracksExpiration = maxDiffTracksExpiration/lensContainer.Count;
               midDiffTracksExpiration = midDiffTracksExpiration / MidDiffTracks.Count;

               for (int i = 0; i < 10; i++) ampExpiration[i] = ampExpiration[i]/ampContainer.Count;
           }      
       }

        public void Variance(int counter)
       {
           if (counter > 0)
           {
               midDiffTracksDispertion = 0;
               for (int i = 1; i < MidDiffTracks.Count; i++)
               {
                   midDiffTracksDispertion = Math.Sqrt((i - 1) * midDiffTracksDispertion * midDiffTracksDispertion
                       / i + Math.Pow(MidDiffTracks[i] - midDiffTracksExpiration, 2));
               }

               maxDiffTracksDispersion = 0;          
               for (int i = 1; i < maxDiffTracks.Count; i++)
               {
                   maxDiffTracksDispersion = Math.Sqrt((i - 1) * maxDiffTracksDispersion * maxDiffTracksDispersion
                       / i + Math.Pow(maxDiffTracks[i] - maxDiffTracksExpiration, 2));
               }

               tDispertion = 0;              
               for (int i = 1; i < t.Count; i++)
               {
                   tDispertion = Math.Sqrt((i - 1) * tDispertion * tDispertion / i + Math.Pow(t[i] - expirationT, 2));
               }

               List<float> temp = new List<float>();
               foreach (float[] floats in ampContainer)
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

               ampDispertion = new float[10];
               for (int i = 0; i < 10; i++) ampDispertion[i] = 0;

               for (int i = 1; i < ampContainer.Count; i++)
               {
                   ampDispertion[0] = (float)Math.Sqrt((i - 1) * ampDispertion[0] * ampDispertion[0]
                       / i + Math.Pow(ampContainer[i][0] - ampExpiration[0], 2));
                   ampDispertion[1] = (float)Math.Sqrt((i - 1) * ampDispertion[1] * ampDispertion[1]
                       / i + Math.Pow(ampContainer[i][1] - ampExpiration[1], 2));
                   ampDispertion[2] = (float)Math.Sqrt((i - 1) * ampDispertion[2] * ampDispertion[2] 
                       / i + Math.Pow(ampContainer[i][2] - ampExpiration[2], 2));
                   ampDispertion[3] = (float)Math.Sqrt((i - 1) * ampDispertion[3] * ampDispertion[3] 
                       / i + Math.Pow(ampContainer[i][3] - ampExpiration[3], 2));
                   ampDispertion[4] = (float)Math.Sqrt((i - 1) * ampDispertion[4] * ampDispertion[4] 
                       / i + Math.Pow(ampContainer[i][4] - ampExpiration[4], 2));
                   ampDispertion[5] = (float)Math.Sqrt((i - 1) * ampDispertion[5] * ampDispertion[5]
                       / i + Math.Pow(ampContainer[i][5] - ampExpiration[5], 2));
                   ampDispertion[6] = (float)Math.Sqrt((i - 1) * ampDispertion[6] * ampDispertion[6]
                       / i + Math.Pow(ampContainer[i][6] - ampExpiration[6], 2));
                   ampDispertion[7] = (float)Math.Sqrt((i - 1) * ampDispertion[7] * ampDispertion[7] 
                       / i + Math.Pow(ampContainer[i][7] - ampExpiration[7], 2));
                   ampDispertion[8] = (float)Math.Sqrt((i - 1) * ampDispertion[8] * ampDispertion[8]
                       / i + Math.Pow(ampContainer[i][8] - ampExpiration[8], 2));
                   ampDispertion[9] = (float)Math.Sqrt((i - 1) * ampDispertion[9] * ampDispertion[9]
                       / i + Math.Pow(ampContainer[i][9] - ampExpiration[9], 2));
               }
           }
       }

        public void MidV(DateTime t, int counter)
       {         
           if (counter > 0)
           {
               double temp = 0;
               for (int i = 1; i < MouseTrack.Count; i++)
               {
                   temp += Math.Sqrt(Math.Pow(MouseTrack[i].X - MouseTrack[i - 1].X, 2)) 
                       + Math.Sqrt(Math.Pow(MouseTrack[i].Y - MouseTrack[i-1].Y, 2));
               }
               midMouseSpeed = temp * 10000000 / t.Ticks;              
           }
           
       }

        public void SaverParam(int w, int x, int y, int counter, DateTime time)
        {
            if (counter > 0)
            {
                double a;
                double C;
                try
                {
                    a = (y + w / 2 - mousePoint.Y - w / 2) / (x - mousePoint.X - x / 2);
                    C = mousePoint.Y - a * (mousePoint.Y + w / 2);
                }
                catch (Exception e)
                {
                    a = 0;
                    C = mousePoint.Y - a * (mousePoint.Y + w / 2);
                }
               
                if (mouseTracksContainer[counter - 1].Count >= 128)
                {
                    lensContainer.Add(Math.Sqrt(Math.Pow(x + w / 2 - mousePoint.X + w / 2, 2)) 
                        + Math.Sqrt(Math.Pow(y + w / 2 - mousePoint.Y + w / 2, 2)));
                    shapeSize.Add(w);
                    MidDiffTracks.Add(0);

                    for (int i = 0; i < mouseTracksContainer[counter - 1].Count; )
                    {
                        diffContainer.Add(Math.Abs(a * mouseTracksContainer[counter - 1][i].X + 
                            mouseTracksContainer[counter - 1][i].Y + C) / Math.Sqrt(a * a + 1));
                        i += (int)(mouseTracksContainer[counter - 1].Count / 64);
                        MidDiffTracks[MidDiffTracks.Count - 1] += diffContainer[diffContainer.Count - 1];
                    }
                    MidDiffTracks[MidDiffTracks.Count - 1] = MidDiffTracks[MidDiffTracks.Count - 1] / diffContainer.Count / 
                        lensContainer[lensContainer.Count - 1];
                    
                    for (int i = 64; i < mouseTracksContainer[counter - 1].Count; i += 64)
                    {
                        distanceLen.Add(Math.Sqrt(Math.Pow(mouseTracksContainer[counter - 1][i].X - 
                            mouseTracksContainer[counter - 1][i - 64].X, 2)) + 
                            Math.Sqrt(Math.Pow(mouseTracksContainer[counter - 1][i].Y - 
                            mouseTracksContainer[counter - 1][i - 64].Y, 2)));
                    }
                }
                else return;

                double max = diffContainer[0];
                foreach (double d in diffContainer) if (d > max) max = d;
                 
                maxDiffTracks.Add(max / lensContainer[lensContainer.Count - 1]);
                
                int n = 128;
                double temp = 0;
                 
                float[] ar = new float[n];
                float[] ai = new float[n];
                float[] amp;

                for (int i = 0; i < n; i++)
                {
                    if (i < distanceLen.Count) ar[i] = (float)distanceLen[i];
                    else ar[i] = 0;
                    ai[i] = 0;
                }

                FFT.complexToComplex(-1, n, ar, ai);

                float[] am = new float[n];
                double energy = 0;
                for (int i = 0; i < n; i++)
                {
                    ar[ar.Length - i - 1] = ar[ar.Length - i - 1] - ar[i];
                    ai[ai.Length - i - 1] = ai[ai.Length - i - 1] + ai[i];
                    am[i] = ((ar[i] * ar[i] + ai[i] * ai[i]) / am.Length);
                }

                for (int i = 0; i < distanceLen.Count; i++)
                {
                    temp += distanceLen[i];
                    energy += Math.Pow(distanceLen[i], 2); 
                }

                energyList.Add(energy);

                amp = new float[n];

                for (int i = 0; i < n; i++) amp[i] = am[i] / (float)energy;                 

                ampContainer.Add(amp);
               
                V.Add(temp * 10000000 / time.Ticks / lensContainer[lensContainer.Count - 1]);
                tracksDiffContainer.Add(diffContainer);
                diffContainer = new List<double>();
            }

            this.MousePoint = new Point(x, y);
        }

        public void Trecker(MouseEventArgs e)
       {
           MouseTrack.Add(e.Location);
       }

        public void RefreshList(List<Point> l)
       {
           l = new List<Point>();
       }

        public void Refresher()
       {
           MouseTrack = new List<Point>();
           shapeSize = new List<double>();
           lensContainer = new List<double>();
           clickTimeContainer = new List<DateTime>();
           t = new List<double>();
           ampContainer = new List<float[]>();
           maxDiffTracks = new List<double>();
           Cmid = new List<double>();
           energyList = new List<double>();
           timeList = new List<DateTime>();
           V = new List<double>();
       }
    }
}
