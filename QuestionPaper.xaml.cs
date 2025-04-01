using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Configuration;
using System.Windows.Threading;

namespace Hindalco
{
    /// <summary>
    /// Interaction logic for QuestionPaper.xaml
    /// </summary>
    public partial class QuestionPaper : Page
    {

        string Question, A, B, C, D;
        public static bool[] bGreen = new bool[15];

        public QuestionPaper(string Question, string A, string B, string C, string D)
        {
            InitializeComponent();
            this.Question = Question;
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;


        }

        DispatcherTimer TimerForGreenCheck_Q = new DispatcherTimer();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bGreen[0] = false;
                bGreen[1] = false;
                bGreen[2] = false;
                bGreen[3] = false;
                bGreen[4] = false;
                bGreen[5] = false;
                bGreen[6] = false;
                bGreen[7] = false;
                bGreen[8] = false;
                bGreen[9] = false;
                bGreen[10] = false;
                bGreen[11] = false;
                bGreen[12] = false;
                bGreen[13] = false;
                bGreen[14] = false;

                GlobelObject.RightAnsw = 0;
                GlobelObject.wrongAnsw = 0;
                GlobelObject.Percentage = 0;
              

                Load();
                Btn_Q1_Click(btn_Q1, new RoutedEventArgs());
                TimerForGreenCheck_Q.Interval = new TimeSpan(0, 0, 0, 0, 1);
                TimerForGreenCheck_Q.Tick += TimerForGreenCheck_Tick1_Q;
                TimerForGreenCheck_Q.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TimerForGreenCheck_Tick1_Q(object sender, EventArgs e)
        {
            try
            {
                if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == true && bGreen[10] == true && bGreen[11] == true && bGreen[12] == true && bGreen[13] == true && bGreen[14] == true)
                {
                    TimerForGreenCheck_Q.Stop();
                    btn_Submit.Visibility = Visibility.Visible;
                    btn_Next.Visibility = Visibility.Hidden;
                }
                else
                {
                    TimerForGreenCheck_Q.Start();
                    btn_Submit.Visibility = Visibility.Hidden;
                    btn_Next.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            OptionA.IsChecked = false;
            OptionB.IsChecked = false;
            OptionC.IsChecked = false;
            OptionD.IsChecked = false;
            lbl_Ques1.Content = "";
            OptionA.Content = "";
            OptionB.Content = "";
            OptionC.Content = "";
            OptionD.Content = "";
        }

        private void Option(string Question, string A, string B, string C, string D)
        {

            this.Question = Question;
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
        }

        private void Btn_Q1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "1";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[0];
                OptionA.Content = GlobalPropertyModel.OptionA[0];
                OptionB.Content = GlobalPropertyModel.OptionB[0];
                OptionC.Content = GlobalPropertyModel.OptionC[0];
                OptionD.Content = GlobalPropertyModel.OptionD[0];

                Option(GlobalPropertyModel.Question[0], GlobalPropertyModel.OptionA[0], GlobalPropertyModel.OptionB[0], GlobalPropertyModel.OptionC[0], GlobalPropertyModel.OptionD[0]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q1.Background = Brushes.Green;
                }
                else
                {
                    bGreen[0] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q1.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "2";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[1];
                OptionA.Content = GlobalPropertyModel.OptionA[1];
                OptionB.Content = GlobalPropertyModel.OptionB[1];
                OptionC.Content = GlobalPropertyModel.OptionC[1];
                OptionD.Content = GlobalPropertyModel.OptionD[1];

                Option(GlobalPropertyModel.Question[1], GlobalPropertyModel.OptionA[1], GlobalPropertyModel.OptionB[1], GlobalPropertyModel.OptionC[1], GlobalPropertyModel.OptionD[1]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q2.Background = Brushes.Green;
                }
                else
                {
                    bGreen[1] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q2.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "3";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[2];
                OptionA.Content = GlobalPropertyModel.OptionA[2];
                OptionB.Content = GlobalPropertyModel.OptionB[2];
                OptionC.Content = GlobalPropertyModel.OptionC[2];
                OptionD.Content = GlobalPropertyModel.OptionD[2];

                Option(GlobalPropertyModel.Question[2], GlobalPropertyModel.OptionA[2], GlobalPropertyModel.OptionB[2], GlobalPropertyModel.OptionC[2], GlobalPropertyModel.OptionD[2]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q3.Background = Brushes.Green;
                }
                else
                {
                    bGreen[2] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q3.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "4";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[3];
                OptionA.Content = GlobalPropertyModel.OptionA[3];
                OptionB.Content = GlobalPropertyModel.OptionB[3];
                OptionC.Content = GlobalPropertyModel.OptionC[3];
                OptionD.Content = GlobalPropertyModel.OptionD[3];

                Option(GlobalPropertyModel.Question[3], GlobalPropertyModel.OptionA[3], GlobalPropertyModel.OptionB[3], GlobalPropertyModel.OptionC[3], GlobalPropertyModel.OptionD[3]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q4.Background = Brushes.Green;
                }
                else
                {
                    bGreen[3] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q4.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "5";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[4];
                OptionA.Content = GlobalPropertyModel.OptionA[4];
                OptionB.Content = GlobalPropertyModel.OptionB[4];
                OptionC.Content = GlobalPropertyModel.OptionC[4];
                OptionD.Content = GlobalPropertyModel.OptionD[4];

                Option(GlobalPropertyModel.Question[4], GlobalPropertyModel.OptionA[4], GlobalPropertyModel.OptionB[4], GlobalPropertyModel.OptionC[4], GlobalPropertyModel.OptionD[4]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q5.Background = Brushes.Green;
                }
                else
                {
                    bGreen[4] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q5.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "6";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[5];
                OptionA.Content = GlobalPropertyModel.OptionA[5];
                OptionB.Content = GlobalPropertyModel.OptionB[5];
                OptionC.Content = GlobalPropertyModel.OptionC[5];
                OptionD.Content = GlobalPropertyModel.OptionD[5];

                Option(GlobalPropertyModel.Question[5], GlobalPropertyModel.OptionA[5], GlobalPropertyModel.OptionB[5], GlobalPropertyModel.OptionC[5], GlobalPropertyModel.OptionD[5]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q6.Background = Brushes.Green;
                }
                else
                {
                    bGreen[5] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q6.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "7";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[6];
                OptionA.Content = GlobalPropertyModel.OptionA[6];
                OptionB.Content = GlobalPropertyModel.OptionB[6];
                OptionC.Content = GlobalPropertyModel.OptionC[6];
                OptionD.Content = GlobalPropertyModel.OptionD[6];

                Option(GlobalPropertyModel.Question[6], GlobalPropertyModel.OptionA[6], GlobalPropertyModel.OptionB[6], GlobalPropertyModel.OptionC[6], GlobalPropertyModel.OptionD[6]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q7.Background = Brushes.Green;
                }
                else
                {
                    bGreen[6] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q7.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "8";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[7];
                OptionA.Content = GlobalPropertyModel.OptionA[7];
                OptionB.Content = GlobalPropertyModel.OptionB[7];
                OptionC.Content = GlobalPropertyModel.OptionC[7];
                OptionD.Content = GlobalPropertyModel.OptionD[7];

                Option(GlobalPropertyModel.Question[7], GlobalPropertyModel.OptionA[7], GlobalPropertyModel.OptionB[7], GlobalPropertyModel.OptionC[7], GlobalPropertyModel.OptionD[7]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q8.Background = Brushes.Green;
                }
                else
                {
                    bGreen[7] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q8.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q9_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "9";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[8];
                OptionA.Content = GlobalPropertyModel.OptionA[8];
                OptionB.Content = GlobalPropertyModel.OptionB[8];
                OptionC.Content = GlobalPropertyModel.OptionC[8];
                OptionD.Content = GlobalPropertyModel.OptionD[8];

                Option(GlobalPropertyModel.Question[8], GlobalPropertyModel.OptionA[8], GlobalPropertyModel.OptionB[8], GlobalPropertyModel.OptionC[8], GlobalPropertyModel.OptionD[8]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q9.Background = Brushes.Green;
                }
                else
                {
                    bGreen[8] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q9.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Q10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "10";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[9];
                OptionA.Content = GlobalPropertyModel.OptionA[9];
                OptionB.Content = GlobalPropertyModel.OptionB[9];
                OptionC.Content = GlobalPropertyModel.OptionC[9];
                OptionD.Content = GlobalPropertyModel.OptionD[9];

                Option(GlobalPropertyModel.Question[9], GlobalPropertyModel.OptionA[9], GlobalPropertyModel.OptionB[9], GlobalPropertyModel.OptionC[9], GlobalPropertyModel.OptionD[9]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q10.Background = Brushes.Green;
                }
                else
                {
                    bGreen[9] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q10.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Q11_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "11";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[10];
                OptionA.Content = GlobalPropertyModel.OptionA[10];
                OptionB.Content = GlobalPropertyModel.OptionB[10];
                OptionC.Content = GlobalPropertyModel.OptionC[10];
                OptionD.Content = GlobalPropertyModel.OptionD[10];

                Option(GlobalPropertyModel.Question[10], GlobalPropertyModel.OptionA[10], GlobalPropertyModel.OptionB[10], GlobalPropertyModel.OptionC[10], GlobalPropertyModel.OptionD[10]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q11.Background = Brushes.Green;
                }
                else
                {
                    bGreen[10] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q11.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Q12_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "12";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[11];
                OptionA.Content = GlobalPropertyModel.OptionA[11];
                OptionB.Content = GlobalPropertyModel.OptionB[11];
                OptionC.Content = GlobalPropertyModel.OptionC[11];
                OptionD.Content = GlobalPropertyModel.OptionD[11];

                Option(GlobalPropertyModel.Question[11], GlobalPropertyModel.OptionA[11], GlobalPropertyModel.OptionB[11], GlobalPropertyModel.OptionC[11], GlobalPropertyModel.OptionD[11]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q12.Background = Brushes.Green;
                }
                else
                {
                    bGreen[11] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q12.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Q13_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "13";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[12];
                OptionA.Content = GlobalPropertyModel.OptionA[12];
                OptionB.Content = GlobalPropertyModel.OptionB[12];
                OptionC.Content = GlobalPropertyModel.OptionC[12];
                OptionD.Content = GlobalPropertyModel.OptionD[12];

                Option(GlobalPropertyModel.Question[12], GlobalPropertyModel.OptionA[12], GlobalPropertyModel.OptionB[12], GlobalPropertyModel.OptionC[12], GlobalPropertyModel.OptionD[12]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q13.Background = Brushes.Green;
                }
                else
                {
                    bGreen[12] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q13.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Q14_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "14";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[13];
                OptionA.Content = GlobalPropertyModel.OptionA[13];
                OptionB.Content = GlobalPropertyModel.OptionB[13];
                OptionC.Content = GlobalPropertyModel.OptionC[13];
                OptionD.Content = GlobalPropertyModel.OptionD[13];

                Option(GlobalPropertyModel.Question[13], GlobalPropertyModel.OptionA[13], GlobalPropertyModel.OptionB[13], GlobalPropertyModel.OptionC[13], GlobalPropertyModel.OptionD[13]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q14.Background = Brushes.Green;
                }
                else
                {
                    bGreen[13] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q14.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Q15_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobelObject.SelectQuestion = "15";
                clear();

                lbl_Ques1.Content = GlobalPropertyModel.Question[14];
                OptionA.Content = GlobalPropertyModel.OptionA[14];
                OptionB.Content = GlobalPropertyModel.OptionB[14];
                OptionC.Content = GlobalPropertyModel.OptionC[14];
                OptionD.Content = GlobalPropertyModel.OptionD[14];

                Option(GlobalPropertyModel.Question[14], GlobalPropertyModel.OptionA[14], GlobalPropertyModel.OptionB[14], GlobalPropertyModel.OptionC[14], GlobalPropertyModel.OptionD[14]);

                if (OptionA.IsChecked == true || OptionB.IsChecked == true || OptionC.IsChecked == true || OptionD.IsChecked == true)
                {
                    btn_Q15.Background = Brushes.Green;
                }
                else
                {
                    bGreen[14] = false;
                    TimerForGreenCheck_Q.Start();
                    btn_Q15.Background = Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Exam_Result(Convert.ToInt32(GlobelObject.GetPassID)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LblError.Content = "";

                if (bGreen[0] == true && bGreen[1] == false && bGreen[2] == false && bGreen[3] == false && bGreen[4] == false && bGreen[5] == false && bGreen[6] == false && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    // Call the button click handler directly
                     btn_Q2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == false && bGreen[3] == false && bGreen[4] == false && bGreen[5] == false && bGreen[6] == false && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == false && bGreen[4] == false && bGreen[5] == false && bGreen[6] == false && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == false && bGreen[5] == false && bGreen[6] == false && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == false && bGreen[6] == false && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == false && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == false && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == false && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == false && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q10.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == true && bGreen[10] == false && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q11.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == true && bGreen[10] == true && bGreen[11] == false && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q12.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == true && bGreen[10] == true && bGreen[11] == true && bGreen[12] == false && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q13.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == true && bGreen[10] == true && bGreen[11] == true && bGreen[12] == true && bGreen[13] == false && bGreen[14] == false)
                {
                    btn_Q14.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else if (bGreen[0] == true && bGreen[1] == true && bGreen[2] == true && bGreen[3] == true && bGreen[4] == true && bGreen[5] == true && bGreen[6] == true && bGreen[7] == true && bGreen[8] == true && bGreen[9] == true && bGreen[10] == true && bGreen[11] == true && bGreen[12] == true && bGreen[13] == true && bGreen[14] == false)
                {
                    btn_Q15.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OptionA_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GlobelObject.SelectQuestion == "1")
                {
                    GlobelObject.Q_Answers[0] = this.A;
                    btn_Q1.Background = Brushes.Green;
                    bGreen[0] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[0] + " -- Q no. 1");
                }
                else if (GlobelObject.SelectQuestion == "2")
                {
                    GlobelObject.Q_Answers[1] = this.A;
                    //btn_Q2.IsEnabled = false;
                    btn_Q2.Background = Brushes.Green;
                    bGreen[1] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[1] + " -- Q no. 2");
                }
                else if (GlobelObject.SelectQuestion == "3")
                {
                    GlobelObject.Q_Answers[2] = this.A;
                    btn_Q3.Background = Brushes.Green;
                    bGreen[2] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[2] + " -- Q no. 3");
                }
                else if (GlobelObject.SelectQuestion == "4")
                {
                    GlobelObject.Q_Answers[3] = this.A;
                    btn_Q4.Background = Brushes.Green;
                    bGreen[3] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[3] + "- Q no. 4");
                }
                else if (GlobelObject.SelectQuestion == "5")
                {
                    GlobelObject.Q_Answers[4] = this.A;
                    btn_Q5.Background = Brushes.Green;
                    bGreen[4] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[4] + "- Q no. 5");
                }
                else if (GlobelObject.SelectQuestion == "6")
                {
                    GlobelObject.Q_Answers[5] = this.A;
                    btn_Q6.Background = Brushes.Green;
                    bGreen[5] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[5] + "- Q no. 6");
                }
                else if (GlobelObject.SelectQuestion == "7")
                {
                    GlobelObject.Q_Answers[6] = this.A;
                    btn_Q7.Background = Brushes.Green;
                    bGreen[6] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[6] + "- Q no. 7");
                }
                else if (GlobelObject.SelectQuestion == "8")
                {
                    GlobelObject.Q_Answers[7] = this.A;
                    btn_Q8.Background = Brushes.Green;
                    bGreen[7] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[7] + "- Q no. 8");
                }
                else if (GlobelObject.SelectQuestion == "9")
                {
                    GlobelObject.Q_Answers[8] = this.A;
                    btn_Q9.Background = Brushes.Green;
                    bGreen[8] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[8] + "- Q no. 9");
                }
                else if (GlobelObject.SelectQuestion == "10")
                {
                    GlobelObject.Q_Answers[9] = this.A;
                    btn_Q10.Background = Brushes.Green;
                    bGreen[9] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[9] + "- Q no. 10");
                }
                else if (GlobelObject.SelectQuestion == "11")
                {
                    GlobelObject.Q_Answers[10] = this.A;
                    btn_Q11.Background = Brushes.Green;
                    btn_Q11.Background = Brushes.Green;
                    bGreen[10] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[10] + "- Q no. 11");
                }
                else if (GlobelObject.SelectQuestion == "12")
                {
                    GlobelObject.Q_Answers[11] = this.A;
                    btn_Q12.Background = Brushes.Green;
                    bGreen[11] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[11] + "- Q no. 12");
                }
                else if (GlobelObject.SelectQuestion == "13")
                {
                    GlobelObject.Q_Answers[12] = this.A;
                    btn_Q13.Background = Brushes.Green;
                    bGreen[12] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[12] + "- Q no. 13");
                }
                else if (GlobelObject.SelectQuestion == "14")
                {
                    GlobelObject.Q_Answers[13] = this.A;
                    btn_Q14.Background = Brushes.Green;
                    bGreen[13] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[13] + "- Q no. 14");
                }
                else if (GlobelObject.SelectQuestion == "15")
                {
                    GlobelObject.Q_Answers[14] = this.A;
                    btn_Q15.Background = Brushes.Green;
                    bGreen[14] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[14] + "- Q no. 15");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OptionB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GlobelObject.SelectQuestion == "1")
                {
                    GlobelObject.Q_Answers[0] = this.B;
                    btn_Q1.Background = Brushes.Green;
                    bGreen[0] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[0] + " -- Q no. 1");
                }
                else if (GlobelObject.SelectQuestion == "2")
                {
                    GlobelObject.Q_Answers[1] = this.B;
                    //btn_Q2.IsEnabled = false;
                    btn_Q2.Background = Brushes.Green;
                    bGreen[1] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[1] + " -- Q no. 2");
                }
                else if (GlobelObject.SelectQuestion == "3")
                {
                    GlobelObject.Q_Answers[2] = this.B;
                    btn_Q3.Background = Brushes.Green;
                    bGreen[2] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[2] + " -- Q no. 3");
                }
                else if (GlobelObject.SelectQuestion == "4")
                {
                    GlobelObject.Q_Answers[3] = this.B;
                    btn_Q4.Background = Brushes.Green;
                    bGreen[3] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[3] + "- Q no. 4");
                }
                else if (GlobelObject.SelectQuestion == "5")
                {
                    GlobelObject.Q_Answers[4] = this.B;
                    btn_Q5.Background = Brushes.Green;
                    bGreen[4] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[4] + "- Q no. 5");
                }
                else if (GlobelObject.SelectQuestion == "6")
                {
                    GlobelObject.Q_Answers[5] = this.B;
                    btn_Q6.Background = Brushes.Green;
                    bGreen[5] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[5] + "- Q no. 6");
                }
                else if (GlobelObject.SelectQuestion == "7")
                {
                    GlobelObject.Q_Answers[6] = this.B;
                    btn_Q7.Background = Brushes.Green;
                    bGreen[6] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[6] + "- Q no. 7");
                }
                else if (GlobelObject.SelectQuestion == "8")
                {
                    GlobelObject.Q_Answers[7] = this.B;
                    btn_Q8.Background = Brushes.Green;
                    bGreen[7] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[7] + "- Q no. 8");
                }
                else if (GlobelObject.SelectQuestion == "9")
                {
                    GlobelObject.Q_Answers[8] = this.B;
                    btn_Q9.Background = Brushes.Green;
                    bGreen[8] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[8] + "- Q no. 9");
                }
                else if (GlobelObject.SelectQuestion == "10")
                {
                    GlobelObject.Q_Answers[9] = this.B;
                    btn_Q10.Background = Brushes.Green;
                    bGreen[9] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[9] + "- Q no. 10");
                }
                else if (GlobelObject.SelectQuestion == "11")
                {
                    GlobelObject.Q_Answers[10] = this.B;
                    btn_Q11.Background = Brushes.Green;
                    btn_Q11.Background = Brushes.Green;
                    bGreen[10] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[10] + "- Q no. 11");
                }
                else if (GlobelObject.SelectQuestion == "12")
                {
                    GlobelObject.Q_Answers[11] = this.A;
                    btn_Q12.Background = Brushes.Green;
                    bGreen[11] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[11] + "- Q no. 12");
                }
                else if (GlobelObject.SelectQuestion == "13")
                {
                    GlobelObject.Q_Answers[12] = this.B;
                    btn_Q13.Background = Brushes.Green;
                    bGreen[12] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[12] + "- Q no. 13");
                }
                else if (GlobelObject.SelectQuestion == "14")
                {
                    GlobelObject.Q_Answers[13] = this.B;
                    btn_Q14.Background = Brushes.Green;
                    bGreen[13] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[13] + "- Q no. 14");
                }
                else if (GlobelObject.SelectQuestion == "15")
                {
                    GlobelObject.Q_Answers[14] = this.B;
                    btn_Q15.Background = Brushes.Green;
                    bGreen[14] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[14] + "- Q no. 15");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OptionC_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GlobelObject.SelectQuestion == "1")
                {
                    GlobelObject.Q_Answers[0] = this.C;
                    btn_Q1.Background = Brushes.Green;
                    bGreen[0] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[0] + " -- Q no. 1");
                }
                else if (GlobelObject.SelectQuestion == "2")
                {
                    GlobelObject.Q_Answers[1] = this.C;
                    //btn_Q2.IsEnabled = false;
                    btn_Q2.Background = Brushes.Green;
                    bGreen[1] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[1] + " -- Q no. 2");
                }
                else if (GlobelObject.SelectQuestion == "3")
                {
                    GlobelObject.Q_Answers[2] = this.C;
                    btn_Q3.Background = Brushes.Green;
                    bGreen[2] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[2] + " -- Q no. 3");
                }
                else if (GlobelObject.SelectQuestion == "4")
                {
                    GlobelObject.Q_Answers[3] = this.C;
                    btn_Q4.Background = Brushes.Green;
                    bGreen[3] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[3] + "- Q no. 4");
                }
                else if (GlobelObject.SelectQuestion == "5")
                {
                    GlobelObject.Q_Answers[4] = this.C;
                    btn_Q5.Background = Brushes.Green;
                    bGreen[4] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[4] + "- Q no. 5");
                }
                else if (GlobelObject.SelectQuestion == "6")
                {
                    GlobelObject.Q_Answers[5] = this.C;
                    btn_Q6.Background = Brushes.Green;
                    bGreen[5] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[5] + "- Q no. 6");
                }
                else if (GlobelObject.SelectQuestion == "7")
                {
                    GlobelObject.Q_Answers[6] = this.C;
                    btn_Q7.Background = Brushes.Green;
                    bGreen[6] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[6] + "- Q no. 7");
                }
                else if (GlobelObject.SelectQuestion == "8")
                {
                    GlobelObject.Q_Answers[7] = this.C;
                    btn_Q8.Background = Brushes.Green;
                    bGreen[7] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[7] + "- Q no. 8");
                }
                else if (GlobelObject.SelectQuestion == "9")
                {
                    GlobelObject.Q_Answers[8] = this.C;
                    btn_Q9.Background = Brushes.Green;
                    bGreen[8] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[8] + "- Q no. 9");
                }
                else if (GlobelObject.SelectQuestion == "10")
                {
                    GlobelObject.Q_Answers[9] = this.C;
                    btn_Q10.Background = Brushes.Green;
                    bGreen[9] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[9] + "- Q no. 10");
                }
                else if (GlobelObject.SelectQuestion == "11")
                {
                    GlobelObject.Q_Answers[10] = this.C;
                    btn_Q11.Background = Brushes.Green;
                    bGreen[10] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[10] + "- Q no. 11");
                }
                else if (GlobelObject.SelectQuestion == "12")
                {
                    GlobelObject.Q_Answers[11] = this.C;
                    btn_Q12.Background = Brushes.Green;
                    bGreen[11] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[11] + "- Q no. 12");
                }
                else if (GlobelObject.SelectQuestion == "13")
                {
                    GlobelObject.Q_Answers[12] = this.C;
                    btn_Q13.Background = Brushes.Green;
                    bGreen[12] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[12] + "- Q no. 13");
                }
                else if (GlobelObject.SelectQuestion == "14")
                {
                    GlobelObject.Q_Answers[13] = this.C;
                    btn_Q14.Background = Brushes.Green;
                    bGreen[13] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[13] + "- Q no. 14");
                }
                else if (GlobelObject.SelectQuestion == "15")
                {
                    GlobelObject.Q_Answers[14] = this.C;
                    btn_Q15.Background = Brushes.Green;
                    bGreen[14] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[14] + "- Q no. 15");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OptionD_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GlobelObject.SelectQuestion == "1")
                {
                    GlobelObject.Q_Answers[0] = this.D;
                    btn_Q1.Background = Brushes.Green;
                    bGreen[0] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[0] + " -- Q no. 1");
                }
                else if (GlobelObject.SelectQuestion == "2")
                {
                    GlobelObject.Q_Answers[1] = this.D;
                    //btn_Q2.IsEnabled = false;
                    btn_Q2.Background = Brushes.Green;
                    bGreen[1] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[1] + " -- Q no. 2");
                }
                else if (GlobelObject.SelectQuestion == "3")
                {
                    GlobelObject.Q_Answers[2] = this.D;
                    btn_Q3.Background = Brushes.Green;
                    bGreen[2] = true;
                    DataLog.WriteDataLog("Q1 : Q1: User Click On : " + GlobelObject.Q_Answers[2] + " -- Q no. 3");
                }
                else if (GlobelObject.SelectQuestion == "4")
                {
                    GlobelObject.Q_Answers[3] = this.D;
                    btn_Q4.Background = Brushes.Green;
                    bGreen[3] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[3] + "- Q no. 4");
                }
                else if (GlobelObject.SelectQuestion == "5")
                {
                    GlobelObject.Q_Answers[4] = this.D;
                    btn_Q5.Background = Brushes.Green;
                    bGreen[4] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[4] + "- Q no. 5");
                }
                else if (GlobelObject.SelectQuestion == "6")
                {
                    GlobelObject.Q_Answers[5] = this.D;
                    btn_Q6.Background = Brushes.Green;
                    bGreen[5] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[5] + "- Q no. 6");
                }
                else if (GlobelObject.SelectQuestion == "7")
                {
                    GlobelObject.Q_Answers[6] = this.D;
                    btn_Q7.Background = Brushes.Green;
                    bGreen[6] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[6] + "- Q no. 7");
                }
                else if (GlobelObject.SelectQuestion == "8")
                {
                    GlobelObject.Q_Answers[7] = this.D;
                    btn_Q8.Background = Brushes.Green;
                    bGreen[7] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[7] + "- Q no. 8");
                }
                else if (GlobelObject.SelectQuestion == "9")
                {
                    GlobelObject.Q_Answers[8] = this.D;
                    btn_Q9.Background = Brushes.Green;
                    bGreen[8] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[8] + "- Q no. 9");
                }
                else if (GlobelObject.SelectQuestion == "10")
                {
                    GlobelObject.Q_Answers[9] = this.D;
                    btn_Q10.Background = Brushes.Green;
                    bGreen[9] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[9] + "- Q no. 10");
                }
                else if (GlobelObject.SelectQuestion == "11")
                {
                    GlobelObject.Q_Answers[10] = this.D;
                    btn_Q11.Background = Brushes.Green;
                    bGreen[10] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[10] + "- Q no. 11");
                }
                else if (GlobelObject.SelectQuestion == "12")
                {
                    GlobelObject.Q_Answers[11] = this.D;
                    btn_Q12.Background = Brushes.Green;
                    bGreen[11] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[11] + "- Q no. 12");
                }
                else if (GlobelObject.SelectQuestion == "13")
                {
                    GlobelObject.Q_Answers[12] = this.D;
                    btn_Q13.Background = Brushes.Green;
                    bGreen[12] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[12] + "- Q no. 13");
                }
                else if (GlobelObject.SelectQuestion == "14")
                {
                    GlobelObject.Q_Answers[13] = this.D;
                    btn_Q14.Background = Brushes.Green;
                    bGreen[13] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[13] + "- Q no. 14");
                }
                else if (GlobelObject.SelectQuestion == "15")
                {
                    GlobelObject.Q_Answers[14] = this.D;
                    btn_Q15.Background = Brushes.Green;
                    bGreen[14] = true;
                    DataLog.WriteDataLog("Q1: User Click On : " + GlobelObject.Q_Answers[14] + "- Q no. 15");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Load()
        {
            try
            {
                string MyConString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;

                SqlDataAdapter da = new SqlDataAdapter();

                da = new SqlDataAdapter("SELECT * from ExamQuestionsPaper where Language = '" + GlobelObject.SelectedLanguage + " '", MyConString);/**/
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GlobalPropertyModel.Question[i]  = ds.Tables[0].Rows[i]["QuestionText"].ToString();
                        GlobalPropertyModel.OptionA[i]  = ds.Tables[0].Rows[i]["OptionA"].ToString();
                        GlobalPropertyModel.OptionB[i]  = ds.Tables[0].Rows[i]["OptionB"].ToString();
                        GlobalPropertyModel.OptionC[i]  = ds.Tables[0].Rows[i]["OptionC"].ToString();
                        GlobalPropertyModel.OptionD[i]  = ds.Tables[0].Rows[i]["OptionD"].ToString();
                        GlobalPropertyModel.Answer[i]   = ds.Tables[0].Rows[i]["CorrectAnswer"].ToString();
                    }
                }
                btn_Q1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
