using System.Windows;
using System.Windows.Controls;
using LibraryApp.Models;

namespace LibraryApp.Pages
{
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void btnLoadBooks_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (var db = new LibraryContext())
            {
                lvBooks.ItemsSource = db.Books.ToList();
            }
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new AddBookPage());
        }
        private void btnIssueBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IssueBookPage());
        }
        private void btnGoToReaders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReadersPage());
        }
        private void btnGoToLoans_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoansPage());
        }
    }
}