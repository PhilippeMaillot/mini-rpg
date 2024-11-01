using System.Text.Json;

namespace MiniProjet
{
    public static class GestionSauvegarde
    {
        private readonly static string dossierSauvegarde = "sauvegardes";

        public static void SauvegarderJoueur(Joueur joueur)
        {
            if (!Directory.Exists(dossierSauvegarde))
            {
                Directory.CreateDirectory(dossierSauvegarde);
            }

            string nomSauvegarde = $"{joueur.Nom}-{DateTime.Now:yyyyMMdd_HHmmss}";
            string cheminFichier = Path.Combine(dossierSauvegarde, $"{nomSauvegarde}.json");

            string json = JsonSerializer.Serialize(joueur, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(cheminFichier, json);

            Console.WriteLine($"Sauvegarde effectuée sous le nom : {nomSauvegarde}");
        }

        public static Joueur ChargerJoueur(string nomSauvegarde)
        {
            string cheminFichier = Path.Combine(dossierSauvegarde, $"{nomSauvegarde}.json");

            if (File.Exists(cheminFichier))
            {
                string json = File.ReadAllText(cheminFichier);
                Joueur joueur = JsonSerializer.Deserialize<Joueur>(json);
                Console.WriteLine($"Chargement de la sauvegarde : {nomSauvegarde}");
                return joueur;
            }
            else
            {
                Console.WriteLine("Sauvegarde introuvable.");
                return null;
            }
        }


        public static List<string> ListerSauvegardes()
        {
            if (!Directory.Exists(dossierSauvegarde))
            {
                return [];
            }

            return Directory.GetFiles(dossierSauvegarde, "*.json")
                            .Select(Path.GetFileNameWithoutExtension)
                            .ToList();
        }

        public static Joueur ChargerSauvegardeDepuisMenu()
        {
            List<string> sauvegardes = ListerSauvegardes();
            if (sauvegardes.Count == 0)
            {
                Console.WriteLine("Aucune sauvegarde trouvée.");
                return null;
            }

            Console.WriteLine("Sélectionnez une sauvegarde :");
            for (int i = 0; i < sauvegardes.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {sauvegardes[i]}");
            }

            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= sauvegardes.Count)
            {
                return ChargerJoueur(sauvegardes[choix - 1]);
            }
            else
            {
                Console.WriteLine("Choix invalide.");
                return null;
            }
        }
    }
}
