using System;
using MessagePack;

namespace RecombinePatch.Ingress.DtoV1
{
    [MessagePackObject]
    public readonly struct FormKeyV1: IEquatable<FormKeyV1>
    {
        [Key(0)]
        public readonly ushort PluginIndex;
        [Key(1)]
        public readonly uint LocalFormKey;

        public FormKeyV1(ushort pluginIndex, uint localFormKey)
        {
            PluginIndex = pluginIndex;
            LocalFormKey = localFormKey;
        }

        public bool Equals(FormKeyV1 other)
        {
            return PluginIndex == other.PluginIndex && LocalFormKey == other.LocalFormKey;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FormKeyV1 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PluginIndex, LocalFormKey);
        }
    };
}