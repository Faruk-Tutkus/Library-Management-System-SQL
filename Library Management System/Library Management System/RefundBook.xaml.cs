using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for RefundBook.xaml
    /// </summary>
    public partial class RefundBook : Window
    {
        int id;
        string serverName; //MainWindowdan parametre alan server adı
        public RefundBook(int id, string serverName)
        {
            InitializeComponent();
            this.id = id;
            this.serverName = serverName;
        }
        //bir kitabı iade et
        private void btn_refundbook_Click(object sender, RoutedEventArgs e)
        {
            string loginName = ""; //bir kullanıcının giriş adı
            int bookId = 0; //tablo üzerindeki kitabın ID değeri
            int userId = this.id; //MainWindowdan gelen kullanıcının id değeri

            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT LoginName, BookOwner FROM dbo.[User] WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    loginName = reader["LoginName"].ToString(); //sql tablosundaki bir kullanıcı adı
                }

                // Books tablosundan 'BookId' değerini al
                query = "SELECT BookId FROM Books WHERE BookOwner = @LoginName";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LoginName", loginName);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bookId = Convert.ToInt32(reader["BookId"].ToString()); //sql tablsundaki bir kitabın ID değeri
                }

                if (bookId != 0) // 'bookId' değeri kontrol ediliyor
                {
                    // Books tablosunu güncelle
                    query = "UPDATE Books SET BookOwner = NULL WHERE BookId = @BookId";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.ExecuteNonQuery();

                    // User tablosunu güncelle
                    query = "UPDATE dbo.[User] SET BookOwner = NULL WHERE UserID = @UserID";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kitap başarıyla iade edildi");
                    tb_bookname.Text = "";
                }
                else
                {
                    MessageBox.Show("Bu kullanıcı herhangi bir kitaba sahip değil");
                }

                connection.Close();
            }


        }

        //Window yüklendiğinde kullanıcının varsa kiralanmış kitabını textboxa yaz eğer yok ise boş bırak
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string bookName = ""; //sql üzerinden gelen kitabın adı
            int userId = this.id; //MainWindowdan gelen kullanıcının id değeri

            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT BookOwner FROM dbo.[User] WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bookName = reader["BookOwner"].ToString(); //sql üzerindeki bir kitabın adı
                }

                connection.Close();
            }
            // TextBox'a kitap ismini yaz
            tb_bookname.Text = bookName.TrimEnd();
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
    }
}
