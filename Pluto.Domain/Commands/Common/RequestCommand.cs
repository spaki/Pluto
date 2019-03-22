using MediatR;

namespace Pluto.Domain.Commands.Common
{
    public abstract class RequestCommand<TResult> : Command, IRequest<TResult>
    {
    }
}
