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
    /// Logique d'interaction pour UCJoueur1.xaml
    /// </summary>
    public partial class UCJoueur1 : UserControl
    {
        public Button[] lesBoutons = new Button[81];
        public UCJoueur1()
        {
            InitializeComponent();
            InitialiseGrilleAttaque();
            InitialiseGrilleDefense();
        }
        public void InitialiseGrilleAttaque()
        {
            for (int i = 0; i < lesBoutons.Length; i++)
            {
                lesBoutons[i] = new Button();
                lesBoutons[i].Width = 50;
                lesBoutons[i].Height = 50;
                lesBoutons[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutons[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutons[i].Margin = new Thickness(lesBoutons[i].Height * (i % 9), lesBoutons[i].Width * (i / 9), 0, 0);
                this.Grille.Children.Add(lesBoutons[i]);
                Grid.SetColumn(lesBoutons[i], 1);
                // ici il est placé dans la 2eme colonne da ma grille
                lesBoutons[i].Click += this.UnBouton_Click;
            }
        }
        public void InitialiseGrilleDefense()
        {
            for (int i = 0; i < lesBoutons.Length; i++)
            {
                lesBoutons[i] = new Button();
                lesBoutons[i].Width = 50;
                lesBoutons[i].Height = 50;
                lesBoutons[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutons[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutons[i].Margin = new Thickness(lesBoutons[i].Height * (i % 9), lesBoutons[i].Width * (i / 9), 0, 0);
                this.Grille.Children.Add(lesBoutons[i]);
                Grid.SetColumn(lesBoutons[i], 1);
                // ici il est placé dans la 2eme colonne da ma grille
                lesBoutons[i].Click += this.UnBouton_Click;
            }
        }

        private void UnBouton_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = ((Button)sender);
            bouton.IsEnabled = false;
        }
    }
}
