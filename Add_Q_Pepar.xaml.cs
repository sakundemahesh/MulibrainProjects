using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Hindalco
{
    public partial class Add_Q_Pepar : Page
    {
        private string excelFilePath = ""; // Stores the selected file path
        private string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString; // Fetch connection string from App.config

        public Add_Q_Pepar()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set EPPlus license context
        }

        // Open File Dialog to Select Excel File
        private void btnUploadExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Select an Excel File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                excelFilePath = openFileDialog.FileName;
                txtFilePath.Text = $"Selected: {Path.GetFileName(excelFilePath)}"; // Display selected file
            }
        }

        // Read Excel and Insert into Database
        private void ImporttoDatabase_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(excelFilePath))
            {
                MessageBox.Show("Please select an Excel file!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbLanguage.SelectedItem == null)
            {
                MessageBox.Show("Please select a language!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedLanguage = (cmbLanguage.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(selectedLanguage))
            {
                MessageBox.Show("Invalid language selection!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set EPPlus license context

                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Read the first worksheet
                    int rowCount = worksheet.Dimension.Rows;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string lastKnownLanguage = selectedLanguage; // Store the last valid language

                        for (int row = 2; row <= rowCount; row++) // Start from row 2 (Skip header row)
                        {
                            string questionText = worksheet.Cells[row, 1].Value?.ToString();
                            string optionA = worksheet.Cells[row, 2].Value?.ToString();
                            string optionB = worksheet.Cells[row, 3].Value?.ToString();
                            string optionC = worksheet.Cells[row, 4].Value?.ToString();
                            string optionD = worksheet.Cells[row, 5].Value?.ToString();
                            string correctAnswer = worksheet.Cells[row, 6].Value?.ToString();
                            string currentLanguage = worksheet.Cells[row, 7].Value?.ToString(); // Read Language from Excel

                            // जर Language NULL किंवा रिकामा असेल, तर मागील भाषा वापरा
                            if (string.IsNullOrWhiteSpace(currentLanguage))
                            {
                                currentLanguage = lastKnownLanguage;
                            }
                            else
                            {
                                lastKnownLanguage = currentLanguage; // नवीन भाषा मिळाल्यास ती अपडेट करा
                            }

                            if (string.IsNullOrWhiteSpace(questionText) || string.IsNullOrWhiteSpace(optionA) ||
                                string.IsNullOrWhiteSpace(optionB) || string.IsNullOrWhiteSpace(optionC) ||
                                string.IsNullOrWhiteSpace(optionD) || string.IsNullOrWhiteSpace(correctAnswer))
                            {
                                continue; // Skip if any field is empty
                            }

                            string query = "INSERT INTO ExamQuestions (Language, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) " +
                                           "VALUES (@Language, @QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer)";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@Language", currentLanguage); // आता NULL नाही, मागील भाषा असेल
                                cmd.Parameters.AddWithValue("@QuestionText", questionText);
                                cmd.Parameters.AddWithValue("@OptionA", optionA);
                                cmd.Parameters.AddWithValue("@OptionB", optionB);
                                cmd.Parameters.AddWithValue("@OptionC", optionC);
                                cmd.Parameters.AddWithValue("@OptionD", optionD);
                                cmd.Parameters.AddWithValue("@CorrectAnswer", correctAnswer.ToUpper());

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Questions imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                txtFilePath.Text = "No file selected"; // Reset file path display
                excelFilePath = ""; // Clear stored file path
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Admin_Panel());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
