using System;
using System.Text.RegularExpressions;
using Combinet.Core.Abstraction;

namespace Combinet.Core.Matchers;

public class StringMatcher : DelegateMatcher<string>
{
    public StringMatcher(Func<string, bool> predicate) : base(predicate) { }
}

public class IntegerMatcher : DelegateMatcher<int>
{
    public IntegerMatcher(Func<int, bool> predicate) : base(predicate) { }
}
