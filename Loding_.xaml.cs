using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Hindalco
{
    public partial class Loding_ : Page
    {
        private double _progressValue = 0;
        private DispatcherTimer _timer;

        public Loding_()
        {
            InitializeComponent();
            StartLoadingAnimation();
        }

        private void StartLoadingAnimation()
        {
            // Smooth Rotation Animation
            DoubleAnimation rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(2)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            RotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

            // Start Percentage Update Timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += UpdatePercentage;
            _timer.Start();
        }

        private void UpdatePercentage(object sender, EventArgs e)
        {
            _progressValue += 1;
            PercentageText.Text = $"{_progressValue}%";

            if (_progressValue >= 100)
            {
                _timer.Stop(); // Stop the timer when progress reaches 100%
                NavigateToLoginScreen();
            }
        }

        private void NavigateToLoginScreen()
        {
            // Navigate to Login_Screen.xaml
            NavigationService?.Navigate(new Uri("Login_Screen.xaml", UriKind.Relative));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
