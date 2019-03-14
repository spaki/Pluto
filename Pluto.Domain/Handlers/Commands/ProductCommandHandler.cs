using MediatR;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.Product;
using Pluto.Domain.Events.Product;
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
    public class ProductCommandHandler : CommandHandler, IRequestHandler<CreateProductCommand>, IRequestHandler<UpdateProductCommand>, IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;

        public ProductCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications,
            IProductRepository productRepository
        ) : base(uow, bus, notifications)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            request
                .IsNullEmptyOrWhitespace(e => e.Name, async () => await bus.InvokeDomainNotificationAsync("Invalid name."))
                .Is(e => e.Price <= 0, async () => await bus.InvokeDomainNotificationAsync("Invalid price."));

            var entity = new Product(request.Name, request.Description, request.Price);

            if (request.PicturesUrls != null)
                foreach (var pictureUrl in request.PicturesUrls)
                    entity.AddPicture(pictureUrl);

            await productRepository.AddAsync(entity);

            Commit();
            await bus.InvokeAsync(new CreateProductEvent(entity.Id, entity.Name, entity.Description, entity.Price, entity.GetPictureUrls()));

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var original = await productRepository.GetAsync(request.Id); // -> Get entity from db.

            if (original == null) // -> If it is null, notificate and stop to avoid null exception.
            {
                await bus.InvokeDomainNotificationAsync("Not found.");
                return Unit.Value;
            }

            request
                .IsNullEmptyOrWhitespace(e => e.Name, async () => await bus.InvokeDomainNotificationAsync("Invalid name."))
                .Is(e => e.Price <= 0, async () => await bus.InvokeDomainNotificationAsync("Invalid price."));

            original.UpdateInfo(request.Name, request.Description, request.Price);

            await productRepository.UpdateAsync(original);

            Commit();
            await bus.InvokeAsync(new UpdateProductEvent(original.Id, original.Name, original.Description, original.Price));

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productRepository.DeleteAsync(request.Id);

            Commit();
            await bus.InvokeAsync(new DeleteProductEvent(request.Id));

            return Unit.Value;
        }

        public void Dispose() => productRepository.Dispose();
    }
}
