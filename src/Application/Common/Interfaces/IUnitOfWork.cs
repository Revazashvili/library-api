namespace Application.Common.Interfaces;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }

    void CommitAsync();
    void RejectChangesAsync();
}