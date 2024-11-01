namespace MiniProjet
{
        public class Ennemis(string nom, double pointsDeVie, double force, double agilite, double intelligence, double defense, double defenseMagique, double vitesse, int experienceDonne, int pieceDorDonnee, string typeEnnemi)
    {
        public string Nom { get; set; } = nom;
        public double PointsDeVie { get; set; } = pointsDeVie;
        public double Force { get; set; } = force;
        public double Agilite { get; set; } = agilite;
        public double Intelligence { get; set; } = intelligence;
        public double Defense { get; set; } = defense;
        public double DefenseMagique { get; set; } = defenseMagique;
        public double Vitesse { get; set; } = vitesse;
        public int ExperienceDonne { get; set; } = experienceDonne;
        public int PieceDorDonnee { get; set; } = pieceDorDonnee;
        public string TypeEnnemi { get; set; } = typeEnnemi;
        public bool EstStun { get; set; } = false;
        public int ToursStun { get; set; } = 0;
        public Dictionary<string, double> BuffsActifs { get; set; } = [];
        public Dictionary<string, double> NerfsActifs { get; set; } = [];

        public static Ennemis CréerEnnemiAleatoire(Joueur joueur)
        {
            string[] nomsEnnemis = [
                "Gobelin Mage",
                "Gobelin Guerrier",
                "Gobelin Archer",
                "Gobelin Prêtre",
                "Gobelin Défenseur",
                "Orc Brute",
                "Orc Chaman",
                "Troll des Cavernes",
                "Dragonnet des Flammes",
                "Loup Garou Alpha",
                "Nécromancien Sombre",
                "Sorcière de l'Ombre",
                "Squelette Guerrier",
                "Squelette Arbalétrier",
                "Vampire Seigneur",
                "Liche Infernale",
                "Assassin du Crépuscule",
                "Godrick le Gréffé",
                "Balrog de l'Abîme",
                "Démon Infernal",
                "Esprit de la Forêt",
                "Garde Royal Déchu",
                "Golem de Pierre",
            ];
            Random random = new();
            string nomAleatoire = nomsEnnemis[random.Next(nomsEnnemis.Length)];
            Ennemis ennemi = GenererEnnemiAleatoire(nomAleatoire, joueur.Classe.Niveau);
            return ennemi;
        }

        public static Ennemis GenererEnnemiAleatoire(string nom, int niveau)
        {
            Random random = new();
            double pointsRestants = 50;
            string[] typesEnnemis = ["offensifPhysique", "offensifMagique", "defensifPhysique", "defensifMagique", "agile", "equilibre"];
            string typeEnnemiRand = typesEnnemis[random.Next(typesEnnemis.Length)];

            double force = 0, agilite = 0, intelligence = 0, defense = 0, defenseMagique = 0, vitesse = 50;
            double vieDeBase = 90;
            //Je veux qu'on assigne à niveau une nouvelle valeur soit niveau est égale à niveau soit elle est entre niveau et niveau + 5 ce qui va faire varier la difficultée
            niveau = random.Next(niveau, niveau + 4);

            switch (typeEnnemiRand)
            {
                case "offensifPhysique":
                    vieDeBase = 85 + (niveau * 9); // Réduit légèrement pour compenser la haute force
                    force = Math.Round(pointsRestants * 0.6 + (niveau * 4.5), 2);
                    defense = Math.Round(pointsRestants * 0.12 + (niveau * 2.5), 2);
                    defenseMagique = Math.Round(pointsRestants * 0.10 + (niveau * 1.5), 2);
                    intelligence = Math.Round(pointsRestants * 0.08 + (niveau * 1), 2);
                    agilite = Math.Min(Math.Round(pointsRestants * 0.10 + (niveau * 0.3), 2), 50);
                    vitesse += 1;
                    break;

                case "offensifMagique":
                    vieDeBase = 85 + (niveau * 9);
                    intelligence = Math.Round(pointsRestants * 0.6 + (niveau * 4.5), 2);
                    force = Math.Round(pointsRestants * 0.12 + (niveau * 1.5), 2);
                    defense = Math.Round(pointsRestants * 0.10 + (niveau * 1.5), 2);
                    defenseMagique = Math.Round(pointsRestants * 0.10 + (niveau * 2.5), 2);
                    agilite = Math.Min(Math.Round(pointsRestants * 0.10 + (niveau * 0.3), 2), 50);
                    vitesse += 1;
                    break;

                case "defensifPhysique":
                    vieDeBase = 130 + (niveau * 10); // Plus de PV pour équilibrer la défense physique élevée
                    defense = Math.Round(pointsRestants * 0.55 + (niveau * 3.2), 2);
                    force = Math.Round(pointsRestants * 0.15 + (niveau * 1.5), 2);
                    defenseMagique = Math.Round(pointsRestants * 0.08 + (niveau * 1.4), 2);
                    intelligence = Math.Round(pointsRestants * 0.05 + (niveau * 1), 2);
                    agilite = Math.Min(Math.Round(pointsRestants * 0.07 + (niveau * 0.1), 2), 45);
                    vitesse += 0.9;
                    break;

                case "defensifMagique":
                    vieDeBase = 130 + (niveau * 10);
                    defenseMagique = Math.Round(pointsRestants * 0.55 + (niveau * 3.2), 2);
                    defense = Math.Round(pointsRestants * 0.08 + (niveau * 1.4), 2);
                    force = Math.Round(pointsRestants * 0.05 + (niveau * 1), 2);
                    intelligence = Math.Round(pointsRestants * 0.15 + (niveau * 1.5), 2);
                    agilite = Math.Min(Math.Round(pointsRestants * 0.07 + (niveau * 0.1), 2), 45);
                    vitesse += 0.9;
                    break;

                case "agile":
                    vieDeBase = 95 + (niveau * 8); // Moins de PV pour compenser l'agilité élevée
                    agilite = Math.Min(Math.Round(pointsRestants * 0.55 + (niveau * 0.4), 2), 60);
                    force = Math.Round(pointsRestants * 0.18 + (niveau * 2.5), 2);
                    intelligence = Math.Round(pointsRestants * 0.08 + (niveau * 1.5), 2);
                    defense = Math.Round(pointsRestants * 0.10 + (niveau * 1.8), 2);
                    defenseMagique = Math.Round(pointsRestants * 0.10 + (niveau * 1.8), 2);
                    vitesse += 1.2;
                    break;

                case "equilibre":
                    vieDeBase = 90 + (niveau * 8.5); // Valeur moyenne pour équilibrer avec le reste
                    force = Math.Round(pointsRestants * 0.18 + (niveau * 2.5), 2);
                    agilite = Math.Min(Math.Round(pointsRestants * 0.15, 2) + (niveau * 0.2), 55);
                    intelligence = Math.Round(pointsRestants * 0.18 + (niveau * 2.5), 2);
                    defense = Math.Round(pointsRestants * 0.15 + (niveau * 2.5), 2);
                    defenseMagique = Math.Round(pointsRestants * 0.15 + (niveau * 2.5), 2);
                    vitesse += 1.1;
                    break;
            }

            int experienceDonne = GenererRessourcesDonnee(random);
            int pieceDorDonnee = GenererRessourcesDonnee(random);
            return new Ennemis(nom, vieDeBase, force, agilite, intelligence, defense, defenseMagique, vitesse, experienceDonne, pieceDorDonnee, typeEnnemiRand);
        }

        private static int GenererRessourcesDonnee(Random random)
        {
            int tirage = random.Next(1, 101);
            if (tirage <= 68)
            {
                return random.Next(10, 71);
            }
            else if (tirage <= 83)
            {
                return random.Next(71, 101);
            }
            else if (tirage <= 93)
            {
                return random.Next(101, 201);
            }
            else if (tirage <= 98)
            {
                return random.Next(201, 401);
            }
            else
            {
                return random.Next(401, 501);
            }
        }

        public void AfficherStatistiques()
        {
            Console.WriteLine($"\n--- Statistiques de l'ennemi {Nom} ---");
            Console.WriteLine($"Force : {Force:F2}");
            Console.WriteLine($"Agilité : {Agilite:F2}");
            Console.WriteLine($"Intelligence : {Intelligence:F2}");
            Console.WriteLine($"Défense physique : {Defense:F2}");
            Console.WriteLine($"Défense magique : {DefenseMagique:F2}");
            Console.WriteLine("--------------------------------------\n");
        }

        public void RecevoirDegats(double degats)
        {
            PointsDeVie -= degats;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Nom} a subi {degats:F1} points de dégâts.");
            Console.ResetColor();
            if (PointsDeVie <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Nom} a été vaincu !");
                Console.ResetColor();
            }
        }

        public void RecevoirSoins(double soins, double pvMax)
        {
            PointsDeVie += soins;
            if (PointsDeVie > pvMax)
            {
                PointsDeVie = pvMax;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Nom} a été soigné de {soins:F1} points de vie.");
            Console.ResetColor();
        }

        public void AppliquerStun(int tours)
        {
            EstStun = true;
            ToursStun = tours;
        }

        public bool EstEncoreStun()
        {
            if (EstStun)
            {
                if (ToursStun > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Nom} est étourdi et ne peut pas attaquer pendant encore {ToursStun} tours.");
                    Console.ResetColor();
                    ToursStun--;
                    return true;
                }
                else
                {
                    EstStun = false;
                    Console.WriteLine($"{Nom} n'est plus étourdi !");
                }
            }
            return false;
        }

        public void AppliquerBuff(string attribut, double montant)
        {
            switch (attribut)
            {
                case "force":
                    Force += montant;
                    break;
                case "agilite":
                    Agilite += montant;
                    break;
                case "intelligence":
                    Intelligence += montant;
                    break;
                case "defense":
                    Defense += montant;
                    break;
                case "defenseMagique":
                    DefenseMagique += montant;
                    break;
                default:
                    break;
            }
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine($"{Nom} a reçu un buff de {montant} sur {attribut}.");
            Console.ResetColor();
        }

        public void AppliquerNerf(string attribut, double montant)
        {
            switch (attribut)
            {
                case "force":
                    Force = Math.Max(Force - montant, 0.5);
                    break;
                case "agilite":
                    Agilite = Math.Max(Agilite - montant, 0.5);
                    break;
                case "intelligence":
                    Intelligence = Math.Max(Intelligence - montant, 0.5);
                    break;
                case "defense":
                    Defense = Math.Max(Defense - montant, 0.5);
                    break;
                case "defenseMagique":
                    DefenseMagique = Math.Max(DefenseMagique - montant, 0.5);
                    break;
                case "vitesse":
                    Vitesse = Math.Max(Vitesse - montant, 0.5);
                    break;
                default:
                    break;
            }
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine($"{Nom} a reçu un nerf de {montant} sur {attribut}.");
            Console.ResetColor();
        }

        public void NettoyerEffets()
        {
            BuffsActifs.Clear();
            NerfsActifs.Clear();
        }
    }
}
