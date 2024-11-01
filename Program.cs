using MiniProjet;

class Program
{
    static void Main()
    {
        bool continuer = true;

        // Menu initial pour créer un personnage ou charger une sauvegarde
        Console.Clear();
        Console.WriteLine("=== Bienvenue dans le jeu ===");
        Console.WriteLine("1 - Créer un nouveau personnage");
        Console.WriteLine("2 - Charger une sauvegarde existante");
        Console.Write("Votre choix : ");
        string choixInitial = Console.ReadLine();

        Joueur joueur;
        switch (choixInitial)
        {
            case "1":
                joueur = Joueur.CreerJoueur();
                Console.WriteLine("Personnage créé avec succès !");
                break;
            case "2":
                joueur = GestionSauvegarde.ChargerSauvegardeDepuisMenu();
                if (joueur == null)
                {
                    Console.WriteLine("Aucune sauvegarde trouvée ou sélection annulée. Création d'un nouveau personnage.");
                    joueur = Joueur.CreerJoueur();
                }
                break;
            default:
                Console.WriteLine("Choix invalide. Création d'un nouveau personnage par défaut.");
                joueur = Joueur.CreerJoueur();
                break;
        }

        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("\nQue voulez-vous faire ?");
            Console.WriteLine("1 - Combattre");
            Console.WriteLine("2 - Voir mes statistiques");
            Console.WriteLine("3 - Voir mon inventaire");
            Console.WriteLine("5 - Voir l'armurerie");
            Console.WriteLine("6 - Voir mes classes débloquées");
            if (Boutique.nouveauxItems)
            {
                Console.Write("7 - Afficher la boutique |");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Nouveaux équipements disponibles dans la boutique !");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("7 - Afficher la boutique");
            }
            Console.WriteLine("8 - Sauvegarder les données");
            Console.WriteLine("'reset' - Recréer un personnage");
            Console.WriteLine("0 - Quitter le jeu");
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
                case "reset":
                    Console.Clear();
                    joueur = Joueur.CreerJoueur();
                    Console.WriteLine("Votre personnage a été recréé.");
                    PauseRetourMenu();
                    break;
                case "5":
                    joueur.AfficherArmurerie();
                    PauseRetourMenu();
                    break;
                case "6":
                    joueur.AfficherClassesDebloquee();
                    PauseRetourMenu();
                    break;
                case "7":
                    Boutique.AfficherBoutique(joueur, joueur.Niveau);
                    break;
                case "8":
                    GestionSauvegarde.SauvegarderJoueur(joueur);
                    Console.WriteLine("Jeu sauvegardé avec succès !");
                    PauseRetourMenu();
                    break;
                case "test":
                    PauseRetourMenu();
                    break;
                case "0":
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
