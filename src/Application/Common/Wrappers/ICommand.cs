using Application.Common.Models;
using MediatR;

namespace Application.Common.Wrappers;

public interface ICommand<out T> : IRequest<IResponse<T>> { }

public interface ICommandHandler<in TRequest, TResponse> :
    IRequestHandler<TRequest, IResponse<TResponse>> 
    where TRequest : ICommand<TResponse> { }