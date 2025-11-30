using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Persistence.User;
using Internship_4_OOP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Internship_4_OOP.Infrastructure.Database.Configuration.Users.UnitOfWork;

public class UserUnitOfWork(UserDbContext userDbContext, IUserRepository repository) : IUserUnitOfWork
{
    public IUserRepository Repository { get; } = repository;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await userDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {

       await userDbContext.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        var currTransaction=userDbContext.Database.CurrentTransaction;
        if(currTransaction!=null)
            await currTransaction.RollbackAsync();
    }
}