using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LibraryApp.Models;

namespace LibraryApp.Pages
{
    public partial class IssueBookPage : Page
    {
        public IssueBookPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            using (var db = new LibraryContext())
            {
                cmbBooks.ItemsSource = db.Books.ToList();
                cmbReaders.ItemsSource = db.Readers.ToList();
            }
            dpIssueDate.SelectedDate = DateTime.Today;
            dpDueDate.SelectedDate = DateTime.Today.AddDays(14);
        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBooks.SelectedItem == null || cmbReaders.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу и читателя", "Ошибка");
                return;
            }

            if (dpIssueDate.SelectedDate == null || dpDueDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка");
                return;
            }

            DateTime issueDate = dpIssueDate.SelectedDate.Value;
            DateTime dueDate = dpDueDate.SelectedDate.Value;

            if (dueDate <= issueDate)
            {
                MessageBox.Show("Срок возврата должен быть позже даты выдачи", "Ошибка");
                return;
            }

            var selectedBook = (Book)cmbBooks.SelectedItem;
            var selectedReader = (Reader)cmbReaders.SelectedItem;

            using (var db = new LibraryContext())
            {
                int activeLoans = db.Loans.Count(l => l.BookId == selectedBook.Id && l.ReturnDate == null);

                if (activeLoans >= selectedBook.Quantity)
                {
                    MessageBox.Show("Нет свободных экземпляров этой книги", "Ошибка");
                    return;
                }

                var today = DateOnly.FromDateTime(DateTime.Today);

                bool isDebtor = db.Loans.Any(l => l.ReaderId == selectedReader.Id && l.ReturnDate == null && l.DueDate < today);
                if (isDebtor)
                {
                    MessageBox.Show("Читатель является должником, выдача запрещена.", "Ошибка");
                    return;
                }
                var newLoan = new Loan
                {
                    BookId = selectedBook.Id,
                    ReaderId = selectedReader.Id,
                    LibrarianId = 1,
                    IssueDate = DateOnly.FromDateTime(issueDate),
                    DueDate = DateOnly.FromDateTime(dueDate),
                    ReturnDate = null
                };
                db.Loans.Add(newLoan);
                db.SaveChanges();
            }
            MessageBox.Show("Книга успешно выдана.");

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