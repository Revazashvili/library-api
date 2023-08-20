using Application.Common.Either;
using Application.Common.Validation;
using MediatR;

namespace Application.Common.Wrappers;

public interface IValidateQuery<T> : IRequest<Either<T,ValidationResult>> { }

public interface IValidatedQueryHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, Either<TResponse,ValidationResult>> 
    where TRequest : IValidateQuery<TResponse> { }