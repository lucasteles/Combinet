using System;

namespace Combinet.Core;

internal static class Prelude
{
    internal static Func<A, C> Compose<A, B, C>(Func<A, B> left, Func<B, C> right) => a => right(left(a));
}