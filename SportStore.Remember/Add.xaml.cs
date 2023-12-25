using SportStore.Remember.Models;
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
//using SportStore.Remember.

namespace SportStore.Remember
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        Product? currentProduct;
        public Add(Product p)
        {
            InitializeComponent();
            this.Title = "Добавление товара";

            

            //using (SportStoreRememberContext db = new SportStoreRememberContext())

            if (p != null)
            {
                currentProduct = p;
                this.Title = "Редактирование товара";
                DataContext = currentProduct;
            }
        }

        private void saveProductButtonClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(nameBox.Text))
                errors.Append("Укажите название");
            if (string.IsNullOrWhiteSpace(decriptionBox.Text))
                errors.Append("Укажите описание");
            if (string.IsNullOrWhiteSpace(costBox.Text))
                errors.Append("Укажите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //po novoy
            using (SportStoreRememberContext db = new SportStoreRememberContext())
            {
                if (currentProduct == null)
                {
                    try
                    {
                        Product product = new Product()
                        {
                            Name = nameBox.Text,
                            Cost = int.Parse(costBox.Text),
                            Decription = decriptionBox.Text,
                        };

                        db.Products.Add(product);
                        db.SaveChanges();

                        MessageBox.Show("Товар добавлен");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
                else
                {
                    try
                    {

                        db.Products.Update(currentProduct);
                        db.SaveChanges();

                        MessageBox.Show("Товар обновлен");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                Main mainWindow = Application.Current.Windows.OfType<Main>().FirstOrDefault();
                (mainWindow.FindName("productlistView") as ListView).ItemsSource = db.Products.ToList();
            }
        }
    }
}
