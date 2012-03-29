using System;
using System.Collections.Generic;

namespace Depot.Extensions
{
    public static class EnumerableExtensions
    {
         public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
         {
             foreach (var item in instance)
                 action(item);
         }
    }
}