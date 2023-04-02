using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class MyExtensions
{
    public static TSource MinBy<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable<TKey>
    {
        // can't believe i actually had to implement this. Min included in Linq will return whatever the selector returns intead of just using it for comparison
        var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            return default(TSource);
        }
        var min = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (keySelector(enumerator.Current).CompareTo(keySelector(min)) < 0)
            {
                min = enumerator.Current;
            }
        }
        return min;
    }
}
