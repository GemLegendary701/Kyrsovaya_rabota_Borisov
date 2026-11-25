using System.Windows;
using System.Windows.Controls;
using LibraryApp.Models;
using System.Linq;

namespace LibraryApp.Pages
{
    public partial class AddReaderPage : Page
    {
        public AddReaderPage()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtTicket.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
                return;
            }

            using (var db = new LibraryContext())
            {
                if (db.Readers.Any(r => r.TicketNumber == txtTicket.Text))
                {
                    MessageBox.Show("Читатель с таким номером билета уже существует", "Ошибка");
                    return;
                }

                var newReader = new Reader
                {
                    FullName = txtFullName.Text,
                    TicketNumber = txtTicket.Text
                };

                db.Readers.Add(newReader);
                db.SaveChanges();
            }

            MessageBox.Show("Читатель добавлен", "Успех");
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