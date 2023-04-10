using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Features;

public static class AddAuthor
{
    public record Command(string FirstName, string LastName) : ICommand<Author>;
    
    public class Handler : ICommandHandler<Command,Author>
    {
        public Task<IResponse<Author>> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}