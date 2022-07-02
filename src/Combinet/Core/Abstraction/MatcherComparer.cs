using System;
using System.Collections.Generic;
using DeepEqual;

namespace Combinet.Core.Abstraction;

public class MatcherComparer<T> : IEqualityComparer<T>, IComparison
{
    public bool Equals(T x, T y) =>
        (x, y) switch
        {
            (IObjectMatcher m, _) => m.MatchEquals(y),
            (_, IObjectMatcher m) => m.MatchEquals(x),
            ({ } nx, { } ny) => nx.Equals(ny) || ny.Equals(nx),
            (null, null) => true,
        };

    public int GetHashCode(T obj) => obj.GetHashCode();

    readonly IComparison defaultComparer = new DefaultComparison();
    public bool CanCompare(Type type1, Type type2) => defaultComparer.CanCompare(type1, type2);

    public (ComparisonResult result, IComparisonContext context) Compare(
        IComparisonContext context,
        object value1,
        object value2)
    {
        if (value2 is IObjectMatcher matcher2) 
            return matcher2.Equals(value1) 
                ? (ComparisonResult.Pass, context) 
                : (ComparisonResult.Fail, context.AddDifference(value1,$"{{ {matcher2.Description} }}"));
        
        if (value1 is IObjectMatcher matcher1) 
            return matcher1.Equals(value2) 
                ? (ComparisonResult.Pass, context) 
                : (ComparisonResult.Fail, context.AddDifference($"{{ {matcher1.Description} }}", value2));


        return defaultComparer.Compare(context, value1, value2);
    }
}