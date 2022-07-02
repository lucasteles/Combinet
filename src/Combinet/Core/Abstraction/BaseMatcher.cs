using System;
using System.Linq;

namespace Combinet.Core.Abstraction;

public interface IObjectMatcher
{
    string Description { get; }
    bool MatchEquals(object other);
}

public interface IObjectMatcher<in T> : IObjectMatcher
{
    bool MatchEquals(T other);
}

public abstract class BaseMatcher<T> : IObjectMatcher<T>, IEquatable<T>, IComparable<T>
{
    protected readonly Guid UniqueIdentifier = Guid.NewGuid();

    public abstract bool MatchEquals(T other);

    public abstract string Description { get; }
    bool IObjectMatcher.MatchEquals(object other) => (this as IObjectMatcher<T>).MatchEquals(other);
    bool IEquatable<T>.Equals(T other) => MatchEquals(other);

    public override int GetHashCode() => UniqueIdentifier.GetHashCode();

    public override bool Equals(object obj) =>
        obj is T t && (this as IEquatable<T>).Equals(t);

    public int CompareTo(T other)
    {
        if (MatchEquals(other)) return 0;
        return -1;
    }

    public static bool operator ==(BaseMatcher<T> left, object right) => left?.Equals(right) == true;
    public static bool operator ==(object left, BaseMatcher<T> rigth) => rigth == left;
    public static bool operator !=(BaseMatcher<T> left, object right) => !(left == right);
    public static bool operator !=(object left, BaseMatcher<T> right) => right != left;
    public static NegateMatcher<T> operator !(BaseMatcher<T> matcher) => new(matcher);
    public static OrMatcher<T> operator |(BaseMatcher<T> left, BaseMatcher<T> right) => new(left, right);
    public static AndMatcher<T> operator &(BaseMatcher<T> left, BaseMatcher<T> right) => new(left, right);

    // public static implicit operator T(BaseMatcher<T> matcher) => default;
    public override string ToString() => Description;
}