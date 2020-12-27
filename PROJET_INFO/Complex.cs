using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_INFO
{
    class Complex
    {
        public double x; // réel
        public double y; // imaginaire

        public Complex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Calcul le carré d'un nombre complexe.
        /// </summary>
        public void Carré()
        {
            double rslt = (x * x) - (y * y);
            y = 2 * x * y;
            x = rslt;
        }

        public double Module()
        {
            return Math.Sqrt((x * x) + (y * y));
        }

        public void Addition(Complex z)
        {
            x += z.x;
            y += z.y;
        }
    }
}
