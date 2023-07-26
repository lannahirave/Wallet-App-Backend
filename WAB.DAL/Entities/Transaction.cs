namespace WAB.DAL.Entities;

// Transaction class
public class Transaction
{
    // Constructor
    public Transaction(decimal amount, string name, string description, DateTime date, bool pending, string icon,
        User authorizedUser, User? user)
    {
        Type = amount > 0 ? TransactionType.Payment : TransactionType.Credit;
        Amount = amount;
        Name = name;
        Description = description;
        Date = date;
        Pending = pending;
        Icon = icon;
        UserId = user.Id;
        User = user;
        AuthorizedUser = authorizedUser;
        AuthorizedUserId = authorizedUser.Id;
    }

    public Transaction()
    {
        // You can initialize properties with default values here if needed.
    }

    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool Pending { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int? AuthorizedUserId { get; set; }
    public User? AuthorizedUser { get; set; }
    public string Icon { get; set; } // Icon property to store the icon path or icon class reference
}