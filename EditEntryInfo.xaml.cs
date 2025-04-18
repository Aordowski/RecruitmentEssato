using Microsoft.Data.SqlClient;
using System.Windows;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Logika interakcji dla klasy EditEntryInfo.xaml
    /// </summary>
    public partial class EditEntryInfo : Window
    {
        string ConnectionString;
        int editedId = -1;
        MainWindow mainWindow;
        public EditEntryInfo(int id, string connectionString,string dateTime,float temperature,bool? isDay,string Weather,MainWindow window)
        {
            mainWindow = window;
            editedId = id;
            ConnectionString = connectionString;
            InitializeComponent();
            TemperatureEdit.Text = temperature.ToString();
            DateTimeEdit.Text = dateTime;
            IsDayEdit.IsChecked = isDay;
            WeatherEdit.Text = Weather;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Edit
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    DateTime Time = DateTime.Parse(DateTimeEdit.Text, null);
                    if (IsDayEdit.IsChecked == null)
                    {
                        using var Command = new SqlCommand("UPDATE WeatherRecords SET Time=@Time, Temperature2m=@Temperature2m, IsDay=@IsDay, WeatherType=@WeatherType WHERE Id=@Id", connection);
                        Command.Parameters.AddWithValue("@Time", Time);
                        Command.Parameters.AddWithValue("@Temperature2m", float.Parse(TemperatureEdit.Text));
                        Command.Parameters.AddWithValue("@IsDay", DBNull.Value);
                        Command.Parameters.AddWithValue("@WeatherType", WeatherEdit.Text);
                        Command.Parameters.AddWithValue("@Id", editedId);
                        Command.ExecuteNonQuery();
                    }
                    else
                    {
                        using var Command = new SqlCommand("UPDATE WeatherRecords SET Time=@Time, Temperature2m=@Temperature2m, IsDay=@IsDay, WeatherType=@WeatherType WHERE Id=@Id", connection);
                        Command.Parameters.AddWithValue("@Time", Time);
                        Command.Parameters.AddWithValue("@Temperature2m", float.Parse(TemperatureEdit.Text));
                        Command.Parameters.AddWithValue("@IsDay", IsDayEdit.IsChecked);
                        Command.Parameters.AddWithValue("@WeatherType", WeatherEdit.Text);
                        Command.Parameters.AddWithValue("@Id", editedId);
                        Command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Cancel
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.FillDataGrid(mainWindow.SelectPageBox.SelectedIndex);
        }
    }
}
