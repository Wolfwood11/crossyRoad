using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class CollectionExtensions
    {
        public static T RandomElement<T>(this IList<T> list)
        {
            return list.Count <= 0 ? default : list[Random.Range(0, list.Count - 1)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array.Length <= 0 ? default : array[Random.Range(0, array.Length - 1)];
        }
    }
}