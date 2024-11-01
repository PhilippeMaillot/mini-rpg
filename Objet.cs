namespace MiniProjet
{
    public class Objets(string nom, string type, int effet)
    {
        public string Nom { get; set; } = nom;
        public string Type { get; set; } = type;
        public int Effet { get; set; } = effet;

        public static readonly Objets potionSoin = new("Potion de Soin", "soin", 20);
        public static readonly Objets potionDegats = new("Potion de Dégâts", "force", 20);
        public static readonly Objets potionAgilite = new("Potion d'Agilité", "agilite", 20);
        public static readonly Objets potionDefense = new("Potion de Défense", "defense", 20);
        public static readonly Objets potionDefenseMagique = new("Potion de Défense Magique", "defenseMagique", 20);
        private static readonly List<Objets> objetsDisponibles = [potionSoin, potionDegats, potionAgilite, potionDefense, potionDefenseMagique];

        public readonly static List<Objets> ListeObjets = objetsDisponibles;
        public void Utiliser(Joueur joueur, Ennemis ennemi, int Effet)
        {
            if (Type == "soin")
            {
                double pointsDeVieMax = joueur.Classe.PointsDeVie;
                joueur.PointsDeVieActuels = Math.Min(joueur.PointsDeVieActuels + Effet, pointsDeVieMax);
                Console.WriteLine($"{Nom} utilisé : {joueur.Nom} récupère {Effet} points de vie !");
            } 
            else if (Type == "force") {
                ennemi.PointsDeVie -= Effet;
                Console.WriteLine($"{Nom} utilisé : {ennemi.Nom} perd {Effet} points de vie !");
            } 
            else if (Type == "agilite")
            {
                joueur.AgiliteActuelle += Effet;
                Console.WriteLine($"{Nom} utilisé : {joueur.Nom} gagne {Effet} points d'agilité !");
            }
            else if (Type == "defense")
            {
                joueur.DefenseActuelle += Effet;
                Console.WriteLine($"{Nom} utilisé : {joueur.Nom} gagne {Effet} points de défense !");
            }
            else if (Type == "defenseMagique")
            {
                joueur.DefenseMagiqueActuelle += Effet;
                Console.WriteLine($"{Nom} utilisé : {joueur.Nom} gagne {Effet} points de défense magique !");
            }
        }
    }
}
