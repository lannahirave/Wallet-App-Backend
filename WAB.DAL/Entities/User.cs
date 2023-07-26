using WAB.DAL.Entities.Utility;

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
            if (value > 1500) throw new ArgumentException("Card balance cannot exceed $1500");
            _cardBalance = value;
        }
    }

    public double DailyPoints { get; set; }
    public DateTime LastDailyPoints { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = null!;
    public ICollection<Transaction> AuthorizedTransactions { get; set; } = null!;

    public void CalculateDailyPoints()
    {
        var currentDate = DateTime.Today;

        // Check if DailyPoints need to be recalculated for the current date
        if (currentDate != LastDailyPoints.Date)
        {
            // Calculate the points for the current day based on the season
            var dayNumber = GetDayNumberInSeason(currentDate);

            // Calculate the points for the current day based on the day number and season
            var calculator = new DailyPointsCalculator();
            DailyPoints = calculator.CalculatePoints(dayNumber);

            // Update the LastDailyPoints to the current date
            LastDailyPoints = currentDate;
        }
    }

    private int GetDayNumberInSeason(DateTime currentDate)
    {
        var dayNumber = 0;
        var currentYear = currentDate.Year;

        // Determine the season start dates
        var springSeasonStartDate = new DateTime(currentYear, 3, 1);
        var summerSeasonStartDate = new DateTime(currentYear, 6, 1);
        var fallSeasonStartDate = new DateTime(currentYear, 9, 1);
        var winterSeasonStartDate = new DateTime(currentYear, 12, 1);

        // Determine the season based on the current date
        if (currentDate >= springSeasonStartDate && currentDate < summerSeasonStartDate)
            // Spring season starts on March 1
            dayNumber = (currentDate - springSeasonStartDate).Days + 1;
        else if (currentDate >= summerSeasonStartDate && currentDate < fallSeasonStartDate)
            // Summer season starts on June 1
            dayNumber = (currentDate - summerSeasonStartDate).Days + 1;
        else if (currentDate >= fallSeasonStartDate && currentDate < winterSeasonStartDate)
            // Fall season starts on September 1
            dayNumber = (currentDate - fallSeasonStartDate).Days + 1;
        else if (currentDate >= winterSeasonStartDate)
            // Winter season starts on December 1
            dayNumber = (currentDate - winterSeasonStartDate).Days + 1;

        if (dayNumber <= 0) throw new ArgumentException("Invalid date for calculating day number.");

        return dayNumber;
    }
}