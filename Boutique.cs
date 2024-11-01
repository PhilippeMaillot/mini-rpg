namespace MiniProjet
{
    public class Boutique
    {
        public static bool nouveauxItems = false;
        public class Objet(string nom, int prix, int niveauRequis, Objets objets)
        {
            public string Nom { get; set; } = nom;
            public int Prix { get; set; } = prix;
            public int NiveauRequis { get; set; } = niveauRequis;
            public Objets Objets { get; set; } = objets;
        }

        public class Arme(string nom, int prix, int niveauRequis, Armes armes)
        {
            public string Nom { get; set; } = nom;
            public int Prix { get; set; } = prix;
            public int NiveauRequis { get; set; } = niveauRequis;
            public Armes Armes { get; set; } = armes;
        }

        public class Classe(string nom, int prix, int niveauRequis, Classes classes)
        {
            public string Nom { get; set; } = nom;
            public int Prix { get; set; } = prix;
            public int NiveauRequis { get; set; } = niveauRequis;
            public Classes Classes { get; set; } = classes;
        }

        public static readonly List<Objet> ListObjets =
            [
                new Objet("Potion de soin", 50, 0, Objets.potionSoin),
                new Objet("Potion de force", 50, 0, Objets.potionDegats),
                new Objet("Potion d'agilité", 50, 5, Objets.potionAgilite),
                new Objet("Potion de défense", 50, 5, Objets.potionDefense),
                new Objet("Potion de défense magique", 50, 5, Objets.potionDefenseMagique)
            ];

        public static readonly List<Arme> ListArmes =
            [
                new Arme("Epée en bois", 100, 0, Armes.epeeEnBois),
                new Arme("Arc en bois", 100, 0, Armes.arcEnBois),
                new Arme("Bâton en bois", 100, 0, Armes.batonEnBois),
                new Arme("Bouclier en bois", 100, 0, Armes.bouclierEnBois),
                new Arme("Dague en bois", 100, 0, Armes.dagueEnBois),
            //Niveau 5
                new Arme("Espadon Lumineux", 500, 5, Armes.espadonLumineux),
                new Arme("Lance", 500, 5, Armes.Lance),
                new Arme("Marteau de chasse", 500, 5, Armes.marteauDeChasse)
            ];
        public static readonly List<Classe> ListClasses =
            [
                new Classe("Guerrier", 250, 0, Classes.guerrier),
                new Classe("Archer", 250, 0, Classes.archer),
                new Classe("Mage", 250, 0, Classes.mage),
                new Classe("Assassin", 250, 0, Classes.assassin),
                new Classe("Défenseur", 250, 0, Classes.defenseur),
            //Niveau 5
                new Classe("Paladin", 700, 5, Classes.paladin),
                new Classe("Lancier", 700, 5, Classes.lancier),
                new Classe("Revenant", 700, 5, Classes.revenant),
                new Classe("Arcaniste", 700, 5, Classes.arcaniste),
                new Classe("Artilleur", 700, 5, Classes.artilleur),
            //Niveau 15
                new Classe("Berserker", 1100, 15, Classes.berserker),
                new Classe("Samurai", 1100, 15, Classes.samurai),
                new Classe("Sniper", 1100, 15, Classes.sniper),
                new Classe("Valkyrie", 1100, 15, Classes.valkyrie),
                new Classe("Vampire", 1100, 15, Classes.vampire),
            //Niveau 30
                new Classe("Démon", 2000, 30, Classes.demon),
                new Classe("Pyromancien", 2000, 30, Classes.pyromancien),
                new Classe("Cryomancien", 2000, 30, Classes.cryomancien),
                new Classe("Shinobi", 2000, 30, Classes.shinobi),
                new Classe("Pugiliste", 2000, 30, Classes.pugiliste)
            ];

        static void AfficherEnteteDeLaBoutique()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Bienvenue dans la Boutique ! ===");
            Console.ResetColor();
            Console.WriteLine("Voici les objets, armes et classes que vous pouvez acheter :");
            Console.WriteLine();
        }

        public static void AfficherLePiedDePage(Joueur joueur, int niveau)
        {
            Console.WriteLine("0 - Quitter la boutique");
            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();
            AppliquerChoix(joueur, choix, niveau);
        }

        public static void AppliquerChoix(Joueur joueur, string choix, int niveau)
        {
            int totalItems = ListObjets.Count + ListArmes.Count + ListClasses.Count;

            if (int.TryParse(choix, out int indexChoix))
            {
                if (indexChoix == 0)
                {
                    Console.WriteLine("Merci d'avoir visité la boutique !");
                    Console.ReadKey();
                    return;
                }
                else if (indexChoix > 0 && indexChoix <= totalItems)
                {
                    Console.WriteLine($"Vous avez choisi l'item n°{indexChoix}");
                    AcheterChoix(indexChoix, joueur);
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nChoix invalide. Veuillez entrer un numéro valide.");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    AfficherBoutique(joueur, niveau);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nChoix invalide. Veuillez entrer un numéro valide.");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                AfficherBoutique(joueur, niveau);
            }
        }

        public static void AfficherBoutiqueParNiveau(Joueur joueur, int niveau)
        {
            int compteur = 1;

            Console.WriteLine($"Niveau actuel : {niveau}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Objets disponibles ---");
            Console.ResetColor();
            foreach (var objet in ListObjets.Where(o => o.NiveauRequis <= niveau))
            {
                Console.WriteLine($"{compteur++}. {objet.Nom} - {objet.Prix} pièces d'or");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n--- Armes disponibles ---");
            Console.ResetColor();
            foreach (var arme in ListArmes.Where(a => a.NiveauRequis <= niveau))
            {
                Console.WriteLine($"{compteur++}. {arme.Nom} - {arme.Prix} pièces d'or");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n--- Classes disponibles ---");
            Console.ResetColor();
            foreach (var classe in ListClasses.Where(c => c.NiveauRequis <= niveau))
            {
                Console.WriteLine($"{compteur++}. {classe.Nom} - {classe.Prix} pièces d'or");
            }
            Console.WriteLine();
        }

        public static void AfficherBoutique(Joueur joueur ,int niveau)
        {
            nouveauxItems = false;
            AfficherEnteteDeLaBoutique();
            AfficherBoutiqueParNiveau(joueur, niveau);
            AfficherLePiedDePage(joueur, niveau);
        }

        public static void AcheterChoix(int indexChoix, Joueur joueur)
        {
            int totalObjets = ListObjets.Count;
            int totalArmes = ListArmes.Count;
            int totalItems = totalObjets + totalArmes + ListClasses.Count;

            if (indexChoix > 0 && indexChoix <= totalObjets)
            {
                var objetSelectionne = ListObjets[indexChoix - 1];
                if (joueur.PieceDor >= objetSelectionne.Prix)
                {
                    joueur.PieceDor -= objetSelectionne.Prix;
                    joueur.AjouterObjet(objetSelectionne.Objets);
                    Console.WriteLine($"Vous avez acheté : {objetSelectionne.Nom} pour {objetSelectionne.Prix} pièces d'or.");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de pièces d'or pour cet objet.");
                }
            }
            else if (indexChoix > totalObjets && indexChoix <= totalObjets + totalArmes)
            {
                var armeSelectionnee = ListArmes[indexChoix - totalObjets - 1];
                if (joueur.PieceDor >= armeSelectionnee.Prix)
                {
                    joueur.PieceDor -= armeSelectionnee.Prix;
                    joueur.AjouterArme(armeSelectionnee.Armes);
                    Console.WriteLine($"Vous avez acheté : {armeSelectionnee.Nom} pour {armeSelectionnee.Prix} pièces d'or.");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de pièces d'or pour cette arme.");
                }
            }
            else if (indexChoix > totalObjets + totalArmes && indexChoix <= totalItems)
            {
                var classeSelectionnee = ListClasses[indexChoix - totalObjets - totalArmes - 1];
                if (joueur.PieceDor >= classeSelectionnee.Prix)
                {
                    joueur.PieceDor -= classeSelectionnee.Prix;
                    joueur.AjouterClasse(classeSelectionnee.Classes);
                    Console.WriteLine($"Vous avez acheté : {classeSelectionnee.Nom} pour {classeSelectionnee.Prix} pièces d'or.");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de pièces d'or pour cette classe.");
                }
            }
            else
            {
                Console.WriteLine("Erreur de sélection. Veuillez entrer un numéro valide.");
                Console.Clear();
                AfficherBoutique(joueur, joueur.Niveau);
            }
        }

        public static void VerificationNouveauxItems(int niveau)
        {
            switch (niveau)
            {
                case 5: nouveauxItems = true; break;
                case 15: nouveauxItems = true; break;
                case 30: nouveauxItems = true; break;
            }
        }
    }
}
