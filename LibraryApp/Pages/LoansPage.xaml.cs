using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibraryApp.Models;

namespace LibraryApp.Pages
{
    public partial class LoansPage : Page
    {
        public LoansPage()
        {
            InitializeComponent();
            LoadLoans();
        }
        private void LoadLoans()
        {
            using (var db = new LibraryContext())
            {
                var loans = db.Loans.ToList();
                var today = DateOnly.FromDateTime(DateTime.Today);
                foreach(var loan in loans)
                {
                    if(loan.ReturnDate == null && loan.DueDate < today)
                    {
                        loan.IsOverdue = true;
                    }
                    else
                    {
                        loan.IsOverdue = false;
                    }
                }
                lvLoans.ItemsSource = loans;            
            }
        }

        private void btnLoadLoans_Click(object sender, RoutedEventArgs e)
        {
            LoadLoans();
        }

        private void btnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (lvLoans.SelectedItem == null)
            {
                MessageBox.Show("Выберите выдачу из списка, которую хотите вернуть.", "Ошибка");
                return;
            }

            var selectedLoan = (Loan)lvLoans.SelectedItem;

            if (selectedLoan.ReturnDate != null)
            {
                MessageBox.Show("Эта книга уже возвращена.", "Ошибка");
                return;
            }

            using (var db = new LibraryContext())
            {
                var loanToUpdate = db.Loans.FirstOrDefault(l => l.Id == selectedLoan.Id);

                if (loanToUpdate != null)
                {
                    loanToUpdate.ReturnDate = DateOnly.FromDateTime(DateTime.Today);
                    db.SaveChanges();
                }
            }

            MessageBox.Show("Книга успешно возвращена.", "Успех");
            LoadLoans();
        }

        private void btnGoToBooks_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BooksPage());
        }
    }
}