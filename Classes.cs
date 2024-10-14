namespace MiniProjet
{
    public class Classes(string nom, double pointsDeVie, double force, double agilite, double intelligence, double defense, double defenseMagique)
    {
        public string Nom { get; set; } = nom;
        public double PointsDeVie { get; set; } = pointsDeVie;
        public double Force { get; set; } = force;
        public double Agilite { get; set; } = agilite;
        public double Intelligence { get; set; } = intelligence;
        public double Defense { get; set; } = defense;
        public double DefenseMagique { get; set; } = defenseMagique;
    }
}
