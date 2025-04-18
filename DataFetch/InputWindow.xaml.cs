using System.Windows;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZadanieRekrutacyjne.DataFetch
{
    /// <summary>
    /// Logika interakcji dla klasy InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string ConStringNoDb = "Server= " + ServerNameBox.Text + "; Integrated Security=True;Encrypt=False;";

            string ConString = "Server= " + ServerNameBox.Text + "; Database = RecruitmentTask; Integrated Security=True;Encrypt=False;";
            try
            {
                using (SqlConnection con = new SqlConnection(ConStringNoDb))

                {
                    con.Open();
                    using var Command = new SqlCommand("CREATE DATABASE RecruitmentTask", con);

                    Command.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch
            { 

            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConString))

                {
                    con.Open();
                    using var Command = new SqlCommand("CREATE TABLE WeatherRecords (Id INT IDENTITY PRIMARY KEY, Time DATETIME, Temperature2m FLOAT, IsDay BIT, WeatherType NVARCHAR(100));", con);

                    Command.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {

            }



            try
            {
                using (SqlConnection con = new SqlConnection(ConString))

                {
                    string CmdString = "SELECT * FROM WeatherRecords";

                    SqlCommand cmd = new SqlCommand(CmdString, con);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                }
                MainWindow window = new MainWindow(ConString);
                this.Close();
                window.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong, recheck Database and Server names, and check if server was configured properly");
            }
        }
    }
}
