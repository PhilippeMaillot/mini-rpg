using System;

namespace MiniProjet
{
    public class Ennemis(string nom, double pointsDeVie, double force, double agilite, double intelligence, double defense, double defenseMagique, int experienceDonne, int pieceDorDonnee, string typeEnnemi)
    {
        public string Nom { get; set; } = nom;
        public double PointsDeVie { get; set; } = pointsDeVie;
        public double Force { get; set; } = force;
        public double Agilite { get; set; } = agilite;
        public double Intelligence { get; set; } = intelligence;
        public double Defense { get; set; } = defense;
        public double DefenseMagique { get; set; } = defenseMagique;
        public int ExperienceDonne { get; set; } = experienceDonne;
        public int PieceDorDonnee { get; set; } = pieceDorDonnee;
        public string TypeEnnemi { get; set; } = typeEnnemi;

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
            Ennemis ennemi = GenererEnnemiAleatoire(nomAleatoire, joueur.Niveau);
            return ennemi;
        }

        public static Ennemis GenererEnnemiAleatoire(string nom, int niveauJoueur)
        {
            Random random = new();
            double pointsRestants = Math.Round(Math.Pow(45, 0.025) * niveauJoueur + 45, 2);
            string[] typesEnnemis = ["offensifPhysique", "offensifMagique", "defensifPhysique", "defensifMagique", "agile", "equilibre"];
            string typeEnnemiRand = typesEnnemis[random.Next(typesEnnemis.Length)];

            double force = 0, agilite = 0, intelligence = 0, defense = 0, defenseMagique = 0;
            double vieDeBase = 90;

            switch (typeEnnemiRand)
            {
                case "offensifPhysique":
                    vieDeBase = 80;
                    force = Math.Round(pointsRestants * 0.65, 2);
                    defense = Math.Round(pointsRestants * 0.10, 2);
                    defenseMagique = Math.Round(pointsRestants * 0.15, 2);
                    intelligence = Math.Round(pointsRestants * 0.05, 2);
                    agilite = Math.Round(pointsRestants * 0.05, 2);
                    break;

                case "offensifMagique":
                    vieDeBase = 80;
                    intelligence = Math.Round(pointsRestants * 0.65, 2);
                    force = Math.Round(pointsRestants * 0.15, 2);
                    defense = Math.Round(pointsRestants * 0.05, 2);
                    defenseMagique = Math.Round(pointsRestants * 0.10, 2);
                    agilite = Math.Round(pointsRestants * 0.05, 2);
                    break;

                case "defensifPhysique":
                    vieDeBase = 140;
                    defense = Math.Round(pointsRestants * 0.65, 2);
                    force = Math.Round(pointsRestants * 0.10, 2);
                    defenseMagique = Math.Round(pointsRestants * 0.05, 2);
                    intelligence = Math.Round(pointsRestants * 0.05, 2);
                    agilite = Math.Round(pointsRestants * 0.15, 2);
                    break;

                case "defensifMagique":
                    vieDeBase = 140;
                    defenseMagique = Math.Round(pointsRestants * 0.65, 2);
                    defense = Math.Round(pointsRestants * 0.05, 2);
                    force = Math.Round(pointsRestants * 0.10, 2);
                    intelligence = Math.Round(pointsRestants * 0.05, 2);
                    agilite = Math.Round(pointsRestants * 0.15, 2);
                    break;

                case "agile":
                    vieDeBase = 100;
                    agilite = Math.Round(pointsRestants * 0.50, 2);
                    force = Math.Round(pointsRestants * 0.15, 2);
                    intelligence = Math.Round(pointsRestants * 0.15, 2);
                    defense = Math.Round(pointsRestants * 0.10, 2);
                    defenseMagique = Math.Round(pointsRestants * 0.10, 2);
                    break;

                case "equilibre":
                    vieDeBase = 90;
                    force = Math.Round(pointsRestants * 0.20, 2);
                    agilite = Math.Round(pointsRestants * 0.20, 2);
                    intelligence = Math.Round(pointsRestants * 0.20, 2);
                    defense = Math.Round(pointsRestants * 0.20, 2);
                    defenseMagique = Math.Round(pointsRestants * 0.20, 2);
                    break;
            }

            double pointsDeVie = Math.Round(Math.Pow(vieDeBase, 0.015) * niveauJoueur + vieDeBase, 2);
            int experienceDonne = GenererExperienceDonnee(random);
            int pieceDorDonnee = GenererExperienceDonnee(random);
            return new Ennemis(nom, pointsDeVie, force, agilite, intelligence, defense, defenseMagique, experienceDonne, pieceDorDonnee, typeEnnemiRand);
        }

        private static int GenererExperienceDonnee(Random random)
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
            Console.WriteLine($"{Nom} a subi {degats:F1} points de dégâts.");
            if (PointsDeVie <= 0)
            {
                Console.WriteLine($"{Nom} a été vaincu !");
            }
        }

        public void RecevoirSoins(double soins, double pvMax)
        {
            PointsDeVie += soins;
            if (PointsDeVie > pvMax)
            {
                PointsDeVie = pvMax;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Nom} a été soigné de {soins:F1} points de vie.");
            Console.ResetColor();
        }
    }
}
