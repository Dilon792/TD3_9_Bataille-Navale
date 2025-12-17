using System.Text;
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

namespace Bataille_Navale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int nbTour { set; get; } = 1;
        UCJoueur2 ucJoueur2 = new UCJoueur2();
        UCTransition ucTransition = new UCTransition();
        UCJoueur1 ucJoueur1 = new UCJoueur1();
        UCRegle ucRegle = new UCRegle();
        UCDemarrage ucDemarrage = new UCDemarrage();
        UCFinDePartie ucEcranFin = new UCFinDePartie();
        public MainWindow()
        {
            InitializeComponent();
            AffichageDemarrage();
        }
        public void AffichageDemarrage()
        {
            

            // associe l'écran au conteneur
            ZoneJeu.Content = ucDemarrage;
            ucDemarrage.but2Joueur += AfficherJoueur1;
            ucDemarrage.butRegles.Click += AfficherRegle;
        }

        private void AfficherJoueur1(object sender, RoutedEventArgs e)
        {
            nbTour += 1;
            Console.WriteLine(nbTour);

            // Vérification de la victoire de J2
            if (UCJoueur2.FinDePartie == true)
            {
                ZoneJeu.Content = ucEcranFin;
                return; // Arrêter et afficher l'écran de fin
            }

            ZoneJeu.Content = ucJoueur1;
            this.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/fond_jeu.png")));

            ucJoueur1.butSuivant.Click += AfficherTransition;

            if (nbTour >= 4) // Phase d'attaque
            {
                // Synchroniser J1 Attaque avec J2 Défense
                for (int i = 0; i < UCJoueur1.lesBoutonsAttJoueur1.Length; i++)
                {
                    // J1 Attaque prend l'état de J2 Défense
                    UCJoueur1.lesBoutonsAttJoueur1[i].Tag = UCJoueur2.lesBoutonsDefJoueur2[i].Tag;
                }
                ucJoueur1.ActiverModeAttaque();
            }
        }

        private void AfficherTransition(object sender, RoutedEventArgs e)
        {
            
            ZoneJeu.Content = ucTransition;
            // Supprimer les gestionnaire disponible pour éviter une surcharge
            ucTransition.butSuivantTransition.Click -= AfficherJoueur1;
            ucTransition.butSuivantTransition.Click -= AfficherJoueur2;
            if (nbTour%2 == 0)
                ucTransition.butSuivantTransition.Click += AfficherJoueur2;
            else
                ucTransition.butSuivantTransition.Click += AfficherJoueur1;
        }

        private void AfficherJoueur2(object sender, RoutedEventArgs e)
        {
            nbTour += 1;
            Console.WriteLine(nbTour);

            // Vérification de la victoire de J1
            if (UCJoueur1.FinDePartie == true)
            {
                ZoneJeu.Content = ucEcranFin;
                return; // Arrêter et afficher l'écran de fin
            }

            ZoneJeu.Content = ucJoueur2;
            ucJoueur2.butSuivant.Click += AfficherTransition;

            if (nbTour == 3)
            {
                ucJoueur2.DemarrerPlacementJoueur2();
            }
            else if (nbTour >= 5) // Phase d'attaque
            {
                // Synchroniser J2 Attaque avec J1 Défense
                for (int i = 0; i < UCJoueur2.lesBoutonsAttJoueur2.Length; i++)
                {
                    // J2 Attaque prend l'état de J1 Défense
                    UCJoueur2.lesBoutonsAttJoueur2[i].Tag = UCJoueur1.lesBoutonsDefJoueur1[i].Tag;
                }
                ucJoueur2.ActiverModeAttaque();
            }
        }

        private void AfficherRegle(object sender, RoutedEventArgs e)
        {
            ZoneJeu.Content = ucRegle;
        }
    }
}