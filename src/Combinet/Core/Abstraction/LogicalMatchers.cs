
namespace Combinet.Core.Abstraction;

public class OrMatcher<T> : BaseMatcher<T>
{
    readonly IObjectMatcher<T> left;
    readonly IObjectMatcher<T> right;

    public override string Description => $"({left} *OR* {right})";
    public OrMatcher(IObjectMatcher<T> left, IObjectMatcher<T> right)
    {
        this.left = left;
        this.right = right;
    }
    
    public override bool MatchEquals(T other) => 
        left.MatchEquals(other) || right.MatchEquals(other);

}

public class AndMatcher<T> : BaseMatcher<T>
{
    readonly IObjectMatcher<T> left;
    readonly IObjectMatcher<T> right;

    public override string Description => $"({left} *AND* {right})";
    public AndMatcher(IObjectMatcher<T> left, IObjectMatcher<T> right)
    {
        this.left = left;
        this.right = right;
    }
    
    public override bool MatchEquals(T other) => 
        left.MatchEquals(other) && right.MatchEquals(other);
}


public class NegateMatcher<T> : BaseMatcher<T>
{
    readonly IObjectMatcher<T> matcher;

    public override string Description => $"*NOT* ({matcher})";
    public NegateMatcher(IObjectMatcher<T> matcher) => this.matcher = matcher;

    public override bool MatchEquals(T other) => !matcher.MatchEquals(other);
}
