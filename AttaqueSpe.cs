namespace MiniProjet
{
    public class AttaqueSpe(string nom, string type, int nombreDeTour, int utilisation, string description, int dgtPhysiques = 0, int dgtMagiques = 0, int dotDegatsParTour = 0, string buffStatistiques = "", int montantBuff = 0, string nerfStatistiques = "", int montantNerf = 0, int soins = 0, double multiplier = 0, double chanceCrit = 10)
    {
        public string Nom { get; set; } = nom;
        public string Type { get; set; } = type;
        public int NombreDeTour { get; set; } = nombreDeTour;
        public int Utilisation { get; set; } = utilisation;
        public int UtilisationMax { get; set; } = utilisation;
        public string Description { get; set; } = description;
        public int DgtPhysiques { get; set; } = dgtPhysiques;
        public int DgtMagiques { get; set; } = dgtMagiques;
        public int DotDegatsParTour { get; set; } = dotDegatsParTour;
        public string BuffStatistiques { get; set; } = buffStatistiques;
        public int MontantBuff { get; set; } = montantBuff;
        public string NerfStatistiques { get; set; } = nerfStatistiques;
        public int MontantNerf { get; set; } = montantNerf;
        public int Soins { get; set; } = soins;
        public double Multiplier { get; set; } = multiplier;
        public double ChanceCrit { get; set; } = chanceCrit;

        public static readonly AttaqueSpe coupDePied = new(
            nom: "Coup de Pied",
            type: "physique",
            nombreDeTour: 0,
            utilisation: 30,
            description: "Un coup de pied simple eclaté au sol.",
            dgtPhysiques: 1
        );
        public static readonly AttaqueSpe flecheEmpoisonnee = new(
            nom: "Flèche Empoisonnée",
            type: "physique&dot",
            nombreDeTour: 3,
            utilisation: 3,
            description: "Lance une flèche empoisonnée qui inflige des dégâts magiques initiaux et des dégâts de poison pendant 3 tours.",
            dgtPhysiques: 15,
            dotDegatsParTour: 50
        );
        public static readonly AttaqueSpe coupDeTonnerre = new(
            nom: "Coup de Tonnerre",
            type: "physique&stun",
            nombreDeTour: 3,
            utilisation: 2,
            description: "Un coup puissant qui étourdit l'ennemi pour 1 tour et inflige des dégâts physiques.",
            dgtPhysiques: 30
        );
        public static readonly AttaqueSpe frappeLumineuse = new(
            nom: "Frappe Mortelle",
            type: "magique",
            nombreDeTour: 0,
            utilisation: 1,
            description: "Inflige une énorme quantité de dégâts magique à l'ennemi en une seule frappe.",
            dgtPhysiques: 35
        );
        public static readonly AttaqueSpe ventDeTerreur = new(
            nom: "Vent de Terreur",
            type: "nerf",
            nombreDeTour: 2,
            utilisation: 2,
            description: "Réduit la force de l'ennemi.",
            nerfStatistiques: "defense",
            montantNerf: 10
        );
        public static readonly AttaqueSpe explosionMagique = new(
            nom: "Explosion Magique",
            type: "magique&buff",
            nombreDeTour: 2,
            utilisation: 2,
            description: "Inflige des dégâts magiques puissants et augmente l'intelligence du joueur.",
            dgtMagiques: 40,
            buffStatistiques: "intelligence",
            montantBuff: 5
        );
        public static readonly AttaqueSpe auraDeProtection = new(
            nom: "Aura de Protection",
            type: "heal&buff",
            nombreDeTour: 3,
            utilisation: 2,
            description: "Soigne le joueur et augmente sa défense.",
            soins: 15,
            buffStatistiques: "defense", 
            montantBuff: 10
        );
        public static readonly AttaqueSpe sacrificeMortel = new(
            nom: "Sacrifice Mortel",
            type: "multiplierVie&buff",
            nombreDeTour: 0,
            utilisation: 1,
            multiplier: 0.4,
            description: "Réduit sa vie de 60% mais gagne 60% de défense physique.",
            buffStatistiques: "defense",
            montantBuff: 150
        );
        public static readonly AttaqueSpe PerceeCritique = new(
            nom: "Percée Critique",
            type: "physique&nerf",
            nombreDeTour: 0,
            utilisation: 4,
            description: "Le joueur perce l'armure de l'ennemi en lui infligeant un coup critique et en baissant sa défense.",
            dgtPhysiques: 22,
            chanceCrit: 110,
            nerfStatistiques: "defense",
            montantNerf: 3
        );
    }
}
