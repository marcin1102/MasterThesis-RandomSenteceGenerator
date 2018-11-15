using System.Collections.Generic;

namespace RandomSentenceGenerator.Infrastructure
{
    public static class CollectionHelpers
    {
        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;
    }
}
