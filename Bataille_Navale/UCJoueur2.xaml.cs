using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Bataille_Navale.UCJoueur2;

namespace Bataille_Navale
{
    /// <summary>
    /// Logique d'interaction pour UCJoueur2.xaml
    /// </summary>
    public partial class UCJoueur2 : UserControl
    {
        public static Button[] lesBoutonsAttJoueur2 { get; set; } = new Button[81];
        public static Button[] lesBoutonsDefJoueur2 { get; set; } = new Button[81];
        public int[] Bateau = { 2, 3, 3, 4, 5 };
        public int numBateau = 0;
        private bool estVertical = true;
        public bool enModePlacement = true;
        public string[] nomBateau = ["Torpilleur", "Contre-Torpilleur", "Sous-marin", "Croiseur", "Porte-avion"];
        public static bool FinDePartie { get; set; } = false;
        public UCJoueur2()
        {
            InitializeComponent();
            InitialiseGrilleAttaque();
            InitialiseGrilleDefense();
        }

        // Ajoutez cette méthode dans UCJoueur2.xaml.cs
        public void DemarrerPlacement()
        {
            // On s'assure que les événements ne sont pas ajoutés deux fois
            for (int i = 0; i < lesBoutonsDefJoueur2.Length; i++)
            {
                lesBoutonsDefJoueur2[i].Click -= Placement_Click; // Sécurité
                lesBoutonsDefJoueur2[i].Click += Placement_Click;
            }

            // Initialisation du premier bateau
            numBateau = 0;
            labReponse.Content = $"Placez le bateau de longueur {Bateau[numBateau]}.";

            // Mettre à jour l'interface pour dire que c'est le placement
            labReponse.Content = "Placez vos bateaux sur la grille de défense";
        }


        public void InitialiseGrilleAttaque()
        {
            for (int i = 0; i < lesBoutonsAttJoueur2.Length; i++)
            {
                lesBoutonsAttJoueur2[i] = new Button();
                lesBoutonsAttJoueur2[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsAttJoueur2[i].Width = 50;
                lesBoutonsAttJoueur2[i].Height = 50;
                lesBoutonsAttJoueur2[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsAttJoueur2[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsAttJoueur2[i].Margin = new Thickness(lesBoutonsAttJoueur2[i].Height * (i % 9), lesBoutonsAttJoueur2[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur2.Children.Add(lesBoutonsAttJoueur2[i]);
                lesBoutonsAttJoueur2[i].Tag = 1;
                Grid.SetColumn(lesBoutonsAttJoueur2[i], 1);
                // ici il est placé dans la 1ere colonne da ma grille
                if (MainWindow.nbTour == 2)
                    labReponse.Content = "Placer vos bateau sur la grille de defense";
                else
                    lesBoutonsAttJoueur2[i].Click += this.UnBouton_Click;
            }
        }

        public void DemarrerPlacementJoueur2()
        {
            enModePlacement = true;
            // On s'assure que les événements ne sont pas ajoutés deux fois
            for (int i = 0; i < lesBoutonsDefJoueur2.Length; i++)
            {
                lesBoutonsDefJoueur2[i].Click -= Placement_Click; // Sécurité
                lesBoutonsDefJoueur2[i].Click += Placement_Click;
            }

            // Initialisation du premier bateau
            numBateau = 0;
            labReponse.Content = $"Placez le bateau de longueur {Bateau[numBateau]}.";

            // Mettre à jour l'interface pour dire que c'est le placement
            labReponse.Content = "Placez vos bateaux sur la grille de défense";
        }
        public void InitialiseGrilleDefense()
        {
            for (int i = 0; i < lesBoutonsDefJoueur2.Length; i++)
            {
                lesBoutonsDefJoueur2[i] = new Button();
                lesBoutonsDefJoueur2[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsDefJoueur2[i].Width = 50;
                lesBoutonsDefJoueur2[i].Height = 50;
                lesBoutonsDefJoueur2[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsDefJoueur2[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsDefJoueur2[i].Margin = new Thickness(lesBoutonsDefJoueur2[i].Height * (i % 9), lesBoutonsDefJoueur2[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur2.Children.Add(lesBoutonsDefJoueur2[i]);
                lesBoutonsDefJoueur2[i].Tag = 0;
                Grid.SetColumn(lesBoutonsDefJoueur2[i], 0);
                // ici il est placé dans la 2eme colonne da ma grille
            }
        }

        private void UnBouton_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = ((Button)sender);
            for (int i = 0; i < lesBoutonsAttJoueur2.Length; i++)
            {
                if (bouton == lesBoutonsAttJoueur2[i])
                {
                    if ((int)bouton.Tag == 1 || (int)bouton.Tag < 0)
                    {
                        labReponse.Content = "Veuiller saisir une case non jouée."; //
                    }
                    else if (UCJoueur1.nbTir == false)
                    {
                        labReponse.Content = "Vous avez déjà joué. Appuyer sur Suivant.";
                    }
                    else
                    {
                        labReponse.Content = "";
                        Verif_Bateau(bouton);
                        UCJoueur1.nbTir = false;
                        butSuivant.Opacity = 1;
                        butSuivant.IsEnabled = true;
                    }
                }
            }
        }


        private void Verif_Bateau(Button bouton)
{
    // On trouve l'index du bouton cliqué pour mettre à jour la grille de J1
    int index = Array.IndexOf(lesBoutonsAttJoueur2, bouton); 
    int tagBateau = (int)bouton.Tag;

    // --- 1. Gérer le Miss (Eau, Tag=0) ---
    if (tagBateau == 0) 
    {
        bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_raté.png", UriKind.Absolute)));
        bouton.Tag = 1; 
        
        // *** CORRECTION 2.B.i : Mise à jour de la grille de défense ADVERSE (J1) ***
        UCJoueur1.lesBoutonsDefJoueur1[index].Tag = 1; 
        UCJoueur1.lesBoutonsDefJoueur1[index].Background = bouton.Background;
    }
    // --- 2. Gérer le Hit (Bateau, Tag > 1) ---
    else if (tagBateau > 1) 
    {
        // Hit
        bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_toucher.png", UriKind.Absolute)));
        bouton.Tag = -tagBateau; // Marquer comme touché

        // *** CORRECTION 2.B.i : Mise à jour de la grille de défense ADVERSE (J1) ***
        UCJoueur1.lesBoutonsDefJoueur1[index].Tag = -tagBateau; 
        UCJoueur1.lesBoutonsDefJoueur1[index].Background = bouton.Background;

        // --- 3. Vérification de la destruction du bateau ---
        int caseBateauRestante = 0;
        // Compter les cases du même bateau qui n'ont pas encore été touchées (Tag == tagBateau) sur la grille de DÉFENSE ADVERSE (J1)
        for (int i = 0; i < UCJoueur1.lesBoutonsDefJoueur1.Length; i++) 
        {
            if (UCJoueur1.lesBoutonsDefJoueur1[i].Tag is int tag && tag == tagBateau)
            {
                caseBateauRestante++;
            }
        }
        
        // Si plus de cases non touchées, le bateau est coulé.
        if (caseBateauRestante == 0)
        {
            // Mettre à jour TOUTES les cases touchées (-tagBateau) en coulé (1) sur les deux grilles
            for (int i = 0; i < lesBoutonsAttJoueur2.Length; i++) 
            {
                if (lesBoutonsAttJoueur2[i].Tag is int attTag && attTag == -tagBateau)
                {
                    // Grille d'attaque (J2)
                    lesBoutonsAttJoueur2[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_détruit.png", UriKind.Absolute)));
                    lesBoutonsAttJoueur2[i].Tag = 1; 

                    // Grille de défense (J1)
                    UCJoueur1.lesBoutonsDefJoueur1[i].Background = lesBoutonsAttJoueur2[i].Background;
                    UCJoueur1.lesBoutonsDefJoueur1[i].Tag = 1;
                }
            }
        }
    }

    // --- 4. Vérification de la fin de partie (Tour 6 Corrigé) ---
    int nbCasesBateauRestantes = 0;

    // Compter toutes les parties de bateau (Tag > 1 ou Tag < 0) restantes sur la grille de DÉFENSE ADVERSE (J1)
    for (int i = 0; i < UCJoueur1.lesBoutonsDefJoueur1.Length; i++)
    {
        if (UCJoueur1.lesBoutonsDefJoueur1[i].Tag is int tag)
        {
            if (tag > 1 || tag < 0) // Si c'est un bateau non coulé
            {
                nbCasesBateauRestantes++;
            }
        }
    }

    if (nbCasesBateauRestantes == 0)
    {
        FinDePartie = true; // J2 a gagné
    }
}

        private void butRotation_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = ((Button)sender);
            estVertical = !estVertical; // Inverse l'orientation

            // Mettre à jour le texte du bouton pour informer le joueur
            if (bouton.Content.ToString().Contains("Verticale"))
                bouton.Content = "Orientation: Horizontale";
            else
                bouton.Content = "Orientation: Verticale";

            int longueur = Bateau[numBateau];
            labReponse.Content = "Orientation changée. Placez le bateau de longueur " + longueur + ".";
        }

        // Dans UCJoueur2.xaml.cs
        private void Placement_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            int indexDepart = Array.IndexOf(lesBoutonsDefJoueur2, bouton);
            int longueur = Bateau[numBateau];

            // On utilise la fonction de vérification
            if (VerifPlacement(lesBoutonsDefJoueur2, indexDepart, longueur, estVertical))
            {
                // 1. Placer le bateau (mise à jour du Tag et de la couleur)
                Placer(lesBoutonsDefJoueur2, indexDepart, longueur, estVertical);
                // 2. Passer au bateau suivant
                numBateau++;

                if (numBateau < Bateau.Length)
                {
                    // Bateau suivant
                    labReponse.Content = $"Bateau placé ! Placez le bateau de longueur {Bateau[numBateau]}. (Orientation: {(estVertical ? "Verticale" : "Horizontale")})";
                }
                else
                {
                    // Fin du placement
                    labReponse.Content = "Tous les bateaux sont placés ! Cliquez sur Suivant.";
                    for (int i = 0; i < lesBoutonsDefJoueur2.Length; i++)
                    {
                        lesBoutonsDefJoueur2[i].Click -= Placement_Click;
                        // Optionnel : Retirer la prévisualisation/surlignement de la souris si vous l'ajoutez
                    }
                    butSuivant.Opacity = 1;
                    butSuivant.IsEnabled = true;
                    // Désactiver le bouton de rotation à la fin du placement
                    butRotation.IsEnabled = false;
                }
            }
            else
            {
                labReponse.Content = "Placement invalide (chevauchement ou hors grille). Réessayez. (Orientation: " + (estVertical ? "Verticale" : "Horizontale") + ")";
            }
        }

        private bool VerifPlacement(Button[] grilleDefense, int indexDepart, int longueur, bool estVertical)
        {
            // ... Logique de vérification du chevauchement et des limites (voir réponse précédente)
            // C'est ici que vous vérifiez que (indexCourant >= 0 && indexCourant < 81) et que
            // grilleDefense[indexCourant].Tag n'est pas déjà '1'
            for (int k = 0; k < longueur; k++)
            {
                int indexCourant = estVertical ? indexDepart + k * 9 : indexDepart + k;
                //Vérification des limites de la grille
                if (indexCourant < 0 || indexCourant >= 81)
                    return false;
                //Vérification du retour à la ligne (uniquement pour horizontal)
                if (!estVertical && indexDepart / 9 != indexCourant / 9)
                    return false;
                //Vérification du chevauchement
                if (grilleDefense[indexCourant].Tag is 1)
                    return false;
            }
            return true;
        }

        // Fonction pour placer le bateau et mettre à jour l'affichage
        private void Placer(Button[] grilleDefense, int indexDepart, int longueur, bool estVertical)
        {
            // L'ID du bateau est maintenant numBateau + 1 (donc 2, 3, 4, 5, 6)
            int bateauID = numBateau + 2;
            for (int k = 0; k < longueur; k++)
            {
                int indexCourant = estVertical ? indexDepart + k * 9 : indexDepart + k;
                // Mettre à jour le Tag avec l'ID unique du bateau
                grilleDefense[indexCourant].Tag = bateauID;

                // Mettre à jour l'affichage
                grilleDefense[indexCourant].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/tempoBateau.jpg")));
            }
        }
        public void FinPhasePlacement()
        {
            enModePlacement = false;

            // Retirer le gestionnaire de clic de placement
            foreach (var bouton in lesBoutonsDefJoueur2) // Utiliser lesBoutonsDefJoueur2 pour UCJoueur2
            {
                bouton.Click -= Placement_Click;
            }

            labReponse.Content = "Tous les bateaux sont placés ! Appuyez sur Suivant.";

            // Activer le bouton qui permet de passer au jeu/transition
            butSuivant.Content = "Commencer le Tour";
            butSuivant.Opacity = 1;
            butSuivant.IsEnabled = true;
        }
        public void ActiverModeAttaque()
        {
            labReponse.Content = "À l'attaque ! Sélectionnez une case adverse.";

            for (int i = 0; i < lesBoutonsAttJoueur2.Length; i++)
            {
                lesBoutonsAttJoueur2[i].Click -= UnBouton_Click;
                lesBoutonsAttJoueur2[i].Click += UnBouton_Click;
            }
        }
    }
}
