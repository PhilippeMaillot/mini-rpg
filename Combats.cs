using System;

namespace MiniProjet
{
    internal class Combat
    {
        private static List<(int toursRestants, int degatsParTour)> dotActifs = new();

        public static void LancerCombat(Joueur joueur, Ennemis ennemi)
        {
            Console.Clear();
            AfficherEnteteCombat(joueur.Nom, ennemi.Nom);

            if (joueur.PointsDeVieActuels < joueur.Classe.PointsDeVie)
            {
                joueur.PointsDeVieActuels = joueur.Classe.PointsDeVie;
            }

            joueur.AfficherStatistiques();
            ennemi.AfficherStatistiques();

            double pointsDeVieMaxJoueur = joueur.Classe.PointsDeVie;
            double pointsDeVieMaxEnnemi = ennemi.PointsDeVie;
            int compteurDeTour = 1;

            while (joueur.PointsDeVieActuels > 0 && ennemi.PointsDeVie > 0)
            {
                Console.Clear();
                AfficherEnteteCombat(joueur.Nom, ennemi.Nom);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n--- Tour {compteurDeTour} ---");
                Console.ResetColor();

                joueur.AfficherStatistiques();
                ennemi.AfficherStatistiques();

                // Appliquer les DoT actifs
                AppliquerDot(ennemi);

                AfficherEtatVie(joueur.Nom, joueur.PointsDeVieActuels, pointsDeVieMaxJoueur, ennemi.Nom, ennemi.PointsDeVie, pointsDeVieMaxEnnemi);
                string action = ChoisirAction();

                if (action == "attaquer")
                {
                    string typeAttaque = ChoisirTypeAttaque(joueur);

                    if (typeAttaque == "physique")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"\n{joueur.Nom} attaque physiquement {ennemi.Nom} !");
                        Console.ResetColor();
                        if (!Esquiver(ennemi.Agilite, new Random()))
                        {
                            double degatsJoueur = CalculerDegats(joueur.CalculerStatistiquesFinales().forceFinale, ennemi.Defense);
                            ennemi.RecevoirDegats(degatsJoueur);
                            AfficherBarreVie(ennemi.Nom, Math.Max(ennemi.PointsDeVie, 0), pointsDeVieMaxEnnemi);

                            if (ennemi.PointsDeVie <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{ennemi.Nom} a été vaincu !");
                                Console.ResetColor();
                                break;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{ennemi.Nom} a esquivé l'attaque !");
                            Console.ResetColor();
                        }
                    }
                    else if (typeAttaque == "magique")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\n{joueur.Nom} attaque magiquement {ennemi.Nom} !");
                        Console.ResetColor();
                        if (!Esquiver(ennemi.Agilite, new Random()))
                        {
                            double degatsJoueur = CalculerDegats(joueur.CalculerStatistiquesFinales().intelligenceFinale, ennemi.DefenseMagique);
                            ennemi.RecevoirDegats(degatsJoueur);
                            AfficherBarreVie(ennemi.Nom, Math.Max(ennemi.PointsDeVie, 0), pointsDeVieMaxEnnemi);

                            if (ennemi.PointsDeVie <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{ennemi.Nom} a été vaincu !");
                                Console.ResetColor();
                                break;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{ennemi.Nom} a esquivé l'attaque !");
                            Console.ResetColor();
                        }
                    }
                    else if (typeAttaque == "spe")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        if (joueur.Arme.AttaqueSpe.Utilisation > 0)
                        {
                            ActiverEffetAttaqueSpe(joueur, ennemi, compteurDeTour);
                        }
                        else
                        {
                            Console.WriteLine("Votre attaque spécial n'a plus d'utilisation restante.");
                        }
                    }
                }
                else if (action == "objet")
                {
                    UtiliserObjet(joueur, ennemi);
                }
                else if (action == "quitter")
                {
                    Console.WriteLine("Vous quittez le combat.");
                    Environment.Exit(0);
                }

                if (ennemi.PointsDeVie > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{ennemi.Nom} attaque {joueur.Nom} !");
                    Console.ResetColor();

                    if (!Esquiver(joueur.AgiliteActuelle, new Random()))
                    {
                        var (degatsPhysiques, critPhysique) = SimulerDegats(ennemi.Force, joueur.CalculerStatistiquesFinales().defenseFinale, new Random());
                        var (degatsMagiques, critMagique) = SimulerDegats(ennemi.Intelligence, joueur.CalculerStatistiquesFinales().defenseMagiqueFinale, new Random());

                        double degatsEnnemi = degatsPhysiques > degatsMagiques ? degatsPhysiques : degatsMagiques;
                        bool isCrit = degatsPhysiques > degatsMagiques ? critPhysique : critMagique;

                        if (isCrit)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Coup critique !");
                            Console.ResetColor();
                        }

                        joueur.PointsDeVieActuels -= degatsEnnemi;
                        Console.WriteLine($"{joueur.Nom} a subi {degatsEnnemi:F1} points de dégâts.");
                        AfficherBarreVie(joueur.Nom, Math.Max(joueur.PointsDeVieActuels, 0), pointsDeVieMaxJoueur);

                        if (joueur.PointsDeVieActuels <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{joueur.Nom} a été vaincu !");
                            Console.ResetColor();
                            break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{joueur.Nom} a esquivé l'attaque !");
                        Console.ResetColor();
                    }
                }

                ActiverPassifs(compteurDeTour, ennemi, joueur, pointsDeVieMaxEnnemi);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ResetColor();
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                compteurDeTour++;
            }

            if (joueur.PointsDeVieActuels > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{joueur.Nom} a gagné le combat !");
                joueur.GagnerExperience(ennemi.ExperienceDonne);
                joueur.GagnerPieceDor(ennemi.PieceDorDonnee);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{ennemi.Nom} a gagné le combat !");
            }

            TerminerCombat(joueur, ennemi);
            joueur.RestaurerStatistiques();
        }

        private static void AppliquerDot(Ennemis ennemi)
        {
            for (int i = dotActifs.Count - 1; i >= 0; i--) // Parcourir les DoT actifs
            {
                var dot = dotActifs[i];

                // Appliquer les dégâts du DoT
                Console.WriteLine($"Le DoT inflige {dot.degatsParTour} dégâts à {ennemi.Nom}.");
                ennemi.RecevoirDegats(dot.degatsParTour);

                // Diminuer le nombre de tours restants pour ce DoT
                dotActifs[i] = (dot.toursRestants - 1, dot.degatsParTour);

                // Si le DoT n'a plus de tours restants, le supprimer
                if (dotActifs[i].toursRestants <= 0)
                {
                    dotActifs.RemoveAt(i);
                    Console.WriteLine("Un DoT est terminé.");
                }
            }
        }

        private static void ActiverEffetAttaqueSpe(Joueur joueur, Ennemis ennemi, int compteurDeTour)
        {
            if (joueur.Arme.AttaqueSpe.Type == "dot")
            {
                dotActifs.Add((joueur.Arme.AttaqueSpe.NombreDeTour, joueur.Arme.AttaqueSpe.Effet));
                joueur.Arme.AttaqueSpe.Utilisation--;
                Console.WriteLine($"{joueur.Nom} utilise {joueur.Arme.AttaqueSpe.Nom} ! DoT activé pour {joueur.Arme.AttaqueSpe.NombreDeTour} tours.");
            }
        }

        private static void TerminerCombat(Joueur joueur, Ennemis ennemi)
        {
            dotActifs.Clear();
            joueur.Arme.AttaqueSpe.Utilisation = joueur.Arme.AttaqueSpe.UtilisationMax;
        }


        private static void UtiliserObjet(Joueur joueur, Ennemis ennemi)
        {
            joueur.AfficherInventaire();
            Console.WriteLine("Choisissez un objet à utiliser ou tapez '0' pour retour :");
            string choix = Console.ReadLine();

            if (choix == "0")
            {
                return;
            }

            if (int.TryParse(choix, out int index) && index > 0 && index <= joueur.Inventaire.Count)
            {
                var objet = new List<Objet>(joueur.Inventaire.Keys)[index - 1];
                joueur.UtiliserObjet(objet, ennemi);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{objet.Nom} utilisé !");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Choix invalide.");
            }
        }

        private static string ChoisirAction()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nQue voulez-vous faire ?");
            Console.WriteLine("1 - Attaquer");
            Console.WriteLine("2 - Utiliser un objet");
            Console.WriteLine("3 - Quitter");
            Console.ResetColor();
            string choix = Console.ReadLine();

            return choix switch
            {
                "1" => "attaquer",
                "2" => "objet",
                "3" => "quitter",
                _ => ChoisirAction(),
            };
        }

        private static string ChoisirTypeAttaque(Joueur joueur)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nQuel type d'attaque voulez-vous effectuer ?");
            Console.WriteLine("1 - Attaque physique");
            Console.WriteLine("2 - Attaque magique");
            Console.WriteLine($"3 - Attaque spéciale ({joueur.Arme.AttaqueSpe.Nom})");
            Console.WriteLine("4 - Retour");
            Console.ResetColor();
            string choix = Console.ReadLine();

            return choix switch
            {
                "1" => "physique",
                "2" => "magique",
                "3" => "spe",
                "4" => "retour",
                _ => ChoisirTypeAttaque(joueur),
            };
        }

        private static bool CalculerCrit(double chanceCritique, Random random)
        {
            int chance = random.Next(1, 101);
            return chance <= chanceCritique;
        }

        private static double CalculerDegats(double attaque, double defense)
        {
            double reduction = 1 - (defense / 100.0);
            if (CalculerCrit(10, new Random()))
            {
                attaque *= 1.5;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Coup critique !");
                Console.ResetColor();
            }
            double degats = attaque * reduction;
            return degats > 0 ? degats : 0;
        }

        private static (double degats, bool isCrit) SimulerDegats(double attaque, double defense, Random random)
        {
            double reduction = 1 - (defense / 100.0);
            bool isCrit = false;

            if (CalculerCrit(10, random))
            {
                attaque *= 1.5;
                isCrit = true;
            }

            double degats = attaque * reduction;
            return (degats > 0 ? degats : 0, isCrit);
        }

        private static bool Esquiver(double agilite, Random random)
        {
            int chanceEsquive = random.Next(1, 101);
            return chanceEsquive <= agilite;
        }

        private static void AfficherBarreVie(string nom, double pointsDeVie, double pointsDeVieMax)
        {
            pointsDeVie = Math.Max(pointsDeVie, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[{nom}] ");
            int barLength = 20;
            int filledBar = (int)((pointsDeVie / pointsDeVieMax) * barLength);
            filledBar = Math.Max(filledBar, 0);
            Console.Write("[");
            Console.ForegroundColor = pointsDeVie > (0.3 * pointsDeVieMax) ? ConsoleColor.Green : ConsoleColor.Red;

            Console.Write(new string('=', filledBar));
            Console.ResetColor();
            Console.WriteLine($"{new string(' ', barLength - filledBar)}] {pointsDeVie:F1} / {pointsDeVieMax:F1} PV");
        }

        private static void AfficherEtatVie(string nomJoueur, double pvJoueur, double pvMaxJoueur, string nomEnnemi, double pvEnnemi, double pvMaxEnnemi)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nÉtat des points de vie :");
            Console.ResetColor();
            AfficherBarreVie(nomJoueur, pvJoueur, pvMaxJoueur);
            AfficherBarreVie(nomEnnemi, pvEnnemi, pvMaxEnnemi);
        }

        private static void AfficherEnteteCombat(string nomJoueur, string nomEnnemi)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine($" COMBAT: {nomJoueur} VS {nomEnnemi}");
            Console.WriteLine("===============================================\n");
            Console.ResetColor();
        }

        private static void ActiverPassifs(int compteurDeTour, Ennemis ennemi, Joueur joueur, double pointsDeVieMaxEnnemi )
        {
            //Tour 1

            //Tour 2
            if (compteurDeTour == 2 && joueur.Arme.ClasseDefaut == "Assassin")
            {
                joueur.AgiliteActuelle += 50;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"La {joueur.Arme.Nom} de {joueur.Nom} le/la rend plus agile pendant 3 tours !");
                Console.ResetColor();
            }

            //Tour 3
            if (compteurDeTour >= 3 && (ennemi.TypeEnnemi == "defensifPhysique" || ennemi.TypeEnnemi == "defensifMagique"))
            {
                ennemi.RecevoirSoins(0.07 * pointsDeVieMaxEnnemi, pointsDeVieMaxEnnemi);
            }

            //Tour 4


            //Tour 5
            if (compteurDeTour == 5 && joueur.Arme.ClasseDefaut == "Assassin")
            {
                joueur.AgiliteActuelle = joueur.Classe.Agilite;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{joueur.Nom} redevient normal !");
            }

            //Tour 6
            if (compteurDeTour >= 6 && joueur.Classe.Nom == "Défenseur")
            {
                joueur.ForceActuelle += joueur.Classe.Force * 0.10;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{joueur.Nom} s'enrage !");
                Console.ResetColor();
            }

            //Tour 7
        }
    }
}
