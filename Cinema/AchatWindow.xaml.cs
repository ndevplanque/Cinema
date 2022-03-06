using System;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class AchatWindow : Window
    {
        string[] horaires = { "11h10", "14h10", "17h00", "20h10", "22h00" };
        string[] jours = { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" };

        public AchatWindow()
        {
            InitializeComponent();
            InitialiserComboBox(comboHoraire, horaires);
            InitialiserComboBox(comboJour, jours);
            ActiverAutresTarifs();
        }

        private void InitialiserComboBox(ComboBox comboBox, object[] elements)
        {
            comboBox.Items.Clear();
            foreach (object element in elements) comboBox.Items.Add(element);
            comboBox.SelectedIndex = 0;
        }

        private void ActiverAutresTarifs()
        {
            pleinTarif.IsChecked = true;
            grTarifs.Visibility = Visibility.Visible;
        }
        private void DesactiverAutresTarifs()
        {
            tarifCE.IsChecked = true;
            grTarifs.Visibility = Visibility.Hidden;
            pleinTarif.IsChecked = false;
            etudMineur.IsChecked = false;
            autreReduit.IsChecked = false;
        }

        private void AfficherTarif() => labelTarif.Content = String.Format("Prix final : {0:0.00}€", CalculerTarif());

        private float CalculerTarif()
        {
            float tarif = 0f;
            if (pleinTarif.IsChecked == true) tarif = 8.7f;
            if (autreReduit.IsChecked == true) tarif = 6.9f;
            if (etudMineur.IsChecked == true) tarif = 5.9f;
            if (comboJour.SelectedItem != null && comboJour.SelectedItem.ToString() == "lundi") tarif = 5.7f;
            if (comboJour.SelectedItem != null && comboHoraire.SelectedItem.ToString() == "11h10") tarif = 5.2f;
            if (tarifCE.IsChecked == true) tarif = 5.2f;
            if (check3d.IsChecked == true) tarif += 1.5f;
            if (checkReduc.IsChecked == true) tarif -= 1f;
            return tarif;
        }

        private void tarifCE_Checked(object sender, RoutedEventArgs e)
        {
            DesactiverAutresTarifs();
            AfficherTarif();
        }

        private void tarifCE_Unchecked(object sender, RoutedEventArgs e)
        {
            ActiverAutresTarifs();
            AfficherTarif();
        }

        private void buttonReserver_Click(object sender, RoutedEventArgs e)
        {
            string message = String.Format("Merci d'avoir réservé.\nVeuillez préparer {0:0.00}€ pour payer au guichet.", CalculerTarif());
            MessageBox.Show(message, "Réservation réussie");
        }




        // Autres évènements seulement utiles pour rafraichir le tarif
        private void comboJour_SelectionChanged(object sender, SelectionChangedEventArgs e) => AfficherTarif();
        private void comboHoraire_SelectionChanged(object sender, SelectionChangedEventArgs e) => AfficherTarif();
        private void check3d_Checked(object sender, RoutedEventArgs e) => AfficherTarif();
        private void check3d_Unchecked(object sender, RoutedEventArgs e) => AfficherTarif();
        private void checkReduc_Checked(object sender, RoutedEventArgs e) => AfficherTarif();
        private void checkReduc_Unchecked(object sender, RoutedEventArgs e) => AfficherTarif();
        private void pleinTarif_Checked(object sender, RoutedEventArgs e) => AfficherTarif();
        private void etudMineur_Checked(object sender, RoutedEventArgs e) => AfficherTarif();
        private void autreReduit_Checked(object sender, RoutedEventArgs e) => AfficherTarif();
    }
}
