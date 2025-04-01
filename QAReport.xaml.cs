using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Windows.Navigation;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;


namespace Hindalco
{
    public partial class QAReport : Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
        private readonly int passID;

        public QAReport(int selectedResultID)
        {
            InitializeComponent();
            this.passID = selectedResultID;
            LoadQAData();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Optional: You can add additional logic here if needed when the page loads.
        }
        private void LoadQAData()
        {
            string query = @"SELECT PassID, QuestionText, CorrectAnswer, UserAnswer 
                             FROM ExamQuestions WHERE PassID = ' " + passID + "'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PassID", passID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            QADataGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error loading Q&A data: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (QADataGrid.Items.Count == 0)
                {
                    MessageBox.Show("❌ No data available to export!", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // ✅ Create Folder if not exists
                string folderPath = @"D:\QA_Reports";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // ✅ Generate file name based on date
                string fileName = $"QA_Report_{passID}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
                string filePath = Path.Combine(folderPath, fileName);

                // ✅ Create Excel Application
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

                // ✅ Add Headers
                worksheet.Cells[1, 1] = "Pass ID";
                worksheet.Cells[1, 2] = "Question";
                worksheet.Cells[1, 3] = "Correct Answer";
                worksheet.Cells[1, 4] = "User Answer";

                // ✅ Add Data from DataGrid
                int row = 2;
                foreach (var item in QADataGrid.Items)
                {
                    if (item is DataRowView rowView)
                    {
                        worksheet.Cells[row, 1] = rowView["PassID"];
                        worksheet.Cells[row, 2] = rowView["QuestionText"];
                        worksheet.Cells[row, 3] = rowView["CorrectAnswer"];
                        worksheet.Cells[row, 4] = rowView["UserAnswer"];
                        row++;
                    }
                }

                // ✅ Auto-fit columns
                worksheet.Columns.AutoFit();

                // ✅ Save & Close Excel
                workbook.SaveAs(filePath);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"✅ Exported Successfully!\nFile saved at: {filePath}", "Export Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error exporting to Excel: {ex.Message}", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Report());
        }
    }
}
