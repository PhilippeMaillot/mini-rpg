namespace MiniProjet
{
    public class Calculs
    {
        public static double CalculerDegatsJoueur(Joueur joueur, Ennemis ennemi, AttaqueSpe attaqueSpe, string typeAttaque)
        {
            double chanceCrit = 10;

            if (typeAttaque == "physique")
            {
                double degatsBase = ((((joueur.Classe.Niveau * 0.4 + 2) * joueur.CalculerStatistiquesFinales().forceFinale * joueur.Arme.DegatsPhysiques) / ennemi.Defense) / 10) + 2;
                bool isCrit = CalculerCrit(chanceCrit, new Random());
                if (isCrit)
                {
                    degatsBase *= 1.5;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Coup critique !");
                    Console.ResetColor();
                }

                return Math.Round(degatsBase, 2);
            }
            else if (typeAttaque == "magique")
            {
                double degatsBase = ((((joueur.Classe.Niveau * 0.4 + 2) * joueur.CalculerStatistiquesFinales().defenseMagiqueFinale * joueur.Arme.DegatsMagiques) / ennemi.DefenseMagique) / 10) + 2;
                bool isCrit = CalculerCrit(chanceCrit, new Random());
                if (isCrit)
                {
                    degatsBase *= 1.5;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Coup critique !");
                    Console.ResetColor();
                }

                return Math.Round(degatsBase, 2);
            }
            else if (typeAttaque == "spePhys")
            {
                double degatsBase = ((((joueur.Classe.Niveau * 0.4 + 2) * joueur.CalculerStatistiquesFinales().forceFinale * attaqueSpe.DgtPhysiques) / ennemi.Defense) / 10) + 2;
                chanceCrit = attaqueSpe.ChanceCrit;
                bool isCrit = CalculerCrit(chanceCrit, new Random());
                if (isCrit)
                {
                    degatsBase *= 1.5;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Coup critique !");
                    Console.ResetColor();
                }
                return Math.Round(degatsBase, 2);

            } else if (typeAttaque == "speMag")
            {
                double degatsBase = ((((joueur.Classe.Niveau * 0.4 + 2) * joueur.CalculerStatistiquesFinales().defenseMagiqueFinale * attaqueSpe.DgtMagiques) / ennemi.DefenseMagique) / 10) + 2;
                bool isCrit = CalculerCrit(chanceCrit, new Random());
                if (isCrit)
                {
                    degatsBase *= 1.5;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Coup critique !");
                    Console.ResetColor();
                }
                return Math.Round(degatsBase, 2);
            }
            else
            {
                return 0;

            }
        }

        public static double CalculerDegatsEnnemi(Joueur joueur, Ennemis ennemi)
        {
            double degatsDeCompetence = Math.Max(joueur.Arme.DegatsMagiques, joueur.Arme.DegatsPhysiques);
            double degatsPhysPotentiels = ((((joueur.Classe.Niveau * 0.4 + 2) * ennemi.Force * degatsDeCompetence) / joueur.CalculerStatistiquesFinales().defenseFinale) / 10) + 2;
            double degatsMagPotentiels = ((((joueur.Classe.Niveau * 0.4 + 2) * ennemi.Intelligence * degatsDeCompetence) / joueur.CalculerStatistiquesFinales().defenseMagiqueFinale) / 10) + 2;
            bool isCrit = CalculerCrit(10, new Random());
            if (isCrit)
            {
                degatsPhysPotentiels *= 1.5;
                degatsMagPotentiels *= 1.5;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Coup critique !");
                Console.ResetColor();
            }
            return Math.Round(Math.Max(degatsPhysPotentiels, degatsMagPotentiels), 2);
        }

        private static bool CalculerCrit(double chanceCritique, Random random)
        {
            int chance = random.Next(1, 101);
            return chance <= chanceCritique;
        }

        public static bool CalculerEsquive(double agilite, Random random)
        {
            double chanceEsquive = Math.Round((Math.Sqrt(agilite) * 2.5) / 100, 2) * 100;
            Console.WriteLine($"Chance d'esquive : {chanceEsquive}%");
            int chance = random.Next(1, 101);
            return chance <= chanceEsquive;
        }
    }  
}
