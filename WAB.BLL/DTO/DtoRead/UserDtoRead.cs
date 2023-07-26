namespace WAB.BLL.DTO.DtoRead;

public class UserDtoRead
{
    public int Id { get; set; }
    public decimal CardBalance { get; set; }
    public decimal DailyPoints { get; set; }
    public DateTime LastDailyPoints { get; set; }
}