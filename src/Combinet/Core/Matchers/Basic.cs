using System;
using Combinet.Core.Abstraction;

namespace Combinet.Core.Matchers;

public class StringMatcher : DelegateMatcher<string>
{
    public StringMatcher(string description, Func<string, bool> predicate) : base(description, predicate)
    {
    }
}

public class IntegerMatcher : DelegateMatcher<int>
{
    public IntegerMatcher(string description, Func<int, bool> predicate) : base(description, predicate)
    {
    }
}