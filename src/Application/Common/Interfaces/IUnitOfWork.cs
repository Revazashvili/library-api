namespace Application.Common.Interfaces;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }

    Task<int> CommitAsync();
    void RejectChanges();
}