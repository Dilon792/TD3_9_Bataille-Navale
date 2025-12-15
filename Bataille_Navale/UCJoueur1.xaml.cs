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
        public int[] Bateau = [2, 3, 3, 4, 5];
        public string[] nomBateau = ["Torpilleur","Contre-Torpilleur", "Sous-marin", "Croiseur", "Porte-avion"];
        public UCJoueur1()
        {
            InitializeComponent();
            InitialiseGrilleAttaque();
            InitialiseGrilleDefense();
        }

        private void PlacerBateau()
        {
            for (int i = 0; i < Bateau.Length; i++)
            {
                labReponse.Content = "Placer votre " + nomBateau[i] + " et il fait " + Bateau[i] + " cases de long.";
            }
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
                if (MainWindow.nbTour == 1)
                    PlacerBateau();
            }
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
            if (bouton.Tag is 0)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_raté.png", UriKind.Absolute)));
                bouton.Tag = 2;
            }
            else if (bouton.Tag is 1)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_toucher.png", UriKind.Absolute)));
                bouton.Tag = 3;
            }
        }
        
    }
}
