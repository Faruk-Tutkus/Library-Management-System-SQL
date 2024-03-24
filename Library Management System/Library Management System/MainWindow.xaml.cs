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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_Management_System
{

    public partial class MainWindow : Window
    {
        string serverName = "DESKTOP-M6PSKVD\\SQLEXPRESS"; //server adı
        public MainWindow()
        {
            InitializeComponent();
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
        private void tb_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_password.Password))
            {
                lb_tb_password.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_password.Visibility = Visibility.Visible;
        }
        //Kayıt ve Giriş yap ekranlarının animasyon geçişleri
        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = this.FindResource("transition") as Storyboard;
            storyboard.Begin();
        }
        //Kayıt ve Giriş yap ekranlarının animasyon geçişleri
        private void btn_login_Copy_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = this.FindResource("transition_reverse") as Storyboard;
            storyboard.Begin();
        }
        //textbox için placeholder
        private void tb_username_register_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_username_register.Text))
            {
                lb_tb_username_register.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_username_register.Visibility = Visibility.Visible;
        }
        //textbox için placeholder
        private void tb_password_register_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_password_register.Password))
            {
                lb_tb_password_register.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_password_register.Visibility = Visibility.Visible;
        }
        //kayıt ol butonu
        private void btn_register_register_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionString);

            string checkUserQuery = "SELECT COUNT(*) FROM dbo.[User] WHERE LoginName = @username";
            SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
            checkUserCommand.Parameters.AddWithValue("@username", tb_username_register.Text);
            if (tb_password_register.Password == tb_confirmpassword_register.Password && !string.IsNullOrEmpty(tb_username_register.Text)
                && !string.IsNullOrEmpty(tb_password_register.Password) && !string.IsNullOrEmpty(tb_confirmpassword_register.Password))
            {
                connection.Open();
                int userCount = (int)checkUserCommand.ExecuteScalar();

                if (userCount > 0) //Kullanıcılar eşsiz olmalı, eğer birden fazla aynı kullanıcı varsa uyarı ver
                {
                    // Kullanıcı adı zaten var
                    MessageBox.Show("Kullanıcı adı zaten var.");
                }
                else
                {
                    // Kullanıcı adı mevcut değil, yeni kullanıcıyı ekleyin
                    string query = "EXEC dbo.uspAddUser @pLogin, @pPassword, @pBookOwner, @pRole, @responseMessage OUTPUT";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pLogin", tb_username_register.Text);
                        command.Parameters.AddWithValue("@pPassword", tb_password_register.Password);
                        command.Parameters.AddWithValue("@pBookOwner", DBNull.Value);
                        command.Parameters.AddWithValue("@pRole", 0);

                        SqlParameter responseMessageParam = new SqlParameter("@responseMessage", SqlDbType.NVarChar, 250);
                        responseMessageParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(responseMessageParam);

                        command.ExecuteNonQuery();

                        string responseMessage = (string)responseMessageParam.Value;
                        MessageBox.Show("Kayıt Başarılı");
                    }
                }

                connection.Close();
            }
            else
                MessageBox.Show("Girdiğiniz şifreler eşleşmedi. Tekrar deneyin.");
        }
        //textbox için placeholder
        private void tb_confirmpassword_register_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_confirmpassword_register.Password))
            {
                lb_tb_confirmpassword_register.Visibility = Visibility.Hidden;
            }
            else
                lb_tb_confirmpassword_register.Visibility = Visibility.Visible;
        }
        //giriş yap butonu
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={serverName};Database=Library Management System;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT UserID, Role FROM dbo.[User] WHERE LoginName = @username AND PasswordHash = HASHBYTES('SHA2_512', @password)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", tb_username.Text);
                command.Parameters.AddWithValue("@password", tb_password.Password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserID"]); //sql tablosundaki userID
                    bool isAdmin = Convert.ToBoolean(reader["Role"]); //sql tablosundaki role biti
                    reader.Close();
                    connection.Close();
                    //eğer giriş yapan admin ise admin paneline yönlendir
                    if (isAdmin)
                    {
                        AdminPanel admin = new AdminPanel(serverName);
                        admin.Show();
                        
                    }
                    //eğer giriş yapan kişi kullanıcı ise kullanıcı paneline yönlendir
                    else
                    {
                        UserPanel userPanel = new UserPanel(userId,serverName,false);
                        userPanel.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
                }
            }
        }

    }
}
