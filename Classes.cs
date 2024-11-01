namespace MiniProjet
{
    public class Classes(string nom, double pointsDeVie, double force, double agilite, double intelligence, double defense, double defenseMagique, double vitesse = 50, int niveau = 0)
    {
        public string Nom { get; set; } = nom;
        public int Niveau { get; set; } = niveau;
        public double PointsDeVie { get; set; } = pointsDeVie;
        public double Force { get; set; } = force;
        public double Agilite { get; set; } = agilite;
        public double Intelligence { get; set; } = intelligence;
        public double Defense { get; set; } = defense;
        public double DefenseMagique { get; set; } = defenseMagique;
        public double Vitesse { get; set; } = vitesse;


        // Classes de base
        public static readonly Classes guerrier = new("Guerrier", 140, 14, 6, 2, 12, 5);
        public static readonly Classes mage = new("Mage", 100, 4, 5, 24, 5, 10);
        public static readonly Classes archer = new("Archer", 100, 16, 14, 0, 7, 6);
        public static readonly Classes assassin = new("Assassin", 95, 10, 20, 3, 5, 5);
        public static readonly Classes defenseur = new("Défenseur", 160, 6, 4, 0, 15, 12);

        // Classes peu communes (niveau 5)
        public static readonly Classes paladin = new("Paladin", 150, 8, 6, 8, 12, 12);
        public static readonly Classes lancier = new("Lancier", 115, 14, 9, 0, 10, 6);
        public static readonly Classes revenant = new("Revenant", 125, 10, 7, 0, 9, 8);
        public static readonly Classes arcaniste = new("Arcaniste", 90, 0, 5, 24, 6, 10);
        public static readonly Classes artilleur = new("Artilleur", 115, 12, 12, 0, 8, 6);

        // Classes rares (niveau 15)
        public static readonly Classes berserker = new("Berserker", 150, 24, 8, 0, 7, 6);
        public static readonly Classes samurai = new("Samurai", 125, 16, 14, 0, 8, 6);
        public static readonly Classes sniper = new("Sniper", 100, 0, 24, 0, 6, 6);
        public static readonly Classes valkyrie = new("Valkyrie", 115, 10, 6, 14, 8, 10);
        public static readonly Classes vampire = new("Vampire", 135, 8, 10, 10, 7, 8);

        // Classes épiques (niveau 30)
        public static readonly Classes demon = new("Démon", 200, 24, 12, 24, 12, 15);
        public static readonly Classes pyromancien = new("Pyromancien", 150, 12, 8, 28, 7, 12);
        public static readonly Classes cryomancien = new("Cryomancien", 150, 12, 8, 28, 7, 12);
        public static readonly Classes shinobi = new("Shinobi", 140, 14, 24, 12, 7, 7);
        public static readonly Classes pugiliste = new("Pugiliste", 145, 18, 15, 6, 10, 8);

        //Classes légendaire (niveau 50)

        //Classes mythique (niveau 75)

        //Classes suprême (niveau 105)

        //classes parangon I (niveau 140)

        //classes parangon II (niveau 180)

        //classes parangon III (niveau 220)

        //classes parangon IV (niveau 275)

        //classes parangon V (niveau 330)

        public static void CalculerMonteeDeNiveau(Joueur joueur)
        {
            string nomClasse = joueur.Classe.Nom;
            switch (nomClasse)
            {
                case "Guerrier":
                    joueur.Classe.Force += 4.5;
                    joueur.Classe.Defense += 4;
                    joueur.Classe.DefenseMagique += 2.5;
                    joueur.Classe.Intelligence += 1.5;
                    joueur.Classe.PointsDeVie += 8;
                    joueur.Classe.Vitesse += 0.8;
                    joueur.Classe.Agilite += 0.25;
                    break;

                case "Mage":
                    joueur.Classe.Force += 1.5;
                    joueur.Classe.Defense += 2;
                    joueur.Classe.DefenseMagique += 4.5;
                    joueur.Classe.Intelligence += 5.5;
                    joueur.Classe.PointsDeVie += 6;
                    joueur.Classe.Vitesse += 0.8;
                    joueur.Classe.Agilite += 0.3;
                    break;

                case "Archer":
                    joueur.Classe.Force += 4.5;
                    joueur.Classe.Defense += 2.5;
                    joueur.Classe.DefenseMagique += 2;
                    joueur.Classe.Intelligence += 2.5;
                    joueur.Classe.PointsDeVie += 7;
                    joueur.Classe.Vitesse += 1.1;
                    joueur.Classe.Agilite += 0.5;
                    break;

                case "Assassin":
                    joueur.Classe.Force += 3;
                    joueur.Classe.Defense += 2;
                    joueur.Classe.DefenseMagique += 2.5;
                    joueur.Classe.Intelligence += 3.5;
                    joueur.Classe.PointsDeVie += 5.5;
                    joueur.Classe.Vitesse += 1.2;
                    joueur.Classe.Agilite += 0.55;
                    break;

                case "Défenseur":
                    joueur.Classe.Force += 2.5;
                    joueur.Classe.Defense += 5;
                    joueur.Classe.DefenseMagique += 4;
                    joueur.Classe.Intelligence += 1.5;
                    joueur.Classe.PointsDeVie += 9;
                    joueur.Classe.Vitesse += 0.8;
                    joueur.Classe.Agilite += 0.2;
                    break;

                case "Paladin":
                    joueur.Classe.Force += 3;
                    joueur.Classe.Defense += 4.5;
                    joueur.Classe.DefenseMagique += 3.5;
                    joueur.Classe.Intelligence += 2.5;
                    joueur.Classe.PointsDeVie += 8.5;
                    joueur.Classe.Vitesse += 0.85;
                    joueur.Classe.Agilite += 0.25;
                    break;

                case "Lancier":
                    joueur.Classe.Force += 4.5;
                    joueur.Classe.Defense += 3;
                    joueur.Classe.DefenseMagique += 2;
                    joueur.Classe.Intelligence += 2.5;
                    joueur.Classe.PointsDeVie += 7.5;
                    joueur.Classe.Vitesse += 1.0;
                    joueur.Classe.Agilite += 0.45;
                    break;

                case "Revenant":
                    joueur.Classe.Force += 4.5;
                    joueur.Classe.Defense += 3;
                    joueur.Classe.DefenseMagique += 2.5;
                    joueur.Classe.Intelligence += 2.5;
                    joueur.Classe.PointsDeVie += 8;
                    joueur.Classe.Vitesse += 0.9;
                    joueur.Classe.Agilite += 0.3;
                    break;

                case "Arcaniste":
                    joueur.Classe.Force += 2;
                    joueur.Classe.Defense += 2;
                    joueur.Classe.DefenseMagique += 5;
                    joueur.Classe.Intelligence += 5.5;
                    joueur.Classe.PointsDeVie += 6.5;
                    joueur.Classe.Vitesse += 0.85;
                    joueur.Classe.Agilite += 0.3;
                    break;

                case "Artilleur":
                    joueur.Classe.Force += 3;
                    joueur.Classe.Defense += 4;
                    joueur.Classe.DefenseMagique += 3.5;
                    joueur.Classe.Intelligence += 4;
                    joueur.Classe.PointsDeVie += 7;
                    joueur.Classe.Vitesse += 0.8;
                    joueur.Classe.Agilite += 0.3;
                    break;

                case "Berserker":
                    joueur.Classe.Force += 7;
                    joueur.Classe.Defense += 3.5;
                    joueur.Classe.DefenseMagique += 2;
                    joueur.Classe.Intelligence += 2;
                    joueur.Classe.PointsDeVie += 9;
                    joueur.Classe.Vitesse += 0.9;
                    joueur.Classe.Agilite += 0.2;
                    break;

                case "Samurai":
                    joueur.Classe.Force += 5;
                    joueur.Classe.Defense += 3.5;
                    joueur.Classe.DefenseMagique += 2.5;
                    joueur.Classe.Intelligence += 2.5;
                    joueur.Classe.PointsDeVie += 8;
                    joueur.Classe.Vitesse += 1.0;
                    joueur.Classe.Agilite += 0.5;
                    break;

                case "Sniper":
                    joueur.Classe.Force += 3.5;
                    joueur.Classe.Defense += 2.5;
                    joueur.Classe.DefenseMagique += 2.5;
                    joueur.Classe.Intelligence += 4.5;
                    joueur.Classe.PointsDeVie += 6.5;
                    joueur.Classe.Vitesse += 1.15;
                    joueur.Classe.Agilite += 0.45;
                    break;

                case "Valkyrie":
                    joueur.Classe.Force += 3.5;
                    joueur.Classe.Defense += 4;
                    joueur.Classe.DefenseMagique += 3.5;
                    joueur.Classe.Intelligence += 3.5;
                    joueur.Classe.PointsDeVie += 7.5;
                    joueur.Classe.Vitesse += 1.05;
                    joueur.Classe.Agilite += 0.4;
                    break;

                case "Vampire":
                    joueur.Classe.Force += 3.5;
                    joueur.Classe.Defense += 3.5;
                    joueur.Classe.DefenseMagique += 3.5;
                    joueur.Classe.Intelligence += 4;
                    joueur.Classe.PointsDeVie += 7;
                    joueur.Classe.Vitesse += 1.0;
                    joueur.Classe.Agilite += 0.35;
                    break;

                case "Démon":
                    joueur.Classe.Force += 6;
                    joueur.Classe.Defense += 4.5;
                    joueur.Classe.DefenseMagique += 4;
                    joueur.Classe.Intelligence += 5;
                    joueur.Classe.PointsDeVie += 9;
                    joueur.Classe.Vitesse += 0.95;
                    joueur.Classe.Agilite += 0.3;
                    break;

                case "Pyromancien":
                    joueur.Classe.Force += 3;
                    joueur.Classe.Defense += 3.5;
                    joueur.Classe.DefenseMagique += 3.5;
                    joueur.Classe.Intelligence += 5.5;
                    joueur.Classe.PointsDeVie += 6.5;
                    joueur.Classe.Vitesse += 1.0;
                    joueur.Classe.Agilite += 0.35;
                    break;

                case "Cryomancien":
                    joueur.Classe.Force += 3;
                    joueur.Classe.Defense += 4;
                    joueur.Classe.DefenseMagique += 4;
                    joueur.Classe.Intelligence += 5;
                    joueur.Classe.PointsDeVie += 7;
                    joueur.Classe.Vitesse += 0.95;
                    joueur.Classe.Agilite += 0.3;
                    break;

                case "Shinobi":
                    joueur.Classe.Force += 4;
                    joueur.Classe.Defense += 3;
                    joueur.Classe.DefenseMagique += 3;
                    joueur.Classe.Intelligence += 4.5;
                    joueur.Classe.PointsDeVie += 6.5;
                    joueur.Classe.Vitesse += 1.2;
                    joueur.Classe.Agilite += 0.55;
                    break;

                case "Pugiliste":
                    joueur.Classe.Force += 5.5;
                    joueur.Classe.Defense += 4;
                    joueur.Classe.DefenseMagique += 3.5;
                    joueur.Classe.Intelligence += 3.5;
                    joueur.Classe.PointsDeVie += 7.5;
                    joueur.Classe.Vitesse += 0.9;
                    joueur.Classe.Agilite += 0.3;
                    break;
            }
        }
    }
}
