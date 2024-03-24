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
using System.Windows.Shapes;

namespace Library_Management_System
{
    /// <summary>
    /// Interaction logic for ListUser.xaml
    /// </summary>
    public partial class ListUser : Window
    {
        string serverName; //MainWindowdan parametre alan server adı
        public ListUser(string serverName)
        {
            InitializeComponent();
            this.serverName = serverName;
        }
        //Window yüklendiğinde tüm kullanıcı listesini döndür
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.[User]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Verileri göstermek için yeni bir SqlCommand oluştur
                    SqlCommand bookCommand = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(bookCommand);
                    DataTable dt = new DataTable("dbo.[User]");
                    adapter.Fill(dt);
                    // DataGrid'e verileri yükle
                    dataGrid.ItemsSource = dt.DefaultView;
                }
            }
        }
        //textbox üzerine girilen bir metni listede ara
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SearchUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LoginName", SearchTextBox.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("dbo.[User]");
                adapter.Fill(dt);
                // DataGrid'e verileri yükle
                dataGrid.ItemsSource = dt.DefaultView;
            }

        }
        // Tüm kullanıcı listesi
        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.[User]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("dbo.[User]");
                adapter.Fill(dt);
                // DataGrid'e verileri yükle
                dataGrid.ItemsSource = dt.DefaultView;
            }
        }
        //textbox için placeholder
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                lb_tb_username.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_username.Visibility = Visibility.Visible;
        }

    }
}
