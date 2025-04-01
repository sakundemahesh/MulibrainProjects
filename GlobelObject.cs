using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hindalco
{
    internal class GlobelObject
    {
        public static string GetPassID = "";
        public static string SelectedLanguage { get; set; }
        public static string SelectedPosition { get; set; }
        public static int   VideosWatched { get; set; } = 0;  
        public static int PDFsViewed { get; set; } = 0;      
        public static int TotalScore { get; set; } = 0;  
        public static int TotalQuestions { get; set; } = 0;
        public static double Percentage { get; set; } = 0.0;

        public static List<Tuple<string, string, string>> UserAnswers { get; set; } = new List<Tuple<string, string, string>>();

        public static string[] Q_Answers = new string[15];
        public static string[] Q_Question = new string[15];
        public static string[] Q_OptionA = new string[4];
        public static string[] Q_OptionB = new string[4];
        public static string[] Q_OptionC = new string[4];
        public static string[] Q_OptionD = new string[4];
        public static double wrongAnsw = 0;
        public static double RightAnsw = 0;


        public static string SelectQuestion = "";
        public static void ResetExamData()
        {
            TotalScore = 0;
            TotalQuestions = 0;
            Percentage = 0.0;
            UserAnswers.Clear();
        }
    }
}
