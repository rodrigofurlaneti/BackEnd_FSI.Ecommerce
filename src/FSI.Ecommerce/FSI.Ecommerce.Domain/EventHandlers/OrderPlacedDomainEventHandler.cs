using FSI.Ecommerce.Domain.DomainEvents;
using FSI.Ecommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Domain.EventHandlers
{
    public sealed class OrderPlacedDomainEventHandler : IDomainEventHandler<OrderPlacedDomainEvent>
    {
        public Task HandleAsync(OrderPlacedDomainEvent domainEvent, CancellationToken ct = default)
        {
            // Aqui você poderia atualizar alguma propriedade de domínio, log interno, etc.
            // Nenhuma dependência externa (infra) deve ser usada no domínio.

            return Task.CompletedTask;
        }
    }
}
