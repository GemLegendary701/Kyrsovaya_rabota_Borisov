using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models;

public partial class Loan
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int ReaderId { get; set; }

    public int LibrarianId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Librarian Librarian { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
    [NotMapped]
    public bool IsOverdue { get; set; }
}
