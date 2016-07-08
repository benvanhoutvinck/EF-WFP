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

namespace EFInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VDABContext context = new VDABContext();
        public MainWindow()
        {
            InitializeComponent();
            VulListMentors();
        }

        private void VulListMentors()
        {
            listMentors.ItemsSource = (from cursist in context.Cursisten
                                       where cursist.Beschermelingen.Count() != 0
                                       orderby cursist.Voornaam, cursist.Familienaam
                                       select cursist).ToList();
        }

        private void ListMentorsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listMentors.SelectedItem != null)
            {
                var mentor = (Cursist)listMentors.SelectedItem;
                gridBeschermelingen.ItemsSource = mentor.Beschermelingen;
            }
        }

        private void Opslaan(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
            // Opdat de wijzigingen die je in de grid deed ook zichtbaar zijn in de list:
            VulListMentors();
        }

       

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }
    }
}
