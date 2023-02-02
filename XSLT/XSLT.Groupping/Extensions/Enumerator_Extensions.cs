﻿using System.Collections;

namespace XSLT.Extensions
{
    public static class Enumerator_Extensions
    {
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return (T)enumerator.Current;
            }
        }
    }
}
