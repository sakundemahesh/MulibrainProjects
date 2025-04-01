using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Windows.Input;

namespace Hindalco
{
    public partial class Login_Screen : Page
    {
        private string enteredPin = "";

        public Login_Screen()
        {
            InitializeComponent();
        }

        // Numeric Button Click Event (For On-Screen Keypad)
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (enteredPin.Length < 10)
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    enteredPin += btn.Content.ToString();
                    EnteredText.Text = enteredPin; // Update TextBox
                }
            }
        }

        // Delete Button Click Event
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (enteredPin.Length > 0)
            {
                enteredPin = enteredPin.Substring(0, enteredPin.Length - 1);
                EnteredText.Text = enteredPin; // Update TextBox
            }
        }

        // Handle Laptop Keyboard Input (Only Allow Numbers)
        private void EnteredText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) || enteredPin.Length >= 10)
            {
                e.Handled = true; // Block non-numeric input
            }
        }

        // Update enteredPin when user types from laptop keyboard
        private void EnteredText_TextChanged(object sender, TextChangedEventArgs e)
        {
            enteredPin = EnteredText.Text; // Sync with TextBox
        }



        // Login Button Click Event (Database Authentication)
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(enteredPin))
            {
                ErrorLabel.Text = "Please enter a PIN before logging in.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

                    // Check today's login count
                    string checkQuery = @"SELECT LoginCount FROM GatePassLogins 
                                  WHERE GetPassID = @pin AND LoginDate = @date";

                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@pin", enteredPin);
                    checkCmd.Parameters.AddWithValue("@date", todayDate);

                    object result = checkCmd.ExecuteScalar();
                    int loginCount = result != null ? Convert.ToInt32(result) : 0;

                    if (loginCount >= 2)
                    {
                        ErrorLabel.Text = "Login limit reached for today! Try again tomorrow.";
                        return;
                    }

                    // If first login of the day, insert a new record
                    if (loginCount == 0)
                    {
                        string insertQuery = @"INSERT INTO GatePassLogins (GetPassID, LoginDate, LoginCount)
                                       VALUES (@pin, @date, 1)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                        insertCmd.Parameters.AddWithValue("@pin", enteredPin);
                        insertCmd.Parameters.AddWithValue("@date", todayDate);
                        insertCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Second login - Update login count
                        string updateQuery = @"UPDATE GatePassLogins 
                                       SET LoginCount = LoginCount + 1 
                                       WHERE GetPassID = @pin AND LoginDate = @date";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                        updateCmd.Parameters.AddWithValue("@pin", enteredPin);
                        updateCmd.Parameters.AddWithValue("@date", todayDate);
                        updateCmd.ExecuteNonQuery();
                    }

                    // Successful login
                    ErrorLabel.Text = "Login Successful!";
                    NavigationService?.Navigate(new Language_Choose());
                    GlobelObject.GetPassID = EnteredText.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                EnteredText.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int count;
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                count++;
                if (count > 5)
                {
                    this.NavigationService.Navigate(new Admin_Panel());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
