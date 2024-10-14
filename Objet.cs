namespace MiniProjet
{
    public class Objet(string nom, string type, int effet)
    {
        public string Nom { get; set; } = nom;
        public string Type { get; set; } = type;
        public int Effet { get; set; } = effet;

        //Types:
        //Soin (soigne le joueur)
        //Force (Fait des dégats à l'ennemi)
        //Agilite (Augmente l'esquive pendant le combat)
        //Defense (Augmente la defense du joueur pendant le combat)
        //DefenseMagique (Augmente la defense magique du joueur pendant le combat)
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
