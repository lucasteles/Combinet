# CombiNet [WIP]

Assertions with matching combinators on C#

```csharp
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

```
