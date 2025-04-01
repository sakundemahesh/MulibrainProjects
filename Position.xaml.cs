using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hindalco
{
    public partial class Position : Page
    {
        private string selectedLanguage;

        public Position(string language)
        {
            InitializeComponent();
            selectedLanguage = language;
            UpdatePositionNames(); // Update position names based on selected language
        }

        // Global Object to store English position names
        private Dictionary<string, string> positionNames = new Dictionary<string, string>
{
    { "Admin", "Admin" },
    { "Supervisor", "Supervisor" },
    { "Manager", "Manager" },
    { "HR", "Human Resources (HR)" },
    { "ITSupport", "IT Support" },
    { "Accountant", "Accountant" }
};

        // Function to update button content based on selected language
        private void UpdatePositionNames()
        {
            // Define language-specific translations
            Dictionary<string, string> translatedNames = new Dictionary<string, string>();

            switch (selectedLanguage)
            {
                case "Marathi":
                    translatedNames = new Dictionary<string, string>
            {
                { "Admin", "प्रशासक (Admin)" },
                { "Supervisor", "पर्यवेक्षक (Supervisor)" },
                { "Manager", "व्यवस्थापक (Manager)" },
                { "HR", "मानव संसाधन (HR)" },
                { "ITSupport", "आयटी समर्थन (IT Support)" },
                { "Accountant", "लेखापाल (Accountant)" }
            };
                    break;

                case "Hindi":
                    translatedNames = new Dictionary<string, string>
            {
                { "Admin", "व्यवस्थापक (Admin)" },
                { "Supervisor", "पर्यवेक्षक (Supervisor)" },
                { "Manager", "प्रबंधक (Manager)" },
                { "HR", "मानव संसाधन (HR)" },
                { "ITSupport", "आईटी समर्थन (IT Support)" },
                { "Accountant", "लेखाकार (Accountant)" }
            };
                    break;

                default: // English
                    translatedNames = positionNames; // Use default English names
                    break;
            }

            // Assign values to buttons
            AdminButton.Content = translatedNames["Admin"];
            SupervisorButton.Content = translatedNames["Supervisor"];
            ManagerButton.Content = translatedNames["Manager"];
            HRButton.Content = translatedNames["HR"];
            ITSupportButton.Content = translatedNames["ITSupport"];
            AccountantButton.Content = translatedNames["Accountant"];
        }


        private void Position_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                string selectedPosition = clickedButton.Content.ToString();

                GlobelObject.SelectedPosition = selectedPosition;

               // MessageBox.Show("Selected Position: " + GlobelObject.SelectedPosition);

                NavigationService?.Navigate(new Training_Content_Video(GlobelObject.SelectedPosition, GlobelObject.SelectedLanguage));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Backbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Language_Choose());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
