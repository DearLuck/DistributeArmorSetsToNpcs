using MessagePack;

namespace RecombinePatch.Ingress.DtoV1
{
    [MessagePackObject]
    public struct LeveledItemV1
    {
        [Key(0)]
        public readonly FormKeyV1 FormKey;

        [Key(1)]
        public readonly uint? GroupIndex;

        [Key(2)]
        public readonly string? EditorId;

        public LeveledItemV1(FormKeyV1 formKey, uint? groupIndex, string? editorId)
        {
            FormKey = formKey;
            GroupIndex = groupIndex;
            EditorId = editorId;
        }
    }

    [MessagePackObject]
    public struct OutfitV1
    {
        [Key(0)]
        public readonly FormKeyV1 FormKey;

        [Key(1)]
        public readonly uint? GroupIndex;

        [Key(2)]
        public readonly string? EditorId;
        
        public OutfitV1(FormKeyV1 formKey, uint? groupIndex, string? editorId)
        {
            FormKey = formKey;
            GroupIndex = groupIndex;
            EditorId = editorId;
        }
    }

    [MessagePackObject]
    public struct ContainerV1
    {
        [Key(0)]
        public readonly FormKeyV1 FormKey;

        [Key(1)]
        public readonly uint? GroupIndex;

        [Key(2)]
        public readonly string? EditorId;
        
        [Key(3)]
        public readonly string? Name;
        
        public ContainerV1(FormKeyV1 formKey, uint? groupIndex, string? editorId, string? name)
        {
            FormKey = formKey;
            GroupIndex = groupIndex;
            EditorId = editorId;
            Name = name;
        }
    }
}