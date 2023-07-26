using AutoMapper;
using WAB.BLL.DTO.DtoRead;
using WAB.DAL.Entities;

namespace WAB.BLL.MapingProfiles;

public sealed class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionDtoRead, Transaction>();
        CreateMap<Transaction, TransactionDtoRead>();
    }
}