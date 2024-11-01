namespace MiniProjet
{
    public class Armes(string nom, int degatsPhysiques, int degatsMagiques, string classeDefaut, double multiplicateur, int defense, int defenseMagique, int agilite, AttaqueSpe attaqueSpe, int vitesse = 0, AttaqueSpe attaqueSpe2 = null)
    {
        public string Nom { get; set; } = nom;
        public int DegatsPhysiques { get; set; } = degatsPhysiques;
        public int DegatsMagiques { get; set; } = degatsMagiques;
        public string ClasseDefaut { get; set; } = classeDefaut;
        public double Multiplicateur { get; set; } = multiplicateur;
        public int Defense { get; set; } = defense;
        public int DefenseMagique { get; set; } = defenseMagique;
        public int Agilite { get; set; } = agilite;
        public int Vitesse { get; set; } = vitesse;
        public AttaqueSpe AttaqueSpe { get; set; } = attaqueSpe;
        public AttaqueSpe AttaqueSpe2 { get; set; } = attaqueSpe2 ?? AttaqueSpe.coupDePied;

        //Armes de base
        public static readonly Armes epeeEnBois = new("Epée en bois", 10, 0, "Guerrier", 1.2, 3, 0, 0, AttaqueSpe.coupDeTonnerre);
        public static readonly Armes arcEnBois = new("Arc en bois", 14, 0, "Archer", 1.3, 0, 0, 1, AttaqueSpe.flecheEmpoisonnee);
        public static readonly Armes batonEnBois = new("Bâton en bois", 0, 10, "Mage", 1.4, 0, 3, 0, AttaqueSpe.explosionMagique);
        public static readonly Armes bouclierEnBois = new("Bouclier en bois", 6, 0, "Défenseur", 1.5, 7, 0, 0, AttaqueSpe.auraDeProtection, -3);
        public static readonly Armes dagueEnBois = new("Dague en bois", 7, 0, "Assassin", 1.7, 0, 0, 5, AttaqueSpe.ventDeTerreur, 3);

        //Armes peu communes (niveau 5)
        public static readonly Armes espadonLumineux = new("Espadon Lumineux", 20, 0, "Paladin", 1.2, 5, 0, 0, AttaqueSpe.frappeLumineuse);
        public static readonly Armes Lance = new("Lance", 13, 0, "Lancier", 1.2, 5, 0, 0, AttaqueSpe.PerceeCritique, 5);
        public static readonly Armes marteauDeChasse = new("Marteau de chasse", 12, 0, "Revenant", 1.3, 5, 0, 5, AttaqueSpe.sacrificeMortel, -10);
        public static readonly Armes grimoireDapprenti = new ("Grimoire d'apprenti", 0, 15, "Arcaniste", 1.4, 0, 5, 0, AttaqueSpe.ventDeTerreur, 3);

        //Armes rares (niveau 15)

        //Armes épiques (niveau 30)

        //Armes légendaires (niveau 50)

        //Armes mythiques (niveau 75)

        //Armes suprêmes (niveau 105)

        //Armes parangon I (niveau 140)

        //Armes parangon II (niveau 180)

        //Armes parangon III (niveau 225)

        //Armes parangon IV (niveau 275)

        //Armes parangon V (niveau 330)

        public static Armes RecupArme(string nomArme)
        {
            if (nomArme == "Epée en bois")
            {
                return epeeEnBois;
            }
            else if (nomArme == "Arc en bois")
            {
                return arcEnBois;
            }
            else if (nomArme == "Bâton en bois")
            {
                return batonEnBois;
            }
            else if (nomArme == "Bouclier en bois")
            {
                return bouclierEnBois;
            }
            else if (nomArme == "Dague en bois")
            {
                return dagueEnBois;
            }
            else
            {
                return epeeEnBois;
            }
        }
    }
}
