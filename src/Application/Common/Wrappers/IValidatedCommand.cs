using Application.Common.Either;
using Application.Common.Validation;
using MediatR;
namespace Application.Common.Wrappers;

public interface IValidatedCommand<T> : IRequest<Either<T,ValidationResult>> { }

public interface IValidatedCommandHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, Either<TResponse,ValidationResult>> 
    where TRequest : IValidatedCommand<TResponse> { }