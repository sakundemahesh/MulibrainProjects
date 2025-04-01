using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hindalco
{
    public partial class Training_Content_Video : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
        private string position;
        private string language;
        private int totalFilesCount = 0;
        private int watchedFiles = 0;
        private HashSet<string> viewedFiles = new HashSet<string>();
        private List<string> fileList = new List<string>();


        public Training_Content_Video(string position, string language)
        {
            InitializeComponent();
            this.position = position;
            this.language = language;
            LoadTrainingContent();
        }

        private void LoadTrainingContent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_Hindalco", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Position", position);
                        cmd.Parameters.AddWithValue("@Language", language);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fileType = reader["FileType"].ToString();
                                string fileName = reader["FileName"].ToString();
                                string filePath = reader["FilePath"].ToString();

                                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                                {
                                    MessageBox.Show($"⚠ File Not Found: {filePath}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    continue;
                                }

                                fileList.Add(fileName); // ✅ Store file names in the list
                                totalFilesCount++; // ✅ Increment count

                                if (fileType == "mp4")
                                    AddVideoBox(fileName, filePath);
                                else if (fileType == "pdf")
                                    AddPdfBox(fileName, filePath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading content: " + ex.Message);
            }
        }

        private void AddVideoBox(string videoName, string videoPath)
        {
            Border videoBox = new Border
            {
                Width = 350,
                Height = 310,
                Margin = new Thickness(20),
                Background = Brushes.Black,
                CornerRadius = new CornerRadius(10),
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.White
            };

            StackPanel panel = new StackPanel { Width = 350, Height = 250 };

            MediaElement videoPlayer = new MediaElement
            {
                Source = new Uri(videoPath, UriKind.Absolute),
                Width = 350,
                Height = 200,
                LoadedBehavior = MediaState.Manual,
                UnloadedBehavior = MediaState.Stop,
                Stretch = Stretch.UniformToFill,
                IsMuted = true
            };

            videoPlayer.MouseDown += (sender, e) =>
            {
                PlayFullScreenVideo(videoPath);
                MarkAsViewed(videoName, panel);
            };

            TextBlock title = new TextBlock
            {
                Text = videoName,
                FontSize = 10,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5)
            };

            panel.Children.Add(videoPlayer);
            panel.Children.Add(title);
            videoBox.Child = panel;
            videoPanel.Children.Add(videoBox);

            videoPlayer.Loaded += (s, e) => videoPlayer.Play();
        }

        private void AddPdfBox(string pdfName, string pdfPath)
        {
            Border pdfBox = new Border
            {
                Width = 350,
                Height = 250,
                Margin = new Thickness(20),
                Background = Brushes.DarkBlue,
                CornerRadius = new CornerRadius(10),
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.White
            };

            StackPanel panel = new StackPanel { Width = 350, Height = 250 };

            Button pdfButton = new Button
            {
                Content = pdfName,
                Width = 355,
                Height = 170,
                Margin = new Thickness(20),
                Background = Brushes.DarkBlue,
                Foreground = Brushes.White,
                FontSize = 16
            };

            pdfButton.Click += (sender, e) =>
            {
                OpenPdf(pdfPath);
                MarkAsViewed(pdfName, panel);
            };

            panel.Children.Add(pdfButton);
            pdfBox.Child = panel;
            pdfPanel.Children.Add(pdfBox);
        }

        private void MarkAsViewed(string fileName, StackPanel parentPanel)
        {
            if (!viewedFiles.Contains(fileName))
            {
                viewedFiles.Add(fileName);

                // ✅ Video किंवा PDF आहे का ते Check करून Count वाढवतो
                if (fileName.EndsWith(".mp4"))
                {
                    GlobelObject.VideosWatched++;
                }
                else if (fileName.EndsWith(".pdf"))
                {
                    GlobelObject.PDFsViewed++;
                }

                Dispatcher.Invoke(() =>
                {
                    TextBlock viewedText = new TextBlock
                    {
                        Text = "✔ Viewed",
                        Foreground = Brushes.Green,
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    parentPanel.Children.Add(viewedText);
                });

                CheckAllFilesViewed();
            }
        }


        private void CheckAllFilesViewed()
        {
            Dispatcher.Invoke(() =>
            {
                if (viewedFiles.Count == totalFilesCount)
                {
                    btnNext.Visibility = Visibility.Visible;
                }
                else
                {
                    btnNext.Visibility = Visibility.Hidden;
                }
            });
        }

        private void PlayFullScreenVideo(string videoPath)
        {
            Window fullScreenWindow = new Window
            {
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Background = Brushes.Black
            };

            MediaElement videoPlayer = new MediaElement
            {
                Source = new Uri(videoPath, UriKind.Absolute),
                LoadedBehavior = MediaState.Manual,
                Stretch = Stretch.Uniform,
                IsMuted = false
            };

            videoPlayer.MouseDown += (sender, e) => fullScreenWindow.Close();
            fullScreenWindow.Content = videoPlayer;
            fullScreenWindow.Show();
            videoPlayer.Play();
        }

        private void OpenPdf(string pdfPath)
        {
            try
            {
                if (File.Exists(pdfPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = pdfPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("❌ Error: PDF file not found!", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error opening PDF: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            
            Exam_Page examPage = new Exam_Page(language);
            this.NavigationService.Navigate(new QuestionPaper(GlobalPropertyModel.Question[0], GlobalPropertyModel.OptionA[0], GlobalPropertyModel.OptionB[0], GlobalPropertyModel.OptionC[0], GlobalPropertyModel.OptionD[0]));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GlobelObject.PDFsViewed = 0;
            GlobelObject.VideosWatched = 0;
        }
    }
}
