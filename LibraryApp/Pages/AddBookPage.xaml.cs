using System.Windows;
using System.Windows.Controls;
using LibraryApp.Models;

namespace LibraryApp.Pages
{
    public partial class AddBookPage : Page
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название книги.", "Ошибка");
                return;
            }
            int quantity = Convert.ToInt32(txtQuantity.Text);
            if (quantity < 0)
            {
                MessageBox.Show("Количество экземпляров должно быть целым положительным числом.", "Ошибка");
                return;
            }

            int? year = null;
            if (!string.IsNullOrWhiteSpace(txtYear.Text))
            {
                try
                {
                    int y = Convert.ToInt32(txtYear.Text);
                    year = y;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Год должен быть числом.", "Ошибка");
                    return;
                }
            }

            using (var db = new LibraryContext())
            {
                var newBook = new Book
                {
                    Title = txtTitle.Text,
                    Year = year,
                    Quantity = quantity
                };

                db.Books.Add(newBook);
                db.SaveChanges();
            }

            MessageBox.Show("Книга успешно добавлена.");

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}