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

namespace Bataille_Navale
{
    /// <summary>
    /// Logique d'interaction pour UCJoueur1.xaml
    /// </summary>
    public partial class UCJoueur1 : UserControl
    {
        bool nbTirJoueur1 = false;
        public Button[] lesBoutonsAtt = new Button[81];
        public Button[] lesBoutonsDef = new Button[81];
        public UCJoueur1()
        {
            nbTirJoueur1 = false;
            InitializeComponent();
            InitialiseGrilleAttaque();
            InitialiseGrilleDefense();
        }
        public void InitialiseGrilleAttaque()
        {
            for (int i = 0; i < lesBoutonsAtt.Length; i++)
            {
                lesBoutonsAtt[i] = new Button();
                lesBoutonsAtt[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsAtt[i].Width = 50;
                lesBoutonsAtt[i].Height = 50;
                lesBoutonsAtt[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsAtt[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsAtt[i].Margin = new Thickness(lesBoutonsAtt[i].Height * (i % 9), lesBoutonsAtt[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur1.Children.Add(lesBoutonsAtt[i]);
                lesBoutonsAtt[i].Tag = 0;
                Grid.SetColumn(lesBoutonsAtt[i], 1);
                // ici il est placé dans la 1ere colonne da ma grille
                lesBoutonsAtt[i].Click += this.UnBouton_Click;
            }
        }
        public void InitialiseGrilleDefense()
        {
            for (int i = 0; i < lesBoutonsDef.Length; i++)
            {
                lesBoutonsDef[i] = new Button();
                lesBoutonsDef[i].Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/carreaux_normal.png", UriKind.Absolute)));
                lesBoutonsDef[i].Width = 50;
                lesBoutonsDef[i].Height = 50;
                lesBoutonsDef[i].VerticalAlignment = VerticalAlignment.Top;
                lesBoutonsDef[i].HorizontalAlignment = HorizontalAlignment.Left;
                lesBoutonsDef[i].Margin = new Thickness(lesBoutonsDef[i].Height * (i % 9), lesBoutonsDef[i].Width * (i / 9), 0, 0);
                this.GrilleJoueur1.Children.Add(lesBoutonsDef[i]);
                lesBoutonsDef[i].Tag = 0;
                Grid.SetColumn(lesBoutonsDef[i], 0);
                // ici il est placé dans la 2eme colonne da ma grille
            }
        }

       private void UnBouton_Click(object sender, RoutedEventArgs e)
       {
            
            Button bouton = ((Button)sender);
            if (bouton.Tag is not 0 && bouton.Tag is not 1)
            {
                labReponse.Content = "Veuiller saisir une case pas utilisée.";
            }
            else if (nbTirJoueur1 == true)
            {
                labReponse.Content = "Vous avez déjà joué. Appuyer sur Suivant.";
            }
            else
            {
                labReponse.Content = "";
                Verif_Bateau(bouton);
                nbTirJoueur1 = true;
                butSuivant.Opacity = 1;
                butSuivant.IsEnabled = true;
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
