using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.User;
using Pluto.Domain.Events;
using Pluto.Domain.Handlers.Common;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Domain.Models;
using Pluto.Domain.Notifications;
using Pluto.Utils.Validations;

namespace Pluto.Domain.Handlers
{
    public class UserHandler : CommandHandler, IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository userRepository;

        public UserHandler(
            IUnitOfWork uow, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications, 
            IUserRepository userRepository
        ) 
        : base(
                uow, 
                bus, 
                notifications
        )
        {
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request
                .IsInvalidEmail(e => e.Email, async () => await bus.InvokeDomainNotificationAsync("Invalid e-mail."))
                .IsNullEmptyOrWhitespace(e => e.Name, async () => await bus.InvokeDomainNotificationAsync("Invalid name."))
                .IsNullEmptyOrWhitespace(e => e.Password, async () => await bus.InvokeDomainNotificationAsync("Invalid password."))
                .Is(e => e.PasswordConfirmation != e.Password, async () => await bus.InvokeDomainNotificationAsync("Invalid password confirmation."))
                .Is(e => userRepository.AnyAsync(u => u.Email == request.Email).Result, async () => await bus.InvokeDomainNotificationAsync("E-mail already exists."));

            var entity = new User(request.Name, request.Email, request.Password);
            await userRepository.AddAsync(entity);

            Commit();
            await bus.InvokeAsync(new CreateUserEvent(entity.Id, entity.Name, entity.Email, entity.Password));

            return Unit.Value;
        }

        public void Dispose()
        {
            userRepository.Dispose();
        }
    }
}
