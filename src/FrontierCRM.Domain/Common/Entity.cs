using System.ComponentModel.DataAnnotations;

namespace FrontierCRM.Domain.Common;

/// <summary>
/// Base entity class that all domain entities inherit from
/// </summary>
public abstract class Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    private readonly List<object> _domainEvents = new();
    
    /// <summary>
    /// Domain events raised by this entity
    /// </summary>
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();
    
    /// <summary>
    /// Raise a domain event
    /// </summary>
    /// <param name="domainEvent">The domain event to raise</param>
    protected void Raise(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    /// <summary>
    /// Clear all domain events
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;
            
        if (ReferenceEquals(this, other))
            return true;
            
        if (GetType() != other.GetType())
            return false;
            
        return Id == other.Id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    public static bool operator ==(Entity? left, Entity? right)
    {
        return Equals(left, right);
    }
    
    public static bool operator !=(Entity? left, Entity? right)
    {
        return !Equals(left, right);
    }
}
