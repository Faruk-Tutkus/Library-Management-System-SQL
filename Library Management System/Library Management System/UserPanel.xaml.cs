using System;
using System.Collections.Generic;
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
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        int id; //kullanıcının sql üzerindeki ID değeri MainWindowdan gelen parametre
        bool role; //kişinin admin mi yoksa kullanıcı mı olduğunu belirleyen bit türünden parametre
        string serverName; //MainWindowdan parametre alan server adı
        public UserPanel(int ID, string serverName, bool role = false)
        {
            InitializeComponent();
            this.id = ID;
            this.role = role;
            this.serverName = serverName;
            
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            //Application.Current.Shutdown();
        }
        bool click = false;
        private void list_book_MouseDown(object sender, MouseButtonEventArgs e)
        {
            click = true;
        }
        private void list_book_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (click)
            {
                Listbook listbook = new Listbook(id, role, serverName); //listbook panelini aç
                listbook.Show();
            }
            click = false;
        }

        private void rent_book_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RentBook rentBook = new RentBook(id, serverName); //rentbook panelini aç
            rentBook.Show();
        }

        private void remove_books_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RefundBook refundBook = new RefundBook(id, serverName); //refundbook panelini aç
            refundBook.Show();
        }
    }
}
