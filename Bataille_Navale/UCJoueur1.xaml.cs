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
using static System.Net.Mime.MediaTypeNames;
using static Bataille_Navale.UCJoueur2;

namespace Bataille_Navale
{
    /// <summary>
    /// Logique d'interaction pour UCJoueur1.xaml
    /// </summary>
    public partial class UCJoueur1 : UserControl
    {
        public static bool nbTir { get; set; } = false;
        public static Button[] lesBoutonsAttJoueur1 { get; set; } = new Button[81];
        public static Button[] lesBoutonsDefJoueur1 { get; set; } = new Button[81];
        public int[] Bateau = { 2, 3, 3, 4, 5 };
        public int numBateau = 0;
        public bool estVertical = true;
        public bool enModePlacement = false;
        public string[] nomBateau = ["Torpilleur","Contre-Torpilleur", "Sous-marin", "Croiseur", "Porte-avion"];
        public UCJoueur1()
        {
            InitializeComponent();
            InitialiseGrilleAttaque();
            InitialiseGrilleDefense();
        }

        public void DemarrerPlacementJoueur1()
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

        private void PlacerBateau()
        {
            for (int i = 0; i< lesBoutonsDefJoueur1.Length; i++) // Utilisez lesBoutonsDefJoueur2 pour UCJoueur2
            {
                // Si vous avez déjà attaché un gestionnaire pour le jeu, vous devez le retirer.
                // Sinon, ajoutez simplement le gestionnaire de placement :
                lesBoutonsDefJoueur1[i].Click += Placement_Click;
            }
            // Afficher un message au joueur :
            labReponse.Content = $"Placez le bateau de longueur {Bateau[numBateau]}.";
        }

        public void InitialiseGrilleAttaque()
        {
            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                lesBoutonsAttJoueur1[i] = new Button();
                lesBoutonsAttJoueur1[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsAttJoueur1[i].Width = 50;
                lesBoutonsAttJoueur1[i].Height = 50;
                lesBoutonsAttJoueur1[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsAttJoueur1[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsAttJoueur1[i].Margin = new Thickness(lesBoutonsAttJoueur1[i].Height * (i % 9), lesBoutonsAttJoueur1[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur1.Children.Add(lesBoutonsAttJoueur1[i]);
                lesBoutonsAttJoueur1[i].Tag = 0;
                Grid.SetColumn(lesBoutonsAttJoueur1[i], 1);
                // ici il est placé dans la 1ere colonne da ma grille
                if (MainWindow.nbTour == 1)
                    labReponse.Content = "Placer vos bateau sur la grille de defense";
                else
                    lesBoutonsAttJoueur1[i].Click += this.UnBouton_Click;
            }
        }
        public void InitialiseGrilleDefense()
        {
            for (int i = 0; i < lesBoutonsDefJoueur1.Length; i++)
            {
                lesBoutonsDefJoueur1[i] = new Button();
                lesBoutonsDefJoueur1[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsDefJoueur1[i].Width = 50;
                lesBoutonsDefJoueur1[i].Height = 50;
                lesBoutonsDefJoueur1[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsDefJoueur1[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsDefJoueur1[i].Margin = new Thickness(lesBoutonsDefJoueur1[i].Height * (i % 9), lesBoutonsDefJoueur1[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur1.Children.Add(lesBoutonsDefJoueur1[i]);
                lesBoutonsDefJoueur1[i].Tag = 0;
                Grid.SetColumn(lesBoutonsDefJoueur1[i], 0);
                // ici il est placé dans la 2eme colonne da ma grille
            }
            if (MainWindow.nbTour == 1)
                PlacerBateau();           
        }

       private void UnBouton_Click(object sender, RoutedEventArgs e)
       {
            
            Button bouton = ((Button)sender);
            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                if (bouton == lesBoutonsAttJoueur1[i])
                {
                    if (bouton.Tag is not 0 && bouton.Tag is not 1)
                    {
                        labReponse.Content = "Veuiller saisir une case pas utilisée.";
                    }
                    else if (nbTir == true)
                    {
                        labReponse.Content = "Vous avez déjà joué. Appuyer sur Suivant.";
                    }
                    else
                    {
                        labReponse.Content = "";
                        Verif_Bateau(bouton);
                        nbTir = true;
                        butSuivant.Opacity = 1;
                        butSuivant.IsEnabled = true;
                    }
                }
            }                        
       }

        private void Verif_Bateau(Button bouton)
        {
            int caseBateauRestante = 0;
            if (bouton.Tag is 0)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_raté.png", UriKind.Absolute)));
                bouton.Tag = 2;
            }
            else if (bouton.Tag is not 0)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_toucher.png", UriKind.Absolute)));
                bouton.Tag = 3;
            }
            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                if () ;
            }
        }

        // Dans UCJoueur1.xaml.cs
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

        // Dans UCJoueur1.xaml.cs
        private void Placement_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            int indexDepart = Array.IndexOf(lesBoutonsDefJoueur1, bouton);
            int longueur = Bateau[numBateau];

            // On utilise la fonction de vérification
            if (VerifPlacement(lesBoutonsDefJoueur1, indexDepart, longueur, estVertical))
            {
                // 1. Placer le bateau (mise à jour du Tag et de la couleur)
                Placer(lesBoutonsDefJoueur1, indexDepart, longueur, estVertical);
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
                    FinPhasePlacement();
                }
            }
            else
            {
                labReponse.Content = "Placement invalide (chevauchement ou hors grille). Réessayez. (Orientation: " + (estVertical ? "Verticale" : "Horizontale") + ")";
            }
        }
        public void FinPhasePlacement()
        {
            enModePlacement = false;

            // Retirer le gestionnaire de clic de placement
            foreach (var bouton in lesBoutonsDefJoueur1) // Utiliser lesBoutonsDefJoueur2 pour UCJoueur2
            {
                bouton.Click -= Placement_Click;
            }

            labReponse.Content = "Tous les bateaux sont placés ! Appuyez sur Suivant.";

            // Activer le bouton qui permet de passer au jeu/transition
            butSuivant.Content = "Commencer le Tour";
            butSuivant.Opacity = 1;
            butSuivant.IsEnabled = true;
        }

        private bool VerifPlacement(Button[] grilleDefense, int indexDepart, int longueur, bool estVertical)
            {
            // ... Logique de vérification du chevauchement et des limites (voir réponse précédente)
            // C'est ici que vous vérifiez que (indexCourant >= 0 && indexCourant < 81) et que
            // grilleDefense[indexCourant].Tag n'est pas déjà '1'
                for (int k = 0; k < longueur; k++)
                {
                    int indexCourant = estVertical ? indexDepart + k * 9 : indexDepart + k;
                    // 1. Vérification des limites de la grille
                    if (indexCourant < 0 || indexCourant >= 81) return false;
                    // 2. Vérification du retour à la ligne (uniquement pour horizontal)
                    if (!estVertical && indexDepart / 9 != indexCourant / 9) return false;
                    // 3. Vérification du chevauchement
                    if (grilleDefense[indexCourant].Tag is 1) return false;
                }
            return true;
            }

        // Fonction pour placer le bateau et mettre à jour l'affichage
            private void Placer(Button[] grilleDefense, int indexDepart, int longueur, bool estVertical)
            {
                int bateauID = numBateau + 1;
                for (int k = 0; k < longueur; k++)
                {
                    int indexCourant = estVertical ? indexDepart + k * 9 : indexDepart + k;
                    // Mettre à jour le Tag (0=vide, 1=bateau)
                    grilleDefense[indexCourant].Tag = bateauID;

                    // Mettre à jour l'affichage
                    grilleDefense[indexCourant].Background = new ImageBrush(new BitmapImage(new Uri("P:\\SAE1.01 + SAE1.02\\15_12_2025\\Bataille_Navale\\images\\tempoBateau.jpg")));
                }
            }
        public void ActiverModeAttaque()
        {
            labReponse.Content = "À l'attaque ! Sélectionnez une case adverse.";

            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                // On retire l'événement au cas où pour éviter les doublons
                lesBoutonsAttJoueur1[i].Click -= UnBouton_Click;
                // On ajoute l'événement de tir
                lesBoutonsAttJoueur1[i].Click += UnBouton_Click;
            }
        }

    }
}
