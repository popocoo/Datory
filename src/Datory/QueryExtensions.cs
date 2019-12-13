﻿using Datory.Caching;
using Microsoft.Extensions.Caching.Distributed;
using SqlKata;

namespace Datory
{
    /// <summary>
    /// https://stackoverflow.com/a/35273581
    /// </summary>
    public static class QueryExtensions
    {
        public static Query CachingGet(this Query query, string cacheKey, DistributedCacheEntryOptions options = null)
        {
            query.ClearComponent("cache").AddComponent("cache", new CachingCondition
            {
                Action = CachingAction.Get,
                CacheKey = cacheKey,
                Options = options
            });

            return query;
        }

        public static Query CachingSet(this Query query, string cacheKey, DistributedCacheEntryOptions options = null)
        {
            query.ClearComponent("cache").AddComponent("cache", new CachingCondition
            {
                Action = CachingAction.Set,
                CacheKey = cacheKey,
                Options = options
            });

            return query;
        }

        public static Query CachingRemove(this Query query, params string[] cacheKeys)
        {
            query.ClearComponent("cache").AddComponent("cache", new CachingCondition
            {
                Action = CachingAction.Remove,
                CacheKeysToRemove = cacheKeys
            });

            return query;
        }

        public static Query AllowIdentityInsert(this Query query)
        {
            query.ClearComponent("identity").AddComponent("identity", new BasicCondition
            {
                Value = true
            });

            return query;
        }
    }
}
