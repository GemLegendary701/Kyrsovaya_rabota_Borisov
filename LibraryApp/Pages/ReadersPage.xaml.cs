using System.Windows;
using System.Windows.Controls;
using LibraryApp.Models;

namespace LibraryApp.Pages
{
    public partial class ReadersPage : Page
    {
        public ReadersPage()
        {
            InitializeComponent();
            LoadReaders();
        }

        private void LoadReaders()
        {
            using (var db = new LibraryContext())
            {
                lvReaders.ItemsSource = db.Readers.ToList();
            }
        }

        private void btnAddReader_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddReaderPage());
        }
        private void btnGoToBooks_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BooksPage());
        }
    }
}