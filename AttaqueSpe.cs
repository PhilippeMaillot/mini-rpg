namespace MiniProjet
{
    public class AttaqueSpe(string nom, string type, int nombreDeTour, int effet, int utilisation, string description)
    {
        public string Nom { get; set; } = nom;
        public string Type { get; set; } = type;
        public int NombreDeTour { get; set; } = nombreDeTour;
        public int Effet { get; set; } = effet;
        public int Utilisation { get; set; } = utilisation;
        public int UtilisationMax { get; set; } = utilisation;
        public string Description { get; set; } = description;
    }
}
