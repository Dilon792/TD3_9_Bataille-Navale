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
        public UCJoueur2()
        {
            InitializeComponent();
            InitialiseGrilleAttaque();
            InitialiseGrilleDefense();
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
                lesBoutonsAttJoueur2[i].Tag = 0;
                Grid.SetColumn(lesBoutonsAttJoueur2[i], 0);
                // ici il est placé dans la 1ere colonne da ma grille
                if (MainWindow.nbTour == 1)
                    labReponse.Content = "Placer vos bateau sur la grille de defense";
                else
                    lesBoutonsAttJoueur2[i].Click += this.UnBouton_Click;
            }
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
                Grid.SetColumn(lesBoutonsDefJoueur2[i], 1);
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
                    if (bouton.Tag is not 0 && bouton.Tag is not 1)
                    {
                        labReponse.Content = "Veuiller saisir une case pas utilisée.";
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
            if (bouton.Tag is 0)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_raté.png", UriKind.Absolute)));
            }
            else if (bouton.Tag is 1)
            {
                bouton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_toucher.png", UriKind.Absolute)));
            }
        }
    }
}
