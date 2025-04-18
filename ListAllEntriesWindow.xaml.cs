using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Logika interakcji dla klasy ListAllEntriesWindow.xaml
    /// </summary>
    public partial class ListAllEntriesWindow : Window
    {
        DataGridTemplateColumn buttonColumnDelete;
        DataGridTemplateColumn buttonColumnEdit;
        EditEntryInfo EditEntryInfoWindow { get; set; }
        string ConnectionString;
        public ListAllEntriesWindow(string ConnectionStringIn)
        {
            ConnectionString = ConnectionStringIn;
            InitializeComponent();
            getAllRecords();
           
        }
        private void deleteRecord(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using var Command = new SqlCommand($"DELETE FROM WeatherRecords WHERE Id='{Id}'", connection);
                Command.ExecuteNonQuery();
                connection.Close();
                getAllRecords();
            }

        }

        private void getAllRecords()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))

            {
                string CommandString;
                CommandString = "SELECT  Id, Time, Temperature2m, IsDay, WeatherType FROM WeatherRecords ;";

                SqlCommand cmd = new SqlCommand(CommandString, con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("WeatherRecords");

                sda.Fill(dt);
                var buttonFactory = new FrameworkElementFactory(typeof(Button));
                buttonFactory.SetValue(Button.ContentProperty, "Delete");
                buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                {
                    var ButtonClicked = sender as Button;
                    var rowData = ButtonClicked.DataContext;

                    if (rowData is DataRowView dataRow)
                    {
                        int id = Convert.ToInt32(dataRow["id"]);
                        deleteRecord(id);

                    }
                }));

                if (buttonColumnDelete != null)
                {
                    DataGridAll.Columns.Remove(buttonColumnDelete);
                }

                var buttonTemplate = new DataTemplate();
                buttonTemplate.VisualTree = buttonFactory;

                buttonColumnDelete = new DataGridTemplateColumn
                {
                    Header = "Delete",
                    CellTemplate = buttonTemplate
                };
                DataGridAll.Columns.Add(buttonColumnDelete);

                DataGridAll.ItemsSource = dt.DefaultView;



            }




        }
    }
}

