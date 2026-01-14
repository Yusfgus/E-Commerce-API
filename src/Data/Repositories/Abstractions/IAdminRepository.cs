using E_Commerce.Dtos;
using E_Commerce.Models.Auth;
using E_Commerce.Results;

namespace E_Commerce.Data.Repositories.Abstractions;

public interface IAdminRepository
{
    Task AddAsync(Admin admin, CancellationToken ct);
}
