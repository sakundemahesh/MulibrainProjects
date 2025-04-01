using System;
using System.Collections.Generic;
using System.Data;
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

namespace Hindalco
{
    /// <summary>
    /// Interaction logic for Language_Choose.xaml
    /// </summary>
    public partial class Language_Choose : Page
    {
        public Language_Choose()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MarathiButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToExam("Marathi");
        }

        private void HindiButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToExam("Hindi");
        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToExam("English");
        }

        private void NavigateToExam(string language)
        {
            GlobelObject.SelectedLanguage = language;
            NavigationService?.Navigate(new Position(language)); // Passing language to Exam Page
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Login_Screen());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
