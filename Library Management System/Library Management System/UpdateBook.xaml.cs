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
    /// Interaction logic for UpdateBook.xaml
    /// </summary>
    public partial class UpdateBook : Window
    {
        string serverName; //MainWindowdan parametre alan server adı
        public UpdateBook(string serverName)
        {
            InitializeComponent();
            this.serverName = serverName;
        }
        //tüm kitapların bir listesini döndür
        private void LoadData()
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                adapter.Fill(dt);
                // DataGrid'e verileri yükle
                dataGrid.ItemsSource = dt.DefaultView;
            }
        }
        //seçilen kitabı güncelle
        private void btn_updatebook_Click(object sender, RoutedEventArgs e)
        {
            // Kullanıcıdan kitap bilgilerini al
            string bookName = tb_bookname.Text.TrimEnd();
            string bookGenre = tb_bookgenre.Text.TrimEnd();
            string bookAuthor = tb_bookauthor.Text.TrimEnd();

            // Seçili satırın ID'sini al
            int selectedBookID = 0;
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = dataGrid.SelectedItem as DataRowView;
                selectedBookID = (int)rowView.Row["BookID"]; //dataGrid üzerinden seçilen bir kitabın ID değeri
            }
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookName", bookName);
                command.Parameters.AddWithValue("@BookGenre", bookGenre);
                command.Parameters.AddWithValue("@BookAuthor", bookAuthor);
                command.Parameters.AddWithValue("@BookId", selectedBookID);
                command.ExecuteNonQuery();
            }


            // Veritabanındaki güncel listeyi yükle
            LoadData();

        }
        //Window yüklendiğinde tüm kitap listesini döndür
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        //dataGrid üzerinden seçilen satırı textboxlara yaz
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dataGrid.SelectedItem;
                //TrimEnd() fonksiyonu eklenen satırların sonunu siler
                tb_bookname.Text = rowView["BookName"].ToString().TrimEnd();
                tb_bookgenre.Text = rowView["BookGenre"].ToString().TrimEnd();
                tb_bookauthor.Text = rowView["BookAuthor"].ToString().TrimEnd();
            }
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
        //textbox için placeholder
        private void tb_bookgenre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_bookgenre.Text))
            {
                lb_tb_bookgenre.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_bookgenre.Visibility = Visibility.Visible;
        }
        //textbox için placeholder
        private void tb_bookauthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_bookauthor.Text))
            {
                lb_tb_bookauthor.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_bookauthor.Visibility = Visibility.Visible;
        }
    }
}
