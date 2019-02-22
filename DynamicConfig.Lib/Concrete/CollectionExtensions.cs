using System;
using System.Collections.Concurrent;

namespace DynamicConfig.Lib.Concrete
{
    public static class CollectionExtensions
    {
        public static void Clear<T>(this ConcurrentBag<T> blockingCollection)
        {
            if (blockingCollection == null)
            {
                throw new ArgumentNullException("blockingCollection");
            }

            while (blockingCollection.Count > 0)
            {
                T item;
                blockingCollection.TryTake(out item);
            }
        }
    }
}