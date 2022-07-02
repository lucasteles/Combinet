using System;
using System.Text.RegularExpressions;
using Combinet.Core.Abstraction;
using Combinet.Core.Matchers;
using DeepEqual.Syntax;

namespace Combinet;

public static class M
{
    public static WeakDelegateMatcher Null() => new("be null", value => value is null);
    public static WeakDelegateMatcher NotNull() => new("not be null", value => value is not null);
    public static WeakDelegateMatcher Ignore() => new("ignore", _ => true);

    public static class String
    {
        public static StringMatcher NotEmpty() => new("not empty", value => !string.IsNullOrEmpty(value));
        public static StringMatcher Empty() => new("empty", string.IsNullOrEmpty);

        public static StringMatcher NotWhiteSpace() =>
            new("without spaces", value => !string.IsNullOrWhiteSpace(value));

        public static StringMatcher WhiteSpace() => new("with spaces", string.IsNullOrWhiteSpace);
        public static StringMatcher Contains(string sub) => new($"contain '{sub}'", value => value.Contains(sub));

        public static StringMatcher NotContains(string sub) =>
            new($"not contain '{sub}'", value => !value.Contains(sub));

        public static StringMatcher StartsWith(string sub) =>
            new($"starting with '{sub}'", value => value.StartsWith(sub));

        public static StringMatcher EndsWith(string sub) => new($"ending with '{sub}'", value => value.EndsWith(sub));
        public static StringMatcher Size(int size) => new($"size of {size}", value => value.Length == size);

        public static StringMatcher SizeGreaterThan(int size) =>
            new($"size greater than {size}", value => value.Length > size);

        public static StringMatcher SizeLessThan(int size) =>
            new($"size less than {size}", value => value.Length < size);

        public static StringMatcher SizeGreaterThanOrEqual(int size) =>
            new($"size greater or equal than", value => value.Length >= size);

        public static StringMatcher SizeLessThanOrEqual(int size) =>
            new($"size less than or equal {size}", value => value.Length <= size);

        public static StringMatcher Pattern(string pattern) =>
            new($"match pattern {pattern}", value => Regex.Match(value, pattern).Success);

        public static StringMatcher SameAs(string text) => new($"Same as {text}", value => value == text);
        public static StringMatcher Equals(string text) => new($"equals {text}", value => value.Trim() == text.Trim());

        public static StringMatcher Equivalent(string text) => new($"equivalent to {text}", value =>
            string.Equals(value.Trim(), text.Trim(), StringComparison.CurrentCultureIgnoreCase));

        public static StringMatcher NotPattern(string pattern) => new($"Not match pattern {pattern}",
            value => !Regex.Match(value, pattern).Success);
    }

    public class Int
    {
        public static IntegerMatcher Zero() => new("to be zero", value => value is 0);
        public static IntegerMatcher NotZero() => new("not zero", value => value is not 0);
        public static IntegerMatcher Positive() => new("positive", value => value > 0);
        public static IntegerMatcher Negative() => new("negative", value => value < 0);
        public static IntegerMatcher GreaterThan(int n) => new($"greater than {n}", value => value > n);
        public static IntegerMatcher LessThan(int n) => new($"less than {n}", value => value < n);

        public static IntegerMatcher GreaterThanOrEqual(int n) =>
            new($"greater than or equal {n}", value => value >= n);

        public static IntegerMatcher LessThanOrEqual(int n) => new($"greater than or equal {n}", value => value <= n);
    }

    public static void ShouldMatch<T>(this T self, object other) =>
        self
            .WithDeepEqual(other)
            .WithCustomComparison(new MatcherComparer<T>())
            .Assert();
}