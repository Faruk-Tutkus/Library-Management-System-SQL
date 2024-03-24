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
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace Library_Management_System
{
    /// <summary>
    /// Interaction logic for UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Window
    {
        string serverName; //MainWindowdan parametre alan server adı
        public UpdateUser(string serverName)
        {
            InitializeComponent();
            this.serverName = serverName;
        }
        //Window yüklendiğinde tüm kullanıcın ve tüm kitapların listesini döndür
        private void LoadData()
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
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

                query = "SELECT * FROM Books";
                command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    // Verileri göstermek için yeni bir SqlCommand oluştur
                    SqlCommand bookCommand = new SqlCommand(query, connection);
                    adapter = new SqlDataAdapter(bookCommand);
                    dt = new DataTable("Books");
                    adapter.Fill(dt);
                    // DataGrid'e verileri yükle
                    dataGrid_2.ItemsSource = dt.DefaultView;
                }

                connection.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            
        }
        //kullanıcı güncelle butonu
        private void btn_updatebook_Click(object sender, RoutedEventArgs e)
        {
            // Kullanıcıdan kitap bilgilerini al
            string userName = tb_username.Text.TrimEnd();
            string bookName = tb_bookname.Text.TrimEnd();

            // Seçili satırın ID'sini al
            int selectedUserId = 0;
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = dataGrid.SelectedItem as DataRowView;
                selectedUserId = (int)rowView.Row["UserID"]; //datagrid üzerinden seçilen bir kullanıcının ID değeri
            }

            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kullanıcının bir kitaba sahip olup olmadığını kontrol et
                string checkQuery = "SELECT BookOwner FROM dbo.[User] WHERE UserID = @UserID";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@UserID", selectedUserId);
                SqlDataReader reader = checkCommand.ExecuteReader();

                string currentBookOwner = null;
                if (reader.Read())
                {
                    currentBookOwner = reader["BookOwner"] as string; //sql üzerinden kulllanıcının sahip olduğu kitap, NULL olabilir
                }
                reader.Close();

                if (!string.IsNullOrEmpty(currentBookOwner))
                {
                    // Kullanıcı zaten bir kitaba sahipse, önce kitabı iade etmesini isteyin
                    MessageBox.Show("Lütfen mevcut kitabınızı iade edin.");
                }
                else
                {
                    // Kullanıcı bir kitaba sahip değilse, kitap bilgilerini güncelleyin
                    string query = "UPDATE dbo.[User] SET LoginName = @LoginName, BookOwner = @BookOwner WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LoginName", userName);
                    command.Parameters.AddWithValue("@BookOwner", bookName);
                    command.Parameters.AddWithValue("@UserID", selectedUserId);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Veritabanındaki güncel listeyi yükle
            LoadData();
        }
        //textbox için placeholder
        private void tb_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_username.Text))
            {
                lb_tb_username.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_username.Visibility = Visibility.Visible;
        }
        //textbox için placeholder
        private void tb_bookname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_bookname.Text))
            {
                lb_tb_bookname.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_bookname.Visibility = Visibility.Visible;
        }
        //dataGrid üzerinden seçilen satırı textboxlara yaz
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
                tb_username.Text = rowView["LoginName"].ToString().TrimEnd();
                tb_bookname.Text = rowView["BookOwner"].ToString().TrimEnd();
            }
        }
        //dataGrid üzerinden seçilen satırı textboxlara yaz
        private void dataGrid_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid_2.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dataGrid_2.SelectedItem;
                tb_bookname.Text = rowView["BookName"].ToString().TrimEnd();
            }
        }
        //kullanıcının sahip olduğu kitabı iade et, NULL hale getir
        private void btnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            // Seçili satırın ID'sini al
            int selectedUserId = 0;
            int bookId = 0;
            string loginName = "";
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = dataGrid.SelectedItem as DataRowView;
                selectedUserId = (int)rowView.Row["UserID"];
                loginName = rowView["LoginName"].ToString().TrimEnd();
            }

            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE dbo.[User] SET BookOwner = @BookOwner WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookOwner", DBNull.Value); // BookOwner'a NULL değeri atanıyor
                command.Parameters.AddWithValue("@UserID", selectedUserId);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                // Books tablosundan 'BookId' değerini al
                query = "SELECT BookId FROM Books WHERE BookOwner = @LoginName";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LoginName", loginName);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bookId = Convert.ToInt32(reader["BookId"].ToString());
                }
                // Books tablosunu güncelle
                query = "UPDATE Books SET BookOwner = NULL WHERE BookId = @BookId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", bookId);
                command.ExecuteNonQuery();

            }
            LoadData();
        }
    }
}
