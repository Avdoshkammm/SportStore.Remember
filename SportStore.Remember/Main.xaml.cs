using SportStore.Remember.Models;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace SportStore.Remember
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {

        public Main(User user)
        {
            InitializeComponent();
            


            using (SportStoreRememberContext db = new SportStoreRememberContext())
            {

                
                if (user != null)
                {
                    MessageBox.Show($"{user.RoleNavigation.Name}: {user.Surname} {user.Name}. \r\t");
                    statusUser.Text = user.RoleNavigation.Name;
                }
                else
                {
                    MessageBox.Show("Гость");
                    statusUser.Text = "Гость";
                }


                productlistView.ItemsSource = db.Products.ToList();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SportStoreRememberContext db = new SportStoreRememberContext())
            {
                var product = (productlistView.SelectedItem) as Product;

                    if (product != null)
                    {

                        if (MessageBox.Show($"Вы точно хотите удалить {product.Name}", "Внимание!",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            db.Products.Remove(product);
                            db.SaveChanges();
                            MessageBox.Show($"Товар {product.Name} удален!");
                            productlistView.ItemsSource = db.Products.ToList();
                            //countProducts.Text = $"Количество: {db.Products.Count()}";
                        }
                    }
                
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            using (SportStoreRememberContext db = new SportStoreRememberContext())
            {
                new Add(null).ShowDialog();

                
            }

        }
        private void EditProduct_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void updateSButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не рабоает, идите лесом :)");
        }

        private void productlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SportStoreRememberContext db = new SportStoreRememberContext())
            {

                if (sender is ListView listView && listView.SelectedItem != null)
                {
                    try
                    {
                        Product p = (sender as ListView).SelectedItem as Product;
                        new Add(p).ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
