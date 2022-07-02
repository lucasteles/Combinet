using System;
using Combinet.Core.Abstraction;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Combinet.Tests.Core.Matchers;

public class StringMatchersTest
{
    [SetUp]
    public void Setup()
    {
        AssertionOptions.AssertEquivalencyUsing(config => config.Using(new ReflectionMemberMatchingRule()));
    }


    [Test]
    public void EqualOperatorNotEmptyMatcherTest()
    {
        ("foo" == M.String.NotEmpty()).Should().BeTrue();
    }

    [Test]
    public void SimpleObjectAssertTest()
    {
        new {fooValue = "foo"}.ShouldMatch(new {fooValue = M.String.NotEmpty()});
    }

    [Test]
    public void ComposingMatchersTest()
    {
        var sut = new
        {
            Id = Guid.NewGuid(),
            Name = "Lucas",
            Age = 31,
            LastName = "Teles  ",
            Biography = "lorem ipsun lucas",
            SiteUrl = "http://lucasteles.dev"
        };

        var expected = new
        {
            Id = M.Ignore(),
            Name = "Lucas",
            Age = M.Int.GreaterThan(18),
            SiteUrl = M.String.StartsWith("http"),
            Biography = M.String.NotEmpty() & M.String.Contains("lucas"),
            LastName = M.String.Equivalent("teles") | M.String.Equivalent("agostinho")
        };

        sut.ShouldMatch(expected);
    }


    // [Test]
    // public void IndirectEqualsOperator()
    // {
    //     // record Foo(string Value);
    //     // var notEmptyString = new DelegateMatcher<string>(s => !string.IsNullOrEmpty(s));
    //     // var foos = new Foo[] {  new("asdf"), new(notEmptyString) };
    //     // ("foo" == foos[1].Value).Should().BeTrue();
    // }
}