using System.Text.Json.Serialization;

namespace MiniProjet
{
    public class Joueur
    {
        public string Nom { get; set; }
        public int Experience { get; set; }
        public int Niveau { get; set; }
        public Classes Classe { get; set; }
        public Armes Arme { get; set; }

        [JsonIgnore]
        public Dictionary<Objets, int> Inventaire { get; set; }
        public Dictionary<int, Armes> Armurerie { get; set; }
        public Dictionary<int, Classes> ClassesDebloquee { get; set; }
        public double PointsDeVieActuels { get; set; }
        public double ForceActuelle { get; set; }
        public double AgiliteActuelle { get; set; }
        public double IntelligenceActuelle { get; set; }
        public double DefenseActuelle { get; set; }
        public double DefenseMagiqueActuelle { get; set; }
        public double VitesseActuelle { get; set; }
        public int PieceDor { get; set; }
        public int prochainIdArme;
        public int prochainIdClasse;

        public Joueur(string nom, int experience, int niveau, Classes classe, Armes arme, double pointsDeVieActuels, double forceActuelle, double agiliteActuelle, double intelligenceActuelle, double defenseActuelle, double defenseMagiqueActuelle, double vitesseActuelle, int pieceDor)
        {
            Nom = nom;
            Experience = experience;
            Niveau = niveau;
            Classe = classe;
            Arme = arme;
            Inventaire = [];
            Armurerie = [];
            ClassesDebloquee = [];
            PointsDeVieActuels = pointsDeVieActuels;
            ForceActuelle = forceActuelle;
            AgiliteActuelle = agiliteActuelle;
            IntelligenceActuelle = intelligenceActuelle;
            DefenseActuelle = defenseActuelle;
            DefenseMagiqueActuelle = defenseMagiqueActuelle;
            VitesseActuelle = vitesseActuelle;
            PieceDor = pieceDor;
            prochainIdArme = 1;
            prochainIdClasse = 1;
            AjouterObjetsDeBase();
        }

        public Dictionary<string, int> InventaireSerializable
        {
            get => Inventaire.ToDictionary(entry => entry.Key.Nom, entry => entry.Value);
            set
            {
                var inventaireTemporaire = new Dictionary<Objets, int>();

                foreach (var entry in value)
                {
                    Objets objet = ConvertirEnObjet(entry.Key);

                    if (objet != null)
                    {
                        if (!inventaireTemporaire.ContainsKey(objet))
                        {
                            inventaireTemporaire[objet] = entry.Value;
                        }
                        else
                        {
                            Console.WriteLine($"Doublon détecté pour l'objet : {entry.Key}, ignoré lors de la reconstruction.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Objet introuvable : {entry.Key}, ignoré lors de la reconstruction.");
                    }
                }

                Inventaire = inventaireTemporaire;
            }
        }

        private static Objets ConvertirEnObjet(string nomObjet)
        {
            return Objets.ListeObjets.FirstOrDefault(objet => objet.Nom == nomObjet);
        }

        private void AjouterObjetsDeBase()
        {
            Inventaire[Objets.potionSoin] = 5;
            Inventaire[Objets.potionDegats] = 3;
        }

        public void AjouterObjet(Objets objet)
        {
            if (Inventaire.TryGetValue(objet, out int value))
            {
                Inventaire[objet] = ++value;
            }
            else
            {
                Inventaire[objet] = 1;
            }
        }

        public void AjouterArme(Armes arme)
        {
            if (Armurerie.Count > 0)
            {
                prochainIdArme = Armurerie.Keys.Max() + 1;
            }
            else
            {
                prochainIdArme = 1;
            }
            Armurerie[prochainIdArme] = arme;
            prochainIdArme++;
        }

        public void AjouterClasse(Classes classe)
        {
            if (ClassesDebloquee.Count > 0)
            {
                prochainIdClasse = ClassesDebloquee.Keys.Max() + 1;
            }
            else
            {
                prochainIdClasse = 1;
            }
            ClassesDebloquee[prochainIdClasse] = classe;
            prochainIdClasse++;
        }

        public void MettreAJourClassesDebloquees(Classes classe)
        {
            var classeExistante = ClassesDebloquee.FirstOrDefault(c => c.Value.Nom == classe.Nom);

            if (classeExistante.Value != null)
            {
                ClassesDebloquee[classeExistante.Key] = classe;
            }
        }

        public void UtiliserObjet(Objets objet, Ennemis ennemi)
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

        public void AfficherClassesDebloquee()
        {
            Console.WriteLine("\n=== Classes débloquées ===");
            foreach (var item in ClassesDebloquee)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Nom}");
            }

            Console.WriteLine("Veuillez choisir une classe parmi les propositions ci-dessus ou appuyer sur Entrée pour revenir au menu.");
            string choix = Console.ReadLine();

            if (int.TryParse(choix, out int idClasse) && ClassesDebloquee.TryGetValue(idClasse, out Classes? value))
            {
                Joueur.ChangerClasse(this, value);
                Console.WriteLine($"Vous avez choisi la classe {Classe.Nom} !");
            }
            else if (!string.IsNullOrEmpty(choix))
            {
                Console.WriteLine("Classe non trouvée.");
            }
        }

        public static void ChangerClasse(Joueur joueur, Classes classe)
        {
            joueur.Classe = classe;
            joueur.PointsDeVieActuels = classe.PointsDeVie;
            joueur.ForceActuelle = classe.Force;
            joueur.AgiliteActuelle = classe.Agilite;
            joueur.IntelligenceActuelle = classe.Intelligence;
            joueur.DefenseActuelle = classe.Defense;
            joueur.DefenseMagiqueActuelle = classe.DefenseMagique;
        }

        public static Joueur CreerJoueur()
        {
            Armes baseArme = Armes.epeeEnBois;
            Console.Write("Entrez le nom de votre personnage : ");
            string nom = Console.ReadLine();

            Console.WriteLine("Choisissez une classe pour votre personnage :");
            Classes classe = AfficherClasses();
            if (classe == null)
            {
                Console.WriteLine("Choix invalide. Un guerrier par défaut sera choisi.");
                classe = Classes.guerrier;
            }

            Armes arme = AfficherArmesDeBase();
            if (arme == null)
            {
                Console.WriteLine("Choix invalide. Une épée en bois par défaut sera choisie.");
                arme = baseArme;
            }
            Joueur joueur = new(nom, 0, 0, classe, arme, classe.PointsDeVie, classe.Force, classe.Agilite, classe.Intelligence, classe.Defense, classe.DefenseMagique, classe.Vitesse, 50);
            joueur.AjouterArme(arme);
            joueur.AjouterClasse(classe);

            return joueur;
        }

        public void GagnerExperience(int points)
        {
            Experience += points;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Nom} a gagné {points} points d'expérience !");
            Console.ResetColor();

            while (Experience >= 100)
            {
                MonteDeNiveau();
            }
        }

        private void MonteDeNiveau()
        {
            Niveau += 1;
            Classe.Niveau += 1;
            Classe.Niveau = Math.Min(Classe.Niveau, 100);
            Experience -= 100;
            Boutique.VerificationNouveauxItems(Niveau);

            if (Classe.Niveau < 100)
            {
                Classes.CalculerMonteeDeNiveau(this);
            }

            PointsDeVieActuels = Classe.PointsDeVie;
            MettreAJourClassesDebloquees(Classe);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Nom} est monté au niveau {Niveau} !");
            Console.WriteLine($"La classe {Classe.Nom} est montée au niveau {Classe.Niveau} !");
            Console.WriteLine($"Les statistques de la classe {Classe.Nom} ont augmenté !");
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
            Console.WriteLine("1 - Guerrier");
            Console.WriteLine("2 - Archer");
            Console.WriteLine("3 - Mage");
            Console.WriteLine("4 - Défenseur");
            Console.WriteLine("5 - Assassin");
            Console.WriteLine("Veuillez choisir une classe parmi les propositions ci-dessus :");
            string choix = Console.ReadLine();

            return choix switch
            {
                "1" => Classes.guerrier,
                "2" => Classes.archer,
                "3" => Classes.mage,
                "4" => Classes.defenseur,
                "5" => Classes.assassin,
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

        public (double forceFinale, double agiliteFinale, double intelligenceFinale, double defenseFinale, double defenseMagiqueFinale, double vitesseFinale) CalculerStatistiquesFinales()
        {
            double forceFinale = ForceActuelle;
            double agiliteFinale = AgiliteActuelle;
            double intelligenceFinale = IntelligenceActuelle;
            double defenseFinale = DefenseActuelle;
            double defenseMagiqueFinale = DefenseMagiqueActuelle;
            double vitesseFinale = VitesseActuelle;
            double multiplicateur = Arme.ClasseDefaut == Classe.Nom ? Arme.Multiplicateur : 1.0;

            double degatsPhysiquesArme = Arme.DegatsPhysiques * multiplicateur;
            double degatsMagiquesArme = Arme.DegatsMagiques * multiplicateur;
            double defenseArme = Arme.Defense * multiplicateur;
            double defenseMagiqueArme = Arme.DefenseMagique * multiplicateur;
            double agiliteArme = Arme.Agilite * multiplicateur;
            double vitesseArme = Arme.Vitesse * multiplicateur;

            forceFinale += degatsPhysiquesArme;
            intelligenceFinale += degatsMagiquesArme;
            defenseFinale += defenseArme;
            defenseMagiqueFinale += defenseMagiqueArme;
            agiliteFinale += agiliteArme;
            vitesseFinale += vitesseArme;

            return (forceFinale, agiliteFinale, intelligenceFinale, defenseFinale, defenseMagiqueFinale, vitesseFinale);
        }

        public void AfficherStatistiques()
        {
            var (forceFinale, agiliteFinale, intelligenceFinale, defenseFinale, defenseMagiqueFinale, vitesseFinale) = CalculerStatistiquesFinales();

            Console.WriteLine("/***********/ Statistiques du joueur /***********/");
            Console.WriteLine($"Nom : {Nom} | Pièces d'or : {PieceDor}");
            Console.WriteLine($"Classe : {Classe.Nom}");
            Console.WriteLine($"Niveau : {Niveau}  |  Niveau de la classe : {Classe.Niveau}");
            Console.WriteLine($"Expérience : {Experience}");
            Console.WriteLine($"Points de vie : {Classe.PointsDeVie:F2}");
            Console.WriteLine($"Agilité : {agiliteFinale:F2} | Vitesse : {vitesseFinale:F2}");
            Console.WriteLine($"Force (dégâts physiques) : {forceFinale:F2} | Intelligence (dégâts magiques) : {intelligenceFinale:F2}");
            Console.WriteLine($"Défense physique : {defenseFinale:F2} | Défense magique : {defenseMagiqueFinale:F2}");
            Console.WriteLine($"Arme : {Arme.Nom} | Attaque spéciale : {Arme.AttaqueSpe.Nom} | Effet : {Arme.AttaqueSpe.Description}");
            Console.WriteLine("/*************************************************/");
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

        public void AppliquerBuff(string stat, int montant)
        {
            switch (stat)
            {
                case "force":
                    ForceActuelle += montant;
                    break;
                case "agilite":
                    AgiliteActuelle += montant;
                    break;
                case "intelligence":
                    IntelligenceActuelle += montant;
                    break;
                case "defense":
                    DefenseActuelle += montant;
                    break;
                case "defenseMagique":
                    DefenseMagiqueActuelle += montant;
                    break;
                default:
                    break;
            }
        }
    }
}
