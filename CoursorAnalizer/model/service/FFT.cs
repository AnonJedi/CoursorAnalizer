using System;

namespace CursorAnalyzer
{
    /// <summary>
    /// Fourier transformator
    /// </summary>
    class FFT
    {
        /// <summary>
        /// Method for transformation simple arrays to complex
        /// </summary>
        /// <param name="sign">Sign of array members</param>
        /// <param name="n">Size of array</param>
        /// <param name="ar"></param>
        /// <param name="ai"></param>
        public static void ComplexToComplex(int sign, int n, float[] ar, float[] ai)
        {
            var scale = (float) Math.Sqrt(1.0f/n);

            int i, j;

            for (i = j = 0; i < n; ++i)
            {
                if (j >= i)
                {
                    var tempr = ar[j] * scale;
                    var tempi = ai[j] * scale;

                    ar[j] = ar[i] * scale;
                    ai[j] = ai[i] * scale;

                    ar[i] = tempr;
                    ai[i] = tempi;
                }

                var m = n/2;

                while (m >= 1 && j >= m)
                {
                    j -= m;
                    m /= 2;
                }

                j += m;
            }

            int mmax, istep;

            for (mmax = 1, istep = 2; mmax < n; mmax = istep, istep = 2 * mmax)
            {
                var delta = (float)(sign * Math.PI) / mmax;

                for (var k = 0; k < mmax; ++k)
                {
                    var w = k * delta;
                    var wr = (float) Math.Cos(w);
                    var wi = (float) Math.Sin(w);

                    for (i = k;  i < n-1; i += istep)
                    {
                        j = i + mmax;

                        var tr = wr * ar[j] - wi * ai[j];
                        var ti = wr * ai[j] + wi * ar[j];

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
