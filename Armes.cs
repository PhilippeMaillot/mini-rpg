namespace MiniProjet
{
    public class Armes(string nom, int degatsPhysiques, int degatsMagiques, string classeDefaut, double multiplicateur, int defense, int defenseMagique, int agilite, AttaqueSpe attaqueSpe)
    {
        public string Nom { get; set; } = nom;
        public int DegatsPhysiques { get; set; } = degatsPhysiques;
        public int DegatsMagiques { get; set; } = degatsMagiques;
        public string ClasseDefaut { get; set; } = classeDefaut;
        public double Multiplicateur { get; set; } = multiplicateur;
        public int Defense { get; set; } = defense;
        public int DefenseMagique { get; set; } = defenseMagique;
        public int Agilite { get; set; } = agilite;
        public AttaqueSpe AttaqueSpe { get; set; } = attaqueSpe;
        public Dictionary<int, AttaqueSpe> AttaquesSpeciales { get; set; } = [];

        public static Armes RecupArme(string nomArme)
        {
            Armes epeeEnBois = new("Epée en bois", 7, 0, "Guerrier", 1.2, 3, 0, 0, RecupAttaqueSpe(nomArme));
            Armes arcEnBois = new("Arc en bois", 9, 0, "Archer", 1.3, 0, 0, 1, RecupAttaqueSpe(nomArme));
            Armes batonEnBois = new("Bâton en bois", 0, 7, "Mage", 1.4, 0, 3, 0, RecupAttaqueSpe(nomArme));
            Armes bouclierEnBois = new("Bouclier en bois", 3, 0, "Défenseur", 1.5, 7, 0, 0, RecupAttaqueSpe(nomArme));
            Armes dagueEnBois = new("Dague en bois", 5, 0, "Assassin", 1.7, 0, 0, 5, RecupAttaqueSpe(nomArme));

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

        public static AttaqueSpe RecupAttaqueSpe(string nomArme)
        {
            AttaqueSpe echarde = new("Echarde", "dot", 5, 5, 1, "Ouille une echarde ! (l'ennemi est blessé pendant les 5 prochains tours)");
            AttaqueSpe flecheEmpoisonnee = new("Flèche empoisonnée", "dot", 2, 10, 1, "Une flèche empoisonnée qui blesse l'ennemi pendant 2 tours !");
            AttaqueSpe flash = new("Flash", "stun", 3, 0, 1, "Un flash qui étourdit l'ennemi pendant 3 tour !");
            AttaqueSpe charge = new("Charge", "physique-stun", 1, 20, 1, "Une charge qui inflige de lourds dégats !");
            AttaqueSpe volDAtk = new("Vol d'attaque", "buff", 0, 5, 2, "Vol un peu de l'attaque de l'ennemi !");

            switch (nomArme)
            {
                case "Epée en bois":
                    return echarde;
                case "Arc en bois":
                    return flecheEmpoisonnee;
                case "Bâton en bois":
                    return flash;
                case "Bouclier en bois":
                    return charge;
                case "Dague en bois":
                    return volDAtk;
                default:
                    return echarde;
            }
        }
    }

}
