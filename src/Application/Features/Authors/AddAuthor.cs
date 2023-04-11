using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features.Authors;

public record AddAuthorCommand(string FirstName, string LastName) : ICommand<Author>;

internal class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand, Author>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddAuthorCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IResponse<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author(request.FirstName, request.LastName);
        await _unitOfWork.Authors.AddAsync(author, cancellationToken);
        await _unitOfWork.CommitAsync();

        return Response.Success(author);
    }
}