namespace MiniProjet
{
    internal class Combat
    {
        private static readonly List<(int toursRestants, int degatsParTour)> dotActifs = [];
        private static readonly List<(int toursRestants, int soinsParTour)> healActifs = [];
        private static bool PassifClasse1EstActive = false;
        //private static bool PassifClasse2EstActive = false;

        public static void LancerCombat(Joueur joueur, Ennemis ennemi)
        {
            Console.Clear();
            AfficherEnteteCombat(joueur.Nom, ennemi.Nom);
            joueur.AfficherStatistiques();
            ennemi.AfficherStatistiques();
            double pointsDeVieMaxJoueur = joueur.Classe.PointsDeVie;
            double pointsDeVieMaxEnnemi = ennemi.PointsDeVie;
            int compteurDeTour = 1;
            Random random = new();

            while (joueur.PointsDeVieActuels > 0 && ennemi.PointsDeVie > 0)
            {
                Console.Clear();
                AfficherEnteteCombat(joueur.Nom, ennemi.Nom);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n--- Tour {compteurDeTour} ---");
                Console.ResetColor();
                joueur.AfficherStatistiques();
                ennemi.AfficherStatistiques();
                AppliquerEffets(ennemi, joueur);
                if (ennemi.PointsDeVie <= 0)
                {
                    CasVictoireJoueur(joueur, ennemi);
                    break;
                };
                AfficherEtatVie(joueur.Nom, joueur.PointsDeVieActuels, pointsDeVieMaxJoueur, ennemi.Nom, ennemi.PointsDeVie, pointsDeVieMaxEnnemi);
                string action = ChoisirAction();
                if (action == "attaquer")
                {
                    string typeAttaque = ChoisirTypeAttaque(joueur);

                    bool joueurAttaqueEnPremier;
                    if (joueur.CalculerStatistiquesFinales().vitesseFinale > ennemi.Vitesse)
                    {
                        joueurAttaqueEnPremier = true;
                    }
                    else if (joueur.CalculerStatistiquesFinales().vitesseFinale < ennemi.Vitesse)
                    {
                        joueurAttaqueEnPremier = false;
                    }
                    else
                    {
                        joueurAttaqueEnPremier = random.Next(2) == 0;
                    }

                    if (joueurAttaqueEnPremier)
                    {
                        if (EffectuerTourJoueur(joueur, ennemi, pointsDeVieMaxEnnemi, typeAttaque)) break;
                        if (ennemi.PointsDeVie > 0 && !ennemi.EstEncoreStun())
                        {
                            if (EffectuerTourEnnemi(joueur, ennemi, pointsDeVieMaxJoueur, pointsDeVieMaxEnnemi)) break;
                        }
                    }
                    else
                    {
                        if (ennemi.PointsDeVie > 0 && !ennemi.EstEncoreStun())
                        {
                            if (EffectuerTourEnnemi(joueur, ennemi, pointsDeVieMaxJoueur, pointsDeVieMaxEnnemi)) break;
                        }
                        if (joueur.PointsDeVieActuels > 0)
                        {
                            if (EffectuerTourJoueur(joueur, ennemi, pointsDeVieMaxEnnemi, typeAttaque)) break;
                        }
                    }

                }
                else if (action == "objet")
                {
                    UtiliserObjet(joueur, ennemi);
                    EffectuerTourEnnemi(joueur, ennemi, pointsDeVieMaxJoueur, pointsDeVieMaxEnnemi);
                }
                else if (action == "quitter")
                {
                    Console.WriteLine("Vous avez quitté le combat.");
                    break;
                }

                ActiverPassifs(compteurDeTour, ennemi, joueur, pointsDeVieMaxEnnemi);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ResetColor();
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                compteurDeTour++;
            }

            TerminerCombat(joueur);
            joueur.RestaurerStatistiques();
        }

        private static void AfficherEnteteCombat(string nomJoueur, string nomEnnemi)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine($" COMBAT: {nomJoueur} VS {nomEnnemi}");
            Console.WriteLine("===============================================\n");
            Console.ResetColor();
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
            Console.WriteLine($"3 - {joueur.Arme.AttaqueSpe.Nom} (Utilisations restantes : {joueur.Arme.AttaqueSpe.Utilisation} )");
            Console.WriteLine($"4 - {joueur.Arme.AttaqueSpe2.Nom} (Utilisations restantes : {joueur.Arme.AttaqueSpe2.Utilisation}");
            Console.WriteLine("5 - Retour");
            Console.ResetColor();
            string choix = Console.ReadLine();

            return choix switch
            {
                "1" => "physique",
                "2" => "magique",
                "3" => "spe",
                "4" => "spe2",
                "5" => "retour",
                _ => ChoisirTypeAttaque(joueur),
            };
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
                var objet = new List<Objets>(joueur.Inventaire.Keys)[index - 1];
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

        private static bool EffectuerTourJoueur(Joueur joueur, Ennemis ennemi, double pointsDeVieMaxEnnemi, string typeAttaque)
        {
            if (typeAttaque == "physique")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{joueur.Nom} attaque physiquement {ennemi.Nom} !");
                Console.ResetColor();
                if (!Calculs.CalculerEsquive(ennemi.Agilite, new Random()))
                {
                    double degatsJoueur = Calculs.CalculerDegatsJoueur(joueur, ennemi, joueur.Arme.AttaqueSpe, "physique");
                    ennemi.RecevoirDegats(degatsJoueur);

                    if (ennemi.PointsDeVie <= 0)
                    {
                        CasVictoireJoueur(joueur, ennemi);
                        return true;
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
                if (!Calculs.CalculerEsquive(ennemi.Agilite, new Random()))
                {
                    double degatsJoueur = Calculs.CalculerDegatsJoueur(joueur, ennemi, joueur.Arme.AttaqueSpe, "magique");
                    ennemi.RecevoirDegats(degatsJoueur);

                    if (ennemi.PointsDeVie <= 0)
                    {
                        CasVictoireJoueur(joueur, ennemi);
                        return true;
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
                Console.ForegroundColor = ConsoleColor.Blue;
                if (joueur.Arme.AttaqueSpe.Utilisation > 0)
                {
                    Console.WriteLine($"\n{joueur.Nom} utilise {joueur.Arme.AttaqueSpe.Nom} !");
                    ActiverEffetAttaqueSpe(joueur, ennemi, joueur.Arme.AttaqueSpe);
                    if (ennemi.PointsDeVie <= 0)
                    {
                        CasVictoireJoueur(joueur, ennemi);
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Votre attaque spécial n'a plus d'utilisation restante.");
                }
                Console.ResetColor();
            } else if (typeAttaque == "spe2")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (joueur.Arme.AttaqueSpe.Utilisation > 0)
                {
                    Console.WriteLine($"\n{joueur.Nom} utilise {joueur.Arme.AttaqueSpe.Nom} !");
                    ActiverEffetAttaqueSpe(joueur, ennemi, joueur.Arme.AttaqueSpe2);
                    if (ennemi.PointsDeVie <= 0)
                    {
                        CasVictoireJoueur(joueur, ennemi);
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Votre attaque spécial n'a plus d'utilisation restante.");
                }
                Console.ResetColor();
            }

            AfficherEtatVie(joueur.Nom, joueur.PointsDeVieActuels, joueur.Classe.PointsDeVie, ennemi.Nom, ennemi.PointsDeVie, pointsDeVieMaxEnnemi);
            return false;
        }

        private static bool EffectuerTourEnnemi(Joueur joueur, Ennemis ennemi, double pointsDeVieMaxJoueur, double pointsDeVieMaxEnnemi)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{ennemi.Nom} attaque {joueur.Nom} !");
            Console.ResetColor();

            if (!Calculs.CalculerEsquive(joueur.CalculerStatistiquesFinales().agiliteFinale, new Random()))
            {
                double degatsEnnemi = Calculs.CalculerDegatsEnnemi(joueur, ennemi);
                joueur.PointsDeVieActuels -= degatsEnnemi;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{joueur.Nom} a subi {degatsEnnemi:F1} points de dégâts.");
                Console.ResetColor();

                if (joueur.PointsDeVieActuels <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{joueur.Nom} a été vaincu !");
                    Console.ResetColor();
                    return true;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{joueur.Nom} a esquivé l'attaque !");
                Console.ResetColor();
            }

            AfficherEtatVie(joueur.Nom, joueur.PointsDeVieActuels, pointsDeVieMaxJoueur, ennemi.Nom, ennemi.PointsDeVie, pointsDeVieMaxEnnemi);
            return false;
        }

        private static void TerminerCombat(Joueur joueur)
        {
            dotActifs.Clear();
            PassifClasse1EstActive = false;
            joueur.Arme.AttaqueSpe.Utilisation = joueur.Arme.AttaqueSpe.UtilisationMax;
            joueur.Arme.AttaqueSpe2.Utilisation = joueur.Arme.AttaqueSpe2.UtilisationMax;
        }

        private static void CasVictoireJoueur(Joueur joueur, Ennemis ennemi)
        {
            joueur.GagnerExperience(ennemi.ExperienceDonne);
            joueur.GagnerPieceDor(ennemi.PieceDorDonnee);
        }

        private static void ActiverPassifs(int compteurDeTour, Ennemis ennemi, Joueur joueur, double pointsDeVieMaxEnnemi)
        {
            //Passifs liées à l'état//
            if (joueur.PointsDeVieActuels <= (0.40 * joueur.Classe.PointsDeVie) && joueur.Classe.Nom == "Revenant" && PassifClasse1EstActive == false)
            {
                joueur.ForceActuelle += joueur.ForceActuelle + 30;
                joueur.VitesseActuelle += joueur.VitesseActuelle + 20;
                PassifClasse1EstActive = true;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{joueur.Nom} se renforce !");
                Console.ResetColor();
            }

            //Passifs liés au tours//

            //Tour 1

            //Tour 2

            //Tour 3

            //Tour 4

            //Tour 5

            //Tour 6

            //Tour 7
            if (compteurDeTour >= 7 && (ennemi.TypeEnnemi == "defensifPhysique" || ennemi.TypeEnnemi == "defensifMagique"))
            {
                ennemi.RecevoirSoins(0.025 * pointsDeVieMaxEnnemi, pointsDeVieMaxEnnemi);
            }

            if (compteurDeTour >= 7 && joueur.Classe.Nom == "Défenseur")
            {
                joueur.ForceActuelle += joueur.Classe.Force * 0.30;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{joueur.Nom} s'enrage !");
                Console.ResetColor();
            }
        }

        private static void ActiverEffetAttaqueSpe(Joueur joueur, Ennemis ennemi, AttaqueSpe attaqueSpe)
        {
            if (attaqueSpe.Type.Contains("physique"))
                ennemi.RecevoirDegats(Calculs.CalculerDegatsJoueur(joueur, ennemi, attaqueSpe, "spePhys"));

            if (attaqueSpe.Type.Contains("magique"))
                ennemi.RecevoirDegats(Calculs.CalculerDegatsJoueur(joueur, ennemi, attaqueSpe, "speMag"));

            if (attaqueSpe.Type.Contains("heal"))
                AppliquerSoins(joueur, attaqueSpe);

            if (attaqueSpe.Type.Contains("dot"))
                AppliquerDoT(joueur, attaqueSpe);

            if (attaqueSpe.Type.Contains("buff"))
                AppliquerBuff(joueur, attaqueSpe);

            if (attaqueSpe.Type.Contains("nerf"))
                AppliquerNerf(ennemi, attaqueSpe);

            if (attaqueSpe.Type.Contains("stun"))
                ennemi.AppliquerStun(attaqueSpe.NombreDeTour);

            if (attaqueSpe.Type.Contains("multiplierVie"))
                AppliquerMultiplierVie(joueur, attaqueSpe);

            attaqueSpe.Utilisation--;
        }

        private static void AppliquerEffets(Ennemis ennemi, Joueur joueur)
        {
            // Appliquer les DoT
            for (int i = dotActifs.Count - 1; i >= 0; i--)
            {
                var (toursRestants, degatsParTour) = dotActifs[i];
                ennemi.RecevoirDegats(degatsParTour);
                dotActifs[i] = (toursRestants - 1, degatsParTour);
                if (dotActifs[i].toursRestants <= 0)
                {
                    dotActifs.RemoveAt(i);
                    Console.WriteLine("Un DoT est terminé.");
                }
            }

            for (int i = healActifs.Count - 1; i >= 0; i--)
            {
                var (toursRestants, soinsParTour) = healActifs[i];
                Console.WriteLine($"{joueur.Nom} reçoit {soinsParTour} points de vie.");
                joueur.PointsDeVieActuels = Math.Min(joueur.PointsDeVieActuels + soinsParTour, joueur.Classe.PointsDeVie);
                healActifs[i] = (toursRestants - 1, soinsParTour);
                if (healActifs[i].toursRestants <= 0)
                {
                    healActifs.RemoveAt(i);
                    Console.WriteLine("Le soin récurrent est terminé.");
                }
            }
        }

        private static void AppliquerDoT(Joueur joueur, AttaqueSpe attaqueSpe)
        {
            dotActifs.Add((attaqueSpe.NombreDeTour, attaqueSpe.DotDegatsParTour));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{joueur.Nom} utilise {attaqueSpe.Nom} ! DoT activé pour {attaqueSpe.NombreDeTour} tours.");
            Console.ResetColor();
        }

        private static void AppliquerBuff(Joueur joueur, AttaqueSpe attaqueSpe)
        {
            joueur.AppliquerBuff(attaqueSpe.BuffStatistiques, attaqueSpe.MontantBuff);
            Console.WriteLine($"{joueur.Nom} utilise {attaqueSpe.Nom} et reçoit un buff !");
        }

        private static void AppliquerNerf(Ennemis ennemi, AttaqueSpe attaqueSpe)
        {
            ennemi.AppliquerNerf(attaqueSpe.NerfStatistiques, attaqueSpe.MontantNerf);
            Console.WriteLine($"{attaqueSpe.Nom} affaiblit {ennemi.Nom} !");
        }

        private static void AppliquerMultiplierVie(Joueur joueur, AttaqueSpe attaqueSpe)
        {
            joueur.PointsDeVieActuels = Math.Min(joueur.PointsDeVieActuels, joueur.Classe.PointsDeVie * attaqueSpe.Multiplier);
        }

        private static void AppliquerSoins(Joueur joueur, AttaqueSpe attaqueSpe)
        {
            joueur.PointsDeVieActuels = Math.Min(joueur.PointsDeVieActuels + attaqueSpe.Soins, joueur.Classe.PointsDeVie);
            Console.WriteLine($"{joueur.Nom} se soigne de {attaqueSpe.Soins} points de vie.");
        }
    }
}
