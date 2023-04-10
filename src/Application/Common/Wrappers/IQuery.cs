using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers;

public interface IQuery<out T> : IRequest<IResponse<T>> { }

public interface IQueryHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, IResponse<TResponse>> 
    where TRequest : IQuery<TResponse> { }