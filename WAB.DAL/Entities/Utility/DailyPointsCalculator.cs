namespace WAB.DAL.Entities.Utility;

public class DailyPointsCalculator
{
    private readonly int[] _points = {2, 3};

    public double CalculatePoints(int dayNumber)
    {
        if (dayNumber <= 2) return _points[dayNumber - 1];

        double prevDayPoints = _points[1];
        double prevPrevDayPoints = _points[0];
        double newPoints = 0;

        for (var i = 3; i <= dayNumber; i++)
        {
            newPoints = prevDayPoints + 0.6 * prevPrevDayPoints;
            prevPrevDayPoints = prevDayPoints;
            prevDayPoints = newPoints;
        }

        newPoints = Math.Round(newPoints, 2);
        return newPoints;
    }
}