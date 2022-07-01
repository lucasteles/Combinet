using System;
using System.Text.RegularExpressions;
using Combinet.Core.Abstraction;
using Combinet.Core.Matchers;

namespace Combinet;

public static class M
{
    public static WeakDelegateMatcher Null() => new(value => value is null);
    public static WeakDelegateMatcher NotNull() => new(value => value is not null);
    public static WeakDelegateMatcher Ignore() => new(_ => true);

    public static class String
    {
        public static StringMatcher NotEmpty() => new(value => !string.IsNullOrEmpty(value));
        public static StringMatcher Empty() => new(string.IsNullOrEmpty);
        public static StringMatcher NotWhiteSpace() => new(value => !string.IsNullOrWhiteSpace(value));
        public static StringMatcher WhiteSpace() => new(string.IsNullOrWhiteSpace);
        public static StringMatcher Contains(string sub) => new(value => value.Contains(sub));
        public static StringMatcher NotContains(string sub) => new(value => !value.Contains(sub));
        public static StringMatcher StartsWith(string sub) => new(value => value.StartsWith(sub));
        public static StringMatcher EndsWith(string sub) => new(value => value.EndsWith(sub));
        public static StringMatcher Size(int size) => new(value => value.Length == size);
        public static StringMatcher SizeGreaterThan(int size) => new(value => value.Length > size);
        public static StringMatcher SizeLessThan(int size) => new(value => value.Length < size);
        public static StringMatcher SizeGreaterThanOrEqual(int size) => new(value => value.Length >= size);
        public static StringMatcher SizeLessThanOrEqual(int size) => new(value => value.Length <= size);
        public static StringMatcher Pattern(string pattern) => new(value => Regex.Match(value, pattern).Success);
        public static StringMatcher SameAs(string text) => new(value => value == text);
        public static StringMatcher Equals(string text) => new(value => value.Trim() == text.Trim());

        public static StringMatcher Equivalent(string text) => new(value =>
            string.Equals(value.Trim(), text.Trim(), StringComparison.CurrentCultureIgnoreCase));

        public static StringMatcher NotPattern(string pattern) => new(value => !Regex.Match(value, pattern).Success);
    }

    public class Int
    {
        public static IntegerMatcher Zero() => new(value => value is 0);
        public static IntegerMatcher NotZero() => new(value => value is not 0);
        public static IntegerMatcher Positive() => new(value => value > 0);
        public static IntegerMatcher Negative() => new(value => value < 0);
        public static IntegerMatcher GreaterThan(int n) => new(value => value > n);
        public static IntegerMatcher LessThan(int n) => new(value => value < n);
        public static IntegerMatcher GreaterThanOrEqual(int n) => new(value => value >= n);
        public static IntegerMatcher LessThanOrEqual(int n) => new(value => value <= n);
    }
    
}