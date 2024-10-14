namespace MiniProjet
{
    public class Joueur
    {
        public string Nom { get; set; }
        public int Experience { get; set; }
        public int Niveau { get; set; }
        public Classes Classe { get; set; }
        public Armes Arme { get; set; }
        public Dictionary<Objet, int> Inventaire { get; set; }
        public Dictionary<int, Armes> Armurerie { get; set; }
        public double PointsDeVieActuels { get; set; }
        public double ForceActuelle { get; set; }
        public double AgiliteActuelle { get; set; }
        public double IntelligenceActuelle { get; set; }
        public double DefenseActuelle { get; set; }
        public double DefenseMagiqueActuelle { get; set; }
        public int PieceDor { get; set; }

        private int prochainIdArme;

        public Joueur(string nom, int experience, int niveau, Classes classe, Armes arme, double pointsDeVieActuels, double forceActuelle, double agiliteActuelle, double intelligenceActuelle, double defenseActuelle, double defenseMagiqueActuelle, int pieceDor)
        {
            Nom = nom;
            Experience = experience;
            Niveau = niveau;
            Classe = classe;
            Arme = arme;
            Inventaire = [];
            Armurerie = [];
            PointsDeVieActuels = pointsDeVieActuels;
            ForceActuelle = forceActuelle;
            AgiliteActuelle = agiliteActuelle;
            IntelligenceActuelle = intelligenceActuelle;
            DefenseActuelle = defenseActuelle;
            DefenseMagiqueActuelle = defenseMagiqueActuelle;
            PieceDor = pieceDor;
            prochainIdArme = 1;
            AjouterObjetsDeBase();
        }

        private void AjouterObjetsDeBase()
        {
            Objet potionSoin = new("Potion de Soin (50pv)", "soin", 50);
            Inventaire[potionSoin] = 5;
            Objet potionDegats = new("Potion de Dégâts", "force", 20);
            Inventaire[potionDegats] = 3;
        }

        public void UtiliserObjet(Objet objet, Ennemis ennemi)
        {
            if (Inventaire.TryGetValue(objet, out int value) && value > 0)
            {
                objet.Utiliser(this, ennemi, objet.Effet);
                Inventaire[objet] = --value;
                if (value <= 0)
                {
                    Inventaire.Remove(objet);
                }
            }
            else
            {
                Console.WriteLine("Objet non disponible ou quantité insuffisante.");
            }
        }

        private void AjouterArme(Armes arme)
        {
            Armurerie[prochainIdArme] = arme;
            prochainIdArme++;
        }

        public void AfficherInventaire()
        {
            Console.WriteLine("\n=== Inventaire ===");
            foreach (var item in Inventaire)
            {
                Console.WriteLine($"{item.Key.Nom} - Quantité : {item.Value}");
            }
        }

        public void AfficherArmurerie()
        {
            Console.WriteLine("\n=== Armurerie ===");
            foreach (var item in Armurerie)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Nom}");
            }

            Console.WriteLine("Veuillez choisir une arme parmi les propositions ci-dessus ou appuyer sur Entrée pour revenir au menu.");
            string choix = Console.ReadLine();

            if (int.TryParse(choix, out int idArme) && Armurerie.TryGetValue(idArme, out Armes? value))
            {
                Arme = value;
                Console.WriteLine($"Vous avez choisi l'arme {Arme.Nom} !");
            }
            else if (!string.IsNullOrEmpty(choix))
            {
                Console.WriteLine("Arme non trouvée.");
            }
        }

        public static Joueur CreerJoueur()
        {
            AttaqueSpe BaseAttaqueSpe = new("Echarde", "dot", 5, 5, 1, "Ouille une echarde ! (l'ennemi est blessé pendant les 5 prochains tours");
            Armes baseArme = new("Epée en bois", 7, 0, "Guerrier", 1.2, 3, 0, 0, BaseAttaqueSpe);

            Console.Write("Entrez le nom de votre personnage : ");
            string nom = Console.ReadLine();

            Console.WriteLine("Choisissez une classe pour votre personnage :");
            Classes classe = AfficherClasses();
            if (classe == null)
            {
                Console.WriteLine("Choix invalide. Un guerrier par défaut sera choisi.");
                classe = new Classes("Guerrier", 120, 8, 5, 3, 10, 4);
            }

            Armes arme = AfficherArmesDeBase();
            if (arme == null)
            {
                Console.WriteLine("Choix invalide. Une épée en bois par défaut sera choisie.");
                arme = baseArme;
            }
            Joueur joueur = new(nom, 0, 0, classe, arme, classe.PointsDeVie, classe.Force, classe.Agilite, classe.Intelligence, classe.Defense, classe.DefenseMagique, 50);
            joueur.AjouterArme(arme);

            return joueur;
        }

        public void GagnerExperience(int points)
        {
            Experience += points;
            Console.WriteLine($"{Nom} a gagné {points} points d'expérience !");

            while (Experience >= 100)
            {
                MonteDeNiveau();
            }
        }

        private void MonteDeNiveau()
        {
            Niveau += 1;
            Experience -= 100;

            Classe.PointsDeVie = Math.Round(Classe.PointsDeVie * 1.015, 2);
            Classe.Force = Math.Round(Classe.Force * 1.025, 2);
            Classe.Intelligence = Math.Round(Classe.Intelligence * 1.025, 2);
            Classe.Agilite = Math.Round(Classe.Agilite * 1.025, 2);
            Classe.Defense = Math.Round(Classe.Defense * 1.025, 2);
            Classe.DefenseMagique = Math.Round(Classe.DefenseMagique * 1.025, 2);

            PointsDeVieActuels = Classe.PointsDeVie;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Nom} est monté au niveau {Niveau} !");
            Console.WriteLine("Toutes vos statistiques de base ont augmenté !");
            VerifierBonusNiveau();
            Console.ResetColor();
        }
        public void GagnerPieceDor(int pieceDor)
        {
            PieceDor += pieceDor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Nom} a gagné {pieceDor} pièces d'or !");
            Console.ResetColor();
        }

        public static Classes AfficherClasses()
        {
            Classes guerrier = new("Guerrier", 120, 8, 5, 3, 10, 4);
            Classes mage = new("Mage", 100, 2, 5, 15, 3, 5);
            Classes archer = new("Archer", 90, 14, 10, 0, 3, 3);
            Classes assassin = new("Assassin", 85, 7, 15, 2, 2, 4);
            Classes defenseur = new("Défenseur", 140, 5, 7, 3, 10, 5);

            Console.WriteLine("1 - Guerrier");
            Console.WriteLine("2 - Archer");
            Console.WriteLine("3 - Mage");
            Console.WriteLine("4 - Défenseur");
            Console.WriteLine("5 - Assassin");
            Console.WriteLine("Veuillez choisir une classe parmi les propositions ci-dessus :");
            string choix = Console.ReadLine();

            return choix switch
            {
                "1" => guerrier,
                "2" => archer,
                "3" => mage,
                "4" => defenseur,
                "5" => assassin,
                _ => null,
            };
        }

        public static Armes AfficherArmesDeBase()
        {
            Armes epeeEnBois = Armes.RecupArme("Epée en bois");
            Armes arcEnBois = Armes.RecupArme("Arc en bois");
            Armes batonEnBois = Armes.RecupArme("Bâton en bois");
            Armes bouclierEnBois = Armes.RecupArme("Bouclier en bois");
            Armes dagueEnBois = Armes.RecupArme("Dague en bois");

            Console.WriteLine("1 - Epée en bois");
            Console.WriteLine("2 - Arc en bois");
            Console.WriteLine("3 - Bâton en bois");
            Console.WriteLine("4 - Bouclier en bois");
            Console.WriteLine("5 - Dague en bois");
            Console.WriteLine("Veuillez choisir une arme de base parmi les propositions ci-dessus :");
            string choix = Console.ReadLine();

            return choix switch
            {
                "1" => epeeEnBois,
                "2" => arcEnBois,
                "3" => batonEnBois,
                "4" => bouclierEnBois,
                "5" => dagueEnBois,
                _ => null,
            };
        }

        public (double forceFinale, double agiliteFinale, double intelligenceFinale, double defenseFinale, double defenseMagiqueFinale) CalculerStatistiquesFinales()
        {
            double forceFinale = ForceActuelle;
            double agiliteFinale = AgiliteActuelle;
            double intelligenceFinale = IntelligenceActuelle;
            double defenseFinale = DefenseActuelle;
            double defenseMagiqueFinale = DefenseMagiqueActuelle;

            double multiplicateur = Arme.ClasseDefaut == Classe.Nom ? Arme.Multiplicateur : 1.0;

            double degatsPhysiquesArme = Arme.DegatsPhysiques * multiplicateur;
            double degatsMagiquesArme = Arme.DegatsMagiques * multiplicateur;
            double defenseArme = Arme.Defense * multiplicateur;
            double defenseMagiqueArme = Arme.DefenseMagique * multiplicateur;
            double agiliteArme = Arme.Agilite * multiplicateur;

            forceFinale += degatsPhysiquesArme;
            intelligenceFinale += degatsMagiquesArme;
            defenseFinale += defenseArme;
            defenseMagiqueFinale += defenseMagiqueArme;
            agiliteFinale += agiliteArme;

            return (forceFinale, agiliteFinale, intelligenceFinale, defenseFinale, defenseMagiqueFinale);
        }

        public void AfficherStatistiques()
        {
            var (forceFinale, agiliteFinale, intelligenceFinale, defenseFinale, defenseMagiqueFinale) = CalculerStatistiquesFinales();

            Console.WriteLine("/***********/ Statistiques du joueur /***********/");
            Console.WriteLine($"Nom : {Nom}");
            Console.WriteLine($"Classe : {Classe.Nom}");
            Console.WriteLine($"Niveau : {Niveau}");
            Console.WriteLine($"Expérience : {Experience}");
            Console.WriteLine($"Pièces d'or : {PieceDor}");
            Console.WriteLine($"Points de vie : {Classe.PointsDeVie:F2}");
            Console.WriteLine($"Agilité : {agiliteFinale:F2}");
            Console.WriteLine($"Force (dégâts physiques) : {forceFinale:F2}");
            Console.WriteLine($"Intelligence (dégâts magiques) : {intelligenceFinale:F2}");
            Console.WriteLine($"Défense physique : {defenseFinale:F2}");
            Console.WriteLine($"Défense magique : {defenseMagiqueFinale:F2}");
            Console.WriteLine($"Attaque spéciale : {Arme.AttaqueSpe.Nom}; Effet : {Arme.AttaqueSpe.Description}");
            Console.WriteLine($"Arme : {Arme.Nom} (Dégâts Physiques : {Arme.DegatsPhysiques}, Dégâts Magiques : {Arme.DegatsMagiques}, Défense Physique : {Arme.Defense}, Défense Magique : {Arme.DefenseMagique}, Multiplicateur : {Arme.Multiplicateur})");
            Console.WriteLine("/*************************************************/");
        }

        public static void BuffEnCombat(Joueur joueur, string attributABuff, double multiplieur)
        {
            switch (attributABuff)
            {
                case "force":
                    joueur.ForceActuelle *= multiplieur;
                    Console.WriteLine($"La force de {joueur.Nom} a augmenté de {multiplieur} !");
                    Console.ResetColor();
                    break;
                case "agilite":
                    joueur.AgiliteActuelle *= multiplieur;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"L'agilité de {joueur.Nom} a augmenté de {multiplieur} !");
                    Console.ResetColor();
                    break;
                case "intelligence":
                    joueur.IntelligenceActuelle *= multiplieur;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"L'intelligence de {joueur.Nom} a augmenté de {multiplieur} !");
                    Console.ResetColor();
                    break;
                case "defense":
                    joueur.DefenseActuelle *= multiplieur;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"La défense de {joueur.Nom} a augmenté de {multiplieur} !");
                    Console.ResetColor();
                    break;
                case "defenseMagique":
                    joueur.DefenseMagiqueActuelle *= multiplieur;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"La défense magique de {joueur.Nom} a augmenté de {multiplieur} !");
                    Console.ResetColor();
                    break;
                default:
                    Console.WriteLine("Attribut invalide.");
                    break;
            }

        }

        public void RestaurerStatistiques()
        {
            PointsDeVieActuels = Classe.PointsDeVie;
            ForceActuelle = Classe.Force;
            AgiliteActuelle = Classe.Agilite;
            IntelligenceActuelle = Classe.Intelligence;
            DefenseActuelle = Classe.Defense;
            DefenseMagiqueActuelle = Classe.DefenseMagique;
        }

        public void VerifierBonusNiveau()
        {
            //if (Niveau == 5)
            //{
            //    switch (Classe.Nom)
            //    {
            //        case "Guerrier":
            //            AjouterArme(new Armes("Epée en fer", 10, 0, "Guerrier", 1.3, 5, 0, 0));
            //            Console.WriteLine("Vous avez débloqué une nouvelle arme : Epée en fer !");
            //            break;
            //        case "Mage":
            //            AjouterArme(new Armes("Bâton en fer", 0, 10, "Mage", 1.5, 0, 5, 0));
            //            Console.WriteLine("Vous avez débloqué une nouvelle arme : Bâton en fer !");
            //            break;
            //        case "Archer":
            //            AjouterArme(new Armes("Arc en fer", 12, 0, "Archer", 1.3, 0, 0, 3));
            //            Console.WriteLine("Vous avez débloqué une nouvelle arme : Arc en fer !");
            //            break;
            //        case "Assassin":
            //            AjouterArme(new Armes("Dague en fer", 8, 0, "Assassin", 1.8, 0, 0, 7));
            //            Console.WriteLine("Vous avez débloqué une nouvelle arme : Dague en fer !");
            //            break;
            //        case "Défenseur":
            //            AjouterArme(new Armes("Bouclier en fer", 5, 0, "Défenseur", 1.6, 10, 0, 0));
            //            Console.WriteLine("Vous avez débloqué une nouvelle arme : Bouclier en fer !");
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }
    }
}
