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
    /// Interaction logic for Listbook.xaml
    /// </summary>
    public partial class Listbook : Window
    {
        int id;
        bool role; //MainWindowdan parametre alan kişinin kullanıcı mı yoksa admin mi olduğunu gösteren bir bit
        string serverName; //MainWindowdan parametre alan server adı
        public Listbook(int ID, bool Role, string serverName)
        {
            InitializeComponent();
            this.id = ID;
            this.role = Role;
            this.serverName = serverName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.[User]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bool role = this.role;
                    if (role)
                    {
                        // Admin olduğu için tüm sütunları göster
                        query = "SELECT * FROM Books";
                        grid.Background = new SolidColorBrush(Color.FromRgb(40, 78, 112));
                    }
                    else
                    {
                        // Kullanıcı olduğu için sonsahip hariç diğer sütunları göster
                        query = "SELECT BookName, BookGenre, BookAuthor FROM Books";
                        grid.Background = new SolidColorBrush(Color.FromRgb(186, 200, 227));
                    }
                    // Verileri göstermek için yeni bir SqlCommand oluştur
                    SqlCommand bookCommand = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(bookCommand);
                    DataTable dt = new DataTable("Books");
                    adapter.Fill(dt);
                    // DataGrid'e verileri yükle
                    dataGrid.ItemsSource = dt.DefaultView;
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SearchBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookName", SearchTextBox.Text);
                command.Parameters.AddWithValue("@Role", this.role);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                adapter.Fill(dt);
                // DataGrid'e verileri yükle
                dataGrid.ItemsSource = dt.DefaultView;
            }

        }
        //Kitap listesini göster admin ve kullanıcı için ayrı
        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query;
                bool role = this.role;
                if (role)
                {
                    // Admin olduğu için tüm sütunları göster
                    query = "SELECT * FROM Books";
                }
                else
                {
                    // Kullanıcı sadece belirli sütünları görebilsin 
                    query = "SELECT BookName, BookGenre, BookAuthor FROM Books";
                }
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
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
                lb_tb_bookname.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_bookname.Visibility = Visibility.Visible;
        }
    }
}
