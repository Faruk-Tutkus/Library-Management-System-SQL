using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        string serverName; //MainWindowdan parametre alan server adı
        public AddBook(string serverName)
        {
            InitializeComponent();
            this.serverName = serverName;
        }
        private void btn_bookregister_Click(object sender, RoutedEventArgs e)
        {
            // Kullanıcıdan kitap bilgilerini al
            string bookName = tb_bookname.Text;
            string bookGenre = tb_bookgenre.Text;
            string bookAuthor = tb_bookauthor.Text;
            //Kitap bilgileri boş değil ise gerçekleşecek blok
            if (!string.IsNullOrEmpty(tb_bookname.Text) && !string.IsNullOrEmpty(tb_bookgenre.Text) && !string.IsNullOrEmpty(tb_bookauthor.Text))
            {
                string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookName", bookName);
                    command.Parameters.AddWithValue("@BookGenre", bookGenre);
                    command.Parameters.AddWithValue("@BookAuthor", bookAuthor);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Kitap Eklendi");
            }
            else
                MessageBox.Show("Kitap Bilgileri Boş Olamaz");


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
