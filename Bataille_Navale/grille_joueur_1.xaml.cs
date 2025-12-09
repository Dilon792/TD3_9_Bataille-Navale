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
using System.Windows.Shapes;

namespace Bataille_Navale
{
    /// <summary>
    /// Logique d'interaction pour grille_joueur_1.xaml
    /// </summary>
    public partial class grille_joueur_1 : Window
    {
        public Button[] lesBoutons = new Button[81];
        public grille_joueur_1()
        {
            InitializeComponent();
        }
        public void InitialiseLesBoutons()
        {
            for (int i = 0; i < lesBoutons.Length; i++)
            {
                lesBoutons[i] = new Button();
                lesBoutons[i].Content = 1;
                lesBoutons[i].Width = 50;
                lesBoutons[i].Height = 50;
                lesBoutons[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutons[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutons[i].Margin = new Thickness(lesBoutons[i].Height * (i % 8), lesBoutons[i].Width * (i / 8), 0, 0);
                this.grille1.Children.Add(lesBoutons[i]);
                Grid.SetColumn(lesBoutons[i], 1);
                // ici il est placé dans la 2eme colonne da ma grille
                lesBoutons[i].Click += this.Bouton_Click;
            }
        }

        private void Bouton_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = ((Button)sender);
            bouton.IsEnabled = false;
            char lettre = bouton.Content.ToString()[0]; ;
        }
    }
}
