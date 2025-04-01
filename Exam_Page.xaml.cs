using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace Hindalco
{
    public partial class Exam_Page : Page
    {
        private string selectedLanguage;
        private List<QuestionModel> examQuestions;
        private int currentQuestionIndex = 0;

        public Exam_Page(string language)
        {
            InitializeComponent();
            selectedLanguage = language;
            LoadExamQuestionsFromSQL();
            DisplayQuestion();
        }

        private void LoadExamQuestionsFromSQL()
        {
            examQuestions = new List<QuestionModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT QuestionID, RTRIM(LTRIM(QuestionText)) AS QuestionText, " +
                                   "RTRIM(LTRIM(OptionA)) AS OptionA, RTRIM(LTRIM(OptionB)) AS OptionB, " +
                                   "RTRIM(LTRIM(OptionC)) AS OptionC, RTRIM(LTRIM(OptionD)) AS OptionD, " +
                                   "RTRIM(LTRIM(CorrectAnswer)) AS CorrectAnswer FROM ExamQuestionsPaper WHERE Language = @Language";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Language", selectedLanguage);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            examQuestions.Add(new QuestionModel
                            {
                                QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                Question = reader["QuestionText"].ToString(),
                                Options = new List<string>
                                {
                                    reader["OptionA"].ToString(),
                                    reader["OptionB"].ToString(),
                                    reader["OptionC"].ToString(),
                                    reader["OptionD"].ToString()
                                },
                                CorrectAnswer = reader["CorrectAnswer"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DisplayQuestion()
        {
            if (examQuestions.Count == 0)
            {
                MessageBox.Show($"No questions found for {selectedLanguage}!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var question = examQuestions[currentQuestionIndex];
            QuestionTextBlock.Text = question.Question;
            OptionsListBox.Items.Clear();

            foreach (var option in question.Options)
            {
                ListBoxItem item = new ListBoxItem { Content = option };
                OptionsListBox.Items.Add(item);
            }

            // Last Question Check
            NextButton.Visibility = (currentQuestionIndex == examQuestions.Count - 1) ? Visibility.Collapsed : Visibility.Visible;
            SubmitButton.Visibility = (currentQuestionIndex == examQuestions.Count - 1) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (OptionsListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select an answer!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string userAnswer = (OptionsListBox.SelectedItem as ListBoxItem).Content.ToString().Trim().ToUpper();
            string questionText = examQuestions[currentQuestionIndex].Question;
            int questionID = examQuestions[currentQuestionIndex].QuestionID;
            string correctAnswer = GetCorrectAnswer(questionID).Trim().ToUpper();

            // ✅ Store Answer in Global Object
            GlobelObject.UserAnswers.Add(new Tuple<string, string, string>(questionText, userAnswer, correctAnswer));

            if (string.Equals(userAnswer, correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                GlobelObject.TotalScore++;
            }

            // ✅ Move to Next Question
            if (currentQuestionIndex < examQuestions.Count - 1)
            {
                currentQuestionIndex++;
                DisplayQuestion();
            }
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OptionsListBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select an answer!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string userAnswer = (OptionsListBox.SelectedItem as ListBoxItem).Content.ToString().Trim().ToUpper();
                string questionText = examQuestions[currentQuestionIndex].Question;
                int questionID = examQuestions[currentQuestionIndex].QuestionID;
                string correctAnswer = GetCorrectAnswer(questionID).Trim().ToUpper();

                // ✅ Debugging: Show the Correct Answer and User Answer Before Comparison
               // MessageBox.Show($"Q: {questionText}\nYour Answer: {userAnswer}\nCorrect Answer: {correctAnswer}", "DEBUG CHECK", MessageBoxButton.OK, MessageBoxImage.Information);

                // ✅ Store Answer in Global Object
                GlobelObject.UserAnswers.Add(new Tuple<string, string, string>(questionText, userAnswer, correctAnswer));

                // ✅ Compare Answers Properly (Case Insensitive)
                if (string.Equals(userAnswer, correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    GlobelObject.TotalScore++;
                }

                // ✅ Move to Next Question if Available
                if (currentQuestionIndex < examQuestions.Count - 1)
                {
                    currentQuestionIndex++;
                    DisplayQuestion();
                }
                else
                {
                    // ✅ All Questions Answered, Proceed to Result
                    SaveExamResultToDatabase();
                    NavigationService?.Navigate(new Exam_Result(Convert.ToInt32(GlobelObject.GetPassID)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private string GetCorrectAnswer(int questionID)
        {
            string correctAnswer = "";

            string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT LTRIM(RTRIM(CorrectAnswer)) FROM ExamQuestions WHERE QuestionID = @QuestionID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@QuestionID", questionID);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            correctAnswer = result.ToString().Trim().ToUpper();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching correct answer: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // ✅ Debugging: Check if Correct Answer is Being Fetched Properly
         //   MessageBox.Show($"DEBUG: Fetched Correct Answer = {correctAnswer}", "Correct Answer Check", MessageBoxButton.OK, MessageBoxImage.Information);

            return correctAnswer;
        }





        private void SaveExamResultToDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Insert PassID once, if it doesn't already exist
                    string insertPassIDQuery = @"
IF NOT EXISTS (SELECT 1 FROM ExamResults WHERE PassID = @PassID)
BEGIN
    INSERT INTO ExamResults (PassID)
    VALUES (@PassID);
END;";

                    using (SqlCommand passIDCmd = new SqlCommand(insertPassIDQuery, conn))
                    {
                        passIDCmd.Parameters.AddWithValue("@PassID", Convert.ToInt32(GlobelObject.GetPassID));
                        passIDCmd.ExecuteNonQuery();
                    }

                    // Insert question details linked to the existing PassID
                    string insertQuestionsQuery = @"
INSERT INTO ExamResultsDetails (PassID, QuestionText, UserAnswer, CorrectAnswer)
VALUES (@PassID, @QuestionText, @UserAnswer, @CorrectAnswer);";

                    foreach (var answer in GlobelObject.UserAnswers)
                    {
                        using (SqlCommand questionCmd = new SqlCommand(insertQuestionsQuery, conn))
                        {
                            questionCmd.Parameters.AddWithValue("@PassID", Convert.ToInt32(GlobelObject.GetPassID));
                            questionCmd.Parameters.AddWithValue("@QuestionText", answer.Item1.Trim());
                            questionCmd.Parameters.AddWithValue("@UserAnswer", answer.Item2.Trim());
                            questionCmd.Parameters.AddWithValue("@CorrectAnswer", answer.Item3.Trim());

                            questionCmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("✅ Exam Results Saved Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error saving exam results: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class QuestionModel
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
