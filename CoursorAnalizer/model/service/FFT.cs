using System;

namespace CursorAnalyzer
{
    class FFT
    {
        public static void complexToComplex(int sign, int n, float[] ar, float[] ai)
        {
            var scale = (float) Math.Sqrt(1.0f/n);

            int i, j;

            for (i = j = 0; i < n; ++i)
            {
                if (j >= i)
                {
                    float tempr = ar[j]*scale;
                    float tempi = ai[j]*scale;

                    ar[j] = ar[i]*scale;
                    ai[j] = ai[i]*scale;

                    ar[i] = tempr;
                    ai[i] = tempi;
                }

                int m = n/2;

                while (m >= 1 && j >= m)
                {
                    j -= m;
                    m /= 2;
                }

                j += m;
            }

            int mmax, istep;

            for (mmax = 1, istep = 2*mmax; mmax < n; mmax = istep, istep = 2*mmax)
            {
                float delta = (float)(sign*Math.PI)/(float) mmax;

                for (int k = 0; k < mmax; ++k)
                {
                    float w = (float) k*delta;
                    float wr = (float) Math.Cos(w);
                    float wi = (float) Math.Sin(w);

                    for (i = k;  i < n-1; i += istep)
                    {
                        j = i + mmax;

                        float tr = wr*ar[j] - wi*ai[j];
                        float ti = wr*ai[j] + wi*ar[j];

                        ar[j] = ar[i] - tr;
                        ai[j] = ai[i] - ti;

                        ar[i] += tr;
                        ai[i] += ti;
                    }
                }
            }
        }
    }
}
