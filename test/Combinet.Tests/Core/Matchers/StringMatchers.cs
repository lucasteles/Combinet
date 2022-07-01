using System;
using Combinet;
using Combinet.Core.Abstraction;
using FluentAssertions;
using NUnit.Framework;

namespace Combinet.Tests.Core.Matchers;

public class StringMatchersTest
{
    [SetUp]
    public void Setup()
    {
        AssertionOptions.AssertEquivalencyUsing(options =>
            options.Using(ctx =>
                    ctx.Subject.Should().Be(ctx.Expectation))
                .WhenTypeIs<IObjectMatcher>());
    }


    [Test]
    public void EqualOperatorNotEmptyMatcherTest()
    {
        ("foo" == M.String.NotEmpty()).Should().BeTrue();
    }

    [Test]
    public void SimpleObjectAssertTest()
    {
        new {v = "foo"}.Should()
            .BeEquivalentTo(new {v = M.String.NotEmpty()}, options => options.RespectingRuntimeTypes());
    }

    [Test]
    public void ComposingMatchersTest()
    {
        var data = new
        {
            Id = Guid.NewGuid(),
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