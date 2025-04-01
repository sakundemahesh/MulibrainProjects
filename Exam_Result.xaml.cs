using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hindalco
{
    public partial class Exam_Result : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
        private int PassID;

        public Exam_Result(int passID)
        {
            InitializeComponent();
            CheckAnswer();
            CalculatedPercentage();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtPassID.Text = GlobelObject.GetPassID;
                txtLanguage.Text = GlobelObject.SelectedLanguage;
                txtPosition.Text = GlobelObject.SelectedPosition;
                txtVideosWatched.Text = GlobelObject.VideosWatched.ToString();
                txtPDFsViewed.Text = GlobelObject.PDFsViewed.ToString();
                txtPDFsViewed.Text = GlobelObject.PDFsViewed.ToString();
                txtScore.Text = GlobelObject.RightAnsw.ToString();
                txtPassDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtValidTill.Text = DateTime.Now.AddMonths(6).ToString("dd/MM/yyyy"); // Valid for 6 months
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CalculatedPercentage()
        {
            try
            {
                double percentage = GlobelObject.RightAnsw / 30 * 100;
                txtPercentage.Text = percentage + " %";
                GlobelObject.Percentage = percentage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckAnswer()
        {
            try
            {
                if (GlobelObject.Q_Answers[0] == GlobalPropertyModel.Answer[0])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }

                if (GlobelObject.Q_Answers[1] == GlobalPropertyModel.Answer[1])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[2] == GlobalPropertyModel.Answer[2])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[3] == GlobalPropertyModel.Answer[3])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[4] == GlobalPropertyModel.Answer[4])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[5] == GlobalPropertyModel.Answer[5])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[6] == GlobalPropertyModel.Answer[6])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[7] == GlobalPropertyModel.Answer[7])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[8] == GlobalPropertyModel.Answer[8])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[9] == GlobalPropertyModel.Answer[9])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[10] == GlobalPropertyModel.Answer[10])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[11] == GlobalPropertyModel.Answer[11])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[12] == GlobalPropertyModel.Answer[12])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[13] == GlobalPropertyModel.Answer[13])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
                if (GlobelObject.Q_Answers[14] == GlobalPropertyModel.Answer[14])
                {
                    GlobelObject.RightAnsw += 2;
                }
                else
                {
                    GlobelObject.wrongAnsw += 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private int GetTotalQuestionsFromDB()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Hindalco.dbo.ExamQuestions";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int totalQuestions = Convert.ToInt32(cmd.ExecuteScalar());
                        MessageBox.Show($"DEBUG: Total Questions in DB = {totalQuestions}");
                        return totalQuestions;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error fetching total questions: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
            }
        }

        //private void saveexamresulttodatabase()
        //{
        //    using (sqlconnection conn = new sqlconnection(connectionstring))
        //    {
        //        try
        //        {
        //            conn.open();
        //            string query = @"
        //            insert into examresults (passid, language, position, score, percentage, passdate, validtill)
        //            values (@passid, @language, @position, @score, @percentage, @passdate, @validtill)";

        //            using (sqlcommand cmd = new sqlcommand(query, conn))
        //            {
        //                int totalquestions = globelobject.totalquestions;
        //                if (totalquestions == 0)
        //                {
        //                    totalquestions = gettotalquestionsfromdb();
        //                    globelobject.totalquestions = totalquestions;
        //                }

        //                globelobject.totalscore = 0;
        //                foreach (var answer in globelobject.useranswers)
        //                {
        //                    if (answer.item2 == answer.item3)
        //                    {
        //                        globelobject.totalscore++;
        //                    }
        //                }

        //                int score = globelobject.totalscore;
        //                double percentage = (totalquestions > 0) ? (score / (double)totalquestions) * 100 : 0;

        //                datetime passdate = datetime.now;
        //                datetime validtill = passdate.addmonths(6);

        //                cmd.parameters.addwithvalue("@passid", convert.toint32(globelobject.getpassid));
        //                cmd.parameters.addwithvalue("@language", globelobject.selectedlanguage);
        //                cmd.parameters.addwithvalue("@position", globelobject.selectedposition);
        //                cmd.parameters.addwithvalue("@score", score);
        //                cmd.parameters.addwithvalue("@percentage", percentage);
        //                cmd.parameters.addwithvalue("@passdate", passdate);
        //                cmd.parameters.addwithvalue("@validtill", validtill);

        //                messagebox.show($"debug: saving pass date = {passdate.tostring("dd-mm-yyyy", cultureinfo.invariantculture)}");

        //                cmd.executenonquery();
        //            }

        //            messagebox.show("✅ exam result saved successfully!", "success", messageboxbutton.ok, messageboximage.information);
        //        }
        //        catch (exception ex)
        //        {
        //            messagebox.show("❌ error saving exam result: " + ex.message, "database error", messageboxbutton.ok, messageboximage.error);
        //        }
        //    }
        //}

        //private void BindGlobalData()
        //{
        //    txtPassID.Text = GlobelObject.GetPassID.ToString();
        //    txtLanguage.Text = GlobelObject.SelectedLanguage;
        //    txtPosition.Text = GlobelObject.SelectedPosition;
        //    txtVideosWatched.Text = GlobelObject.VideosWatched.ToString();
        //    txtPDFsViewed.Text = GlobelObject.PDFsViewed.ToString();

        //    MessageBox.Show($"DEBUG: Before Display -> TotalScore={GlobelObject.TotalScore}, TotalQuestions={GlobelObject.TotalQuestions}");

        //    if (GlobelObject.TotalQuestions > 0)
        //    {
        //        txtScore.Text = $"{GlobelObject.TotalScore} / {GlobelObject.TotalQuestions}";
        //        double percentage = (GlobelObject.TotalScore / (double)GlobelObject.TotalQuestions) * 100;
        //        txtPercentage.Text = $"{percentage:F2}%";
        //    }
        //    else
        //    {
        //        txtScore.Text = "0 / 0";
        //        txtPercentage.Text = "0.00%";
        //    }
        //}



        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            InsertExamReport();
            InsertQuestionAnswer();
            SaveExamSummary(); // Save data to ExamReport table
            NavigationService.Navigate(new Login_Screen());
            GlobelObject.RightAnsw = 0;
            GlobelObject.wrongAnsw = 0;
            GlobelObject.Percentage = 0;
            GlobelObject.PDFsViewed = 0;
            GlobelObject.VideosWatched = 0;

        }



        public void InsertExamReport()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO ExamReport (PassID, Position, Language, Score, Percentage, PassDate, ValidTill, PdfViewed, VideoViewed) 
                             VALUES (@PassID, @Position, @Language, @Score, @Percentage, @PassDate, @ValidTill, @PdfViewed, @VideoViewed)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // ✅ Replace with actual values from your application
                        cmd.Parameters.AddWithValue("@PassID", GlobelObject.GetPassID);
                        cmd.Parameters.AddWithValue("@Position", GlobelObject.SelectedPosition);
                        cmd.Parameters.AddWithValue("@Language", GlobelObject.SelectedLanguage);
                        cmd.Parameters.AddWithValue("@Score", GlobelObject.RightAnsw);
                        cmd.Parameters.AddWithValue("@Percentage", GlobelObject.Percentage);

                        // ✅ Store only DATE part
                        cmd.Parameters.AddWithValue("@PassDate", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@ValidTill", DateTime.Now.Date.AddMonths(6)); // 1 year validity

                        cmd.Parameters.AddWithValue("@PdfViewed", false); // Default false
                        cmd.Parameters.AddWithValue("@VideoViewed", false); // Default false

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }



        public void InsertQuestionAnswer()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // ✅ Always insert or update ExamResults
                    string upsertPassQuery = @"
                IF EXISTS (SELECT 1 FROM ExamResults WHERE PassID = @PassID)
                BEGIN
                    UPDATE ExamResults 
                    SET Language = @Language, Position = @Position, Score = @Score, 
                        Percentage = @Percentage, PassDate = @PassDate, ValidTill = @ValidTill
                    WHERE PassID = @PassID
                END
                ELSE
                BEGIN
                    INSERT INTO ExamResults (PassID, Language, Position, Score, Percentage, PassDate, ValidTill) 
                    VALUES (@PassID, @Language, @Position, @Score, @Percentage, @PassDate, @ValidTill)
                END";

                    using (SqlCommand upsertCmd = new SqlCommand(upsertPassQuery, con))
                    {
                        upsertCmd.Parameters.AddWithValue("@PassID", GlobelObject.GetPassID);
                        upsertCmd.Parameters.AddWithValue("@Language", GlobelObject.SelectedLanguage);
                        upsertCmd.Parameters.AddWithValue("@Position", GlobelObject.SelectedPosition);
                        upsertCmd.Parameters.AddWithValue("@Score", GlobelObject.RightAnsw);
                        upsertCmd.Parameters.AddWithValue("@Percentage", GlobelObject.Percentage);
                        upsertCmd.Parameters.AddWithValue("@PassDate", DateTime.Now.Date);
                        upsertCmd.Parameters.AddWithValue("@ValidTill", DateTime.Now.Date.AddMonths(6));

                        upsertCmd.ExecuteNonQuery();
                    }

                    // ✅ Now insert questions into ExamQuestions
                    string query = @"INSERT INTO ExamQuestions (PassID, QuestionText, UserAnswer, CorrectAnswer) 
                     VALUES (@PassID, @QuestionText, @UserAnswer, @CorrectAnswer)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int totalQuestions = GlobalPropertyModel.Question.Length;

                        for (int i = 0; i < totalQuestions; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@PassID", GlobelObject.GetPassID);
                            cmd.Parameters.AddWithValue("@QuestionText", GlobalPropertyModel.Question[i]);
                            cmd.Parameters.AddWithValue("@UserAnswer", GlobelObject.Q_Answers[i]);
                            cmd.Parameters.AddWithValue("@CorrectAnswer", GlobalPropertyModel.Answer[i]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }




        private void SaveExamSummary()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    INSERT INTO ExamReport (PassID, Position, Language, Score, Percentage, PassDate, ValidTill, PdfViewed, VideoViewed)
                    VALUES (@PassID, @Position, @Language, @Score, @Percentage, @PassDate, @ValidTill, @PdfViewed, @VideoViewed)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                       // int totalQuestions = GlobelObject.TotalQuestions > 0 ? GlobelObject.TotalQuestions : GetTotalQuestionsFromDB();
                       // int score = GlobelObject.UserAnswers.Count(a => a.Item2 == a.Item3);
                       // double percentage = (totalQuestions > 0) ? (score / (double)totalQuestions) * 100 : 0;

                        DateTime passDate = DateTime.Now;
                        DateTime validTill = passDate.AddMonths(6);

                        cmd.Parameters.AddWithValue("@PassID", Convert.ToInt32(GlobelObject.GetPassID));
                        cmd.Parameters.AddWithValue("@Position", GlobelObject.SelectedPosition);
                        cmd.Parameters.AddWithValue("@Language", GlobelObject.SelectedLanguage);
                        cmd.Parameters.AddWithValue("@Score", GlobelObject.RightAnsw);
                        cmd.Parameters.AddWithValue("@Percentage", GlobelObject.Percentage);
                        cmd.Parameters.AddWithValue("@PassDate", passDate);
                        cmd.Parameters.AddWithValue("@ValidTill", validTill); // Valid for 6 months);
                        cmd.Parameters.AddWithValue("@PdfViewed", GlobelObject.PDFsViewed > 0);
                        cmd.Parameters.AddWithValue("@VideoViewed", GlobelObject.VideosWatched > 0);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("✅ Exam Result Saved Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error saving exam result: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
