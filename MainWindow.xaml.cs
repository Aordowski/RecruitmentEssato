using System.Windows;
using System.Windows.Controls;
using ZadanieRekrutacyjne.DataFetch;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeatherInfo weatherInfoApi;
        int amountOfPages = 1;
        DataGridTemplateColumn buttonColumnDelete;
        DataGridTemplateColumn buttonColumnEdit;
        int recordsOnPage = 35;
        EditEntryInfo editEntryInfo;
        WeatherService service;
      private string ConnectionString;
        public MainWindow(string connectionString)
        {
            ConnectionString = connectionString;
            weatherInfoApi = new WeatherInfo();
            service = new WeatherService();
            InitializeComponent();
            redoPaginationAllRecords();
            SelectPageBox.SelectedIndex = 0;
            FillDataGrid(SelectPageBox.SelectedIndex);
            SearchWeatherTypeBox.Items.Add("Any");
            SearchWeatherTypeBox.Items.Add("Clear sky");
            SearchWeatherTypeBox.Items.Add("Mainly clear, partly cloudy, and overcast");
            SearchWeatherTypeBox.Items.Add("Fog and depositing rime fog");
            SearchWeatherTypeBox.Items.Add("Drizzle: Light, moderate, and dense intensity");
            SearchWeatherTypeBox.Items.Add("Freezing Drizzle: Light and dense intensity");
            SearchWeatherTypeBox.Items.Add("Rain: Slight, moderate and heavy intensity");
            SearchWeatherTypeBox.Items.Add("Freezing Rain: Light and heavy intensity");
            SearchWeatherTypeBox.Items.Add("Snow fall: Slight, moderate, and heavy intensity");
            SearchWeatherTypeBox.Items.Add("Snow grains");
            SearchWeatherTypeBox.Items.Add("Rain showers: Slight, moderate, and violent");
            SearchWeatherTypeBox.Items.Add("Snow showers slight and heavy");
            SearchWeatherTypeBox.Items.Add("Thunderstorm: Slight or moderate");
            SearchWeatherTypeBox.Items.Add("Thunderstorm with slight and heavy hail");
            SearchWeatherTypeBox.SelectedIndex = 0;
        }

        private async void Button_Click(object sender, RoutedEventArgs e) //3DayHourly
        {
            var weatherInfoApi = await service.RunGetData3DayAsync();
            List<WeatherRecord> Records = new List<WeatherRecord>();
            for (int i = 0; i < weatherInfoApi.Hourly.Time.Count - 1; i++)
            {
                Records.Add(new WeatherRecord(weatherInfoApi.Hourly.Time[i], weatherInfoApi.Hourly.Temperature2m[i], weatherInfoApi.Hourly.WeatherCode[i]));

            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (WeatherRecord record in Records)
                {

                    using var Command = new SqlCommand("INSERT INTO WeatherRecords (Time, Temperature2m, IsDay, WeatherType) VALUES (@Time, @Temperature2m, @IsDay, @WeatherType)", connection);
                    Command.Parameters.AddWithValue("@Time", record.Time);
                    Command.Parameters.AddWithValue("@Temperature2m", record.Temperature2m);
                    Command.Parameters.AddWithValue("@IsDay", DBNull.Value); //Hourly prognosis does not provide IsDay
                    Command.Parameters.AddWithValue("@WeatherType", record.WeatherType);
                    Command.ExecuteNonQuery();

                }

                connection.Close();
            }
            redoPaginationAllRecords();
        }

        public void FillDataGrid(int selectedIndex)
        {
            string CmdString = string.Empty;
            DataTable dt = new DataTable("WeatherRecords");

            using (SqlConnection con = new SqlConnection(ConnectionString))

            {
                if (selectedIndex == -1)
                {
                    selectedIndex = 0;
                }
                CmdString = "SELECT  Id, Time, Temperature2m, IsDay, WeatherType FROM WeatherRecords ORDER BY Id OFFSET " + recordsOnPage * selectedIndex + " ROWS FETCH NEXT " + recordsOnPage + " ROWS ONLY"; //that was easier, and you cant really inject anything there

                SqlCommand cmd = new SqlCommand(CmdString, con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);

            }

            if (buttonColumnDelete != null)
            {
                DataGridView.Columns.Remove(buttonColumnDelete);
            }

            if (buttonColumnEdit != null)
            {
                DataGridView.Columns.Remove(buttonColumnEdit);
            }
            var buttonFactoryDelete = new FrameworkElementFactory(typeof(Button));
            buttonFactoryDelete.SetValue(Button.ContentProperty, "Delete Record");
            buttonFactoryDelete.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
            {
                var ButtonClicked = sender as Button;
                var rowData = ButtonClicked.DataContext;

                if (rowData is DataRowView dataRow)
                {
                    int id = Convert.ToInt32(dataRow["id"]);
                    deleteRecord(id);

                }
            }));

            var buttonDeleteTemplate = new DataTemplate();
            buttonDeleteTemplate.VisualTree = buttonFactoryDelete;

            buttonColumnDelete = new DataGridTemplateColumn
            {
                Header = "Delete",
                CellTemplate = buttonDeleteTemplate
            };

            var buttonFactoryEdit = new FrameworkElementFactory(typeof(Button));
            buttonFactoryEdit.SetValue(Button.ContentProperty, "Edit Record");
            buttonFactoryEdit.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
            {
                var ButtonClicked = sender as Button;
                var rowData = ButtonClicked.DataContext;

                if (rowData is DataRowView dataRow)
                {
                    int id = Convert.ToInt32(dataRow["id"]);
                    string dateTime = dataRow["Time"].ToString();
                    string WeatherType = dataRow["WeatherType"].ToString();
                    float temperature = float.Parse(dataRow["Temperature2m"].ToString());
                    bool? isDay = null;
                    if (bool.TryParse(dataRow["IsDay"]?.ToString(), out bool parsed))
                        isDay = parsed;
                    editEntryInfo = new EditEntryInfo(id, ConnectionString, dateTime, temperature, isDay, WeatherType, this);
                    editEntryInfo.ShowDialog();

                }
            }));

            var buttonEditTemplate = new DataTemplate();
            buttonEditTemplate.VisualTree = buttonFactoryEdit;

            buttonColumnEdit = new DataGridTemplateColumn
            {
                Header = "Edit",
                CellTemplate = buttonEditTemplate
            };


            DataGridView.Columns.Add(buttonColumnDelete);
            DataGridView.Columns.Add(buttonColumnEdit);

            DataGridView.ItemsSource = dt.DefaultView;

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) //Current weather
        {

            var weatherInfoApi = await service.RunGetDataAsync();


            WeatherRecord Record = new WeatherRecord(weatherInfoApi.Current.Time, weatherInfoApi.Current.Temperature2m, weatherInfoApi.Current.IsDay, weatherInfoApi.Current.WeatherCode);
            InsertWeatherRecord(Record);
            redoPaginationAllRecords();
            FillDataGrid(SelectPageBox.SelectedIndex);

        }

        public void InsertWeatherRecord(WeatherRecord record)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using var Command = new SqlCommand("INSERT INTO WeatherRecords (Time, Temperature2m, IsDay, WeatherType) VALUES (@Time, @Temperature2m, @IsDay, @WeatherType)", connection);
                Command.Parameters.AddWithValue("@Time", record.Time);
                Command.Parameters.AddWithValue("@Temperature2m", record.Temperature2m);
                Command.Parameters.AddWithValue("@IsDay", record.IsDay);
                Command.Parameters.AddWithValue("@WeatherType", record.WeatherType);
                connection.Open();
                Command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private int getAllRecordsCount()
        {
            int recordCount;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using var Command = new SqlCommand("SELECT COUNT(*) FROM WeatherRecords;", connection);
                recordCount = (int)Command.ExecuteScalar();
                connection.Close();
            }

            return recordCount;
        }

        public void deleteRecord(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using var Command = new SqlCommand($"DELETE FROM WeatherRecords WHERE Id='{Id}'", connection);
                Command.ExecuteNonQuery();
                connection.Close();
            }
            redoPaginationAllRecords();
            FillDataGrid(SelectPageBox.SelectedIndex);

        }

        private void SelectPageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDataGrid(SelectPageBox.SelectedIndex);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //List all
        {
            ListAllEntriesWindow AllEntriesWindow = new ListAllEntriesWindow(ConnectionString);
            AllEntriesWindow.Show();
        }


        private void redoPaginationAllRecords()
        {
            int amountOfRecords = getAllRecordsCount();
            int amountOfPagesNew;
            if (amountOfRecords % recordsOnPage == 0)
            {
                amountOfPagesNew = amountOfRecords / recordsOnPage;
            }
            else
            {
                amountOfPagesNew = amountOfRecords / recordsOnPage + 1;
            }
            if (amountOfPagesNew != amountOfPages)
            {
                SelectPageBox.Items.Clear();
                for (int i = 1; i < amountOfPagesNew + 1; i++)
                {
                    SelectPageBox.Items.Add("Page " + i.ToString());
                }
                if (SelectPageBox.SelectedIndex == amountOfPagesNew)
                {
                    SelectPageBox.SelectedIndex = SelectPageBox.SelectedIndex - 1;
                }

            }
            amountOfPages = amountOfPagesNew;
        }


       /* private void redoPaginationRecords(int amountOfRecords)
        {

            int amountOfPagesNew;
            if (amountOfRecords % recordsOnPage == 0)
            {
                amountOfPagesNew = amountOfRecords / recordsOnPage;
            }
            else
            {
                amountOfPagesNew = amountOfRecords / recordsOnPage + 1;
            }
            if (amountOfPagesNew != amountOfPages)
            {
                SelectPageBox.Items.Clear();
                for (int i = 1; i < amountOfPagesNew + 1; i++)
                {
                    SelectPageBox.Items.Add("Page " + i.ToString());
                }
                if (SelectPageBox.SelectedIndex == amountOfPagesNew)
                {
                    SelectPageBox.SelectedIndex = SelectPageBox.SelectedIndex - 1;
                }

            }
            amountOfPages = amountOfPagesNew;
        }*/

        private int getRecordsCount(string Command)
       {
            int recordCount;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using var ExecuteCommand = new SqlCommand("SELECT COUNT(*) FROM WeatherRecords "+ Command, connection);
                recordCount = (int)ExecuteCommand.ExecuteScalar();
                connection.Close();
            }

            return recordCount;
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)//search
        {
            try
            {
                string CmdString;
                DataTable dt;
                int recordsCount;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (SearchWeatherTypeBox.Text != "Any")
                    {        
                        CmdString = $"SELECT* FROM WeatherRecords WHERE WeatherType = '{SearchWeatherTypeBox.Text}' AND Temperature2m> {TempMinBox.Text} AND Temperature2m<{TempMaxBox.Text};";
                         dt = new DataTable();
                        SqlCommand cmd = new SqlCommand(CmdString, connection);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        //recordsCount = getRecordsCount($"WHERE WeatherType = '{SearchWeatherTypeBox.Text}' AND Temperature2m> {TempMinBox.Text} AND Temperature2m<{TempMaxBox.Text};");
                    }
                    else
                    {

                        CmdString = $"SELECT* FROM WeatherRecords WHERE Temperature2m> {TempMinBox.Text} AND Temperature2m<{TempMaxBox.Text};";
                         dt = new DataTable();
                        SqlCommand cmd = new SqlCommand(CmdString, connection);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        //recordsCount = getRecordsCount($"WHERE Temperature2m> {TempMinBox.Text} AND Temperature2m<{TempMaxBox.Text};");


                    }

                    connection.Close();

                    if (buttonColumnDelete != null)
                    {
                        DataGridView.Columns.Remove(buttonColumnDelete);
                    }

                    if (buttonColumnEdit != null)
                    {
                        DataGridView.Columns.Remove(buttonColumnEdit);
                    }
                    var buttonFactoryDelete = new FrameworkElementFactory(typeof(Button));
                    buttonFactoryDelete.SetValue(Button.ContentProperty, "Delete Record");
                    buttonFactoryDelete.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                    {
                        var ButtonClicked = sender as Button;
                        var rowData = ButtonClicked.DataContext;

                        if (rowData is DataRowView dataRow)
                        {
                            int id = Convert.ToInt32(dataRow["id"]);
                            deleteRecord(id);

                        }
                    }));

                    var buttonDeleteTemplate = new DataTemplate();
                    buttonDeleteTemplate.VisualTree = buttonFactoryDelete;

                    buttonColumnDelete = new DataGridTemplateColumn
                    {
                        Header = "Delete",
                        CellTemplate = buttonDeleteTemplate
                    };

                    var buttonFactoryEdit = new FrameworkElementFactory(typeof(Button));
                    buttonFactoryEdit.SetValue(Button.ContentProperty, "Edit Record");
                    buttonFactoryEdit.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                    {
                        var ButtonClicked = sender as Button;
                        var rowData = ButtonClicked.DataContext;

                        if (rowData is DataRowView dataRow)
                        {
                            int id = Convert.ToInt32(dataRow["id"]);
                            string dateTime = dataRow["Time"].ToString();
                            string WeatherType = dataRow["WeatherType"].ToString();
                            float temperature = float.Parse(dataRow["Temperature2m"].ToString());
                            bool? isDay = null;
                            if (bool.TryParse(dataRow["IsDay"]?.ToString(), out bool parsed))
                                isDay = parsed;
                            editEntryInfo = new EditEntryInfo(id, ConnectionString, dateTime, temperature, isDay, WeatherType, this);
                            editEntryInfo.ShowDialog();

                        }
                    }));

                    var buttonEditTemplate = new DataTemplate();
                    buttonEditTemplate.VisualTree = buttonFactoryEdit;

                    buttonColumnEdit = new DataGridTemplateColumn
                    {
                        Header = "Edit",
                        CellTemplate = buttonEditTemplate
                    };


                    DataGridView.Columns.Add(buttonColumnDelete);
                    DataGridView.Columns.Add(buttonColumnEdit);

                    DataGridView.ItemsSource = dt.DefaultView;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}