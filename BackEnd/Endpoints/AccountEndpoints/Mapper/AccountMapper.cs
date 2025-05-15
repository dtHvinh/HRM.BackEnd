using BackEnd.Endpoints.AccountEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.AccountEndpoints.Mapper;

[Mapper]
public static partial class AccountMapper
{
    public static partial GetAccountDTO ToGetAccountDTO(this Account account);
}
