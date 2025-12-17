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
        public static bool FinDePartie { get; set; } = false;
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
                // Décharger les boutons
                lesBoutonsDefJoueur2[i].Click -= Placement_Click;
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
            for (int i = 0; i< lesBoutonsDefJoueur1.Length; i++)
            {
                // Ajoute le gestionnaire de placement :
                lesBoutonsDefJoueur1[i].Click += Placement_Click;
            }
            // Afficher un message au joueur :
            labReponse.Content = $"Placez le bateau de longueur {Bateau[numBateau]}.";
        }

        public void InitialiseGrilleAttaque()
        {
            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                // Création des boutons de la grille attaque avec leurs caractéristiques
                lesBoutonsAttJoueur1[i] = new Button();
                lesBoutonsAttJoueur1[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsAttJoueur1[i].Width = 50;
                lesBoutonsAttJoueur1[i].Height = 50;
                lesBoutonsAttJoueur1[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsAttJoueur1[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsAttJoueur1[i].Margin = new Thickness(lesBoutonsAttJoueur1[i].Height * (i % 9), lesBoutonsAttJoueur1[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur1.Children.Add(lesBoutonsAttJoueur1[i]);
                lesBoutonsAttJoueur1[i].Tag = 0;
                // ici il est placé dans la 1ere colonne da ma grille
                Grid.SetColumn(lesBoutonsAttJoueur1[i], 1);
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
                // Création des boutons de la grille défense aevce leurs caractéristiques
                lesBoutonsDefJoueur1[i] = new Button();
                lesBoutonsDefJoueur1[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsDefJoueur1[i].Width = 50;
                lesBoutonsDefJoueur1[i].Height = 50;
                lesBoutonsDefJoueur1[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsDefJoueur1[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsDefJoueur1[i].Margin = new Thickness(lesBoutonsDefJoueur1[i].Height * (i % 9), lesBoutonsDefJoueur1[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur1.Children.Add(lesBoutonsDefJoueur1[i]);
                lesBoutonsDefJoueur1[i].Tag = 0;
                // Indique la colonne dans laquelle se trouve la grille
                Grid.SetColumn(lesBoutonsDefJoueur1[i], 0);
            }
            if (MainWindow.nbTour == 1)
                PlacerBateau();           
        }

       private void UnBouton_Click(object sender, RoutedEventArgs e)
       {
            // Récupération du bouton cliqué
            Button bouton = ((Button)sender);
            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                if (bouton == lesBoutonsAttJoueur1[i])
                {
                    // Vérifie si la case choisi n'a pas déjà été joué
                    if ((int)bouton.Tag == 1 || (int)bouton.Tag < 0) 
                    {
                        labReponse.Content = "Veuiller saisir une case non jouée.";
                    }
                    // Vérifie si le tir n'a pas déjà été joué
                    else if (nbTir == true)
                    {
                        labReponse.Content = "Vous avez déjà joué. Appuyer sur Suivant.";
                    }
                    else
                    {
                        labReponse.Content = "";
                        Verif_Bateau(bouton);
                        // Enregistre que le tir a été effectué ce tour.
                        nbTir = true;
                        // Active le boutton Suivant
                        butSuivant.Opacity = 1;
                        butSuivant.IsEnabled = true;
                    }
                }
            }                        
       }

        private void Verif_Bateau(Button bouton)
        {
            // On trouve l'index du bouton cliqué pour mettre à jour la grille de J2
            int index = Array.IndexOf(lesBoutonsAttJoueur1, bouton);
            int tagBateau = (int)bouton.Tag;

            // Si le tir est ratée
            if (tagBateau == 0)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_raté.png", UriKind.Absolute)));
                bouton.Tag = 1;

                // Mise à jour de la grille du J2
                UCJoueur2.lesBoutonsDefJoueur2[index].Tag = 1;
                UCJoueur2.lesBoutonsDefJoueur2[index].Background = bouton.Background;
            }
            // Si le tir est touché
            else if (tagBateau > 1)
            {
                // Lance les modifications si le bateau est touché
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_toucher.png", UriKind.Absolute)));
                bouton.Tag = -tagBateau;

                // Mise à jour de la grille de défense ADVERSE
                UCJoueur2.lesBoutonsDefJoueur2[index].Tag = -tagBateau;
                UCJoueur2.lesBoutonsDefJoueur2[index].Background = bouton.Background;

                // Vérification de la destruction du bateau
                int caseBateauRestante = 0;
                // Compter les cases du même bateau qui n'ont pas encore été touchées sur la grille du J2
                for (int i = 0; i < UCJoueur2.lesBoutonsDefJoueur2.Length; i++)
                {
                    if (UCJoueur2.lesBoutonsDefJoueur2[i].Tag is int tag && tag == tagBateau)
                    {
                        caseBateauRestante++;
                    }
                }

                // Si plus de cases non touchées, le bateau est coulé.
                if (caseBateauRestante == 0)
                {
                    // Mettre à jour toutes les cases touchées en coulé sur les deux grilles
                    for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
                    {
                        if (lesBoutonsAttJoueur1[i].Tag is int attTag && attTag == -tagBateau)
                        {
                            // Grille d'attaque (J1)
                            lesBoutonsAttJoueur1[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_détruit.png", UriKind.Absolute)));
                            lesBoutonsAttJoueur1[i].Tag = 1;

                            // Grille de défense (J2)
                            UCJoueur2.lesBoutonsDefJoueur2[i].Background = lesBoutonsAttJoueur1[i].Background;
                            UCJoueur2.lesBoutonsDefJoueur2[i].Tag = 1;
                        }
                    }
                }
            }

            // Vérification de la fin de partie
            int nbCasesBateauRestantes = 0;

            // Compter toutes les parties de bateau restantes sur la grille du J2
            for (int i = 0; i < UCJoueur2.lesBoutonsDefJoueur2.Length; i++)
            {
                if (UCJoueur2.lesBoutonsDefJoueur2[i].Tag is int tag)
                {
                    // Si c'est un bateau non coulé
                    if (tag > 1 || tag < 0) 
                    {
                        nbCasesBateauRestantes++;
                    }
                }
            }

            if (nbCasesBateauRestantes == 0)
            { 
                // Lance la fin de la partie
                FinDePartie = true;
            }
        }


        // Dans UCJoueur1.xaml.cs
        private void butRotation_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = ((Button)sender);
            // Inverse l'orientation
            estVertical = !estVertical; 

            // Mettre à jour le texte du bouton pour informer le joueur
            if (bouton.Content.ToString().Contains("Verticale"))
                bouton.Content = "Orientation: Horizontale";
            else
                bouton.Content = "Orientation: Verticale";

            int longueur = Bateau[numBateau];
            labReponse.Content = "Orientation changée. Placez le bateau de longueur " + longueur + ".";
        }

        private void Placement_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            int indexDepart = Array.IndexOf(lesBoutonsDefJoueur1, bouton);
            int longueur = Bateau[numBateau];

            // On utilise la fonction de vérification
            if (VerifPlacement(lesBoutonsDefJoueur1, indexDepart, longueur, estVertical))
            {
                // Placer le bateau
                Placer(lesBoutonsDefJoueur1, indexDepart, longueur, estVertical);
                // Passer au bateau suivant
                numBateau++;

                if (numBateau < Bateau.Length)
                {
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

        // Permet de finir la phase de de placement et de ne pas surcharger les méthodes
        public void FinPhasePlacement()
        {
            enModePlacement = false;

            // Retirer le gestionnaire de clic de placement
            foreach (var bouton in lesBoutonsDefJoueur1)
            {
                bouton.Click -= Placement_Click;
            }

            labReponse.Content = "Tous les bateaux sont placés ! Appuyez sur Suivant.";

            // Activer le bouton qui permet de passer au jeu/transition
            butSuivant.Content = "Suivant";
            butSuivant.Opacity = 1;
            butSuivant.IsEnabled = true;
        }

        // Vérifier si on peut placer le bateau 
        private bool VerifPlacement(Button[] grilleDefense, int indexDepart, int longueur, bool estVertical)
        {
            for (int k = 0; k < longueur; k++)
            {
                 int indexCourant = estVertical ? indexDepart + k * 9 : indexDepart + k;
                 // Vérification des limites de la grille
                 if (indexCourant < 0 || indexCourant >= 81) return false;
                 // Vérification du retour à la ligne (uniquement pour horizontal)
                 if (!estVertical && indexDepart / 9 != indexCourant / 9) return false;
                 // Vérification du chevauchement
                 if ((int)grilleDefense[indexCourant].Tag > 1) return false;
            }
            return true;
        }

        // Fonction pour placer le bateau et mettre à jour l'affichage
        private void Placer(Button[] grilleDefense, int indexDepart, int longueur, bool estVertical)
        {
            int bateauID = numBateau + 2;
            for (int k = 0; k < longueur; k++)
            {
                int indexCourant = estVertical ? indexDepart + k * 9 : indexDepart + k;
                // Mettre à jour le Tag
                grilleDefense[indexCourant].Tag = bateauID;

                // Mettre à jour l'affichage
                grilleDefense[indexCourant].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/tempoBateau.jpg")));
            }
        }
        public void ActiverModeAttaque()
        {
            labReponse.Content = "À l'attaque ! Sélectionnez une case adverse.";

            for (int i = 0; i < lesBoutonsAttJoueur1.Length; i++)
            {
                // On retire l'événement pour éviter les doublons
                lesBoutonsAttJoueur1[i].Click -= UnBouton_Click;
                // On ajoute l'événement de tir
                lesBoutonsAttJoueur1[i].Click += UnBouton_Click;
            }
        }

    }
}
