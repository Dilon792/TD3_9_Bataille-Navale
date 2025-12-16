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
        UCJoueur2 ucJoueur2 = new UCJoueur2();
        UCTransition ucTransition = new UCTransition();
        UCJoueur1 ucJoueur1 = new UCJoueur1();
        public static int nbTour { set; get; } = 1;
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
            ucDemarrage.butJouer_Solo.Click += AfficherJoueur1;
        }

        private void AfficherJoueur1(object sender, RoutedEventArgs e)
        {
            nbTour += 1;
            Console.WriteLine(nbTour);
            ZoneJeu.Content = ucJoueur1;
            this.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/fond_jeu.png")));
            if (UCJoueur1.FinDePartie == true)
                ZoneJeu.Content = ucEcranFin;
            ucJoueur1.butSuivant.Click += AfficherTransition;
            if (nbTour >= 4)
            {
                ucJoueur1.ActiverModeAttaque();
            }

        }

        private void AfficherTransition(object sender, RoutedEventArgs e)
        {
            
            ZoneJeu.Content = ucTransition;
            //Supprimer les gestionnaire disponible pour éviter une surcharge
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
            ZoneJeu.Content = ucJoueur2;
            ucJoueur2.butSuivant.Click += AfficherTransition;
            if (nbTour == 3)
                ucJoueur2.DemarrerPlacementJoueur2();
            else if (nbTour >= 5)
            {
                ucJoueur2.ActiverModeAttaque();
            }

        }
    }
}