using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Storage;

namespace Internship_4_OOP.Domain.Persistence.User;

public interface IUserUnitOfWork
{
    IUserRepository Repository { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}