using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features;

public static class AddAuthor
{
    public record Command(string FirstName, string LastName) : ICommand<Author>;
    
    internal class Handler : ICommandHandler<Command,Author>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse<Author>> Handle(Command request, CancellationToken cancellationToken)
        {
            var author = new Author(request.FirstName, request.LastName);
            await _unitOfWork.Authors.AddAsync(author, cancellationToken);
            await _unitOfWork.CommitAsync();

            return Response.Success(author);
        }
    }
}