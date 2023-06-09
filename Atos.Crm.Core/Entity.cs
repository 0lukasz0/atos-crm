﻿namespace Atos.Crm.Core;

public abstract class Entity
{
    public virtual int Id { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (!(obj is Entity other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id == 0 || other.Id == 0)
            return false;

        return Id == other.Id;
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return (GetType().ToString() + Id).GetHashCode();
    }
}