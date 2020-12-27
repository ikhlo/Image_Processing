using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PROJET_INFO
{
    class MyImage
    {
        byte[] myfile;
        byte[] typeimageb = new byte[2]; string typeimage;
        byte[] taillefichierb = new byte[4]; int taillefichier;
        byte[] tailleoffsetb = new byte[4]; int tailleoffset;
        byte[] largeurimageb = new byte[4]; int largeurimage;
        byte[] hauteurimageb = new byte[4]; int hauteurimage;
        byte[] nboctetparcolorb = new byte[2]; int nboctetparcolor;
        byte[] imageb;
        Pixel[,] matricepixel;

        /// <summary>
        /// Constructeur qui prend un fichier bmp et le lit pour créer une instance de la classe my Imgae
        /// </summary>
        /// <param name="filename">Fichier bmp lu.</param>
        public MyImage(string filename)
        {
            myfile = File.ReadAllBytes(filename);
            imageb = new byte[myfile.Length - 53];
            InitMyImage();
            InitMatrice();
        }

        /// <summary>
        /// Constructeur qui créer un image bitmap en fonction des dimensions renseignées
        /// </summary>
        /// <param name="hauteur">Représente les lignes d'une matrice</param>
        /// <param name="largeur">Représente les colones d'une matrice</param>
        public MyImage(int hauteur, int largeur)
        {
            myfile = new byte[54+(3*hauteur*largeur)];
            myfile[0] = 66;
            myfile[1] = 77;

            taillefichierb = Convertir_Int_To_Endian(myfile.Length);
            for (int i = 2; i < 6; i++)
            {
                myfile[i] = taillefichierb[i - 2];
            }


            myfile[10] = 54;

            tailleoffsetb = Convertir_Int_To_Endian(40);
            for (int i = 14; i < 18; i++)
            {
                myfile[i] = tailleoffsetb[i - 14];
            }

            largeurimageb = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i < 22; i++)
            {
                myfile[i] = largeurimageb[i - 18];
            }

            hauteurimageb = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i < 26; i++)
            {
                myfile[i] = hauteurimageb[i - 22];
            }

            myfile[26] = 1;

            nboctetparcolorb = Convertir_Int_To_Endian(24);
            for (int i = 28; i < 30; i++)
            {
                myfile[i] = nboctetparcolorb[i - 28];
            }

            taillefichierb = Convertir_Int_To_Endian(192054);
            for (int i = 2; i < 6; i++)
            {
                myfile[i] = taillefichierb[i - 2];
            }

            myfile[38] = 35;
            myfile[39] = 46;
            myfile[42] = 35;
            myfile[43] = 46;

            hauteurimage = hauteur;
            largeurimage = largeur;
            matricepixel = new Pixel[hauteurimage, largeurimage];
        }

        /// <summary>
        /// Transforme une instance de la classe en fichier bitmap
        /// </summary>
        /// <param name="filename">Nom assigné au fichier crée</param>
        public void From_Image_To_File(string filename)
        {
            int compteur = 53; //Pour commencer à partir du 54eme bit qui représente le debut de l'image
            hauteurimage = matricepixel.GetLength(0);
            largeurimage = matricepixel.GetLength(1);

            int totalbytesimage = 3 * matricepixel.Length;
            byte[] file = new byte [54+totalbytesimage];

            for(int i =0;i<compteur;i++) { file[i] = myfile[i]; }

            hauteurimageb = Convertir_Int_To_Endian(hauteurimage);
            largeurimageb = Convertir_Int_To_Endian(largeurimage);

            for (int i = 18; i < 22; i++) { file[i] = largeurimageb[i - 18]; }
            for (int i = 22; i < 26; i++) { file[i] = hauteurimageb[i - 22]; }
            

            for (int i = 0; i < matricepixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++)
                {
                    file[compteur] = Convert.ToByte(matricepixel[i, j].Red);
                    file[compteur + 1] = Convert.ToByte(matricepixel[i, j].Green);
                    file[compteur + 2] = Convert.ToByte(matricepixel[i, j].Blue);
                    compteur += 3;
                }
            }
            File.WriteAllBytes(filename, file);
        }

        public int Convertir_Endian_To_Int(byte []tab)
        {
            int resultat = 0;
            for (int i = 0; i < tab.Length; i++) 
            {
                resultat += Convert.ToInt32(tab[i] * (Math.Pow(256, i)));
            }
            return resultat;
        }

        public byte[] Convertir_Int_To_Endian(int valeur)
        {
            byte[] tab = new byte[4];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                tab[i] = Convert.ToByte(valeur / Convert.ToInt32((Math.Pow(256, i))));
                valeur %= Convert.ToInt32((Math.Pow(256, i)));
            }
            return tab;
        }

        public int Convertir_Endian_To_Int2(byte[] tab)
        {
            int resultat = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                resultat += Convert.ToInt32(tab[i] * (Math.Pow(2, i)));
            }
            return resultat;
        }

        public byte[] Convertir_Int_To_Endian2(int valeur)
        {
            byte[] tab = new byte[8];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                tab[i] = Convert.ToByte(valeur / Convert.ToInt32((Math.Pow(2, i))));
                valeur %= Convert.ToInt32((Math.Pow(2, i)));
            }
            return tab;
        }

        public void Inverserordre (byte [] bytes)
        {
            byte permutation;
            for (int i = 0; i< 4; i++) 
            {
                permutation = bytes[i];
                bytes[i] = bytes[i + 4];
                bytes[i + 4] = permutation;
            }
        }
        public void InitMyImage()
        {
            for (int i = 0; i < 2; i++)
            {
                typeimageb[i] = myfile[i];
            }
            for (int i = 2; i < 6; i++)
            {
                taillefichierb[i-2] = myfile[i];
            }
            for (int i = 14; i < 18; i++)
            {
                tailleoffsetb[i-14] = myfile[i];
            }
            for (int i = 18; i < 22; i++)
            {
                largeurimageb[i-18] = myfile[i];
            }
            for (int i = 22; i < 26; i++)
            {
                hauteurimageb[i-22] = myfile[i];
            }
            for (int i = 28; i < 30; i++)
            {
                nboctetparcolorb[i-28] = myfile[i];
            }
            for (int i = 53; i < myfile.Length; i++) 
            {
                imageb[i-53] = myfile[i];
            }

            string part1 = Convert.ToString(Convert.ToChar(typeimageb[0]));
            string part2 = Convert.ToString(Convert.ToChar(typeimageb[1]));
            typeimage = part1 + part2;
            taillefichier = BitConverter.ToInt32(taillefichierb, 0);
            tailleoffset = BitConverter.ToInt32(taillefichierb, 0);
            largeurimage = BitConverter.ToInt32(largeurimageb, 0);
            hauteurimage = BitConverter.ToInt32(hauteurimageb, 0);
            nboctetparcolor = BitConverter.ToInt16(nboctetparcolorb, 0);
        }

        /// <summary>
        /// Crée et remplit la matrice de pixel de l'image avec laquelle on va travailler
        /// </summary>
        public void InitMatrice()
        {
            matricepixel = new Pixel[hauteurimage, largeurimage];

            int compteur1 = 0;
            for (int i = 0; i < matricepixel.GetLength(0); i++) 
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++) 
                {
                    matricepixel[i, j] = new Pixel(imageb[compteur1], imageb[compteur1 + 1], imageb[compteur1 + 2]);
                    compteur1 += 3;
                }
            }
        }


        public void ImageGrise()
        {
            for (int i = 0; i < matricepixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++)
                {
                    matricepixel[i, j].Gris();
                }
            }
        }

        public void ImageNoiretBlanc()
        {
            for (int i = 0; i < matricepixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++)
                {
                    matricepixel[i, j].NoiretBlanc();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Si n est négatif, la fonction réduit la taille de l'image.</param>
        /// <returns>Renvoie l'image avec une nouvelle taille.</returns>
        public Pixel [,] ChangeTailleImage(int n)
        {
            Pixel[,] GrdImage = new Pixel[0, 0];
            if (n > 0)
            {
                GrdImage = new Pixel[hauteurimage * n, largeurimage * n];
                for (int i = 0; i < GrdImage.GetLength(0); i++)
                {
                    for (int j = 0; j < GrdImage.GetLength(1); j++)
                    {
                        GrdImage[i, j] = matricepixel[i / n, j / n];
                    }
                }
            }

            if(n<0)
            {
                int val = -n;
                GrdImage = new Pixel[hauteurimage / val, largeurimage / val];
                for (int i = 0; i < GrdImage.GetLength(0); i++)
                {
                    for (int j = 0; j < GrdImage.GetLength(1); j++)
                    {
                        GrdImage[i, j] = matricepixel[i*val, j*val];
                    }
                }
            }
            return GrdImage;
        }

        public void Mirroir()
        {
            Pixel variablestockage;
            for(int i =0; i<matricepixel.GetLength(0);i++)
            {
                for (int j = 0; j < (matricepixel.GetLength(1)/2); j++)
                {
                    variablestockage = matricepixel[i, j];
                    matricepixel[i, j] = matricepixel[i, matricepixel.GetLength(1) - 1 - j];
                    matricepixel[i, matricepixel.GetLength(1) - 1 - j] = variablestockage;
                }
            }
        }

        public Pixel[,] Rotation90()
        {
            Pixel[,] Rotation90 = new Pixel[largeurimage, hauteurimage];
            for (int i = 0; i < Rotation90.GetLength(0); i++)
            {
                for (int j = 0; j < Rotation90.GetLength(1); j++)
                {
                    Rotation90[i, j] = matricepixel[j, largeurimage - 1 - i];
                }
            }
            return Rotation90;
        }

        public Pixel[,] Rotation180()
        {
            Pixel[,] Rotation180 = new Pixel[hauteurimage, largeurimage];
            for (int i = 0; i < Rotation180.GetLength(0); i++)
            {
                for (int j = 0; j < Rotation180.GetLength(1); j++)
                {
                    Rotation180[i, j] = matricepixel[hauteurimage - 1 - i, largeurimage - 1 - j];
                }
            }
            return Rotation180;
        }

        public Pixel [,] Rotation270()
        {
            Pixel[,] Rotation270 = new Pixel [largeurimage, hauteurimage];
            for(int i=0; i<Rotation270.GetLength(0);i++)
            {
                for (int j=0;j<Rotation270.GetLength(1);j++)
                {
                    Rotation270[i, j] = matricepixel[hauteurimage-1-j, i];
                }
            }
            return Rotation270;
        }

       
        /// <summary>
        /// Fonction qui va prendre une matrice et appliquer le calcul de Gauss à la matrice de pixel en fonction de celle-ci.
        /// </summary>
        /// <param name="matrice">Matrice déterminant le filtre appliqué.</param>
        public void Filtre(int[,] matrice)
        {
            Pixel[,] new_matrice = new Pixel[matricepixel.GetLength(0) + 2, matricepixel.GetLength(1) + 2];
            for (int i = 0; i < new_matrice.GetLength(0); i++) 
            {
                for (int j = 0; j < new_matrice.GetLength(1); j++) 
                {
                    if (i != 0 && j != 0 && i != new_matrice.GetLength(0) - 1 && j != new_matrice.GetLength(1) - 1) 
                    {
                        new_matrice[i, j] = matricepixel[i - 1, j - 1];
                    }
                    else new_matrice[i,j] = new Pixel(0, 0, 0);
                }
            }

            int diviseur = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    diviseur += matrice[i, j];
                }
            }

            int resultatR;
            int resultatG;
            int resultatB;

            for (int i = 1; i < new_matrice.GetLength(0) - 2; i++)
            {
                for (int j = 1; j < new_matrice.GetLength(1) - 2; j++)
                {
                    resultatR = 0;
                    resultatG = 0;
                    resultatB = 0;
                    for (int k = i - 1; k < i + 2; k++)
                    {
                        for (int l = j - 1; l < j + 2; l++)
                        {
                            resultatR += new_matrice[k, l].Red * matrice[k + 1 - i, l + 1 - j];
                            resultatG += new_matrice[k, l].Green * matrice[k + 1 - i, l + 1 - j];
                            resultatB += new_matrice[k, l].Blue * matrice[k + 1 - i, l + 1 - j];
                        }
                    }

                    /*while (resultatR < 0 || resultatG < 0 || resultatB < 0) // en cas de pixel négatif, on ajoute 255 jusqu'à ce que celui devienne positif
                    {
                        if (resultatR < 0) resultatR += 255;
                        if (resultatG < 0) resultatG += 255;
                        if (resultatB < 0) resultatB += 255;
                    }*/

                    if (diviseur <= 0 || resultatR < 0 || resultatG < 0 || resultatB < 0)
                    {
                        
                        while (resultatR < 0 || resultatG < 0 || resultatB < 0)
                        {
                            if (resultatR < 0) resultatR = 0;
                            if (resultatG < 0) resultatG = 0;
                            if (resultatB < 0) resultatB = 0;
                        }

                        diviseur = 1;

                       /* if (diviseur <= 0) // en cas de diviseur nul, on ajoute 128 au résultat de pixel
                        {
                            resultatR += 128;
                            resultatG += 128;
                            resultatB += 128;
                        }*/

                    }
                    else
                    {
                        resultatR /= diviseur;
                        resultatG /= diviseur;
                        resultatB /= diviseur;
                    }
                    
                   
                    if (resultatR > 255) resultatR = 255;
                    if (resultatG > 255) resultatG = 255;
                    if (resultatB > 255) resultatB = 255;

                    matricepixel[i - 1, j - 1] = new Pixel(resultatR, resultatG, resultatB);
                }
            }
        }


        public void Flou()
        {
            int[,] matrice = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            Filtre(matrice);
        }

        public void Repoussage()
        {
            int[,] matrice = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            Filtre(matrice);
        }

        public void DétectionBord()
        {
            int[,] matrice = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
            Filtre(matrice);
        }

        public void RenforcementBord()
        {
            int[,] matrice = { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
            Filtre(matrice);
        }


        /// <summary>
        /// Fonction affichant une fractale.
        /// </summary>
        public void Mandelbrot()
        {

            for (int i = 0; i < hauteurimage; i++) 
            {
                for (int j = 0; j < largeurimage; j++) 
                {
                    double x =( Convert.ToDouble(i - (largeurimage / 2)) / Convert.ToDouble(largeurimage / 4));
                    double y =( Convert.ToDouble(j - (hauteurimage / 2)) / Convert.ToDouble(hauteurimage / 4));
                    Complex a = new Complex(x, y);
                    Complex b = new Complex(0, 0);
                    int répétition = 0;
                    do
                    {
                        répétition++;
                        b.Carré();
                        b.Addition(a);
                        if (b.Module() > 2.0) break;
                    } while (répétition < 100);
                    if (répétition <100) { matricepixel[i, j] = new Pixel(255, 255, 255); }
                    else { matricepixel[i, j] = new Pixel(0, 0, 0); }
                }
            }
        }

        /// <summary>
        /// Fonction donnant l'histogramme des couleurs RVB de l'image.
        /// </summary>
        /// <returns></returns>
        public Pixel[,] Histogramme()
        {
            int[,] Compte = new int[3, 256]; //première matrice qui va compter le nombre de fois que la valeur apparait pour chaque pixel
            int rouge;
            int vert;
            int bleu;

            int max=0;
            //Calcul du nombre de fois qu'apparait les valeurs pour chacun des pixels
            for (int i = 0; i < matricepixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++) 
                {
                    rouge = matricepixel[i, j].Red;
                    vert = matricepixel[i, j].Green;
                    bleu = matricepixel[i, j].Blue;

                    Compte[0, rouge]++;
                    Compte[1, vert]++;
                    Compte[2, bleu]++;


                    if (max < Compte[0, rouge]) max = Compte[0, rouge];
                    if (max < Compte[1, vert]) max = Compte[1, vert];
                    if (max < Compte[2, bleu]) max = Compte[2, bleu];
                }
            }

            //Création de l'image blanche
            Pixel[,] histogramme = new Pixel[max + 20, 32 + (256 * 3)];
            for (int i = 0; i < histogramme.GetLength(0); i++)
            {
                for (int j = 0; j < histogramme.GetLength(1); j++)
                {
                    histogramme[i, j] = new Pixel(255, 255, 255);
                }
            }

            //Création de l'histogramme
            for (int j = 10; j < 266; j++)
            {
                for (int i = 0; i < Compte[0,j-10]; i++)
                {
                    histogramme[i, j] = new Pixel(255, 0, 0);
                }
            }

            for (int j = 276; j < 532; j++)
            {
                for (int i = 0; i < Compte[1, j - 276]; i++)
                {
                    histogramme[i, j] = new Pixel(0, 255, 0);
                    
                }
            }

            for (int j = 542; j < 798; j++)
            {
                for (int i = 0; i < Compte[2, j - 542]; i++)
                {
                    histogramme[i, j] = new Pixel(0, 0, 255);
                }
            }

            return histogramme;
        }


        public MyImage Code  (MyImage Couverture)
        {
            MyImage Imagecode = new MyImage(matricepixel.GetLength(0), matricepixel.GetLength(1));
            for (int i = 0; i < Imagecode.matricepixel.GetLength(0); i++) 
            {
                for (int j = 0; j < Imagecode.matricepixel.GetLength(1); j++)
                {
                    Imagecode.matricepixel[i, j] = new Pixel(0, 0, 0);
                    Imagecode.matricepixel[i, j].Red = (matricepixel[i, j].Red & 15) + (Couverture.matricepixel[i, j].Red & 240);
                    Imagecode.matricepixel[i, j].Green = (matricepixel[i, j].Green & 15) + (Couverture.matricepixel[i, j].Green & 240);
                    Imagecode.matricepixel[i, j].Blue = (matricepixel[i, j].Blue & 15) + (Couverture.matricepixel[i, j].Blue & 240);
                }
            }
            return Imagecode;
        }

        public MyImage DeCodeImgvisible()
        {
            MyImage Imgvisible = new MyImage(matricepixel.GetLength(0), matricepixel.GetLength(1));
            for (int i = 0; i < matricepixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++)
                {
                    Imgvisible.matricepixel[i, j] = new Pixel(0, 0, 0);
                    Imgvisible.matricepixel[i, j].Red = (matricepixel[i, j].Red & 240);
                    Imgvisible.matricepixel[i, j].Green = (matricepixel[i, j].Green & 240);
                    Imgvisible.matricepixel[i, j].Blue = (matricepixel[i, j].Blue & 240);
                }
            }
            return Imgvisible;
        }

        public MyImage DeCodeImgcache()
        {
            MyImage Imgcache = new MyImage(matricepixel.GetLength(0), matricepixel.GetLength(1));
            for (int i = 0; i < matricepixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricepixel.GetLength(1); j++)
                {
                    Imgcache.matricepixel[i, j] = new Pixel(0, 0, 0);

                    byte[] bytes = Convertir_Int_To_Endian2(matricepixel[i, j].Red & 15);
                    byte[] bytes2 = Convertir_Int_To_Endian2(matricepixel[i, j].Green & 15);
                    byte[] bytes3 = Convertir_Int_To_Endian2(matricepixel[i, j].Blue & 15);
                    Inverserordre(bytes);
                    Inverserordre(bytes2);
                    Inverserordre(bytes3);

                    Imgcache.matricepixel[i, j].Red = Convertir_Endian_To_Int2(bytes);
                    Imgcache.matricepixel[i, j].Green = Convertir_Endian_To_Int2(bytes3);
                    Imgcache.matricepixel[i, j].Blue = Convertir_Endian_To_Int2(bytes2);
                }
            }
            return Imgcache;
        }


        public string TypeImage { get { return typeimage; } }
        public byte [] Myfile { get { return myfile; } }
        public int ImagebLength { get { return imageb.Length; } }
        public int Hauteurimage { get { return hauteurimage; } }
        public int Largeurimage { get { return largeurimage; } }
        public Pixel [,] Matricepixel { set { matricepixel = value; } }
    }
}
