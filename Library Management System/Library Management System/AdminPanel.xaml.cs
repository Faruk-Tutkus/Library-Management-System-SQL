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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        string serverName; //MainWindowdan parametre alan server adı
        public AdminPanel(string serverName)
        {
            InitializeComponent();
            this.serverName = serverName;
        }
        //Kitapları listelemek için kullanılan panel
        private void listBook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Listbook listbook = new Listbook(1, true, serverName); //Sırası ile parametreler (userID, Role, serverName) eğer Role 1 ise Admin 0 ise kullanıcıdır
            listbook.Show();
        }
        //Kitap eklemek için kullanılan panel
        private void addbook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddBook addBook = new AddBook(serverName);
            addBook.Show();
        }
        //Kitap güncellemek için kullanılan panel
        private void updatebook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateBook updateBook = new UpdateBook(serverName);
            updateBook.Show();
        }
        //Kullanıcı listelemek için kullanılan panel
        private void listUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListUser listUser = new ListUser(serverName);
            listUser.Show();
        }
        //Kullanıcı güncellemek için kullanılan panel
        private void updateUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateUser updateUser = new UpdateUser(serverName);
            updateUser.Show();
        }
        //Kitap silmek için kullanılan panel
        private void deletebook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteBook deleteBook = new DeleteBook(serverName);
            deleteBook.Show();
        }
    }
}
