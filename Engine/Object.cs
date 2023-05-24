using System;
using System.Collections.Generic;

namespace Engine;

public class Object : IDisposable
{
    public Vec2 globalPos
    {
        get => parent == null ? localPos : localPos + parent.globalPos;
        set => localPos = parent == null ? value : value - parent.globalPos;
    }
    public Vec2 localPos;

    public Object parent { get; private set; }
    private readonly List<Object> children = new();


    public void SetParent(Object parent)
    {
        if(parent == null)
            return;

        this.parent?.RemoveChild(this);
        this.parent = parent;
        parent.children?.Add(this);
    }
    public void RemoveParent()
    {
        parent?.RemoveChild(this);
        parent = null;
    }
    public void FreeChildren()
    {
        foreach(var child in children)
            if(child != null)
                child.parent = null;
        children.Clear();
    }
    public void AddChild(Object child)
        => child?.SetParent(this);
    public void RemoveChild(Object child)
    {
        if(child == null)
            return;

        children?.Remove(child);
    }


    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}