using MediatR;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.User;
using Pluto.Domain.Events.User;
using Pluto.Domain.Handlers.Common;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Domain.Models;
using Pluto.Domain.Notifications;
using Pluto.Utils.Validations;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Commands
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand>, IRequestHandler<UpdateUserCommand>, IRequestHandler<ChangeUserPasswordCommand>, IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository userRepository;

        public UserCommandHandler(
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

            var entity = new User(request.Name, request.Email, request.Password, request.Profile);
            await userRepository.AddAsync(entity);

            Commit();
            await bus.InvokeAsync(new CreateUserEvent(entity.Id, entity.Name, entity.Email, entity.Password, entity.Profile));

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var original = await userRepository.GetAsync(request.Id); // -> Get entity from db.

            if (original == null) // -> If it is null, notificate and stop to avoid null exception.
            {
                await bus.InvokeDomainNotificationAsync("Not found.");
                return Unit.Value;
            }

            request // -> Data validadtion
                .IsInvalidEmail(e => e.Email, async () => await bus.InvokeDomainNotificationAsync("Invalid e-mail."))
                .IsNullEmptyOrWhitespace(e => e.Name, async () => await bus.InvokeDomainNotificationAsync("Invalid name."))
                .Is(e => userRepository.AnyAsync(u => u.Email == request.Email && u.Id != request.Id).Result, async () => await bus.InvokeDomainNotificationAsync("E-mail already exists."));

            // -> Set new info
            original.UpdateInfo(request.Name, request.Email, request.Profile);

            // -> Db persist
            await userRepository.UpdateAsync(original);

            Commit();
            await bus.InvokeAsync(new UpdateUserEvent(original.Id, original.Name, original.Email, original.Profile));

            return Unit.Value;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var original = await userRepository.GetAsync(request.Id); // -> Get entity from db.

            if (original == null) // -> If it is null, notificate and stop to avoid null exception.
            {
                await bus.InvokeDomainNotificationAsync("Not found.");
                return Unit.Value;
            }

            request // -> Data validadtion
                .IsNullEmptyOrWhitespace(e => e.Password, async () => await bus.InvokeDomainNotificationAsync("Invalid password."))
                .Is(e => e.PasswordConfirmation != e.Password, async () => await bus.InvokeDomainNotificationAsync("Invalid password confirmation."));

            // -> Set new info
            original.ChangePassword(request.Password);

            // -> Db persist
            await userRepository.UpdateAsync(original);

            Commit();
            await bus.InvokeAsync(new ChangeUserPasswordEvent(original.Id, original.Password));

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.DeleteAsync(request.Id);

            Commit();
            await bus.InvokeAsync(new DeleteUserEvent(request.Id));

            return Unit.Value;
        }

        public void Dispose() => userRepository.Dispose();
    }
}
