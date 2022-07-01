using System;
using System.Data.SqlTypes;
using static Combinet.Core.Prelude;
using Combinet.Core.Matchers;

namespace Combinet.Core.Abstraction;


public abstract class PredicateMatcher<T> : BaseMatcher<T>
{
    protected abstract bool Predicate(T value);
    
    public override bool MatchEquals(T other) => Predicate(other);

    public virtual DelegateMatcher<TAdapt> Adapt<TAdapt>(Func<TAdapt, T> mapper) => new(Compose(mapper, Predicate));
    
    
}

public class DelegateMatcher<T> : PredicateMatcher<T>
{
    protected readonly Func<T, bool> predicate;

    public DelegateMatcher(Func<T, bool> predicate) => 
        this.predicate = predicate ?? (_ => true);

    protected sealed override bool Predicate(T value) => predicate(value);
}


public class WeakDelegateMatcher : DelegateMatcher<object>
{
    public WeakDelegateMatcher(Func<object, bool> predicate) : base(predicate)
    {
    }

    public override bool Equals(object obj) => predicate(obj);

    public override int GetHashCode() => UniqueIdentifier.GetHashCode();
}
