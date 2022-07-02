using System;
using System.Data.SqlTypes;
using static Combinet.Core.Prelude;
using Combinet.Core.Matchers;

namespace Combinet.Core.Abstraction;


public abstract class PredicateMatcher<T> : BaseMatcher<T>
{
    protected abstract bool Predicate(T value);
    
    public override bool MatchEquals(T other) => Predicate(other);

    public virtual DelegateMatcher<TAdapt> Adapt<TAdapt>(Func<TAdapt, T> mapper) => new(Description, Compose(mapper, Predicate));
    public virtual DelegateMatcher<TAdapt> Adapt<TAdapt>(Func<TAdapt, T> mapper, Func<string, string> changeDesc) => new(changeDesc(Description), Compose(mapper, Predicate));
    
    
}

public class DelegateMatcher<T> : PredicateMatcher<T>
{
    protected readonly Func<T, bool> predicate;
    public override string Description { get; }

    public DelegateMatcher(string description, Func<T, bool> predicate)
    {
        this.predicate = predicate ?? (_ => true);
        Description = description;
    }

    protected sealed override bool Predicate(T value) => predicate(value);
}


public class WeakDelegateMatcher : DelegateMatcher<object>
{
    public WeakDelegateMatcher(string description, Func<object, bool> predicate) : base(description, predicate)
    {
    }

    public override bool Equals(object obj) => predicate(obj);

    public override int GetHashCode() => UniqueIdentifier.GetHashCode();
}


