using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using MediatR;

namespace Application.Features;

public static class DeleteAuthor
{
    public record Command(int Id) : ICommand<Unit>;
    
    internal class Handler : ICommandHandler<Command,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IResponse<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Authors.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.CommitAsync();

            return Response.Success(Unit.Value);
        }
    }
}