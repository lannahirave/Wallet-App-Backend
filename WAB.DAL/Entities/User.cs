namespace WAB.DAL.Entities;

public class User
{
    private decimal _cardBalance;
    public int Id { get; set; }

    public decimal CardBalance
    {
        get => _cardBalance;
        set
        {
            if (value >= 1500) throw new ArgumentException("Card balance cannot exceed $1500");
        }
    }

    public decimal DailyPoints { get; set; }
    public DateTime LastDailyPoints { get; set; }
    public HashSet<Transaction> Transactions { get; set; } = new();
}