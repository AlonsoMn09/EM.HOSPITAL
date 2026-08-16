using EM.Hospital.Domain.Events.Interfaces;

namespace EM.Hospital.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public bool Active { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public string CreatedBy { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent()
        {
            _domainEvents.Clear();
        }
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            Active = true;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = Environment.UserName;
        }
        public void SoftDelete()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = Environment.UserName;
        }
    }
}
