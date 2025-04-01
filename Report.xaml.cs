using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace Hindalco
{
    public partial class Report : Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;

        public Report()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadReportData("Videos and Documents");
        }

        private void ReportTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReportTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedReport = selectedItem.Content.ToString();
                LoadReportData(selectedReport);
            }
        }

     

        private void LoadReportData(string reportType)
        {
             string query = "";
            
             switch (reportType.Trim())
             {
                 case "Videos and Documents":
                     query = "SELECT ID, FileType, FileName, FilePath, Position, Language FROM VideosAndDocs";
                     break;
            
                 case "Pass Login IDs":
                     query = "SELECT Get_Pass_ID FROM Get_Pass_Login_ID";
                     break;
            
                 case "Exam Questions":
                     query = @"SELECT QuestionID, Language, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer 
                               FROM ExamQuestionsPaper";
                     break;
            
                 case "Exam Report":
                     query = @"SELECT ResultID, PassID, Position, Language, Score, Percentage, 
                              CONVERT(VARCHAR, PassDate, 103) AS PassDate, 
                              CONVERT(VARCHAR, ValidTill, 103) AS ValidTill, 
                      PdfViewed, VideoViewed 
                       FROM ExamReport";
                     break;
                 case "Exam Result":
                     query = @"SELECT PassID, Language, Position, Score, Percentage, 
                             CONVERT(VARCHAR, PassDate, 103) AS PassDate, 
                             CONVERT(VARCHAR, ValidTill, 103) AS ValidTill 
                       FROM ExamResults";
                      break;
            
            
            
            
                         default:
                     MessageBox.Show($"Invalid Report Type Selected! Value received: {reportType}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                     return;
             }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        ReportsDataGrid.AutoGenerateColumns = false;
                        ReportsDataGrid.Columns.Clear();

                        // ✅ Add normal columns
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (column.ColumnName != "ViewQnA" &&
                                column.ColumnName != "PdfViewed" &&
                                column.ColumnName != "VideoViewed") // Exclude PdfViewed & VideoViewed for non-"Exam Report"
                            {
                                DataGridTextColumn textColumn = new DataGridTextColumn
                                {
                                    Header = column.ColumnName,
                                    Binding = new System.Windows.Data.Binding(column.ColumnName)
                                };
                                ReportsDataGrid.Columns.Add(textColumn);
                            }
                        }

                        // ✅ Special handling for "Exam Report"
                        if (reportType == "Exam Report")
                        {
                            // ✅ Add Emoji Display for PdfViewed
                            DataGridTemplateColumn pdfColumn = new DataGridTemplateColumn
                            {
                                Header = "PdfViewed"
                            };

                            FrameworkElementFactory pdfFactory = new FrameworkElementFactory(typeof(TextBlock));
                            pdfFactory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("PdfViewed")
                            {
                                Converter = new BooleanToEmojiConverter()  // True/False to ✅/❌
                            });

                            DataTemplate pdfTemplate = new DataTemplate();
                            pdfTemplate.VisualTree = pdfFactory;
                            pdfColumn.CellTemplate = pdfTemplate;
                            ReportsDataGrid.Columns.Add(pdfColumn);

                            // ✅ Add Emoji Display for VideoViewed
                            DataGridTemplateColumn videoColumn = new DataGridTemplateColumn
                            {
                                Header = "VideoViewed"
                            };

                            FrameworkElementFactory videoFactory = new FrameworkElementFactory(typeof(TextBlock));
                            videoFactory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("VideoViewed")
                            {
                                Converter = new BooleanToEmojiConverter()  // True/False to ✅/❌
                            });

                            DataTemplate videoTemplate = new DataTemplate();
                            videoTemplate.VisualTree = videoFactory;
                            videoColumn.CellTemplate = videoTemplate;
                            ReportsDataGrid.Columns.Add(videoColumn);

                            // ✅ Add "View Q&A" Button
                            DataGridTemplateColumn buttonColumn = new DataGridTemplateColumn
                            {
                                Header = "View Q&A"
                            };

                            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
                            buttonFactory.SetValue(Button.ContentProperty, "View");
                            buttonFactory.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ViewQA_Click));

                            DataTemplate buttonTemplate = new DataTemplate();
                            buttonTemplate.VisualTree = buttonFactory;
                            buttonColumn.CellTemplate = buttonTemplate;

                            ReportsDataGrid.Columns.Add(buttonColumn);
                        }

                        ReportsDataGrid.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No data found for this report!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        ReportsDataGrid.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error loading data: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }


        private void ViewQA_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                DataRowView row = (DataRowView)((FrameworkElement)btn).DataContext;
                int passID = Convert.ToInt32(row["PassID"]);

                // ✅ नवीन QAReport.xaml पेज ओपन करा (Frame Navigation)
                QAReport qaReportPage = new QAReport(passID);
                this.NavigationService?.Navigate(qaReportPage);
            }
        }

        private void btnGeneratePDF_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
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
