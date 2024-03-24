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
    /// Interaction logic for DeleteBook.xaml
    /// </summary>
    public partial class DeleteBook : Window
    {
        string serverName; //MainWindowdan parametre alan server adı
        public DeleteBook(string serverName)
        {
            InitializeComponent();
            this.serverName = serverName;
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
        //Tüm kitapların listesini döndüren bir fonksiyon
        void LoadData()
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books"; //tüm kitap listesi
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
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
        //window yüklendiğinde LoadData() fonksiyonunu çalıştır
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        //dataGrid'den bir satır seçildiğinde bu satırı textbox içerisine yaz
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
                SearchTextBox.Text = rowView["BookName"].ToString().TrimEnd();
            }
        }
        //Kitap silme butonu
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // DataGrid'den seçilen kitabın ID'sini al
            DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
            int bookId = Convert.ToInt32(rowView.Row["BookId"]); //seçilen satırın ID değerini al
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string message = reader["Message"].ToString(); //kitap başarıyla eklendi mesajı SQL üzerinden
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show("Kitap Silindi");
                }
            }
            LoadData();
        }

    }
}
