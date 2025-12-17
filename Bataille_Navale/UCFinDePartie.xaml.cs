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

namespace Bataille_Navale
{
    /// <summary>
    /// Logique d'interaction pour UCFinDePartie.xaml
    /// </summary>
    public partial class UCFinDePartie : UserControl
    {
        public UCFinDePartie()
        {
            InitializeComponent();
            Gagnant();
        }

        // Adapte le texte selon le gagnant
        private void Gagnant()
        {
            if (UCJoueur1.FinDePartie == true)
                labGagnant.Content = "Le Joueur 1 a gagné la partie.";
            if(UCJoueur2.FinDePartie == true)
                labGagnant.Content = "Le Joueur 2 a gagné la partie.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
