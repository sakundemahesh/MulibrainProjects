using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Hindalco
{
    public partial class Add_Video_PDF : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
        private string filePath = "";
        private string fileType = "";
        private string selectedPosition = "";
        private string selectedLanguage = "";

        public Add_Video_PDF()
        {
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtFilePath.Text = "📂 No File Selected";
            btnUpload.IsEnabled = false;
        }

        private void cmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLanguage.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedLanguage = selectedItem.Content.ToString(); // ✅ Content वापरून Correct Value घ्या
                UpdatePositionList(selectedLanguage);
            }
        }





        private void UpdatePositionList(string language)
        {
            // Remove previous items
            cmbPosition.Items.Clear();

            switch (language)
            {
                case "Marathi":
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "प्रशासक (Admin)" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "पर्यवेक्षक (Supervisor)" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "व्यवस्थापक (Manager)" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "मानव संसाधन (HR)" });
                    break;

                case "Hindi":
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "व्यवस्थापक (Admin)" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "पर्यवेक्षक (Supervisor)" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "प्रबंधक (Manager)" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "मानव संसाधन (HR)" });
                    break;

                default: // English
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "Admin" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "Supervisor" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "Manager" });
                    cmbPosition.Items.Add(new ComboBoxItem { Content = "HR" });
                    break;
            }

            // ✅ UI Refresh करा
            cmbPosition.SelectedIndex = 0; // Default first item select करा
        }



        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files (*.mp4;*.avi)|*.mp4;*.avi|PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*",
                Title = "Select Video or PDF File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                fileType = Path.GetExtension(filePath).ToLower() == ".pdf" ? "pdf" : "mp4";
                txtFilePath.Text = $"📂 Selected: {Path.GetFileName(filePath)}";
                btnUpload.IsEnabled = true;
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPosition.SelectedItem is ComboBoxItem positionItem)
            {
                selectedPosition = positionItem.Content.ToString(); // ✅ Position Name Properly Get करा
            }

            if (cmbLanguage.SelectedItem is ComboBoxItem languageItem)
            {
                selectedLanguage = languageItem.Content.ToString(); // ✅ Language Name Properly Get करा
            }

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(selectedPosition) || string.IsNullOrEmpty(selectedLanguage))
            {
                MessageBox.Show("⚠ कृपया भाषा आणि पद निवडा आणि नंतर फाइल अपलोड करा!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO VideosAndDocs (FileType, FileName, FilePath, Position, Language) VALUES (@FileType, @FileName, @FilePath, @Position, @Language)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FileType", fileType);
                        cmd.Parameters.AddWithValue("@FileName", Path.GetFileName(filePath));
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@Position", selectedPosition);
                        cmd.Parameters.AddWithValue("@Language", selectedLanguage);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("✅ File Upload Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                txtFilePath.Text = "📂 No File Selected";
                filePath = "";
                btnUpload.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠ Error uploading file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
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
