using Combinet.Core.Abstraction;
using FluentAssertions.Equivalency;

namespace Combinet.Tests;

public class ReflectionMemberMatchingRule : IMemberMatchingRule
{
    public IMember Match(IMember expectedMember, object subject, INode parent, IEquivalencyAssertionOptions options)
    {
        return expectedMember;
    }
}
