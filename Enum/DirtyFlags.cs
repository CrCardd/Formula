namespace Formula.Enum;

public enum DirtyFlags
{
    None = 0,
    MoveDirty = 1 << 0,
    RenderDirty    = 1 << 1,
    BehaviorDirty   = 1 << 2
}