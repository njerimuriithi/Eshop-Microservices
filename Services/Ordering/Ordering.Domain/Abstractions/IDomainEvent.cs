﻿
namespace Ordering.API.Abstractions
{
    public interface IDomainEvent:INotification
    {
        Guid EventId => new Guid();

        public DateTime OccurredOn => DateTime.Now;

        public string EventType => GetType().AssemblyQualifiedName;
    }
}
