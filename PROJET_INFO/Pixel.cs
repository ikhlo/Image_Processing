using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_INFO
{
    class Pixel
    {
        int red;
        int green;
        int blue;

        public Pixel(int red, int green, int blue)
        {
            while (red > 255) red %= 255;
            while(green > 255) green %= 255;
            while (blue > 255) blue %= 255;

            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public void Gris()
        {
            int gris = (red + green + blue) / 3;
            this.red = gris;
            this.green = gris;
            this.blue = gris;
        }

        public void Noir()
        {
            red = 0;
            green = 0;
            blue = 0;
        }

        public void Blanc()
        {
            red = 255;
            green = 255;
            blue = 255;
        }

        public void NoiretBlanc ()
        {
            int gris = (red + green + blue) / 3;
            if (gris < 128) Noir();
            else Blanc();
        }

        public int Red { get { return red; } set { red = value; } }
        public int Green { get { return green; } set { green = value; } }
        public int Blue { get { return blue; } set { blue = value; } }

    }
}
