using System;
using  Combinet;
using FluentAssertions;
using NUnit.Framework;

namespace Combinet.Tests.Core.Matchers;

public class StringMatchersTest
{
    [Test]
    public void EqualOperatorNotEmptyMatcherTest()
    {
        ("foo" == M.String.NotEmpty()).Should().BeTrue();
    }

    [Test]
    public void ComposingMatchersTest()
    {
        var data = new
        {
            Id  = Guid.NewGuid(),
            Name = "Lucas",
            Age = 30,
            LastName = "Teles  ",
            Biography = "lorem ipsun lucas",
            SiteUrl = "http://lucasteles.dev"
        };

        data.Should().Be(new
        {
            Id = M.Ignore(),
            Name = "Lucas",
            Age = M.Int.GreaterThan(18),
            SiteUrl = M.String.StartsWith("http"),
            Biography = M.String.NotEmpty() & M.String.Contains("lucas"),
            LastName = M.String.Equivalent("teles") | M.String.Equivalent("agostinho")
        });
    }


    [Test]
    public void IndirectEqualsOperator()
    {
        // record Foo(string Value);
        // var notEmptyString = new DelegateMatcher<string>(s => !string.IsNullOrEmpty(s));
        // var foos = new Foo[] {  new("asdf"), new(notEmptyString) };
        // ("foo" == foos[1].Value).Should().BeTrue();
    }
}