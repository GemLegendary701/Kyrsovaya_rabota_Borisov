using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? GenreId { get; set; }

    public int? Year { get; set; }

    public int Quantity { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
