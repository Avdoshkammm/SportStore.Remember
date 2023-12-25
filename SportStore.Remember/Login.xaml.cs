using Microsoft.EntityFrameworkCore;
using SportStore.Remember.Models;
using System.Windows;

namespace SportStore.Remember
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            new Main(null).Show();
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            SportStoreRememberContext db = new SportStoreRememberContext();

            //User user = db.Users.Include(u => u.Role).Where(u => u.Login == loginBox.Text && u.Password == passwordBox.Password).FirstOrDefault() as User;
            User user = db.Users.Where(u => u.Login == loginBox.Text && u.Password == passwordBox.Password).Include(u => u.RoleNavigation).FirstOrDefault() as User;
            if (loginBox != null)
            {
                Main main = new Main(user);
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин/пароль");
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
