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
        public MainWindow()
        {
            InitializeComponent();
            AffichageDemarrage();
        }
        public void AffichageDemarrage()
        {
            UCDemarrage uc = new UCDemarrage();

            // associe l'écran au conteneur
            ZoneJeu.Content = uc;
            uc.butJouer_Solo.Click += AfficherJoueur1;
        }

        private void AfficherJoueur1(object sender, RoutedEventArgs e)
        {
            UCJoueur1 uc = new UCJoueur1();
            ZoneJeu.Content = uc;
            this.Background = new ImageBrush(new BitmapImage(new Uri("P:\\SAE1.01 + SAE1.02\\10_12_2025\\Bataille_Navale\\images\\66398065-5fd7-40bc-8f7f-b46fd6e77522.png")));
        }
    }
}