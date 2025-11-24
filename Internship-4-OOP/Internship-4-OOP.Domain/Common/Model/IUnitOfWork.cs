namespace Internship_4_OOP.Domain.Common.Model;

public interface IUnitOfWork
{
    Task CreateTransaction();
    Task DeleteTransaction();
    Task RollbackTransaction();
    Task SaveAsync();
}