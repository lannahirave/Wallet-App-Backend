using WAB.DAL.Entities;

namespace WAB.BLL.DTO.DtoRead;

public class TransactionDtoRead
{
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool Pending { get; set; }
    public string Icon { get; set; }
    public int AuthorizedUserId { get; set; } // If you need to show the authorized user ID.
}