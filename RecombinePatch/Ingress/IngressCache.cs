using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using RecombinePatch.Analysis.Groups;

namespace RecombinePatch.Ingress
{
    public class IngressCache
    {
        public readonly Dictionary<FormKey, RecombineGroup?> GroupCache = new();
        public readonly ILinkCache LinkCache;

        public IngressCache(ILinkCache linkCache)
        {
            LinkCache = linkCache;
        }
    }
}