using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PROJET_INFO
{
    class Program
    {
        static string SaisieMot()
        {
            string result="a";
            while (result != "coco" && result != "calme")
            {
                result = Console.ReadLine();
                if (result != "coco" && result != "calme")
                {
                    Console.WriteLine("Erreur, veuillez rentrer un nom d'image valide.");
                    //Console.WriteLine();
                }
            }
            return result;
        }

        static int SaisieNombre()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Erreur, veuillez rentrer un nombre valide.");
            }
            return result;
        }

        static int SaisieNombre2()
        {
            int result = 2;
            while (result%4!=0)
            {
                result = Convert.ToInt32(Console.ReadLine());
                if (result % 4 != 0) { Console.WriteLine("Erreur, veuillez rentrer un nombre valide."); }
            }
            return result;
        }

        static int SaisieNombre3()
        {
            int result = -5;
            while (result <= 0)
            {
                result = Convert.ToInt32(Console.ReadLine());
                if (result <= 0) { Console.WriteLine("Erreur, veuillez rentrer un nombre valide."); }
            }
            return result;
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            string Texte = "Bienvenue dans le projet info de traitement d'image. Appuyez sur une touche pour continuer.";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Texte.Length / 2)) + "}", Texte));
            Console.ReadKey();
            Console.Clear();
            string Texte2 = "Choisissez l'image à traiter parmi les suivantes : coco et calme ";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Texte2.Length / 2)) + "}", Texte2));
            string mot =SaisieMot();

            do
            {
                Console.Clear();
                Console.WriteLine("Sélectionnez l'opération à réaliser en entrant un chiffre :");
                Console.WriteLine(" 1 : Rotation de l'image ");
                Console.WriteLine(" 2 : Effet miroir ");
                Console.WriteLine(" 3 : Nuance de gris ");
                Console.WriteLine(" 4 : Noir et Blanc ");
                Console.WriteLine(" 5 : Agrandissement / Rétrecissement ");
                Console.WriteLine(" 6 : Filtres ");
                Console.WriteLine(" 7 : Histogramme ");
                Console.WriteLine(" 8 : Fractale");
                Console.WriteLine(" 9 : Coder et décoder");
                int choisi = SaisieNombre();
                string image = mot;
                MyImage image_test = new MyImage(image + ".bmp");
                switch (choisi)
                {
                    case 1:
                        Console.WriteLine(" De combien de degré voulez-vous faire tourner l'image ?");
                        Console.WriteLine(" 90 ?    180 ?    270 ? ");
                        int degre = SaisieNombre();
                        if (degre == 90)
                        {
                            image_test.Matricepixel=image_test.Rotation90();
                            image_test.From_Image_To_File(mot+"_90.bmp");
                            Process.Start(mot + "_90.bmp");
                        }
                        if (degre == 180)
                        {
                            image_test.Matricepixel = image_test.Rotation180();
                            image_test.From_Image_To_File(mot + "_180.bmp");
                            Process.Start(mot + "_180.bmp");
                        }
                        if (degre == 270)
                        {
                            image_test.Matricepixel = image_test.Rotation270();
                            image_test.From_Image_To_File(mot + "_270.bmp");
                            Process.Start(mot + "_270.bmp");
                        }
                        if ((degre != 90) && (degre != 180) && (degre != 270))
                        {
                            Console.WriteLine("Veuillez entrer un angle valide");
                        }
                        break;

                    case 2:
                        image_test.Mirroir();
                        image_test.From_Image_To_File(mot+"_miroir.bmp");
                        Process.Start(mot+"_miroir.bmp");
                        break;

                    case 3:
                        image_test.ImageGrise();
                        image_test.From_Image_To_File(mot+"_gris.bmp");
                        Process.Start(mot+"_gris.bmp");
                        break;

                    case 4:
                        image_test.ImageNoiretBlanc();
                        image_test.From_Image_To_File(mot+"_noir_et_blanc.bmp");
                        Process.Start(mot+"_noir_et_blanc.bmp");
                        break;

                    case 5:
                        Console.WriteLine(" Choisissez par combien voulez-vous changer la taille de l'image?");
                        Console.WriteLine("Rentrez un nombre négatif si vous voulez rétrécir. ( Choisissez un multiple de quatre ) ");
                        int changement = SaisieNombre2();
                        image_test.Matricepixel = image_test.ChangeTailleImage(changement);
                        image_test.From_Image_To_File(mot+"_changement.bmp");
                        Process.Start(mot+"_changement.bmp");
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("Quel filtre voulez-vous appliquer à l'image ? ");
                        Console.WriteLine("1 : Flou ");
                        Console.WriteLine("2 : Repoussage");
                        Console.WriteLine("3 : Renforcement des bords ");
                        Console.WriteLine("4 : Détection des bords ");

                        int choix = SaisieNombre();
                        switch (choix)
                        {
                            case 1:
                                
                                image_test.Flou();
                                image_test.From_Image_To_File("coco_flou.bmp");
                                Process.Start(mot+"_flou.bmp");
                                break;
                            case 2:
                                image_test.Repoussage();
                                image_test.From_Image_To_File(mot+"_repoussage.bmp");
                                Process.Start(mot+"_repoussage.bmp");
                                break;
                            case 3:
                                image_test.RenforcementBord();
                                image_test.From_Image_To_File(mot+"_renforcement_des_bords.bmp");
                                Process.Start(mot+"_renforcement_des_bords.bmp");
                                break;
                            case 4:
                                image_test.DétectionBord();
                                image_test.From_Image_To_File(mot+"_detection_des_bords.bmp");
                                Process.Start(mot+"_detection_des_bords.bmp");
                                break;
                            default:
                                break;

                        }
                        break;
                    case 7:
                        image_test.Matricepixel=image_test.Histogramme();
                        image_test.From_Image_To_File(mot+"_histogramme.bmp");
                        Process.Start(mot+"_histogramme.bmp");
                        break;
                    case 8:
                        Console.WriteLine("Choisissez une hauteur et une largeur.");
                        int hauteur = SaisieNombre3();
                        int largeur = SaisieNombre3();
                        MyImage Fractale = new MyImage(hauteur, largeur);
                        Fractale.Mandelbrot();
                        Fractale.From_Image_To_File("fractale.bmp");
                        Process.Start("fractale.bmp");
                        break;
                    case 9:
                        Console.WriteLine("Dans quel image voulez vous coder l'image que vous avez choisi ( à savoir " + mot + " ) : coco ou calme");
                        string mot2 = SaisieMot();
                        MyImage Couverture = new MyImage(mot2 + ".bmp");
                        MyImage Combi = image_test.Code(Couverture);
                        Combi.From_Image_To_File(mot + "_codé_dans_" + mot2+".bmp");
                        Process.Start(mot2 + ".bmp");
                        Process.Start(mot + "_codé_dans_" + mot2+".bmp");
                        Console.ReadKey();
                        Console.WriteLine("On va maintenant décoder l'image obtenu");
                        Console.ReadKey();
                        MyImage Visible = Combi.DeCodeImgvisible();
                        MyImage Cachee = Combi.DeCodeImgcache();
                        Visible.From_Image_To_File(mot2 + "_récupérée.bmp");
                        Cachee.From_Image_To_File(mot + "_récupérée.bmp");
                        Process.Start(mot2 + "_récupérée.bmp");
                        Process.Start(mot + "_récupérée.bmp");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Appuyez sur Echap pour sortir ou un numero correspondant à une opération.");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);

            Console.ReadKey();
        }
    }
}
