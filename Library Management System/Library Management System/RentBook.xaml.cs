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
    /// Interaction logic for RentBook.xaml
    /// </summary>
    public partial class RentBook : Window
    {
        int id;
        string serverName; //MainWindowdan parametre alan server adı
        public RentBook(int ID, string serverName)
        {
            InitializeComponent();
            this.id = ID;
            this.serverName = serverName;
        }
        //Tüm kiralanabilir kitapların bir listesi
        private void LoadData()
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    query = "SELECT BookID, BookName, BookGenre, BookAuthor FROM Books WHERE BookOwner IS NULL";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
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
        //Tüm kiralanabilir kitapları listele
        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        //listenin içerisinden kitap ara
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT BookID, BookName, BookGenre, BookAuthor FROM Books WHERE BookName LIKE @BookName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookName", "%" + SearchTextBox.Text + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                adapter.Fill(dt);
                // DataGrid'e verileri yükle
                dataGrid.ItemsSource = dt.DefaultView;
                connection.Close();
            }
        }
        //kiralanabilir kitapların içerisinden kitap kirala
        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            string loginName = "";
            string bookName = "";
            int bookId = 0;
            int userId = this.id;
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = dataGrid.SelectedItem as DataRowView;
                if (rowView != null)
                {
                    bookId = (int)rowView.Row["BookID"]; //sql üzerinden bir kitabın ID değeri
                }
            }

            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT LoginName, BookOwner FROM dbo.[User] WHERE UserID = @UserID"; // 'BookOwner' sütunu eklendi
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bookName = reader["BookOwner"].ToString(); //sql üzerinden bir kitabın adı
                    loginName = reader["LoginName"].ToString(); //sql üzerinden bir kullanıcının adı

                }
                if (!string.IsNullOrEmpty(bookName)) //book name Null ise kullanıcı kitap kiralayamaz
                {
                    MessageBox.Show("Bu kullanıcının zaten bir kitabı var. Lütfen önce mevcut kitabı iade edin.");
                }
                else
                {
                    // Books tablosunu güncelle
                    query = "UPDATE Books SET BookOwner = @LoginName WHERE BookId = @BookId";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LoginName", loginName);
                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kitap başarıyla kiralandı");
                }
                connection.Close();
            }
        }
        //dataGrid üzerinden seçili satırı textboxa yaz
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
                SearchTextBox.Text = rowView["BookName"].ToString().TrimEnd();
            }
        }
    }
}
