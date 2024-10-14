using MiniProjet;

class Program
{
    static void Main()
    {
        Joueur joueur = Joueur.CreerJoueur();
        bool continuer = true;

        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("\nQue voulez-vous faire ?");
            Console.WriteLine("1 - Combattre");
            Console.WriteLine("2 - Voir mes statistiques");
            Console.WriteLine("3 - Voir mon inventaire");
            Console.WriteLine("4 - Recréer un personnage");
            Console.WriteLine("5 - Voir l'armurerie");
            Console.WriteLine("6 - Quitter");
            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    LancerCombat(joueur);
                    PauseRetourMenu();
                    break;
                case "2":
                    joueur.AfficherStatistiques();
                    PauseRetourMenu();
                    break;
                case "3":
                    joueur.AfficherInventaire();
                    PauseRetourMenu();
                    break;
                case "4":
                    Console.Clear();
                    joueur = Joueur.CreerJoueur();
                    Console.WriteLine("Votre personnage a été recréé.");
                    PauseRetourMenu();
                    break;
                case "5":
                    joueur.AfficherArmurerie();
                    break;
                case "6":
                    Console.WriteLine("Vous quittez le jeu.");
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez choisir une option valide.");
                    PauseRetourMenu();
                    break;
            }
        }
    }

    static void LancerCombat(Joueur joueur)
    {
        Ennemis ennemi = Ennemis.CréerEnnemiAleatoire(joueur);
        Combat.LancerCombat(joueur, ennemi);
    }

    public static void PauseRetourMenu()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nAppuyez sur une touche pour revenir au menu principal...");
        Console.ReadKey();
    }
}
