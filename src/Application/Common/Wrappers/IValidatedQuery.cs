using Application.Common.Either;
using Application.Common.Validation;
using MediatR;

namespace Application.Common.Wrappers;

public interface IValidatedQuery<T> : IRequest<Either<T,ValidationResult>> { }

public interface IValidatedQueryHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, Either<TResponse,ValidationResult>> 
    where TRequest : IValidatedQuery<TResponse> { }